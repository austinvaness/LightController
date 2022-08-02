﻿using LightController.Color;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightController.Pro
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class ProMediaItem
    {
        [ProtoMember(1)]
        private ColorRGB[] data;

        private Dictionary<int, ColorRGB[]> resizedData = new Dictionary<int, ColorRGB[]>();

        public ProMediaItem()
        {

        }

        public ColorRGB[] GetData(int size)
        {
            return new ColorRGB[size];
        }
    }
}
