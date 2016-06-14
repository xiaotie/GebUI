using System;
using System.Reflection;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(Geb.UI.Views.StageView), typeof(Geb.UI.iOS.StageViewRender))]
namespace Geb.UI.iOS
{
	using Geb.UI.Views;
	using Geb.UI.Drawing;
	using Geb.UI.iOS.Drawing;

	public class StageViewRender : ViewRenderer<StageView, UIView>
	{
		private iOSGraphics _drawingCxt = new iOSGraphics();

		public StageViewRender ()
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<StageView> args)
		{
			base.OnElementChanged(args);
			if (Control == null)
			{
				UIView view = new UIView{ };
				SetNativeControl (view);
			}
		}

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
			if (e.PropertyName == "Draw") {
				this.SetNeedsDisplay ();
			}
		}

		public override void Draw (CoreGraphics.CGRect rect)
		{
			base.Draw (rect);
			_drawingCxt.Context = UIGraphics.GetCurrentContext ();
			Element.Draw (_drawingCxt);
		}
	}
}

