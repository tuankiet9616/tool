using FX.Core;

using NPOI.SS.UserModel;
using Parse.Core;
using Parse.Core.Domain;
using Parse.Core.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.VisualBasic.FileIO;
using Parse.Core.Utils;
using Parse.Core.Implement;
using Newtonsoft.Json;

namespace Parse.Lewu
{
    public class ExcelParserService : IExcelParserService, IParserService
	{
		public ExcelParserService()
		{
		}

		private bool CheckFolioNo(string FolioOrigin)
		{
			if ((
				from p in IoC.Resolve<IInvoiceVATService>().Query
				where p.FolioOrigin == FolioOrigin
				select p).Count<InvoiceVAT>() <= 0)
			{
				return false;
			}
			return true;
		}

        private bool CheckInvoiceNoSAP(string InvoiceNoSAP)
        {
            if ((
                from p in IoC.Resolve<IInvoiceVATService>().Query
                where p.InvoiceNoSAP == InvoiceNoSAP && p.Publish == PublishStatus.None
                select p).Count<InvoiceVAT>() > 0)
            {
                return true;
            }
            else
            {
                if ((
                from p in IoC.Resolve<IInvoiceVATService>().Query
                where p.InvoiceNoSAP == InvoiceNoSAP && p.Publish == PublishStatus.Success && p.CodeOfTax != "Lỗi"
                select p).Count<InvoiceVAT>() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CheckRowEmty(DataRow row)
		{
			object[] itemArray = row.ItemArray;
			if (itemArray == null)
			{
				return true;
			}
			return ((IEnumerable<object>)itemArray).All<object>((object x) => string.IsNullOrWhiteSpace(x.ToString()));
		}

		public void ConvertExcelToData(string filePath, out DataTable dTable, ref string mesError)
		{
			using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			{
				dTable = new DataTable();
				IWorkbook workbook = WorkbookFactory.Create(fs);
				ISheet sheet = workbook.GetSheetAt(0);
				ISheet workSheet = workbook.GetSheet(sheet.SheetName);
				IRow row = workSheet.GetRow(0);
				short colExcel = row.LastCellNum;
				dTable.Rows.Clear();
				dTable.Columns.Clear();
				List<string> headerDT = new List<string>()
				{
					"Mã KH",
					"Tên KH",
					"Số điện thoại",
					"Email KH",
					"Tên đơn vị ghi trên hóa đơn",
					"Người đại diện",
					"Địa chỉ xuất hóa đơn",
					"Địa chỉ giao hàng",
					"Mã số thuế",
					"Ngân hàng",
					"Số TK",
					"Số hóa đơn nội bộ",
					"Ngày mua hàng",
					"Tên NVBH",
					"Tên NVGN",
					"Mã hàng",
					"Tên hàng",
					"ĐV Tính",
					"SL SKU",
					"Đơn giá trước VAT",
					"Thuế suất",
					"Hàng KM/ Chiết khấu",
					"Tiền chiết khấu sau VAT",
					"Trọng lượng (kg)",
					"Ghi chú"
				};
				List<string> header = new List<string>();
				foreach (ICell cell in row.Cells)
				{
					header.Add(cell.StringCellValue);
				}
				int e = 0;
				while (e < header.Count)
				{
					if (header[e].Trim().ToUpper() == headerDT[e].ToUpper())
					{
						e++;
					}
					else
					{
						mesError = "File upload sai mẫu!";
						return;
					}
				}
				List<string> lstCol = (
					from c in (IEnumerable<PropertyInfo>)(new InvObj()).GetType().GetProperties()
					select c.Name).ToList<string>();
				if (dTable.Columns.Count <= colExcel + 1)
				{
					for (int i = 0; i < lstCol.Count; i++)
					{
						dTable.Columns.Add(lstCol[i].ToString());
					}
				}
				for (int i = 1; i <= workSheet.LastRowNum; i++)
				{
					if (workSheet.GetRow(i) != null)
					{
						DataRow dRow = dTable.NewRow();
						for (int j = 0; j < colExcel; j++)
						{
							ICell cell = workSheet.GetRow(i).GetCell(j);
							if (cell == null)
							{
								dRow[j + 1] = string.Empty;
							}
							else
							{
								switch (cell.CellType)
								{
									case CellType.Unknown:
									case CellType.Blank:
										{
											dRow[j + 1] = string.Empty;
											break;
										}
									case CellType.Numeric:
										{
											dRow[j + 1] = (DateUtil.IsCellDateFormatted(cell) ? cell.DateCellValue.ToString(CultureInfo.InvariantCulture) : cell.NumericCellValue.ToString(CultureInfo.InvariantCulture));
											break;
										}
									case CellType.String:
										{
											dRow[j + 1] = cell.StringCellValue;
											break;
										}
								}
							}
						}
						if (!this.CheckRowEmty(dRow))
						{
							dRow[0] = i + 1;
							dTable.Rows.Add(dRow);
						}
					}
				}
			}
		}

		public List<T> ConvertTableToList<T>(DataTable dt)
		{
			List<string> list = (
				from c in dt.Columns.Cast<DataColumn>()
				select c.ColumnName).ToList<string>();
			PropertyInfo[] properties = typeof(T).GetProperties();
			dt.AsEnumerable().ToList<DataRow>();
			return dt.AsEnumerable().Select<DataRow, T>((DataRow row) => {
				T objT = Activator.CreateInstance<T>();
				PropertyInfo[] propertyInfoArray = properties;
				for (int i = 0; i < (int)propertyInfoArray.Length; i++)
				{
					PropertyInfo pro = propertyInfoArray[i];
					if (list.Contains(pro.Name))
					{
						PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
						pro.SetValue(objT, (row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name], pI.PropertyType)), null);
					}
				}
				return objT;
			}).ToList<T>();
		}

		public string GetFkey(InvoiceVAT invoice)
		{
            string fkeyCode = invoice.InvoiceNoSAP;
            string fkey = fkeyCode.Length < 36 ? fkeyCode : fkeyCode.Substring(0, 35);

            return fkey;
        }

		public List<InvoiceVAT> GetInvoiceData(string filePath, ref int invSuccess, ref int invTotal, ref string mesError)
		{
			DataTable dTable = new DataTable();
			List<InvoiceVAT> lstInv = new List<InvoiceVAT>();
			List<InvObj> lstObj = new List<InvObj>();
			this.ConvertExcelToData(filePath, out dTable, ref mesError);

			if (mesError == "File upload sai mẫu!")
			{
				return lstInv;
			}

			lstObj = this.ConvertTableToList<InvObj>(dTable);
			if (lstObj.Count == 0)
			{
				mesError = "File không có hóa đơn!";
				return lstInv;
			}
			var lstGrp = (
				from c in lstObj
				group c by new { OrderNumber = c.OrderNumber, DetailVAT = c.DetailVAT }).ToList();
			invTotal = lstGrp.Count();
			foreach (var item in lstGrp)
			{
				InvObj invObj = item.First<InvObj>();
				string orderNumber = invObj.OrderNumber;
				if (!this.CheckFolioNo(orderNumber))
				{
					int invError = 0;
					string mesInvError = "";
					int iVal = 0;
					float fVal = 0f;
					decimal dVal = new decimal();
					DateTime dateVal = DateTime.Now;
					InvoiceVAT inv = new InvoiceVAT()
					{
						CusCode = invObj.CustomerShortCode,
						Buyer = invObj.CustomerName,
						CusPhone = invObj.CustomerMobilePhone,
						CusEmail = invObj.CustomerEmail,
						CusComName = invObj.CustomerCompanyName,
						CusName = invObj.CustomerOutletName,
						CusAddress = invObj.CustomerAddress,
						CusDeliveryAddress = invObj.CustomerDeliveryAddress,
						CusTaxCode = invObj.CustomerTax,
						CusBankName = invObj.CustomerBankName,
						CusBankNo = invObj.CustomerBankNumber
					};
					if (this.tryParseVat(invObj.DetailVAT, ref fVal))
					{
						inv.VATRate = fVal;
					}
					else
					{
						invError++;
						mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", invObj.rowNumber, " sai nội dung thuế" });
					}
					if (!string.IsNullOrEmpty(invObj.OrderNumber))
					{
						inv.FolioOrigin = invObj.OrderNumber;
						if (lstGrp.Count((x) => x.Key.OrderNumber == invObj.OrderNumber) <= 1)
						{
							inv.FolioNo = invObj.OrderNumber;
						}
						else if (inv.VATRate != -1f)
						{
							inv.FolioNo = string.Concat(invObj.OrderNumber, "_", invObj.DetailVAT);
						}
						else
						{
							inv.FolioNo = string.Concat(invObj.OrderNumber, "_KoThue");
						}
					}
					else
					{
						invError++;
						mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", invObj.rowNumber, " không có số hóa đơn" });
					}
					if (this.tryParseDate(invObj.OrderDate, ref dateVal))
					{
						inv.ArisingDate = dateVal;
					}
					else
					{
						invError++;
						mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", invObj.rowNumber, " sai nội dung ngày" });
					}
					inv.Fkey = this.GetFkey(inv);
					inv.StaffId = invObj.StaffId;
					inv.DeliveryId = invObj.OrderDeliveryId;
					inv.Total = decimal.Zero;
					inv.VATAmount = decimal.Zero;
					inv.Amount = decimal.Zero;
					inv.Weight = 0f;
					List<ProductInv> products = new List<ProductInv>();
					foreach (InvObj pro in item)
					{
						int proError = 0;
						ProductInv product = new ProductInv();
						if (!string.IsNullOrEmpty(pro.ProductCode) || !string.IsNullOrEmpty(pro.ProductName))
						{
							product.Code = pro.ProductCode;
							product.Name = pro.ProductName;
						}
						else
						{
							proError++;
							mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", pro.rowNumber, " sai nội dung mã và tên sản phẩm" });
						}
						product.Unit = pro.ProductUom2;
						if (!this.tryParseDecimal(pro.DetailQuantity, ref dVal))
						{
							proError++;
							mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", pro.rowNumber, " sai nội dung số lượng" });
						}
						else if (dVal != decimal.Zero)
						{
							product.Quantity = dVal;
						}
						else
						{
							proError++;
							mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", pro.rowNumber, " sai nội dung số lượng" });
						}
						if (this.tryParseDecimal(pro.DetailPrice, ref dVal))
						{
							product.Price = dVal;
						}
						else
						{
							proError++;
							mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", pro.rowNumber, " sai nội dung đơn giá" });
						}
						product.VATRate = inv.VATRate;
						if (this.tryParseInt(pro.IsFreeItem, ref iVal))
						{
							product.Type = iVal;
						}
						else
						{
							proError++;
							mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", pro.rowNumber, " sai nội dung loại hàng" });
						}
						if (this.tryParseDecimal(pro.DetailDiscountAmount, ref dVal))
						{
							product.DiscountAmount = Math.Round((dVal * new decimal(100)) / (new decimal(100) + (decimal)product.VATRate));
						}
						else
						{
							proError++;
							mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", pro.rowNumber, " sai nội dung chiết khấu" });
						}
						product.Remark = pro.DetailNote;
						if (this.tryParseFloat(pro.Weight, ref fVal))
						{
							product.Weight = fVal;
						}
						else
						{
							proError++;
							mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", pro.rowNumber, " sai nội dung khối lượng" });
						}
						product.Total = Math.Round(product.Quantity * product.Price);
						decimal total = product.Total - product.DiscountAmount;
						float vATRate = product.VATRate;
						product.VATAmount = Math.Round((total * decimal.Parse(vATRate.ToString())) / new decimal(100));
						product.Amount = Math.Round((product.Total - product.DiscountAmount) + product.VATAmount);
						InvoiceVAT weight = inv;
						weight.Weight = weight.Weight + product.Weight;
						InvoiceVAT quantity = inv;
						quantity.Quantity = quantity.Quantity + product.Quantity;
						InvoiceVAT amount = inv;
						amount.Amount = amount.Amount + product.Amount;
						InvoiceVAT invoiceVAT = inv;
						invoiceVAT.Total = invoiceVAT.Total + (product.Total - product.DiscountAmount);
						InvoiceVAT vATAmount = inv;
						vATAmount.VATAmount = vATAmount.VATAmount + product.VATAmount;
						InvoiceVAT discountAmount = inv;
						discountAmount.DiscountAmount = discountAmount.DiscountAmount + product.DiscountAmount;
						if (proError != 0)
						{
							invError += proError;
						}
						else
						{
							products.Add(product);
						}
					}
					if (invError != 0)
					{
						mesError = string.Concat(mesError, string.Format(" - Hóa đơn {0} có {1} lỗi: {2}.", orderNumber, invError, mesInvError));
					}
					else
					{
						inv.Products = products;
						inv.AmountInWords = NumberToLeter.DocTienBangChu(inv.Amount);
						lstInv.Add(inv);
						invSuccess++;
					}
				}
				else
				{
					mesError = string.Concat(mesError, string.Format(" - Hóa đơn {0} đã tồn tại.", orderNumber));
				}
			}
			return lstInv;
		}

		public List<InvoiceVAT> GetInvoiceDataXml(string filePath, ref int invSuccess, ref int invTotal, ref string mesError, string csvPath)
		{
            InvoiceVATService.log.Error(string.Format("--------- Start GetInvoiceDataXml -----------------"));
            DataTable dTable = new DataTable();
			List<InvoiceVAT> lstInv = new List<InvoiceVAT>();
			List<InvObj> lstObj = new List<InvObj>();

			XElement fullText = new XElement("Invoices");
			XElement root = new XElement("InvObj");

			string text = fullText.ToString();
			string roots = root.ToString();
            List<string[]> linne = new List<string[]>();
            List<string> heads = new List<string>();
            if (csvPath != null)
            {
                using (TextFieldParser parser = new TextFieldParser(csvPath, System.Text.Encoding.UTF8))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    bool isFirstRow = true;
                    
                    while (!parser.EndOfData)
                    {
                        if (isFirstRow)
                        {
                            string[] headArr = parser.ReadFields();
                            foreach (string column in headArr) {
                                heads.Add(column);
                            }
                            isFirstRow = false;
                        }
                        else
                        {
                            string[] data = parser.ReadFields();
                            linne.Add(data);
                        }
                    }
                }
            }
            else
            {
                using (TextFieldParser parser = new TextFieldParser(filePath + @"\\toolVina\\InvoicesXML\\test.csv", System.Text.Encoding.UTF8))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    bool isFirstRow = true;

                    while (!parser.EndOfData)
                    {
                        if (isFirstRow)
                        {
                            string[] headArr = parser.ReadFields();
                            foreach (string column in headArr)
                            {
                                heads.Add(column);
                            }
                            isFirstRow = false;
                        }
                        else
                        {
                            string[] data = parser.ReadFields();
                            linne.Add(data);
                        }

                    }
                }
            }

