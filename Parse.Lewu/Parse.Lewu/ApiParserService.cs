using Parse.Core;
using Parse.Core.Domain;
using Parse.Core.IService;
using Parse.Core.Models;
using Parse.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Parse.Lewu
{
	public class ApiParserService : IApiParserService
	{
		public ApiParserService()
		{
		}

		public List<InvoiceModels> ConvertToAPIModel(List<InvoiceVAT> invoices)
		{
			List<InvoiceModels> dsHoaDon = new List<InvoiceModels>();
			foreach (InvoiceVAT invoice in 
				from c in invoices
				//orderby c.ArisingDate
				select c)
			{
                InvoiceModels model = MappingToInvoiceModel(invoice);
                dsHoaDon.Add(model);
			}
			return dsHoaDon;
		}

        public InvoiceModels MappingToInvoiceModel(InvoiceVAT invoice)
        {
            long unixTime;
            string str;
            Company company = Parse.Core.AppContext.Current.company;

            InvoiceInfo objInvoice = new InvoiceInfo()
            {
                transactionUuid = invoice.Fkey,
                invoiceType = (!string.IsNullOrEmpty(company.InvPattern) ? company.InvPattern.Substring(0, 1) : ""),
                templateCode = (!string.IsNullOrEmpty(company.InvPattern) ? company.InvPattern : ""),
                invoiceSeries = (!string.IsNullOrEmpty(company.InvSerial) ? company.InvSerial : "")
            };
            InvoiceInfo invoiceInfo = objInvoice;
            if (invoice.ArisingDate < DateTime.Now.Date)
            {
                DateTime dateTime = invoice.ArisingDate.AddHours(23);
                dateTime = dateTime.AddMinutes(59);
                unixTime = NumberUtil.ConvertToUnixTime(dateTime.AddSeconds(59));
                str = unixTime.ToString();
            }
            else
            {
                str = "";
            }

            invoiceInfo.invoiceIssuedDate = str;
            objInvoice.currencyCode = "VND";
            objInvoice.adjustmentType = "1";
            objInvoice.invoiceNo = "";
            objInvoice.invoiceNote = "";
            objInvoice.paymentStatus = true;
            objInvoice.paymentType = invoice.PaymentMethod;
            objInvoice.paymentTypeName = invoice.PaymentMethod;
            objInvoice.cusGetInvoiceRight = true;

            BuyerInfo objBuyer = new BuyerInfo()
            {
                buyerName = invoice.Buyer,
                buyerLegalName = invoice.Buyer,
                buyerTaxCode = invoice.CusTaxCode,
                buyerAddressLine = invoice.CusAddress,
                buyerCode = invoice.CusCode,
                buyerPhoneNumber = invoice.CusPhone,
                buyerFaxNumber = "",
                buyerEmail = invoice.CusEmail,
                buyerBankName = invoice.CusBankName,
                buyerBankAccount = invoice.CusBankNo,
                buyerIdType = null,
                buyerIdNo = null
            };

            SellerInfo objSeller = new SellerInfo()
            {
                sellerLegalName = invoice.ComName,
                sellerTaxCode = invoice.ComTaxCode,
                sellerAddressLine = invoice.ComAddress,
                sellerPhoneNumber = invoice.ComPhone,
                sellerFaxNumber = "",
                sellerEmail = "",
                sellerBankName = invoice.ComBankName,
                sellerBankAccount = invoice.ComBankNo,
                sellerDistrictName = "",
                sellerCityName = "",
                sellerCountryCode = ""
            };
            List<ItemInfo> lstItem = new List<ItemInfo>();
            List<ItemInfo> lstItemTemp = new List<ItemInfo>();

            foreach (ProductInv pro in invoice.Products)
            {
                ItemInfo item = new ItemInfo()
                {
                    discount = "0.0",
                    itemCode = pro.Code,
                    itemDiscount = pro.DiscountAmount.ToString(),
                    itemName = string.Format("{0} {1}", pro.Name, string.Concat(';',pro.Remark)),
                    itemTotalAmountWithoutTax = Math.Abs(pro.Total).ToString(),
                    lineNumber = "1",
                    quantity = pro.Quantity.ToString(),
                    taxAmount = Math.Abs(pro.VATAmount).ToString(),
                    taxPercentage = pro.VATRate.ToString(),
                    unitName = pro.Unit,
                    unitPrice = Math.Abs(pro.Price).ToString(),
                    expDate = pro.ProDate,
                    itemNote = string.Empty
                };

                List<string> itemCode = pro.Name.Split(' ').ToList();
                if (itemCode.ElementAt(0).Equals("51294") || itemCode.ElementAt(0).Equals("50524"))
                {
                    item.selection = "3";
                }

                lstItemTemp.Add(item);
            }
            lstItem.AddRange(lstItemTemp);
            
            SummarizeInfo objSummary = new SummarizeInfo()
            {
                discountAmount = "0",
                settlementDiscountAmount = "0",
                taxPercentage = invoice.VATRate.ToString(),
                totalAmountWithTax = Math.Abs(invoice.Amount).ToString(),
                totalAmountWithTaxInWords = NumberUtil.DocSoThanhChu(invoice.Amount.ToString()),
                totalTaxAmount = Math.Abs(invoice.VATAmount).ToString(),
                totalAmountWithoutTax = Math.Abs(invoice.Total).ToString(),
                sumOfTotalLineAmountWithoutTax = Math.Abs(invoice.Total).ToString()
            };
            List<Metadata> lstMetaData = new List<Metadata>()
                {
                    new Metadata()
                    {
                        keyTag = "deliveryDate",
                        keyLabel = "Ngày giao hàng",
                        dateValue = null,
                        stringValue = invoice.DeliveryDate,
                        numberValue = null,
                        valueType = "text"
                    },
                    new Metadata()
                    {
                        keyTag = "deliveryNo",
                        keyLabel = "Số giao hàng",
                        dateValue = null,
                        stringValue = invoice.DeliveryNo,
                        numberValue = null,
                        valueType = "text"
                    },
                    new Metadata()
                    {
                        keyTag = "dueDate",
                        keyLabel = "Ngày đáo hạn",
                        dateValue = null,
                        stringValue = invoice.DueDate,
                        numberValue = null,
                        valueType = "text"
                    },
                    new Metadata()
                    {
                        keyTag = "invoiceNoSAP",
                        keyLabel = "Số hóa đơn SAP",
                        dateValue = null,
                        stringValue = invoice.InvoiceNoSAP,
                        numberValue = null,
                        valueType = "text"
                    },
                    new Metadata()
                    {
                        keyTag = "saleOrderNo",
                        keyLabel = "Số đơn bán hàng",
                        dateValue = null,
                        stringValue = invoice.SaleOrderNo,
                        numberValue = null,
                        valueType = "text"
                    }
                };
            List<Metadata> metadatas = lstMetaData;
            
            InvoiceModels model = new InvoiceModels()
            {
                generalInvoiceInfo = objInvoice,
                buyerInfo = objBuyer,
                sellerInfo = objSeller,
                payments = new List<Payment>()
                    {
                        new Payment()
                        {
                            paymentMethodName = invoice.PaymentMethod
                        }
                    },
                deliveryInfo = null,
                itemInfo = lstItem,
                summarizeInfo = objSummary,
                metadata = lstMetaData,
                taxBreakdowns = new List<TaxBreakdown>()
                    {
                        new TaxBreakdown()
                        {
                            taxPercentage = (decimal)invoice.VATRate,
                            taxableAmount = Math.Abs(invoice.Total),
                            taxAmount = Math.Abs(invoice.VATAmount)
                        }
                    }
            };
            return model;
        }

    }
}