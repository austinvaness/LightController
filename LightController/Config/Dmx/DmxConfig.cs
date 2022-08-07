﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace LightController.Config.Dmx
{
    public class DmxConfig
    {
        [YamlMember(Description = "The index of the device to use, starting at 0")]
        public uint DmxDevice { get; set; } = 0;
        [YamlMember(Description = "List of all light fixtures")]
        public List<DmxDeviceProfile> Fixtures { get; set; } = new List<DmxDeviceProfile>();
        [YamlMember(Description = "List of all light fixture addresses")]
        public List<DmxDeviceAddress> Addresses { get; set; } = new List<DmxDeviceAddress>();
    }
}
