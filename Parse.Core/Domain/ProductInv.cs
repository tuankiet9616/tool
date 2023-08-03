using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Parse.Core.Domain
{
	[DataContract(Name="Product")]
	[XmlType("Product")]
	public class ProductInv : IProductInv
	{
		[DataMember(Name="Amount")]
		[XmlElement("Amount")]
		public virtual decimal Amount
		{
			get;
			set;
		}

		[DataMember(Name="Code")]
		[XmlElement("Code")]
		public virtual string Code
		{
			get;
			set;
		}

		public virtual decimal DiscountAmount
		{
			get;
			set;
		}

		public virtual int Id
		{
			get;
			set;
		}

		public virtual int InvoiceID
		{
			get;
			set;
		}

		[DataMember(Name="ProdName")]
		[XmlElement("ProdName")]
		public virtual string Name
		{
			get;
			set;
		}

		[DataMember(Name="ProdPrice")]
		[XmlElement("ProdPrice")]
		public virtual decimal Price
		{
			get;
			set;
		}

		[DataMember(Name="ProDate")]
		[XmlElement("ProDate")]
		public virtual string ProDate
		{
			get;
			set;
		}

		[DataMember(Name="ProdQuantity")]
		[XmlElement("ProdQuantity")]
		public virtual decimal Quantity
		{
			get;
			set;
		}

		[DataMember(Name="Remark")]
		[XmlElement("Remark")]
		public virtual string Remark
		{
			get;
			set;
		}

		[DataMember(Name="Total")]
		[XmlElement("Total")]
		public virtual decimal Total
		{
			get;
			set;
		}

		public virtual int Type
		{
			get;
			set;
		}

		[DataMember(Name="ProdUnit")]
		[XmlElement("ProdUnit")]
		public virtual string Unit
		{
			get;
			set;
		}

		[DataMember(Name="VATAmount")]
		[XmlElement("VATAmount")]
		public virtual decimal VATAmount
		{
			get;
			set;
		}

		[DataMember(Name="VATRate")]
		[XmlElement("VATRate")]
		public virtual float VATRate
		{
			get;
			set;
		}

		public virtual float Weight
		{
			get;
			set;
		}

		public ProductInv()
		{
		}
	}
}