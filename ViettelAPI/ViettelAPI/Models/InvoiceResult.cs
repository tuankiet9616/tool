using System;
using System.Runtime.CompilerServices;

namespace ViettelAPI.Models
{
	public class InvoiceResult
	{
		public string invoiceNo
		{
			get;
			set;
		}

		public string reservationCode
		{
			get;
			set;
		}

		public string supplierTaxCode
		{
			get;
			set;
		}

		public string transactionID
		{
			get;
			set;
		}

        public string issueDate
        {
            get;
            set;
        }

        public string status
        {
            get;
            set;
        }

        public string exchangeStatus
        {
            get;
            set;
        }

        public string exchangeDes
        {
            get;
            set;
        }

        public string codeOfTax
        {
            get;
            set;
        }

        public InvoiceResult()
		{
		}
	}
}