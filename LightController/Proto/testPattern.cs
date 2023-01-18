// <auto-generated>
//   This file was generated by a tool; you should avoid making direct changes.
//   Consider using 'partial classes' to extend these types
//   Input: testPattern.proto
// </auto-generated>

#region Designer generated code

namespace rv.data
{

    [global::ProtoBuf.ProtoContract()]
    public partial class TestPattern : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public Type type { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public BlendGrid blend_grid
        {
            get => __pbn__PatternProperties.Is(2) ? ((BlendGrid)__pbn__PatternProperties.Object) : default;
            set => __pbn__PatternProperties = new global::ProtoBuf.DiscriminatedUnionObject(2, value);
        }
        public bool ShouldSerializeblend_grid() => __pbn__PatternProperties.Is(2);
        public void Resetblend_grid() => global::ProtoBuf.DiscriminatedUnionObject.Reset(ref __pbn__PatternProperties, 2);

        private global::ProtoBuf.DiscriminatedUnionObject __pbn__PatternProperties;

        [global::ProtoBuf.ProtoMember(3)]
        public CustomColor custom_color
        {
            get => __pbn__PatternProperties.Is(3) ? ((CustomColor)__pbn__PatternProperties.Object) : default;
            set => __pbn__PatternProperties = new global::ProtoBuf.DiscriminatedUnionObject(3, value);
        }
        public bool ShouldSerializecustom_color() => __pbn__PatternProperties.Is(3);
        public void Resetcustom_color() => global::ProtoBuf.DiscriminatedUnionObject.Reset(ref __pbn__PatternProperties, 3);

        [global::ProtoBuf.ProtoMember(4, Name = @"intensity")]
        public IntensityColor Intensity
        {
            get => __pbn__PatternProperties.Is(4) ? ((IntensityColor)__pbn__PatternProperties.Object) : default;
            set => __pbn__PatternProperties = new global::ProtoBuf.DiscriminatedUnionObject(4, value);
        }
        public bool ShouldSerializeIntensity() => __pbn__PatternProperties.Is(4);
        public void ResetIntensity() => global::ProtoBuf.DiscriminatedUnionObject.Reset(ref __pbn__PatternProperties, 4);

        [global::ProtoBuf.ProtoContract()]
        public partial class BlendGrid : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1, Name = @"draw_grid")]
            public bool DrawGrid { get; set; }

            [global::ProtoBuf.ProtoMember(2, Name = @"draw_circles")]
            public bool DrawCircles { get; set; }

            [global::ProtoBuf.ProtoMember(3, Name = @"draw_lines")]
            public bool DrawLines { get; set; }

            [global::ProtoBuf.ProtoMember(4, Name = @"invert_colors")]
            public bool InvertColors { get; set; }

            [global::ProtoBuf.ProtoMember(5, Name = @"grid_spacing")]
            public double GridSpacing { get; set; }

        }

        [global::ProtoBuf.ProtoContract()]
        public partial class CustomColor : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1, Name = @"color")]
            public Color Color { get; set; }

        }

        [global::ProtoBuf.ProtoContract()]
        public partial class IntensityColor : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1, Name = @"intensity")]
            public double Intensity { get; set; }

        }

        [global::ProtoBuf.ProtoContract()]
        public enum Type
        {
            [global::ProtoBuf.ProtoEnum(Name = @"TYPE_UNKNOWN")]
            TypeUnknown = 0,
            [global::ProtoBuf.ProtoEnum(Name = @"TYPE_BLEND_GRID")]
            TypeBlendGrid = 1,
            [global::ProtoBuf.ProtoEnum(Name = @"TYPE_COLOR_BARS")]
            TypeColorBars = 2,
            [global::ProtoBuf.ProtoEnum(Name = @"TYPE_FOCUS")]
            TypeFocus = 3,
            [global::ProtoBuf.ProtoEnum(Name = @"TYPE_GRAY_SCALE")]
            TypeGrayScale = 4,
            [global::ProtoBuf.ProtoEnum(Name = @"TYPE_BLACK_COLOR")]
            TypeBlackColor = 5,
            [global::ProtoBuf.ProtoEnum(Name = @"TYPE_WHITE_COLOR")]
            TypeWhiteColor = 6,
            [global::ProtoBuf.ProtoEnum(Name = @"TYPE_CUSTOM_COLOR")]
            TypeCustomColor = 7,
        }

    }

}


#endregion
