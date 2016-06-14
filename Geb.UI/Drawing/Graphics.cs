using System;

namespace Geb.UI.Drawing
{
	public abstract class Graphics
	{
		public abstract Graphics FillRectangle (Color color, RectD rect);

		public abstract Graphics FillEllipse (Color color, RectD rect);

		public abstract Graphics DrawRectangle(Color color, RectD rect, float lineWidth);

		public abstract Graphics DrawLine(Color color, PointD p0, PointD p1, float lineWidth);

		public abstract Graphics DrawString (String txt, Font font, Color color,  RectD rect, Align align, Align lineAlign); 

		public abstract Graphics DrawImage(Image image, RectD rect);

		public abstract Graphics SaveState();

		public abstract Graphics RestoreState ();

		public abstract Graphics Transform (double tx, double ty);
	}
}

