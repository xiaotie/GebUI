using System;

namespace Geb.UI.iOS
{
	public static class Context
	{
		/// <summary>
		/// Xamarin.Forms only looks at loaded assemblies. That means If you are putting renderers 
		/// into a custom library, then that library needs to be loaded before Forms.Init is called. 
		/// That means you must have used some type in the library already.
		/// Call the above Init method just before you call Forms.Init in your platform assemblies 
		/// </summary>
		public static void Init()
		{
		}
	}
}

