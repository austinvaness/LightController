﻿using System;

namespace LightController.Color
{
    public class ColorHSV
    {
        public ColorHSV(double hue, double saturation, double value)
        {
            Hue = hue;
            Saturation = saturation;
            Value = value;
        }

        public double Hue { get; private set; }
        public double Saturation { get; private set; }
        public double Value { get; private set; }


        public static explicit operator ColorRGB(ColorHSV x)
        {
			double r, g, b;
			double hue = x.Hue;
			double saturation = x.Saturation;
			double value = x.Value;

			if (saturation == 0)
			{
				r = value;
				g = value;
				b = value;
			}
			else
			{
				int i;
				double f, p, q, t;

				if (hue == 360)
					hue = 0;
				else
					hue = hue / 60;

				i = (int)Math.Truncate(hue);
				f = hue - i;

				p = value * (1.0 - saturation);
				q = value * (1.0 - (saturation * f));
				t = value * (1.0 - (saturation * (1.0 - f)));

				switch (i)
				{
					case 0:
						r = value;
						g = t;
						b = p;
						break;

					case 1:
						r = q;
						g = value;
						b = p;
						break;

					case 2:
						r = p;
						g = value;
						b = t;
						break;

					case 3:
						r = p;
						g = q;
						b = value;
						break;

					case 4:
						r = t;
						g = p;
						b = value;
						break;

					default:
						r = value;
						g = p;
						b = q;
						break;
				}

			}

			return new ColorRGB((byte)Math.Round(r * 255), (byte)Math.Round(g * 255), (byte)Math.Round(b * 255));
		}

		public override string ToString()
		{
			ColorRGB rgb = (ColorRGB)this;
			return $"#{rgb.Red:X2}{rgb.Green:X2}{rgb.Blue:X2}";
		}
	}
}
