using Parse.Core;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Parse.Forms.Common
{
	public class CustomWatcher : FileSystemWatcher
	{
		public MenuModel Menu
		{
			get;
			set;
		}

		public CustomWatcher()
		{
		}
	}
}