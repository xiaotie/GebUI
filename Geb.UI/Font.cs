using System;

namespace Geb.UI
{
	public class Font
	{
		public String Name { get; set; }
		public float Size { get; set; }

		public Font ()
		{
		}

		public Font(String name, float size)
		{
			this.Name = name;
			this.Size = size;
		}
	}
}

