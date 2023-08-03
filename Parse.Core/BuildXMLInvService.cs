using Parse.Core.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Parse.Core
{
	public static class BuildXMLInvService
	{
		public static string BuildXMLInv(IList<InvoiceVAT> lstInvoice)
		{
			StringBuilder str = new StringBuilder("<Invoices>");
			foreach (InvoiceVAT invoice in lstInvoice)
			{
				if (invoice.Products.Count <= 0)
				{
					continue;
				}
				str.Append("<Inv>");
				str.AppendFormat("<key>{0}</key>", invoice.Fkey);
				str.Append("<Invoice>");
				DateTime arisingDate = invoice.ArisingDate;
				str.AppendFormat("<ArisingDate>{0}</ArisingDate>", arisingDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
				str.AppendFormat("<CusCode>{0}</CusCode>", invoice.CusCode);
				str.AppendFormat("<CusName>{0}</CusName>", BuildXMLInvService.convertSpecialCharacter(invoice.CusName));
				str.AppendFormat("<Buyer>{0}</Buyer>", BuildXMLInvService.convertSpecialCharacter(invoice.Buyer));
				str.AppendFormat("<CusAddress>{0}</CusAddress>", BuildXMLInvService.convertSpecialCharacter(invoice.CusAddress));
				str.AppendFormat("<CusPhone>{0}</CusPhone>", invoice.CusPhone);
				str.AppendFormat("<CusTaxCode>{0}</CusTaxCode>", invoice.CusTaxCode);
				str.AppendFormat("<PaymentMethod>{0}</PaymentMethod>", invoice.PaymentMethod);
				str.AppendFormat("<RoomNo>{0}</RoomNo>", invoice.RoomNo);
				str.AppendFormat("<BookingNo>{0}</BookingNo>", invoice.BookingNo);
				str.AppendFormat("<FolioNo>{0}</FolioNo>", invoice.FolioNo);
				str.AppendFormat("<ArrivalDate>{0}</ArrivalDate>", invoice.ArrivalDate);
				str.AppendFormat("<DepartureDate>{0}</DepartureDate>", invoice.DepartureDate);
				str.Append("<Products>");
				foreach (ProductInv el in invoice.Products)
				{
					str.Append("<Product>");
					str.AppendFormat("<Code>{0}</Code>", BuildXMLInvService.convertSpecialCharacter(el.Code));
					str.AppendFormat("<ProdName>{0}</ProdName>", BuildXMLInvService.convertSpecialCharacter(el.Name));
					str.AppendFormat("<ProdPrice>{0}</ProdPrice>", el.Price);
					str.AppendFormat("<ProdQuantity>{0}</ProdQuantity>", el.Quantity);
					str.AppendFormat("<ProdUnit>{0}</ProdUnit>", el.Unit);
					str.AppendFormat("<Amount>{0}</Amount>", el.Amount);
					str.Append("</Product>");
				}
				str.Append("</Products>");
				str.AppendFormat("<ServiceCharge>{0}</ServiceCharge>", invoice.ServiceCharge);
				str.AppendFormat("<Total>{0}</Total>", invoice.Total);
				str.AppendFormat("<VATRate>{0}</VATRate>", invoice.VATRate);
				str.AppendFormat("<VATAmount>{0}</VATAmount>", invoice.VATAmount);
				str.AppendFormat("<Amount>{0}</Amount>", invoice.Amount);
				str.AppendFormat("<AmountInWords>{0}</AmountInWords>", invoice.AmountInWords);
				str.Append("</Invoice>");
				str.Append("</Inv>");
			}
			str.Append("</Invoices>");
			return str.ToString();
		}

		private static string convertSpecialCharacter(string xmlData)
		{
			return string.Concat("<![CDATA[", xmlData, "]]>");
		}
	}
}