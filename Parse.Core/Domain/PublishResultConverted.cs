using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Parse.Core.Domain
{
	public class PublishResultConverted
	{
		public virtual List<Data> data
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

		public PublishResultConverted()
		{
		}
	}
}