            try
            {
                foreach (string[] line in linne)
                {
                    XElement node = new XElement(fullText);
                    foreach (string column in heads)
                    {
                        string colNm = mapCol(column.Trim());
                        if (colNm == "")
                        {
                            continue;
                        }
                        node.Add(new XElement(colNm, line[heads.IndexOf(column)]));
                    }
                    root.Add(node);
                };
            }
            catch (Exception ex)
            {
                throw new Exception("File Lỗi Format : Số Cột Header & Content Khác Nhau");
            }
            
			XDocument sult = new XDocument(root);
            string fUpdPath = filePath + @"\\toolVina\\InvoicesXMLUpload";
            if (!Directory.Exists(fUpdPath))
            {
                Directory.CreateDirectory(fUpdPath);
            }
            sult.Save(fUpdPath + @"\\invoices.xml");
			string xmlInvoices = "";
			using (FileStream fs = new FileStream(filePath + @"\\toolVina\\InvoicesXMLUpload\\invoices.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (System.IO.StreamReader rdr = new System.IO.StreamReader(fs))
				{
					xmlInvoices = rdr.ReadToEnd();
					fs.Close();
				}
			}
			System.IO.Directory.Move(filePath + @"\\toolVina\\InvoicesXMLUpload\\invoices.xml", filePath + @"\\toolVina\\Response\\" + DateTime.Now.ToString("success-invoices-dd-MM-yyyy-HH-mm-ss") + ".xml");

			try
			{
				StringReader theReader = new StringReader(xmlInvoices);
				DataSet theDataSet = new DataSet();
				theDataSet.ReadXml(theReader);
				dTable = theDataSet.Tables["Invoices"];
			}
			catch (Exception ex)
			{
				mesError = ex.Message;
			}

			lstObj = this.ConvertTableToList<InvObj>(dTable);
			if (mesError == "")
			{
				if (lstObj.Count == 0)
				{
					mesError = "File không có hóa đơn!";
					return lstInv;
				}
			}
			else
			{
				return lstInv;
			}

			var lstGrp = (
				from c in lstObj
				group c by new { OrderNumber = c.OrderNumber }).ToList();
			invTotal = lstGrp.Count();
			foreach (var item in lstGrp)
			{
				InvObj invObj = item.First<InvObj>();
				string invoiceNoSAP = invObj.InvoiceNoSAP;
				if (!this.CheckInvoiceNoSAP(invoiceNoSAP))
				{
					int invError = 0;
					string mesInvError = "";
					int iVal = 0;
					float fVal = 0f;
					decimal dVal = new decimal();
					DateTime dateVal = DateTime.Now;
					InvoiceVAT inv = new InvoiceVAT()
					{
						CusCode = invObj.CustomerShortCode,
						Buyer = invObj.CustomerName,
						CusPhone = invObj.CustomerMobilePhone,
						CusEmail = invObj.CustomerEmail,
						CusComName = invObj.CustomerName,
						CusName = invObj.CustomerName,
						CusAddress = invObj.CustomerAddress,
						CusDeliveryAddress = invObj.CustomerDeliveryAddress,
						CusTaxCode = invObj.CustomerTax,
						CusBankName = invObj.CustomerBankName,
						CusBankNo = invObj.CustomerBankNumber,
                        PaymentMethod = invObj.PaymentMethod,

                        DeliveryDate = invObj.DeliveryDate,
                        DeliveryNo = invObj.DeliveryNo,
                        DueDate = invObj.DueDate,
                        InvoiceNoSAP = invObj.InvoiceNoSAP,
                        SaleOrderNo = invObj.SaleOrderNo
                    };
					if (this.tryParseVat(invObj.DetailVAT, ref fVal))
					{
						inv.VATRate = fVal;
					}
					else
					{
						invError++;
						mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", invObj.rowNumber, " sai nội dung thuế" });
					}
					if (!string.IsNullOrEmpty(invObj.OrderNumber))
					{
						inv.FolioOrigin = invObj.OrderNumber;
						if (lstGrp.Count((x) => x.Key.OrderNumber == invObj.OrderNumber) <= 1)
						{
							inv.FolioNo = invObj.OrderNumber;
						}
						else if (inv.VATRate != -1f)
						{
							inv.FolioNo = string.Concat(invObj.OrderNumber, "_", invObj.DetailVAT);
						}
						else
						{
							inv.FolioNo = string.Concat(invObj.OrderNumber, "_KoThue");
						}
					}
					else
					{
						invError++;
						mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", invObj.rowNumber, " không có số hóa đơn" });
					}
					if (this.tryParseDate(invObj.OrderDate, ref dateVal))
					{
						inv.ArisingDate = dateVal;
					}
					else
					{
						invError++;
						mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", invObj.rowNumber, " sai nội dung ngày" });
					}
					inv.Fkey = this.GetFkey(inv);
					inv.StaffId = invObj.StaffId;
					inv.DeliveryId = invObj.OrderDeliveryId;
					inv.Total = decimal.Zero;
					inv.VATAmount = decimal.Zero;
					inv.Amount = decimal.Zero;
					inv.Weight = 0f;
					List<ProductInv> products = new List<ProductInv>();
					foreach (InvObj pro in item)
					{
						int proError = 0;
						ProductInv product = new ProductInv();
						if (!string.IsNullOrEmpty(pro.ProductCode) || !string.IsNullOrEmpty(pro.ProductName))
						{
							product.Code = pro.ProductCode;
							product.Name = pro.ProductName;
						}
						else
						{
							proError++;
							mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", pro.rowNumber, " sai nội dung mã và tên sản phẩm" });
						}
						product.Unit = pro.ProductUom2;
						if (!this.tryParseDecimal(pro.DetailQuantity, ref dVal))
						{
							proError++;
							mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", pro.rowNumber, " sai nội dung số lượng" });
						}
						else if (dVal != decimal.Zero)
						{
							product.Quantity = dVal;
						}
						else
						{
							proError++;
							mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", pro.rowNumber, " sai nội dung số lượng" });
						}
						if (this.tryParseDecimal(pro.DetailPrice, ref dVal))
						{
							product.Price = dVal;
						}
						else
						{
							proError++;
							mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", pro.rowNumber, " sai nội dung đơn giá" });
						}
						product.VATRate = inv.VATRate;
						if (this.tryParseInt(pro.IsFreeItem, ref iVal))
						{
							product.Type = iVal;
						}
						else
						{
							proError++;
							mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", pro.rowNumber, " sai nội dung loại hàng" });
						}
						if (this.tryParseDecimal(pro.DetailDiscountAmount, ref dVal))
						{
							product.DiscountAmount = Math.Round((dVal * new decimal(100)) / (new decimal(100) + (decimal)product.VATRate));
						}
						else
						{
							proError++;
							mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", pro.rowNumber, " sai nội dung chiết khấu" });
						}
						product.Remark = pro.DetailNote;
						if (this.tryParseFloat(pro.Weight, ref fVal))
						{
							product.Weight = fVal;
						}
						else
						{
							proError++;
							mesInvError = string.Concat(new object[] { mesInvError, " - Dòng ", pro.rowNumber, " sai nội dung khối lượng" });
						}
                        product.Total = decimal.Parse(pro.AmountBeforeTax);
                        decimal total = product.Total - product.DiscountAmount;
						float vATRate = product.VATRate;
                        product.VATAmount = Math.Round((total * decimal.Parse(vATRate.ToString())) / new decimal(100), 1);
                        product.Amount = Math.Round((product.Total - product.DiscountAmount) + product.VATAmount, 1);
						InvoiceVAT weight = inv;
						weight.Weight = weight.Weight + product.Weight;
						InvoiceVAT quantity = inv;
						quantity.Quantity = quantity.Quantity + product.Quantity;
						InvoiceVAT amount = inv;
						InvoiceVAT invoiceVAT = inv;
                        InvoiceVAT vATAmount = inv;

                        List<string> itemCode = product.Name.Split(' ').ToList();
                        if (itemCode.ElementAt(0).Equals("51294") || itemCode.ElementAt(0).Equals("50524"))
                        {
                            if (product.Amount < 0)
                            {
                                amount.Amount = amount.Amount + product.Amount;
                            }
                            else
                            {
                                amount.Amount = amount.Amount - product.Amount;
                            }

                            if (product.Total < 0)
                            {
                                invoiceVAT.Total = invoiceVAT.Total + (product.Total - product.DiscountAmount);
                            }
                            else
                            {
                                invoiceVAT.Total = invoiceVAT.Total - (product.Total - product.DiscountAmount);
                            }

                            if (product.VATAmount < 0)
                            {
                                vATAmount.VATAmount = vATAmount.VATAmount + product.VATAmount;
                            }
                            else
                            {
                                vATAmount.VATAmount = vATAmount.VATAmount - product.VATAmount;
                            }
                        }
                        else
                        {
                            amount.Amount = amount.Amount + product.Amount;
                            invoiceVAT.Total = invoiceVAT.Total + (product.Total - product.DiscountAmount);
                            vATAmount.VATAmount = vATAmount.VATAmount + product.VATAmount;
                        }
						
						InvoiceVAT discountAmount = inv;
						discountAmount.DiscountAmount = discountAmount.DiscountAmount + product.DiscountAmount;
						if (proError != 0)
						{
							invError += proError;
						}
						else
						{
							products.Add(product);
						}
					}

                    inv.Amount = Math.Round(inv.Amount, MidpointRounding.AwayFromZero);
                    inv.VATAmount = Math.Round(inv.VATAmount, MidpointRounding.AwayFromZero);

                    if (invError != 0)
					{
						mesError = string.Concat(mesError, string.Format(" - Hóa đơn {0} có {1} lỗi: {2}.", invoiceNoSAP, invError, mesInvError));
					}
					else
					{
						inv.Products = products;
						inv.AmountInWords = NumberToLeter.DocTienBangChu(inv.Amount);
						lstInv.Add(inv);
						invSuccess++;
					}
				}
				else
				{
					mesError = string.Concat(mesError, string.Format(" - Hóa đơn {0} đã tồn tại.", invoiceNoSAP));
				}
			}

            if (string.Empty.Equals(mesError))
            {
                mesError = lstInv[0].InvoiceNoSAP;
                for (int i = 1; i < lstInv.Count; i++)
                {
                    mesError = string.Concat(mesError, " - ", lstInv[i].InvoiceNoSAP);
                }
            }

            InvoiceVATService.log.Error(string.Format("--------- List Invoice Number : {0}", lstInv.Count));
            InvoiceVATService.log.Error(string.Format("--------- List Invoice From File Excel : {0}", JsonConvert.SerializeObject(lstInv)));
            InvoiceVATService.log.Error(string.Format("--------- Finish GetInvoiceDataXml -----------------"));
            return lstInv;
		}
		private bool IsValidXML(string xmlStr)
		{
			try
			{
				if (!string.IsNullOrEmpty(xmlStr))
				{
					System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
					xmlDoc.LoadXml(xmlStr);
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (System.Xml.XmlException)
			{
				return false;
			}
		}
		private bool tryParseDate(string inp, ref DateTime outp)
		{
			bool check = true;
			if (string.IsNullOrEmpty(inp))
			{
				outp = DateTime.Now;
			}
			else
			{
				check = DateTime.TryParseExact(inp, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out outp);
			}
			return check;
		}

		private bool tryParseDecimal(string inp, ref decimal outp)
		{
			bool check = true;
			if (string.IsNullOrEmpty(inp))
			{
				outp = new decimal();
			}
			else
			{
				check = decimal.TryParse(inp, out outp);
			}
			return check;
		}

		private bool tryParseFloat(string inp, ref float outp)
		{
			bool check = true;
			if (string.IsNullOrEmpty(inp))
			{
				outp = 0f;
			}
			else
			{
				check = float.TryParse(inp, out outp);
			}
			return check;
		}

		private bool tryParseInt(string inp, ref int outp)
		{
			bool check = true;
			if (string.IsNullOrEmpty(inp))
			{
				outp = 0;
			}
			else
			{
				check = int.TryParse(inp, out outp);
			}
			return check;
		}

		private bool tryParseVat(string inp, ref float outp)
		{
			bool check = true;
			if (string.IsNullOrEmpty(inp))
			{
				outp = -1f;
			}
			else
			{
				check = float.TryParse(inp, out outp);
			}
			return check;
		}

        private string mapCol(string colNameCSV)
        {
            string colNm = "";
            switch (colNameCSV)
            {
                case "group":
                    colNm = "OrderNumber";
                    break;

                case "buyerCode":
                    colNm = "CustomerShortCode";
                    break;

                case "buyerName":
                    colNm = "CustomerName";
                    break;

                case "buyerAddress":
                    colNm = "CustomerAddress";
                    break;

                case "buyerPhone":
                    colNm = "CustomerMobilePhone";
                    break;

                case "buyerEmail":
                    colNm = "CustomerEmail";
                    break;

                case "buyerTaxCode":
                    colNm = "CustomerTax";
                    break;

                case "payMethod":
                    colNm = "PaymentMethod";
                    break;

                case "itemName":
                    colNm = "ProductName";
                    break;

                case "itemNote":
                    colNm = "DetailNote";
                    break;

                case "itemUnit":
                    colNm = "ProductUom2";
                    break;

                case "itemQuantity":
                    colNm = "DetailQuantity";
                    break;

                case "itemPrice":
                    colNm = "DetailPrice";
                    break;

                case "taxPercentage":
                    colNm = "DetailVAT";
                    break;

                case "deliveryDate":
                    colNm = "DeliveryDate";
                    break;

                case "deliveryNo":
                    colNm = "DeliveryNo";
                    break;

                case "dueDate":
                    colNm = "DueDate";
                    break;

                case "invoiceNoSAP":
                    colNm = "InvoiceNoSAP";
                    break;

                case "saleOrderNo":
                    colNm = "SaleOrderNo";
                    break;

                case "amountBeforeTax":
                    colNm = "AmountBeforeTax";
                    break;

                default:
                    colNm = "";
                    break;
            }
            return colNm;
        }
    }
}