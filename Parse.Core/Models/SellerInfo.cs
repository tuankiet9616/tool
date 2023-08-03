using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Models
{
	public class SellerInfo
	{
		public string sellerAddressLine
		{
			get;
			set;
		}

		public string sellerBankAccount
		{
			get;
			set;
		}

		public string sellerBankName
		{
			get;
			set;
		}

		public string sellerEmail
		{
			get;
			set;
		}

		public string sellerLegalName
		{
			get;
			set;
		}

		public string sellerPhoneNumber
		{
			get;
			set;
		}

        public string sellerFaxNumber
        {
            get;
            set;
        }

        public string sellerTaxCode
		{
			get;
			set;
		}

        public string sellerDistrictName
        {
            get;
            set;
        }

        public string sellerCityName
        {
            get;
            set;
        }

        public string sellerCountryCode
        {
            get;
            set;
        }

        public SellerInfo()
		{
		}
	}
}