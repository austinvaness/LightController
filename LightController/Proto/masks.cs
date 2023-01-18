// <auto-generated>
//   This file was generated by a tool; you should avoid making direct changes.
//   Consider using 'partial classes' to extend these types
//   Input: masks.proto
// </auto-generated>

#region Designer generated code

namespace rv.data
{

    [global::ProtoBuf.ProtoContract()]
    public partial class Mask : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"uuid")]
        public Uuid Uuid { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"name")]
        [global::System.ComponentModel.DefaultValue("")]
        public string Name { get; set; } = "";

        [global::ProtoBuf.ProtoMember(3, Name = @"color")]
        public Color Color { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public Mode mode { get; set; }

        [global::ProtoBuf.ProtoMember(5, Name = @"shapes")]
        public global::System.Collections.Generic.List<Graphics.Element> Shapes { get; } = new global::System.Collections.Generic.List<Graphics.Element>();

        [global::ProtoBuf.ProtoContract()]
        public enum Mode
        {
            [global::ProtoBuf.ProtoEnum(Name = @"MODE_OVERLAY")]
            ModeOverlay = 0,
            [global::ProtoBuf.ProtoEnum(Name = @"MODE_KEYHOLE")]
            ModeKeyhole = 1,
        }

    }

}


#endregion
