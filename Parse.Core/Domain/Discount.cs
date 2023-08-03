using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Domain
{
	public class Discount
	{
		public virtual string Description
		{
			get;
			set;
		}

		public virtual int Id
		{
			get;
			set;
		}

		public virtual decimal Total
		{
			get;
			set;
		}

		public virtual decimal Value
		{
			get;
			set;
		}

		public Discount()
		{
		}
	}
}