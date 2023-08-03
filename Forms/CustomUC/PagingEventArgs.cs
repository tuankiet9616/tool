using System;
using System.Runtime.CompilerServices;

namespace Parse.Forms.CustomUC
{
	public class PagingEventArgs : EventArgs
	{
		public int NextPageIndex
		{
			get;
			set;
		}

		public PagingEventArgs()
		{
		}
	}
}