using Newtonsoft.Json;
using Parse.Core;
using Parse.Core.Domain;
using Parse.Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ViettelAPI.Models;
using Newtonsoft.Json.Linq;
using Parse.Core.IService;
using FX.Core;
using Parse.Core.Implement;
using System.Text.RegularExpressions;
using System.Net;

namespace ViettelAPI
{
	public class APIHelper
	{
		public APIHelper()
		{
		}

		private static string Base64Decode(string base64EncodedData)
		{
			byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
			return Encoding.UTF8.GetString(base64EncodedBytes);
		}

		private static string Base64Encode(string plainText)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
		}

		public static PDFFileResponse GetInvoicePdf(string invNo, string pattern, string Fkey)
		{
			string userName = Parse.Core.AppContext.Current.company.UserName;
			string pass = Parse.Core.AppContext.Current.company.PassWord;
			string userPass = string.Concat(userName, ":", pass);
			string taxCode = GetCodeTax(Parse.Core.AppContext.Current.company.TaxCode);
			string apiLink = string.Concat(Parse.Core.AppContext.Current.company.Domain, "/InvoiceAPI/InvoiceUtilsWS/getInvoiceRepresentationFile");
			string invPattern = pattern;
			string contentType = "application/json";
			string autStr = ViettelAPI.APIHelper.Base64Encode(userPass);
			string data = JsonConvert.SerializeObject(new { supplierTaxCode = taxCode, invoiceNo = invNo, pattern = invPattern, transactionUuid = Fkey, fileType = "PDF" });
			return ViettelAPI.APIHelper.ParseResult<PDFFileResponse>(ViettelAPI.APIHelper.Request(apiLink, data, autStr, "POST", contentType));
		}

        public static PDFFileResponse GetInvoicePdfV2(string invNo, string pattern, string Fkey)
        {
            string accessToken = String.Empty;
            string tokenUpdateAt = Parse.Core.AppContext.Current.company.Token_Update_At;
            bool isTUpdate = CheckToken(tokenUpdateAt);
            ICompanyService service = IoC.Resolve<ICompanyService>();
            service.BeginTran();

            try
            {
                if (!isTUpdate)
                {
                    string username = Parse.Core.AppContext.Current.company.UserName;
                    string password = Parse.Core.AppContext.Current.company.PassWord;
                    accessToken = GetToken(username, password);

                    Company curCom = AppContext.Current.company;
                    curCom.Token = accessToken;
                    curCom.Token_Update_At = Convert.ToString(Parse.Core.Utils.NumberUtil.ConvertToUnixTime(DateTime.Now));
                    service.Update(curCom);
                    service.CommitTran();
                    AppContext.InitContext(curCom);
                }
                else
                {
                    accessToken = Parse.Core.AppContext.Current.company.Token;
                }
            }
            catch (Exception ex)
            {
                service.RolbackTran();
                throw ex;
            }

            string userName = Parse.Core.AppContext.Current.company.UserName;
            string pass = Parse.Core.AppContext.Current.company.PassWord;
            string taxCode = GetCodeTax(Parse.Core.AppContext.Current.company.UserName);
            string apiLink = string.Concat(Parse.Core.AppContext.Current.company.Domain, "/services/einvoiceapplication/api/InvoiceAPI/InvoiceUtilsWS/getInvoiceRepresentationFile");
            string invPattern = pattern;
            string contentType = "application/json";
            string data = JsonConvert.SerializeObject(new { supplierTaxCode = taxCode, invoiceNo = invNo, templateCode = invPattern, fileType = "PDF" });
            return ViettelAPI.APIHelper.ParseResult<PDFFileResponse>(ViettelAPI.APIHelper.RequestV2(apiLink, data, accessToken, "POST", contentType, null, 0, 1));
        }

