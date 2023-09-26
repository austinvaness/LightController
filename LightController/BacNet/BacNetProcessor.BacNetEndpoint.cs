﻿using System;
using System.Collections.Generic;
using System.IO.BACnet;

namespace LightController.Bacnet
{
    public partial class BacnetProcessor
    {
        private class BacnetEndpoint : IEquatable<BacnetEndpoint>
        {
            public uint DeviceId { get; }
            public BacnetObjectId ObjectId { get; }

            public BacnetEndpoint(uint deviceId, BacnetObjectId objectId)
            {
                DeviceId = deviceId;
                ObjectId = objectId;
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as BacnetEndpoint);
            }

            public bool Equals(BacnetEndpoint other)
            {
                return other is not null &&
                       DeviceId == other.DeviceId &&
                       EqualityComparer<BacnetObjectId>.Default.Equals(ObjectId, other.ObjectId);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(DeviceId, ObjectId);
            }

            public static bool operator ==(BacnetEndpoint left, BacnetEndpoint right)
            {
                return EqualityComparer<BacnetEndpoint>.Default.Equals(left, right);
            }

            public static bool operator !=(BacnetEndpoint left, BacnetEndpoint right)
            {
                return !(left == right);
            }
        }
    }
}