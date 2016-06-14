using System;

namespace Geb.UI
{
	public enum MouseButtons
	{
		Left,
		Middle,
		None,
		Right
	}

	public class MouseEventArgs
	{
		public double StageX, StageY;

		public MouseButtons Button;
		public DisplayObject Owner;
		public double X, Y, Delta;
	}
}

