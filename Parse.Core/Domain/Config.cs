using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Domain
{
	public class Config
	{
		public virtual string Code
		{
			get;
			set;
		}

		public virtual int ComID
		{
			get;
			set;
		}

		public virtual int Id
		{
			get;
			set;
		}

		public virtual string Value
		{
			get;
			set;
		}

		public Config()
		{
		}
	}
}