﻿using LightController.Config.Dmx;
using OpenDMX.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightController.Dmx
{
    public class DmxProcessor
    {
        private List<DmxDevice> fixtures = new List<DmxDevice>();
        private DmxController controller = new DmxController();

        public DmxProcessor(DmxConfig config)
        {
            bool foundDevice = false;
            foreach (OpenDMX.NET.FTDI.Device device in controller.GetDevices())
            {
                if (string.IsNullOrWhiteSpace(config.DmxDevice) || device.Description == config.DmxDevice)
                {
                    controller.Open(device.DeviceIndex);
                    foundDevice = true;
                    break;
                }
            }

            if (!foundDevice)
                throw new Exception("No DMX interface detected!");

            

            Dictionary<string, DmxDeviceProfile> profiles = config.Fixtures.ToDictionary(x => x.Name);
            foreach(DmxDeviceAddress fixtureAddress in config.Addresses)
            {
                DmxDeviceProfile profile = profiles[fixtureAddress.Name];
                for(int i = 0; i < fixtureAddress.Count; i++)
                    fixtures.Add(new DmxDevice(profile, fixtureAddress));
            }
        }


        public void Write(DmxFrame frame)
        {
            controller.SetChannels(frame.StartAddress, frame.Data);
        }
    }
}
