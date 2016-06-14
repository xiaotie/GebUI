using System;

namespace Geb.UI
{
	using Geb.UI.Drawing;

	public enum Cursors
	{
		Default,
		Arrow,
		Hand,
		AppStarting,
		Cross,
		Help,
		HSplit,
		IBeam,
		No,
		NoMove2D,
		NoMoveHoriz,
		NoMoveVert,
		PanEast,
		PanNE,
		PanNorth,
		PanNW,
		PanSE,
		PanSouth,
		PanSW,
		PanWest,
		SizeAll,
		SizeNESW,
		SizeNS,
		SizeNWSE,
		SizeWE,
		UpArrow,
		VSplit,
		WaitCursor
	}

	public class DisplayObject : IComparable<DisplayObject>
	{
		private static long _globalIndex = 0;
		internal static long NextGlobalIndex
		{
			get
			{
				_globalIndex++;
				return _globalIndex;
			}
		}

		public Cursors Cursor { get; set; }
		public double X { get; set; }
		public double Y { get; set; }

		public int ZIndex { get; set; }

		internal long GlobalIndex { get; set; }

		public double Width = 100, Height = 30;

		private Color _backColor = Color.Gray;

		public Color BackColor
		{
			get { return _backColor; }
			set {
				_backColor = value;
				_painted = false;
			}
		}

		public Image BackgroundImage { get; set; }
		public Border Border { get; set; }
		public Boolean IsRoot;

		private Object _stage;
		public Object Stage
		{
			get {
				if (_stage != null) return _stage;
				else if (Parent != null) return Parent._stage;
				return null; 
			}
			set { _stage = value; }
		}

		protected bool _painted = false;

		public Action<DisplayObject> OnInvalidate;

		public Boolean Capture { get; set; }

		public Boolean IsMouseDisable { get; set; }

		public virtual void SetInvalidated(Boolean value)
		{
			this._painted = value;
		}

		public void Invalidate(bool forcePaint = false)
		{
			if (_painted == true || forcePaint == true)
			{
				_painted = false;
				if (Parent != null) Parent.Invalidate();
				if (OnInvalidate != null) OnInvalidate(this);
			}
		}

		protected internal virtual void OnMouseDown(MouseEventArgs e)
		{
		}

		protected internal virtual void OnMouseUp(MouseEventArgs e)
		{
		}

		protected internal virtual void OnMouseMove(MouseEventArgs e)
		{
		}

		protected internal virtual void OnMouseWheel(MouseEventArgs e)
		{
		}

		protected internal virtual void OnMouseEnter()
		{
		}

		protected internal virtual void OnMouseOut()
		{
		}

		protected internal virtual void OnMouseClick(MouseEventArgs e)
		{
		}

		protected internal virtual void OnMouseDoubleClick(MouseEventArgs e)
		{
		}

		private Boolean _hide;

		public Boolean Visible
		{
			get { return !_hide; }
			set { _hide = !value; }
		}

		public PointD LocalToGlobal(PointD pos)
		{
			if (IsRoot == true || Parent == null) return pos + new PointD(X,Y);
			return Parent.LocalToGlobal(pos + new PointD(X,Y));
		}

		public PointD GlobalToLocal(PointD pos)
		{
			if (IsRoot == true || Parent == null) return pos - new PointD(X, Y);
			return Parent.GlobalToLocal(pos - new PointD(X, Y));
		}

		public DisplayObject Parent;

		public virtual DisplayObject HitTest(double x, double y)
		{
			if (Visible == false || IsMouseDisable == true || x < 0 || y < 0 || x > this.Width || y > Height) return null;
			else return this;
		}

		public virtual void Create()
		{
		}

		protected virtual void DrawBackground(Graphics g)
		{
			DrawBackgroundColor(g);

			DrawBackgroundImage(g);

			DrawBorder(g);
		}

		private void DrawBorder(Graphics g)
		{
			if (Border.Thickness > 0 && Border.Color.A > 0)
			{
				g.DrawRectangle (Border.Color, new RectD (0, 0, Width, Height), 1);
			}
		}

		private void DrawBackgroundColor(Graphics g)
		{
			if (BackColor.A > 0)
			{
				RectD r = new RectD (0, 0, Width, Height);
				g.FillRectangle (BackColor, r);
			}
		}

		protected virtual void DrawBackgroundImage(Graphics g)
		{
			if (BackgroundImage != null)
			{
				g.DrawImage(BackgroundImage, new RectD(0,0,Width,Height));
			}
		}

		public virtual void Update()
		{
		}

		protected bool _created;

		public virtual void Draw(Graphics g)
		{
			if (this.Visible == false) return;

			if (_painted == true) return;
			_painted = true;
			DrawBackground(g);
		}

		public int CompareTo(DisplayObject other)
		{
			return this.ZIndex == other.ZIndex ? this.GlobalIndex.CompareTo(other.GlobalIndex) : this.ZIndex.CompareTo(other.ZIndex);
		}
	}
}