        private static void InitiateSSLTrust()
		{
			try
			{
				ServicePointManager.ServerCertificateValidationCallback = (object argument0, X509Certificate argument1, X509Chain argument2, SslPolicyErrors argument3) => true;
			}
			catch (Exception exception)
			{
			}
		}

		private static T ParseResult<T>(string result)
		{
			return JsonConvert.DeserializeObject<T>(result);
		}

		private static string Request(string pzUrl, string pzData, string pzAuthorization, string pzMethod, string pzContentType)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(pzUrl);
			httpWebRequest.ContentType = pzContentType;
			httpWebRequest.Method = pzMethod;
			httpWebRequest.Headers.Add("Authorization", string.Concat("Basic ", pzAuthorization));
			httpWebRequest.Proxy = WebRequest.DefaultWebProxy;
			if (!string.IsNullOrEmpty(pzData))
			{
				using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
				{
					streamWriter.Write(pzData);
					streamWriter.Flush();
					streamWriter.Close();
				}
			}
			ViettelAPI.APIHelper.InitiateSSLTrust();
			HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
			string result = string.Empty;
			using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
			{
				result = streamReader.ReadToEnd();
			}
            InvoiceVATService.log.Error(string.Format("Data Invoice Body Response : {0}", result));
            return result;
		}

        public static string RequestV2(string pzUrl, string pzData, string accessToken, string pzMethod, string pzContentType, string proxyIP, int port, int ssl)
        {
            try
            {
                if (ssl == 1)
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00)
                                                           | SecurityProtocolType.Ssl3;
                }

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(pzUrl);
                httpWebRequest.ContentType = pzContentType;
                httpWebRequest.Method = pzMethod;
                httpWebRequest.Headers.Add("Cookie", "access_token="+accessToken);
                httpWebRequest.Timeout = 30000;

                if (!string.IsNullOrEmpty(proxyIP))
                {
                    WebProxy proxy = new WebProxy(proxyIP, port);
                    httpWebRequest.Proxy = proxy;
                }

                if (!string.IsNullOrEmpty(pzData))
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = pzData;

                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                var result = string.Empty;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                InvoiceVATService.log.Error(string.Format("Data Invoice Body Response : {0}", result));
                return result;
            }
            catch (WebException wex)
            {
                throw wex;
            }
        }

        public static string webRequestgetToken(string pzUrl, string pzData, string pzMethod, string pzContentType, string proxyIP, int port, int ssl)
        {
            try
            {
                if (ssl == 1)
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00)
                                                           | SecurityProtocolType.Ssl3;
                }
                var httpWebRequest = WebRequest.Create(pzUrl);
                httpWebRequest.ContentType = pzContentType;
                httpWebRequest.Method = pzMethod;
                httpWebRequest.Timeout = 30000;

                if (!string.IsNullOrEmpty(proxyIP))
                {
                    WebProxy proxy = new WebProxy(proxyIP, port);
                    httpWebRequest.Proxy = proxy;
                }

                if (!string.IsNullOrEmpty(pzData))
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = pzData;

                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }

                var httpResponse = httpWebRequest.GetResponse();
                var result = string.Empty;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("NOK " + ex.Message);
            }
        }

        public static string webRequestgetCodeOfTax(string pzUrl, string pzData, string accessToken, string pzMethod, string pzContentType, string proxyIP, int port, int ssl)
        {
            try
            {
                if (ssl == 1)
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00 | 0x30);
                }

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(pzUrl);
                httpWebRequest.ContentType = pzContentType;
                httpWebRequest.Method = pzMethod;
                httpWebRequest.Timeout = 20 * 1000;
                httpWebRequest.Headers.Add("Cookie", "access_token=" + accessToken);
                httpWebRequest.Accept = "application/json";

                if (!string.IsNullOrEmpty(pzData))
                {
                    byte[] buffer = Encoding.Default.GetBytes(pzData);
                    if (buffer != null)
                    {
                        httpWebRequest.ContentLength = buffer.Length;
                        httpWebRequest.GetRequestStream().Write(buffer, 0, buffer.Length);
                    }
                }

                var httpResponse = httpWebRequest.GetResponse();
                var result = string.Empty;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                InvoiceVATService.log.Error(string.Format("Data Invoice Get Code Of Tax Body Response : {0}", result));
                return result;
            }
            catch (Exception ex)
            {
                InvoiceVATService.log.Error(string.Format("Get Code Of Tax Exception : {0}", ex.Message));
                return "NOK" + ex.Message;
            }
        }

        public static string SendInvoice(InvoiceVAT invoice, string pattern, out string message)
		{
			message = "";
			string str = string.Concat(Parse.Core.AppContext.Current.company.UserName, ":", Parse.Core.AppContext.Current.company.PassWord);
            string codeTax = GetCodeTax(Parse.Core.AppContext.Current.company.UserName);
            string apiLink = string.Concat(Parse.Core.AppContext.Current.company.Domain, "/InvoiceAPI/InvoiceWS/createInvoice/", codeTax);
			string autStr = ViettelAPI.APIHelper.Base64Encode(str);
			string contentType = "application/json";
			InvoiceInfo objInvoice = new InvoiceInfo()
			{
				transactionUuid = invoice.Fkey,
				invoiceType = "01GTKT",
				templateCode = pattern,
				invoiceSeries = null,
				currencyCode = "VND",
				adjustmentType = "1",
				paymentStatus = true,
				paymentType = invoice.PaymentMethod,
				paymentTypeName = invoice.PaymentMethod,
				cusGetInvoiceRight = true
            };
			BuyerInfo objBuyer = new BuyerInfo()
			{
				buyerAddressLine = invoice.CusAddress,
				buyerIdNo = null,
				buyerIdType = null,
				buyerName = invoice.Buyer,
                buyerLegalName = invoice.Buyer,
                buyerPhoneNumber = invoice.CusPhone
			};
			SellerInfo objSeller = new SellerInfo()
			{
				sellerAddressLine = invoice.ComAddress,
				sellerBankAccount = invoice.ComBankNo,
				sellerBankName = invoice.ComBankName,
				sellerEmail = "",
				sellerLegalName = invoice.ComName,
				sellerPhoneNumber = invoice.ComPhone,
				sellerTaxCode = invoice.ComTaxCode
			};
			decimal sumOfTotalLineAmountWithoutTax = new decimal();
			List<ItemInfo> lstItem = new List<ItemInfo>();
			foreach (ProductInv pro in invoice.Products)
			{
				ItemInfo item = new ItemInfo()
				{
					discount = "0.0",
					itemCode = pro.Code,
					itemDiscount = "0.0",
					itemName = pro.Name,
					itemTotalAmountWithoutTax = pro.Amount.ToString(),
					lineNumber = "1",
					quantity = pro.Quantity.ToString(),
					taxAmount = pro.VATAmount.ToString(),
					taxPercentage = pro.VATRate.ToString(),
					unitName = pro.Unit,
					unitPrice = pro.Price.ToString()
				};
				lstItem.Add(item);
				sumOfTotalLineAmountWithoutTax += pro.Amount;
			}
			SummarizeInfo objSummary = new SummarizeInfo()
			{
				discountAmount = "0",
				settlementDiscountAmount = "0",
				sumOfTotalLineAmountWithoutTax = sumOfTotalLineAmountWithoutTax.ToString(),
				taxPercentage = invoice.VATRate.ToString(),
				totalAmountWithoutTax = sumOfTotalLineAmountWithoutTax.ToString(),
				totalAmountWithTax = invoice.Amount.ToString(),
				totalAmountWithTaxInWords = Parse.Core.Utils.NumberUtil.DocSoThanhChu(invoice.Amount.ToString()),
				totalTaxAmount = invoice.VATAmount.ToString()
			};
			InvoiceModels invoiceModel = new InvoiceModels()
			{
				generalInvoiceInfo = objInvoice,
				buyerInfo = objBuyer,
				sellerInfo = objSeller,
				itemInfo = lstItem,
				summarizeInfo = objSummary,
				payments = new List<Payment>()
				{
					new Payment()
					{
						paymentMethodName = invoice.PaymentMethod
					}
				},
				taxBreakdowns = new List<TaxBreakdown>()
				{
					new TaxBreakdown()
					{
						taxPercentage = (decimal)invoice.VATRate,
						taxableAmount = invoice.Total,
						taxAmount = invoice.VATAmount
					}
				}
			};
			string data = JsonConvert.SerializeObject(invoiceModel);
			return ViettelAPI.APIHelper.Request(apiLink, data, autStr, "POST", contentType);
		}

        public static APIResult SendInvoiceV2(InvoiceModels invoice)
        {
            string accessToken = String.Empty;
            string tokenUpdateAt = Parse.Core.AppContext.Current.company.Token_Update_At;
            bool isTUpdate = CheckToken(tokenUpdateAt);
            ICompanyService service = IoC.Resolve<ICompanyService>();
            service.BeginTran();

            try
            {
                if (!isTUpdate)
                {
                    string username = Parse.Core.AppContext.Current.company.UserName;
                    string password = Parse.Core.AppContext.Current.company.PassWord;
                    accessToken = GetToken(username, password);

                    Company curCom = AppContext.Current.company;
                    curCom.Token = accessToken;
                    curCom.Token_Update_At = Convert.ToString(Parse.Core.Utils.NumberUtil.ConvertToUnixTime(DateTime.Now));
                    service.Update(curCom);
                    service.CommitTran();
                    AppContext.InitContext(curCom);
                }
                else
                {
                    accessToken = Parse.Core.AppContext.Current.company.Token;
                }
            }
            catch (Exception ex) {
                service.RolbackTran();
                throw ex;
            }

            string codeTax = GetCodeTax(Parse.Core.AppContext.Current.company.UserName);
            string apiLink = string.Concat(Parse.Core.AppContext.Current.company.Domain, "/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/createInvoice/", codeTax);
            string contentType = "application/json";
            string data = JsonConvert.SerializeObject(invoice);
            InvoiceVATService.log.Error(string.Format("Data Invoice Body Request : {0}", data));
            APIResult resultTemp = ViettelAPI.APIHelper.ParseResult<APIResult>(ViettelAPI.APIHelper.RequestV2(apiLink, data, accessToken, "POST", contentType, null, 0, 1));
            return resultTemp;
        }

        public static APIResults SendInvoices(List<InvoiceModels> invoices)
		{
			string str = string.Concat(Parse.Core.AppContext.Current.company.UserName, ":", Parse.Core.AppContext.Current.company.PassWord);
            string codeTax = GetCodeTax(Parse.Core.AppContext.Current.company.UserName);
            string apiLink = string.Concat(Parse.Core.AppContext.Current.company.Domain, "/InvoiceAPI/InvoiceWS/createBatchInvoice/", codeTax);
			string autStr = ViettelAPI.APIHelper.Base64Encode(str);
			string record = ConfigurationManager.AppSettings["RecordPublish"].ToString();
			int number = (!string.IsNullOrEmpty(record) ? Convert.ToInt32(record) : 10);
			string contentType = "application/json";
			List<APIResult> lstResult = new List<APIResult>();
			APIResults resultConverted = new APIResults();
			int arrayLength = (int)Math.Ceiling((double)invoices.Count<InvoiceModels>() / (double)number);
			for (int i = 0; i < arrayLength; i++)
			{
				string data = JsonConvert.SerializeObject(invoices.Skip<InvoiceModels>(i * number).Take<InvoiceModels>(number).ToList<InvoiceModels>());
				string loHoaDon = string.Concat("{\"commonInvoiceInputs\": ", data, " }");
                InvoiceVATService.log.Error(string.Format("Data Invoice Body Request : {0}", loHoaDon));
                APIResults resultTemp = ViettelAPI.APIHelper.ParseResult<APIResults>(ViettelAPI.APIHelper.Request(apiLink, loHoaDon, autStr, "POST", contentType));
				lstResult.AddRange(resultTemp.createInvoiceOutputs);
			}
			resultConverted.createInvoiceOutputs = lstResult;
			return resultConverted;
		}

        public static APIResults SendInvoicesV2(List<InvoiceModels> invoices)
        {
            try
            {
                string accessToken = String.Empty;
                string tokenUpdateAt = Parse.Core.AppContext.Current.company.Token_Update_At;
                bool isTUpdate = CheckToken(tokenUpdateAt);
                ICompanyService service = IoC.Resolve<ICompanyService>();
                service.BeginTran();

                try
                {
                    if (!isTUpdate)
                    {
                        string username = Parse.Core.AppContext.Current.company.UserName;
                        string password = Parse.Core.AppContext.Current.company.PassWord;
                        accessToken = GetToken(username, password);

                        Company curCom = AppContext.Current.company;
                        curCom.Token = accessToken;
                        curCom.Token_Update_At = Convert.ToString(Parse.Core.Utils.NumberUtil.ConvertToUnixTime(DateTime.Now));
                        service.Update(curCom);
                        service.CommitTran();
                        AppContext.InitContext(curCom);
                    }
                    else
                    {
                        accessToken = Parse.Core.AppContext.Current.company.Token;
                    }
                }
                catch (Exception ex)
                {
                    service.RolbackTran();
                    throw ex;
                }

                string codeTax = GetCodeTax(Parse.Core.AppContext.Current.company.UserName);
                string apiLink = string.Concat(Parse.Core.AppContext.Current.company.Domain, "/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/createBatchInvoice/", codeTax);

                string contentType = "application/json";
                APIResults resultConverted = new APIResults();
                string record = ConfigurationManager.AppSettings["RecordPublish"].ToString();
                int number = (!string.IsNullOrEmpty(record) ? Convert.ToInt32(record) : 10);
                int arrayLength = (int)Math.Ceiling((double)invoices.Count<InvoiceModels>() / (double)number);
                for (int i = 0; i < arrayLength; i++)
                {
                    string data = JsonConvert.SerializeObject(invoices.Skip<InvoiceModels>(i * number).Take<InvoiceModels>(number).ToList<InvoiceModels>());
                    string loHoaDon = string.Concat("{\"commonInvoiceInputs\": ", data, " }");
                    InvoiceVATService.log.Error(string.Format("Data Invoice Body Request : {0}", loHoaDon));
                    APIResults resultTemp = ViettelAPI.APIHelper.ParseResult<APIResults>(ViettelAPI.APIHelper.RequestV2(apiLink, loHoaDon, accessToken, "POST", contentType, null, 0, 1));
                    resultConverted = resultTemp;
                }
                return resultConverted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetToken(string username, string password)
        {
            try
            {
                APIResults resultConverted = new APIResults();

                var body = new
                {
                    username = username.Trim(),
                    password = password
                };

                string jsonData = JsonConvert.SerializeObject(body);
                string contentType = "application/json";
                string apiLink = "https://api-vinvoice.viettel.vn/auth/login";

                string result = ViettelAPI.APIHelper.webRequestgetToken(apiLink, jsonData, "POST", contentType, null, 0, 1);
                string access_token = JObject.Parse(result)["access_token"].ToString();
                return access_token;
            }
            catch (Exception ex)
            {
                BussinessLog Bussinesslog = new BussinessLog()
                {
                    FileName = "Phát hành hóa đơn",
                    AppName = "Phát hành",
                    CreateDate = DateTime.Now
                };
                Bussinesslog.Error += string.Format("Lấy Token Lỗi : {0} \r\n", ex.Message);
                throw ex;
            }
            
        }

        public static bool CheckToken(string tokenUpdateAt)
        {
            if (string.IsNullOrEmpty(tokenUpdateAt))
            {
                return false;
            }
            long tua = long.Parse(tokenUpdateAt);
            DateTime tuaDt = Parse.Core.Utils.NumberUtil.UnixTimeStampToDateTime(tua);
            DateTime curDt = DateTime.Now;

            if (curDt.Subtract(tuaDt) >= TimeSpan.FromMinutes(4))
            {
                return false;
            }
            return true;
        }

        public static APIResultCodeOfTax GetCodeOfTax(string transactionUuid, ICompanyService service)
        {
            string accessToken = String.Empty;
            string tokenUpdateAt = Parse.Core.AppContext.Current.company.Token_Update_At;
            bool isTUpdate = CheckToken(tokenUpdateAt);
            if (service == null)
            {
                service = IoC.Resolve<ICompanyService>();
            }
            
            service.BeginTran();
            try
            {
                if (!isTUpdate)
                {
                    string username = Parse.Core.AppContext.Current.company.UserName;
                    string password = Parse.Core.AppContext.Current.company.PassWord;
                    accessToken = GetToken(username, password);

                    Company curCom = AppContext.Current.company;
                    curCom.Token = accessToken;
                    curCom.Token_Update_At = Convert.ToString(Parse.Core.Utils.NumberUtil.ConvertToUnixTime(DateTime.Now));
                    service.Update(curCom);
                    service.CommitTran();
                    AppContext.InitContext(curCom);
                }
                else
                {
                    accessToken = Parse.Core.AppContext.Current.company.Token;
                }
            }
            catch (Exception ex)
            {
                service.RolbackTran();
                throw ex;
            }

            string supplierTaxCode = GetCodeTax(Parse.Core.AppContext.Current.company.UserName);
            string apiLink = string.Concat(Parse.Core.AppContext.Current.company.Domain, "/services/einvoiceapplication/api/InvoiceAPI/InvoiceWS/searchInvoiceByTransactionUuid/");
            string contentType = "application/x-www-form-urlencoded";
            string data = "supplierTaxCode=" + supplierTaxCode + "&transactionUuid=" + transactionUuid;
            InvoiceVATService.log.Error(string.Format("Data Invoice Get Code Of Tax Body Request : {0}", data));
            APIResultCodeOfTax resultTemp = ViettelAPI.APIHelper.ParseResult<APIResultCodeOfTax>(ViettelAPI.APIHelper.webRequestgetCodeOfTax(apiLink, data, accessToken, "POST", contentType, null, 0, 1));
            return resultTemp;
        }

        public static string GetCodeTax(string username)
        {
            /* username: 0300813616 
               regex: ^[0 - 9]{ 10}$ */
            string regex_01 = "^[0-9]{10}$";

            /* username: 0300813616_J0428873 
               regex: ^[0-9]{10}[_][0-9a-zA-Z]{8}$ */
            string regex_02 = "^[0-9]{10}[_][0-9a-zA-Z]{8}$";

            /* username: 0300813616-004_J0326208 
               regex: ^[0-9]{10}[-][0-9]{3}[_][0-9a-zA-Z]{8}$ */
            string regex_03 = "^[0-9]{10}[-][0-9]{3}[_][0-9a-zA-Z]{8}$";

            Regex re01 = new Regex(regex_01, RegexOptions.IgnoreCase);
            Regex re02 = new Regex(regex_02, RegexOptions.IgnoreCase);
            Regex re03 = new Regex(regex_03, RegexOptions.IgnoreCase);

            if (re01.IsMatch(username))
            {
                return username;
            }

            if (re02.IsMatch(username))
            {
                string[] us = username.Split('_');
                return us[0];
            }

            if (re03.IsMatch(username))
            {
                string[] us = username.Split('_');
                return us[0];
            }


            return username;
        }
    }
}