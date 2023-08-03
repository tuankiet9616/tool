using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Models
{
	public class TaxBreakdowns
	{
		public string taxableAmount
		{
			get;
			set;
		}

		public string taxAmount
		{
			get;
			set;
		}

		public string taxPercentage
		{
			get;
			set;
		}

		public TaxBreakdowns()
		{
		}
	}
}