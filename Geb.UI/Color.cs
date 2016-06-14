using System;

namespace Geb.UI
{
	public struct Color
	{
		public static Color Transparent = new Color { R = 255, G = 255, B = 255, A = 0 };

		public static Color Gray = new Color {R = 100,G = 100,B = 100, A = 255};

		public static Color Red = new Color { R = 255, G = 0,B = 0, A = 255};

		public static Color Black = new Color { R = 0, G = 0, B = 0, A = 255};

		public static Color White = new Color { R = 255, G = 255, B = 255, A = 255};

		public Byte B,G,R,A;

		public Color (byte blue, byte green, byte red, byte alpha)
		{
			B = blue;
			G = green;
			R = red;
			A = alpha;
		}
	}
}

