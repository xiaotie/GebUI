using System;

namespace Geb.UI.iOS.Drawing
{
	using Geb.UI.Drawing;
	using Foundation;
	using CoreGraphics;
	using CoreText;
	using UIKit;

	public class iOSGraphics : Graphics
	{
		internal CGContext Context { get;set; }

		public iOSGraphics ()
		{
		}

		public static CGColor ToCGColor(Color color)
		{
			return new CGColor ((float)color.R / 255.0f, (float)color.G / 255.0f, (float)color.B / 255.0f, (float)color.A / 255.0f);
		}

		public override Graphics FillRectangle (Color color, RectD rect)
		{
			Context.SetFillColor (ToCGColor (color));
			Context.FillRect(new CGRect(rect.X,rect.Y,rect.Width,rect.Height));
			return this;
		}

		public override Graphics FillEllipse (Color color, RectD rect)
		{
			Context.SetFillColor (ToCGColor (color));
			Context.FillEllipseInRect(new CGRect(rect.X,rect.Y,rect.Width,rect.Height));
			return this;
		}

		public override Graphics DrawRectangle (Color color, RectD rect, float lineWidth)
		{
			Context.SetFillColor (ToCGColor (color));
			CGPath path = CGPath.FromRect(new CGRect(rect.X,rect.Y,rect.Width,rect.Height));
			Context.AddPath (path);
			Context.DrawPath (CGPathDrawingMode.Stroke);
			return this;
		}

		public override Graphics DrawLine (Color color, PointD p0, PointD p1, float lineWidth)
		{
			Context.SetFillColor (ToCGColor (color));
			Context.SetLineWidth (lineWidth);
			Context.MoveTo ((float)p0.X, (float)p0.Y);
			Context.AddLineToPoint ((float)p1.X, (float)p1.Y);
			Context.DrawPath (CGPathDrawingMode.Stroke);
			return this;
		}

		public override Graphics DrawString (string txt, Font font, Color color, RectD rect, Align align, Align lineAlign)
		{
			using (CTFrame frame = CreateFrame (font, rect, txt, color, align)) {
				if (frame == null)
					return this;

				nfloat y = 0;
				nfloat offset = 0;

				nfloat ascent, decent, leading;
				nfloat frameHeight = 0;
				CTLine[] lines = frame.GetLines ();
				if (lines != null && lines.Length > 0) {
					CGPoint[] lineOrigins = new CGPoint[lines.Length];
					frame.GetLineOrigins (new NSRange (0, 0), lineOrigins);

					nfloat firstLineOriginY = lineOrigins [0].Y;
					nfloat lastLineOriginY = lineOrigins [lines.Length - 1].Y;

					CTLine firstLine = lines [0];
					firstLine.GetTypographicBounds (out ascent, out decent, out leading);

					offset = ascent - firstLineOriginY;

					CTLine lastLine = lines [lines.Length - 1];
					lastLine.GetTypographicBounds (out ascent, out decent, out leading);
					frameHeight = lastLineOriginY + decent + offset;
				}

				if (lineAlign == Align.Center) {
					y = offset + (nfloat)(0.5 * (rect.Height - frameHeight));
				} else if (lineAlign == Align.Near) {
					y = offset + (nfloat)(rect.Height - frameHeight);
				} else {
					y = offset;
				}

				Context.SaveState ();
				Context.TextMatrix = CGAffineTransform.MakeScale (1f, -1f);
				Context.TranslateCTM ((float)rect.X, (float)rect.Y + y);
				frame.Draw (Context);
				Context.RestoreState ();
			}
			return this;
		}

		public override Graphics DrawImage (Image image, RectD rect)
		{
			return this;
		}

		public override Graphics SaveState ()
		{
			Context.SaveState ();
			return this;
		}

		public override Graphics RestoreState ()
		{
			Context.RestoreState ();
			return this;
		}

		public override Graphics Transform (double tx, double ty)
		{
			Context.TranslateCTM ((nfloat)tx, (nfloat)ty);
			return this;
		}

		static CTFrame CreateFrame (Font font, RectD rect, String text, Color color, Align align)
		{
			if (string.IsNullOrEmpty (text))
				return null;

			using (CTFramesetter framesetter = new CTFramesetter (CreateAttributedString (font,rect,text,color,align))) {
				CGPath path = new CGPath ();
				path.AddRect (new CGRect (0, 0, rect.Width, rect.Height));
				return framesetter.GetFrame (new NSRange (0, text.Length), path, null);
			}
		}

		static NSAttributedString CreateAttributedString (Font font, RectD rect, String text, Color color, Align align)
		{
			CTTextAlignment ctAlign = CTTextAlignment.Center;
			if (align == Align.Near)
				ctAlign = CTTextAlignment.Left;
			else if (align == Align.Far)
				ctAlign = CTTextAlignment.Right;

			CTParagraphStyle alignStyle = new CTParagraphStyle(new CTParagraphStyleSettings {
				Alignment = ctAlign
			});

			CTFont ctFont = new CTFont (font.Name, font.Size);
			NSDictionary dict;
			if (font != null) {
				dict = NSDictionary.FromObjectsAndKeys (
					new object[] { ctFont, ToCGColor(color), alignStyle },
					new object[] { CTStringAttributeKey.Font, CTStringAttributeKey.ForegroundColor, CTStringAttributeKey.ParagraphStyle }
				);
			} else {
				dict = NSDictionary.FromObjectsAndKeys (
					new object[] { new NSNumber (true) , alignStyle},
					new object[] { CTStringAttributeKey.ForegroundColorFromContext, CTStringAttributeKey.ParagraphStyle }
				);
			}

			return new NSAttributedString (text, dict);
		}
	}
}

