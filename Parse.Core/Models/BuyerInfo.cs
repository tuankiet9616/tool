using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Models
{
	public class BuyerInfo
	{
		public string buyerAddressLine { get; set; } = string.Empty;

		public string buyerBankAccount { get; set; } = string.Empty;

		public string buyerBankName { get; set; } = string.Empty;

        public string buyerCode { get; set; } = string.Empty;

        public string buyerEmail { get; set; } = string.Empty;

		public string buyerIdNo { get; set; } = string.Empty;

		public string buyerIdType { get; set; } = string.Empty;

		public string buyerLegalName { get; set; } = string.Empty;

		public string buyerName { get; set; } = string.Empty;

		public string buyerPhoneNumber { get; set; } = string.Empty;

        public string buyerFaxNumber { get; set; } = string.Empty;

        public string buyerTaxCode { get; set; } = string.Empty;

        public BuyerInfo()
		{
		}
	}
}