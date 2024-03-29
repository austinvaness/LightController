﻿using LightController.Color;
using YamlDotNet.Serialization;

namespace LightController.Config.Input
{
    [YamlTag("!color_input")]
    public class ColorInput : InputBase
    {
        [YamlMember(Alias = "rgb", ApplyNamingConventions = false)]
        public ColorRGB RGB { get; set; }
        
        [YamlMember(Alias = "hsv", ApplyNamingConventions = false)]
        public SerializableColorHSV HSV { get; set; }

        public ColorInput() { }

        public override ColorHSV GetColor(int fixtureId)
        {
            if (RGB != null)
                return (ColorHSV)RGB;
            if (HSV != null)
                return HSV.Color;
            return ColorHSV.Black;
        }

        public override double GetIntensity(int fixtureid, ColorHSV target)
        {
            return intensity.GetIntensity(fixtureid);
        }
    }
}
