using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;

namespace Geb.UI.iOS
{
	public class iOSStageView : UIView
	{
		public iOSStageView ()
		{
		}

		public override void Draw(CGRect rect)
		{
			base.Draw(rect);
			using (CGContext graphics = UIGraphics.GetCurrentContext ()) {
			
			}
		}
	}
}

