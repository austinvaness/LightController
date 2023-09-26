﻿using System;
using System.Collections.Generic;
using System.IO.BACnet;
using LightController.Config.Bacnet;
using System.Net;
using System.Collections.Concurrent;
using System.Threading;
using System.IO.BACnet.Storage;
using System.Threading.Tasks;
using OpenDMX.NET.FTDI;
using LightController.Midi;
using System.Linq;

namespace LightController.Bacnet
{
    public partial class BacnetProcessor
    {
        private readonly BacnetClient bacnetClient;
        private readonly ConcurrentDictionary<uint, BacNode> nodes = new ConcurrentDictionary<uint, BacNode>();
        private readonly Dictionary<string, BacnetEvent> namedEvents = new Dictionary<string, BacnetEvent>();
        private readonly Dictionary<MidiNote, BacnetEvent> midiEvents = new Dictionary<MidiNote, BacnetEvent>();

        private readonly object writeRequestsLock = new object();
        private readonly Dictionary<BacnetEndpoint, BacnetValue> writeRequests = new Dictionary<BacnetEndpoint, BacnetValue>();

        public bool Enabled { get; }

        public BacnetProcessor(BacnetConfig config)
        {
            if (config?.Events == null || config.Events.Count == 0)
                return;

            foreach(BacnetEvent e in config.Events)
            {
                e.Init();
                if(!string.IsNullOrWhiteSpace(e.Name))
                    namedEvents[e.Name] = e;
                if (e.MidiNote != null)
                    midiEvents[e.MidiNote] = e;
            }

            ushort port = 0xBAC0;
            if(config.Port > 0)
                port = config.Port;

            BacnetIpUdpProtocolTransport transport;
            if (string.IsNullOrWhiteSpace(config.BindIp) || !IPAddress.TryParse(config.BindIp, out _))
            {
                transport = new BacnetIpUdpProtocolTransport(port);
                LogFile.Info($"Starting Bacnet client at {IPAddress.Any}:{port}");
            }
            else
            {
                transport = new BacnetIpUdpProtocolTransport(port, localEndpointIp: config.BindIp);
                LogFile.Info($"Starting Bacnet client at {config.BindIp}:{port}");
            }
            bacnetClient = new BacnetClient(transport);
            bacnetClient.Start();
            bacnetClient.OnIam += OnIamReceived;
            bacnetClient.WhoIs();

            Enabled = true;
        }

        public void TriggerEvents(MidiNote note)
        {
            if (!Enabled)
                return;

            if (!midiEvents.TryGetValue(note, out BacnetEvent e))
                return;

            lock (writeRequestsLock)
            {
                foreach (BacnetProperty prop in e.Properties)
                    writeRequests[new BacnetEndpoint(prop.DeviceId, prop.BacnetId)] = prop.BacnetValue;
            }
        }

        public void TriggerEvents(MidiNote note, IEnumerable<string> names)
        {
            if (!Enabled)
                return;
            
            List<BacnetEvent> events = new List<BacnetEvent>();
            if (midiEvents.TryGetValue(note, out BacnetEvent midiEvent))
                events.Add(midiEvent);

            foreach(string name in names)
            {
                if(namedEvents.TryGetValue(name, out BacnetEvent namedEvent))
                    events.Add(namedEvent);
            }

            lock (writeRequestsLock)
            {
                foreach(BacnetEvent e in events)
                {
                    foreach (BacnetProperty prop in e.Properties)
                        writeRequests[new BacnetEndpoint(prop.DeviceId, prop.BacnetId)] = prop.BacnetValue;
                }
            }
        }

        private async Task BacnetTest()
        {
            try
            {
                await Task.Delay(2000);

                BacnetProperty prop = new BacnetProperty()
                {
                    DeviceId = 42001,
                    PropertyId = 2,
                    AnalogType = false,
                    OutputType = false,
                    Value = 1,
                };
                prop.Init();
                bool result = WriteValue(prop.DeviceId, prop.BacnetId, prop.BacnetValue);
            }
            catch (Exception e)
            {
                throw;
            }
        }


