using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Models
{
	public class Metadata
	{
		public virtual string dateValue
		{
			get;
			set;
		}

		public virtual string keyLabel
		{
			get;
			set;
		}

		public virtual string keyTag
		{
			get;
			set;
		}

		public virtual string numberValue
		{
			get;
			set;
		}

		public virtual string stringValue
		{
			get;
			set;
		}

		public virtual string valueType
		{
			get;
			set;
		}

		public Metadata()
		{
		}
	}
}