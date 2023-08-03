using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Domain
{
	public class Data
	{
		public virtual string InvNo
		{
			get;
			set;
		}

		public virtual string InvSerial
		{
			get;
			set;
		}

		public virtual string Key
		{
			get;
			set;
		}

		public Data()
		{
		}
	}
}