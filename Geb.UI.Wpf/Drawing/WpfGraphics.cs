using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Geb.UI.Wpf.Drawing
{
    using Geb.UI.Drawing;

    public class WpfGraphics : Graphics
    {
        internal DrawingContext Context { get; set; }

        static System.Windows.Media.Color ToWpfColor(Color c)
        {
            return new System.Windows.Media.Color { B = c.B, G = c.G, R = c.R, A = c.A };
        }

        public override Graphics FillRectangle(Color color, RectD rect)
        {
            Brush brush = new SolidColorBrush(ToWpfColor(color));
            Context.DrawRectangle(brush, null, new System.Windows.Rect { X = rect.X, Y = rect.Y, Width = rect.Width, Height = rect.Height });
            return this;
        }

        public override Graphics FillEllipse(Color color, RectD rect)
        {
            
            return this;
        }

        public override Graphics DrawRectangle(Color color, RectD rect, float lineWidth)
        {
            
            return this;
        }

        public override Graphics DrawLine(Color color, PointD p0, PointD p1, float lineWidth)
        {
            
            return this;
        }

        public override Graphics DrawString(string txt, Font font, Color color, RectD rect, Align align, Align lineAlign)
        {
            return this;
        }

        public override Graphics DrawImage(Image image, RectD rect)
        {
            return this;
        }

        public override Graphics Transform(double tx, double ty)
        {
            return this;
        }

        public override Graphics SaveState()
        {
            return this;
        }

        public override Graphics RestoreState()
        {
            return this;
        }
    }
}
