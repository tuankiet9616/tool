using System;
using System.Runtime.CompilerServices;

namespace ViettelAPI.Models
{
	public class APIResult
	{
		public string description
		{
			get;
			set;
		}

        public InvoiceResult result
		{
			get;
			set;
		}

		public string transactionUuid
		{
			get;
			set;
		}

        public string errorCode
        {
            get;
            set;
        }

        public APIResult()
		{
		}
	}
}