        private void ReadWriteExample()
        {

            BacnetValue Value;
            bool ret;
            // Read Present_Value property on the object ANALOG_INPUT:0 provided by the device 12345
            // Scalar value only
            ret = ReadScalarValue(12345, new BacnetObjectId(BacnetObjectTypes.OBJECT_ANALOG_INPUT, 0), BacnetPropertyIds.PROP_PRESENT_VALUE, out Value);

            if (ret == true)
            {
                Console.WriteLine("Read value : " + Value.Value.ToString());

                // Write Present_Value property on the object ANALOG_OUTPUT:0 provided by the device 4000
                BacnetValue newValue = new BacnetValue(BacnetApplicationTags.BACNET_APPLICATION_TAG_REAL, Convert.ToSingle(Value.Value));   // expect it's a float
                ret = WriteScalarValue(4000, new BacnetObjectId(BacnetObjectTypes.OBJECT_ANALOG_OUTPUT, 0), BacnetPropertyIds.PROP_PRESENT_VALUE, newValue);

                Console.WriteLine("Write feedback : " + ret.ToString());
            }
            else
                Console.WriteLine("Error somewhere !");
        }


        private void OnIamReceived(BacnetClient sender, BacnetAddress adr, uint deviceId, uint maxApdu, BacnetSegmentations segmentation, ushort vendorId)
        {
            LogFile.Info($"Bacnet device: {adr} - {deviceId}");
            nodes.TryAdd(deviceId, new BacNode(adr, deviceId));
        }


        private bool ReadScalarValue(int deviceId, BacnetObjectId BacnetObjet, BacnetPropertyIds Propriete, out BacnetValue Value)
        {
            BacnetAddress adr;
            IList<BacnetValue> NoScalarValue;

            Value = new BacnetValue(null);

            // Looking for the device
            adr = DeviceAddr((uint)deviceId);
            if (adr == null) return false;  // not found

            // Property Read
            if (bacnetClient.ReadPropertyRequest(adr, BacnetObjet, Propriete, out NoScalarValue) == false)
                return false;

            Value = NoScalarValue[0];
            return true;
        }


        private bool WriteScalarValue(int deviceId, BacnetObjectId BacnetObjet, BacnetPropertyIds Propriete, BacnetValue Value)
        {
            BacnetAddress adr;

            // Looking for the device
            adr = DeviceAddr((uint)deviceId);
            if (adr == null) return false;  // not found

            // Property Write
            BacnetValue[] NoScalarValue = { Value };
            if (bacnetClient.WritePropertyRequest(adr, BacnetObjet, Propriete, NoScalarValue) == false)
                return false;

            return true;
        }


        private BacnetAddress DeviceAddr(uint deviceId)
        {
            if (nodes.TryGetValue(deviceId, out BacNode node))
                return node.Address;
            return null;
        }

        public bool WriteValue(uint deviceId, BacnetObjectId objectId, BacnetValue value)
        {
            if (nodes.TryGetValue(deviceId, out BacNode node))
                return bacnetClient.WritePropertyRequest(node.Address, objectId, BacnetPropertyIds.PROP_PRESENT_VALUE, new[] { value });
            LogFile.Warn("Bacnet device not found: " + deviceId);
            return false;
        }

        public bool ReadValue(uint deviceId, BacnetObjectId objectId, out IList<BacnetValue> value)
        {
            if(nodes.TryGetValue(deviceId, out BacNode node))
                return bacnetClient.ReadPropertyRequest(node.Address, objectId, BacnetPropertyIds.PROP_PRESENT_VALUE, out value);
            value = new List<BacnetValue>();
            return false;
        }

        public void Update()
        {
            (BacnetEndpoint, BacnetValue)[] writeRequests;
            lock (writeRequestsLock)
            {
                writeRequests = this.writeRequests.Select(x => (x.Key, x.Value)).ToArray();
                this.writeRequests.Clear();
            }

            foreach(var tuple in writeRequests)
                WriteValue(tuple.Item1.DeviceId, tuple.Item1.ObjectId, tuple.Item2);
        }
    }
}
