using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Parse.Core.Models
{
	public class InvoiceModels
	{
        public InvoiceInfo generalInvoiceInfo
        {
            get;
            set;
        }

        public BuyerInfo buyerInfo
		{
			get;
			set;
		}

        public SellerInfo sellerInfo
        {
            get;
            set;
        }

        public IList<string> extAttribute { get; set; } = new List<string>();

        public IList<Payment> payments { get; set; } = new List<Payment>();

        public IList<string> deliveryInfo { get; set; } = new List<string>();

        public IList<ItemInfo> itemInfo { get; set; } = new List<ItemInfo>();

        public IList<Metadata> metadata { get; set; } = new List<Metadata>();

        public IList<MeterRead> meterReading { get; set; } = new List<MeterRead>();

		public SummarizeInfo summarizeInfo
		{
			get;
			set;
		}

		public IList<TaxBreakdown> taxBreakdowns { get; set; } = new List<TaxBreakdown>();

		public InvoiceModels()
		{
		}
	}
}