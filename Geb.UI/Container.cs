using System;
using System.Collections.Generic;

namespace Geb.UI
{
	using Geb.UI.Drawing;

	public class Container:DisplayObject
	{
		public List<DisplayObject> Controls { get; set; }

		public Boolean IsChildrenMouseEnable = true;

		public Container()
			: base()
		{ }

		public override DisplayObject HitTest(double x, double y)
		{
			DisplayObject match = base.HitTest(x, y);
			if (match == null) return null;
			else
			{
				if (Controls != null && IsChildrenMouseEnable == true)
				{
					for (int i = Controls.Count - 1; i >= 0; i--)
					{
						DisplayObject item = Controls[i];
						DisplayObject m = item.HitTest(x - item.X, y - item.Y);
						if (m != null) return m;
					}
				}
			}
			return match;
		}

		public override void SetInvalidated(Boolean value)
		{
			this._painted = value;
			if (Controls == null) return;
			foreach (DisplayObject item in this.Controls)
			{
				item.SetInvalidated(value);
			}
		}

		public Container Clear()
		{
			if (Controls != null) Controls.Clear();
			return this;
		}

		public override void Create()
		{
			if (Controls == null) return;
			foreach(DisplayObject item in Controls)
			{
				item.Create();
			}
		}

		public virtual Container Add(DisplayObject element)
		{
			if (element == null) throw new ArgumentNullException("element");
			if (Controls == null) Controls = new List<DisplayObject>();
			element.Parent = this;
			element.GlobalIndex = DisplayObject.NextGlobalIndex ;
			Controls.Add(element);
			Controls.Sort();
			return this;
		}

		public virtual Container Add(params DisplayObject[] objects)
		{
			if (objects != null) {
				if (Controls == null) Controls = new List<DisplayObject>();
				for (int i = 0; i < objects.Length; i++) {
					DisplayObject obj = objects [i];
					obj.Parent = this;
					obj.GlobalIndex = DisplayObject.NextGlobalIndex;
					Controls.Add (obj);
				}
				Controls.Sort ();
			}
			return this;
		}

		public Container Remove(DisplayObject element)
		{
			if (element == null) throw new ArgumentNullException("element");
			if (Controls != null && this.Controls.Contains(element) == true)
			{
				element.Parent = null;
				Controls.Remove(element);
			}
			return this;
		}

		public Container RemoveAt(int idx)
		{
			if (Controls != null)
			{
				Controls.RemoveAt(idx);
			}
			return this;
		}

		public override void Draw(Graphics g)
		{
			if (_painted == true || Visible == false) return;

			SetInvalidated(false);
			_painted = true;

			this.DrawBackground(g);

			if (Controls != null)
			{
				foreach (DisplayObject item in Controls)
				{
					if (item.Visible == false) continue;

					item.Update();
					g.SaveState ();
					g.Transform (item.X, item.Y);
					item.Draw(g);
					g.RestoreState ();
				}
			}
		}
	}
}

