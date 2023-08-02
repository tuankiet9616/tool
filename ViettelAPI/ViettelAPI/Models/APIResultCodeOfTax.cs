using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ViettelAPI.Models
{
	public class APIResultCodeOfTax
	{
		public string description
		{
			get;
			set;
		}

		public string errorCode
		{
			get;
			set;
		}

		public List<InvoiceResult> result
		{
			get;
			set;
		}

		public string transactionUuid
		{
			get;
			set;
		}

		public APIResultCodeOfTax()
		{
		}
	}
}