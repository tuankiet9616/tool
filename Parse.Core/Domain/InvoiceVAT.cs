using Parse.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Parse.Core.Domain
{
	[DataContract(Name = "Invoice", Namespace = "")]
	[KnownType(typeof(InvoiceVAT))]
	[XmlType("Invoice")]
	public class InvoiceVAT : InvoiceBase
	{
		public virtual string AppName
		{
			get;
			set;
		}

		[DataMember(Name = "ArrivalDate")]
		[XmlElement("ArrivalDate")]
		public virtual string ArrivalDate
		{
			get;
			set;
		}

		[DataMember(Name = "BookingNo")]
		[XmlElement("BookingNo")]
		public virtual string BookingNo
		{
			get;
			set;
		}

		public virtual bool Converted
		{
			get;
			set;
		}

		public virtual string CusComName
		{
			get;
			set;
		}

		public virtual string CusDeliveryAddress
		{
			get;
			set;
		}

		public virtual string CusEmail
		{
			get;
			set;
		}

		public virtual string DeliveryId
		{
			get;
			set;
		}

		[DataMember(Name = "DepartureDate")]
		[XmlElement("DepartureDate")]
		public virtual string DepartureDate
		{
			get;
			set;
		}

		public virtual decimal DiscountAmount
		{
			get;
			set;
		}

		[DataMember(Name = "FolioNo")]
		[XmlElement("FolioNo")]
		public virtual string FolioNo
		{
			get;
			set;
		}

		public virtual string FolioOrigin
		{
			get;
			set;
		}

        public virtual string DeliveryDate
        {
            get;
            set;
        }

        public virtual string DeliveryNo
        {
            get;
            set;
        }

        public virtual string DueDate
        {
            get;
            set;
        }

        public virtual string InvoiceNoSAP
        {
            get;
            set;
        }

        public virtual string SaleOrderNo
        {
            get;
            set;
        }

        public virtual string CodeOfTax
        {
            get;
            set;
        }

        public virtual string ExchangeStatus
        {
            get;
            set;
        }

        public virtual string ExchangeDes
        {
            get;
            set;
        }

        [DataMember(Name = "GuestNo")]
		[XmlElement("GuestNo")]
		public virtual string GuestNo
		{
			get;
			set;
		}

		public virtual int Id
		{
			get;
			set;
		}

		public virtual string MessageError
		{
			get;
			set;
		}

		public virtual PublishStatus Publish
		{
			get;
			set;
		}

		public virtual decimal Quantity
		{
			get;
			set;
		}

		[DataMember(Name = "RoomNo")]
		[XmlElement("RoomNo")]
		public virtual string RoomNo
		{
			get;
			set;
		}

		[DataMember(Name = "ServiceCharge")]
		[XmlElement("ServiceCharge")]
		public virtual decimal ServiceCharge
		{
			get;
			set;
		}

		[DataMember(Name = "ServiceSpecial")]
		[XmlElement("ServiceSpecial")]
		public virtual decimal ServiceSpecial
		{
			get;
			set;
		}

		public virtual string StaffId
		{
			get;
			set;
		}

		[DataMember(Name = "VATSerCharge")]
		[XmlElement("VATSerCharge")]
		public virtual float VATSerCharge
		{
			get;
			set;
		}

		[DataMember(Name = "VATSerSpecial")]
		[XmlElement("VATSerSpecial")]
		public virtual float VATSerSpecial
		{
			get;
			set;
		}

		public virtual float Weight
		{
			get;
			set;
		}
		public virtual string CurrencyCode
		{
			get;
			set;
		}
		public virtual string AdjustmentType
		{
			get;
			set;
		}
		public InvoiceVAT()
		{
		}

		private string convertSpecialCharacter(string xmlData)
		{
			return string.Concat("<![CDATA[", xmlData, "]]>");
		}

		public override string SerializeToXML()
		{
			string str1;
			try
			{
				StringBuilder str = new StringBuilder("<Invoice>");
				DateTime arisingDate = this.ArisingDate;
				str.AppendFormat("<ArisingDate>{0}</ArisingDate>", arisingDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
				str.AppendFormat("<CusCode>{0}</CusCode>", this.CusCode);
				str.AppendFormat("<CusName>{0}</CusName>", this.convertSpecialCharacter(this.CusName));
				str.AppendFormat("<Buyer>{0}</Buyer>", this.convertSpecialCharacter(this.Buyer));
				str.AppendFormat("<CusAddress>{0}</CusAddress>", this.convertSpecialCharacter(this.CusAddress));
				str.AppendFormat("<CusPhone>{0}</CusPhone>", this.CusPhone);
				str.AppendFormat("<CusTaxCode>{0}</CusTaxCode>", this.CusTaxCode);
				str.AppendFormat("<PaymentMethod>{0}</PaymentMethod>", this.PaymentMethod);
				str.AppendFormat("<RoomNo>{0}</RoomNo>", this.RoomNo);
				str.AppendFormat("<BookingNo>{0}</BookingNo>", this.BookingNo);
				str.AppendFormat("<FolioNo>{0}</FolioNo>", this.FolioNo);
				str.AppendFormat("<ArrivalDate>{0}</ArrivalDate>", this.ArrivalDate);
				str.AppendFormat("<DepartureDate>{0}</DepartureDate>", this.DepartureDate);
				str.Append("<Products>");
				foreach (ProductInv el in this.Products)
				{
					str.Append("<Product>");
					str.AppendFormat("<Code>{0}</Code>", this.convertSpecialCharacter(el.Code));
					str.AppendFormat("<ProdName>{0}</ProdName>", this.convertSpecialCharacter(el.Name));
					str.AppendFormat("<ProdPrice>{0}</ProdPrice>", el.Price);
					str.AppendFormat("<ProdQuantity>{0}</ProdQuantity>", el.Quantity);
					str.AppendFormat("<ProdUnit>{0}</ProdUnit>", el.Unit);
					str.AppendFormat("<Amount>{0}</Amount>", el.Amount);
					str.Append("</Product>");
				}
				str.Append("</Products>");
				str.AppendFormat("<ServiceCharge>{0}</ServiceCharge>", this.ServiceCharge);
				str.AppendFormat("<Total>{0}</Total>", this.Total);
				str.AppendFormat("<VATRate>{0}</VATRate>", this.VATRate);
				str.AppendFormat("<VATAmount>{0}</VATAmount>", this.VATAmount);
				str.AppendFormat("<Amount>{0}</Amount>", this.Amount);
				str.AppendFormat("<AmountInWords>{0}</AmountInWords>", this.AmountInWords);
				str.Append("</Invoice>");
				str1 = str.ToString();
			}
			catch (Exception exception)
			{
				throw exception;
			}
			return str1;
		}
	}
}