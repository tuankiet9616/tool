using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Domain
{
	public class PublishResult
	{
		public virtual object data
		{
			get;
			set;
		}

		public virtual string messages
		{
			get;
			set;
		}

		public virtual string status
		{
			get;
			set;
		}

		public PublishResult()
		{
		}
	}
}