using System;

namespace Geb.UI
{
	public struct PointD
	{
		public Double X;
		public Double Y;

		public PointD (double x, double y)
		{
			X = x;
			Y = y;
		}

		public static PointD operator  +(PointD left, PointD right)
		{
			return new PointD { X = left.X + right.X, Y = left.Y + right.Y };
		}

		public static PointD operator -(PointD left, PointD right)
		{
			return new PointD { X = left.X - right.X, Y = left.Y - right.Y };
		}
	}
}

