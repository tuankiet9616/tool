using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Domain
{
	public class Setup
	{
		public virtual string Code
		{
			get;
			set;
		}

		public virtual string FilePath
		{
			get;
			set;
		}

		public virtual int Id
		{
			get;
			set;
		}

		public Setup()
		{
		}
	}
}