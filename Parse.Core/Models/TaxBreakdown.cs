using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Models
{
	public class TaxBreakdown
	{
		public decimal taxableAmount
		{
			get;
			set;
		}

		public decimal taxAmount
		{
			get;
			set;
		}

		public decimal taxPercentage
		{
			get;
			set;
		}

		public TaxBreakdown()
		{
		}
	}
}