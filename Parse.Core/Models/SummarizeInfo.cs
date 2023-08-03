using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Models
{
	public class SummarizeInfo
	{
		public string discountAmount
		{
			get;
			set;
		}

		public string settlementDiscountAmount
		{
			get;
			set;
		}

		public string sumOfTotalLineAmountWithoutTax
		{
			get;
			set;
		}

		public string taxPercentage
		{
			get;
			set;
		}

		public string totalAmountWithoutTax
		{
			get;
			set;
		}

		public string totalAmountWithTax
		{
			get;
			set;
		}

		public string totalAmountWithTaxInWords
		{
			get;
			set;
		}

		public string totalTaxAmount
		{
			get;
			set;
		}

		public SummarizeInfo()
		{
		}
	}
}