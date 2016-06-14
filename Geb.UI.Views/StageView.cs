using System;
using Xamarin.Forms;

namespace Geb.UI.Views
{
	using Geb.UI;
	using Geb.UI.Drawing;

	public class StageView : View
	{
		protected Stage _stage;

		public StageView ()
		{
			_stage = new Stage ();
		}

		protected override SizeRequest OnSizeRequest (double widthConstraint, double heightConstraint)
		{
			return base.OnSizeRequest (widthConstraint, heightConstraint);
		}

		protected override void OnSizeAllocated (double width, double height)
		{
			base.OnSizeAllocated (width, height);
			_stage.Width = width;
			_stage.Height = height;
			_stage.Update ();
		}

		public Stage Stage
		{
			get { return _stage; }
		}

		public void Draw(Graphics g)
		{
			_stage.Draw (g);
		}
	}
}

