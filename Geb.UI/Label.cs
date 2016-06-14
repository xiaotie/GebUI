using System;

namespace Geb.UI
{
	public class Label : DisplayObject
	{
		public String Text { get; set; }
		public Align Align;
		public Align LineAlign;
		public Font Font;
		public Color Color;

		public Label ()
		{
			Align = Align.Near;
			LineAlign = Align.Center;
			Font = new Font ("Arial", 12.0f);
			BackColor = Color.Transparent;
			Color = Color.Black;
			Height = 20;
		}

		public override void Draw (Geb.UI.Drawing.Graphics g)
		{
			base.Draw (g);
			if (String.IsNullOrEmpty (Text) == false) {
				g.DrawString (Text, Font, Color, new RectD (0, 0, Width, Height), Align, LineAlign);
			}
		}
	}
}

