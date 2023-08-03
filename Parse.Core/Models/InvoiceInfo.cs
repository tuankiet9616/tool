using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Models
{
	public class InvoiceInfo
	{
		[JsonIgnore]
		public string additionalReferenceDate
		{
			get;
			set;
		}

		[JsonIgnore]
		public string additionalReferenceDesc
		{
			get;
			set;
		}

		[JsonIgnore]
		public string adjustmentInvoiceType
		{
			get;
			set;
		}

		public string adjustmentType
		{
			get;
			set;
		}

		public string currencyCode
		{
			get;
			set;
		}

		public bool cusGetInvoiceRight
		{
			get;
			set;
		}

		public string invoiceIssuedDate
		{
			get;
			set;
		}

        public string invoiceNo
        {
            get;
            set;
        }

        //[JsonIgnore]
        public string invoiceNote
		{
			get;
			set;
		}

		public string invoiceSeries
		{
			get;
			set;
		}

		public string invoiceType
		{
			get;
			set;
		}

		[JsonIgnore]
		public string originalInvoiceId
		{
			get;
			set;
		}

		[JsonIgnore]
		public string originalInvoiceIssueDate
		{
			get;
			set;
		}

		public bool paymentStatus
		{
			get;
			set;
		}

		public string paymentType
		{
			get;
			set;
		}

		public string paymentTypeName
		{
			get;
			set;
		}

		public string templateCode
		{
			get;
			set;
		}

		public string transactionUuid
		{
			get;
			set;
		}

		public InvoiceInfo()
		{
		}
	}
}