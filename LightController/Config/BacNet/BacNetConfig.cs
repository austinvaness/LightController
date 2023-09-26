﻿using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace LightController.Config.BacNet
{
    public class BacNetConfig
    {
        [YamlMember(Description = "The local ip to bind, or blank for any address")]
        public string BindIp { get; set; }

        [YamlMember(Description = "The port to use for communication, or blank for 0xBAC0/47808")]
        public ushort Port { get; set; } = 0xBAC0;

        [YamlMember]
        public List<BacNetEvent> Events { get; set; } = new List<BacNetEvent>();

    }
}
