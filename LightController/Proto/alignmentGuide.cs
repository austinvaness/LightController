// <auto-generated>
//   This file was generated by a tool; you should avoid making direct changes.
//   Consider using 'partial classes' to extend these types
//   Input: alignmentGuide.proto
// </auto-generated>

#region Designer generated code

namespace rv.data
{

    [global::ProtoBuf.ProtoContract()]
    public partial class AlignmentGuide : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"uuid")]
        public Uuid Uuid { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"orientation")]
        public GuidelineOrientation Orientation { get; set; }

        [global::ProtoBuf.ProtoMember(3, Name = @"location")]
        public double Location { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public enum GuidelineOrientation
        {
            [global::ProtoBuf.ProtoEnum(Name = @"GUIDELINE_ORIENTATION_HORIZONTAL")]
            GuidelineOrientationHorizontal = 0,
            [global::ProtoBuf.ProtoEnum(Name = @"GUIDELINE_ORIENTATION_VERTICAL")]
            GuidelineOrientationVertical = 1,
        }

    }

}


#endregion
