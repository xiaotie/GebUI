using System;

namespace Geb.UI
{
	public struct Border
	{
		public static Border Empty = new Border();

		public Color Color ;
		public float Thickness;
		public Boolean BorderLeftVisible { get; set; }
		public Boolean BorderRightVisible { get; set; }
		public Boolean BorderTopVisible { get; set; }
		public Boolean BorderBottomVisible { get; set; }

		public Border(Color color, float thickness = 1):this()
		{
			Color = color;
			Thickness = thickness;
			BorderLeftVisible = true;
			BorderTopVisible = true;
			BorderRightVisible = true;
			BorderBottomVisible = true;
		}
	}
}

