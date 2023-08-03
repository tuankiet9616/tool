using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Domain
{
	public class BussinessLog
	{
		public virtual string AppName
		{
			get;
			set;
		}

		public virtual DateTime CreateDate
		{
			get;
			set;
		}

		public virtual string Error
		{
			get;
			set;
		}

		public virtual string FileName
		{
			get;
			set;
		}

		public virtual int Id
		{
			get;
			set;
		}

		public BussinessLog()
		{
		}
	}
}