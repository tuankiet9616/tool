using Parse.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Parse.Core.Domain
{
	[DataContract(Namespace="")]
	public abstract class InvoiceBase : Invoice
	{
		private IList<ProductInv> _Products = new List<ProductInv>();

		[DataMember(Name="Amount", Order=25)]
		public virtual decimal Amount
		{
			get;
			set;
		}

		[DataMember(Name="AmountInWords", Order=26)]
		public virtual string AmountInWords
		{
			get;
			set;
		}

		public virtual DateTime ArisingDate
		{
			get;
			set;
		}

		[DataMember(Name="Buyer", Order=27)]
		public virtual string Buyer
		{
			get;
			set;
		}

		[DataMember(Name="ComAddress", Order=9)]
		[XmlIgnore]
		public virtual string ComAddress
		{
			get;
			set;
		}

		[DataMember(Name="ComBankName", Order=12)]
		[XmlIgnore]
		public virtual string ComBankName
		{
			get;
			set;
		}

		[DataMember(Name="ComBankNo", Order=11)]
		[XmlIgnore]
		public virtual string ComBankNo
		{
			get;
			set;
		}

		[DataMember(Name="ComFax", Order=27)]
		[XmlIgnore]
		public virtual string ComFax
		{
			get;
			set;
		}

		[DataMember(Name="ComName", Order=7)]
		[XmlIgnore]
		public virtual string ComName
		{
			get;
			set;
		}

		[DataMember(Name="ComPhone", Order=10)]
		[XmlIgnore]
		public virtual string ComPhone
		{
			get;
			set;
		}

		[DataMember(Name="ComTaxCode", Order=8)]
		[XmlIgnore]
		public virtual string ComTaxCode
		{
			get;
			set;
		}

		[XmlElement("CreateBy")]
		public virtual string CreateBy
		{
			get;
			set;
		}

		[DataMember(Name="CusAddress", Order=17)]
		public virtual string CusAddress
		{
			get;
			set;
		}

		[DataMember(Name="CusBankName", Order=18)]
		[XmlIgnore]
		public virtual string CusBankName
		{
			get;
			set;
		}

		[DataMember(Name="CusBankNo", Order=19)]
		[XmlIgnore]
		public virtual string CusBankNo
		{
			get;
			set;
		}

		[DataMember(Name="CusCode", Order=13)]
		public virtual string CusCode
		{
			get;
			set;
		}

		[DataMember(Name="CusName", Order=14)]
		public virtual string CusName
		{
			get;
			set;
		}

		[DataMember(Name="CusPhone", Order=16)]
		[XmlIgnore]
		public virtual string CusPhone
		{
			get;
			set;
		}

		[DataMember(Name="CusTaxCode", Order=15)]
		public virtual string CusTaxCode
		{
			get;
			set;
		}

		[IgnoreDataMember]
		[XmlIgnore]
		public virtual string Fkey
		{
			get;
			set;
		}

		[DataMember(Name="InvoiceName", Order=1)]
		[XmlIgnore]
		public virtual string Name
		{
			get;
			set;
		}

		[DataMember(Name="InvoiceNo", Order=4)]
		[XmlIgnore]
		public virtual string No
		{
			get;
			set;
		}

		[XmlElement("Note")]
		public virtual string Note
		{
			get;
			set;
		}

		[DataMember(Name="InvoicePattern", Order=2)]
		[XmlIgnore]
		public virtual string Pattern
		{
			get;
			set;
		}

		[DataMember(Name="PaymentMethod", Order=6)]
		public virtual string PaymentMethod
		{
			get;
			set;
		}

		[XmlIgnore]
		public virtual int? Paymentstatus
		{
			get;
			set;
		}

		[XmlElement("PaymentStatus")]
		public virtual int PaymentStatus
		{
			get;
			set;
		}

		[DataMember(Name="ArisingDate")]
		private string performanceDateSerialized
		{
			get;
			set;
		}

		[XmlElement("ProcessInvNote")]
		public virtual string ProcessInvNote
		{
			get;
			set;
		}

		[DataMember(Name="Products", Order=27)]
		[XmlArray("Products")]
		public virtual IList<ProductInv> Products
		{
			get
			{
				return this._Products;
			}
			set
			{
				this._Products = value;
			}
		}

		[DataMember(Name="SerialNo", Order=3)]
		[XmlIgnore]
		public virtual string Serial
		{
			get;
			set;
		}

		[DataMember(Name="SignDate")]
		[XmlIgnore]
		public virtual string SignDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");

		[XmlIgnore]
		public virtual int? Status
		{
			get;
			set;
		}

		[DataMember(Name="Total", Order=20)]
		public virtual decimal Total
		{
			get;
			set;
		}

		[XmlIgnore]
		public virtual int? Type
		{
			get;
			set;
		}

		[DataMember(Name="VATAmount", Order=21)]
		public virtual decimal VATAmount
		{
			get;
			set;
		}

		[DataMember(Name="VATRate", Order=22)]
		public virtual float VATRate
		{
			get;
			set;
		}

		protected InvoiceBase()
		{
		}

		public virtual string GetXMLData(Company company)
		{
			this.ComName = company.Name;
			this.ComBankName = company.BankName;
			this.ComBankNo = company.BankNumber;
			this.ComTaxCode = company.TaxCode;
			this.ComPhone = company.Phone;
			this.ComAddress = company.Address;
			List<System.Type> knownTypeList = new List<System.Type>()
			{
				typeof(ProductInv)
			};
			DataContractSerializer dataContractSerializer = new DataContractSerializer(this.GetType(), knownTypeList);
			MemoryStream ms = new MemoryStream();
			dataContractSerializer.WriteObject(ms, this);
			XmlDocument xdoc = new XmlDocument()
			{
				PreserveWhitespace = true
			};
			xdoc.LoadXml(Encoding.UTF8.GetString(ms.GetBuffer()));
			string href = string.Format("{0}/{1}", company.Domain, "InvoiceTemplate/GetXSLTbyPattern?pattern=01GTKT0/001");
			string PItext = string.Concat("type='text/xsl' href='", href, "'");
			XmlProcessingInstruction newPI = xdoc.CreateProcessingInstruction("xml-stylesheet", PItext);
			xdoc.InsertBefore(newPI, xdoc.DocumentElement);
			XDocument xDocument = xdoc.GetXDocument();
			XmlHelper.RemoveAttribte(xDocument);
			XmlHelper.RemoveNamespace(xDocument);
			xdoc = XmlHelper.GetXmlDocument(xDocument);
			XmlNodeList xlist = xdoc.DocumentElement.ChildNodes;
			XmlNode newnode = xdoc.DocumentElement.AppendChild(xdoc.CreateElement("Content"));
			XmlAttribute xa1 = xdoc.CreateAttribute("Id");
			xa1.Value = "SigningData";
			newnode.Attributes.Append(xa1);
			for (int i = 0; i < xlist.Count - 1; i++)
			{
				newnode.AppendChild(xlist[i]);
				i--;
			}
			xdoc.DocumentElement.RemoveAll();
			xdoc.DocumentElement.AppendChild(newnode);
			return xdoc.OuterXml;
		}

		[OnSerializing]
		private void OnSerializing(StreamingContext context)
		{
			this.performanceDateSerialized = this.ArisingDate.ToString("dd/MM/yyyy");
		}

		public virtual string SerializeToXML()
		{
			return string.Empty;
		}
	}
}