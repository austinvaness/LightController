// <auto-generated>
//   This file was generated by a tool; you should avoid making direct changes.
//   Consider using 'partial classes' to extend these types
//   Input: propSlide.proto
// </auto-generated>

#region Designer generated code

namespace rv.data
{

    [global::ProtoBuf.ProtoContract()]
    public partial class PropSlide : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"base_slide")]
        public Slide BaseSlide { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"transition")]
        public Transition Transition { get; set; }

    }

}


#endregion
