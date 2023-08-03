using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Models
{
	public class ItemInfo
	{
		[JsonIgnore]
		public string adjustmentTaxAmount
		{
			get;
			set;
		}

		public string discount
		{
			get;
			set;
		}

		public string expDate
		{
			get;
			set;
		}

		[JsonIgnore]
		public string isIncreaseItem
		{
			get;
			set;
		}

		public string itemCode
		{
			get;
			set;
		}

		public string itemDiscount
		{
			get;
			set;
		}

		public string itemName
		{
			get;
			set;
		}

		public string itemNote
		{
			get;
			set;
		}

		public string itemTotalAmountWithoutTax
		{
			get;
			set;
		}

		public string lineNumber
		{
			get;
			set;
		}

		public string quantity
		{
			get;
			set;
		}

		public string selection { get; set; } = "1";

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

		public string unitName
		{
			get;
			set;
		}

		public string unitPrice
		{
			get;
			set;
		}

		public ItemInfo()
		{
		}
	}
}