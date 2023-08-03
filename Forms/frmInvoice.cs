using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using FX.Core;
using Newtonsoft.Json;
using Parse.Core;
using Parse.Core.Domain;
using Parse.Core.IService;
using Parse.Forms.CustomUC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ViettelAPI;
using ViettelAPI.Models;

namespace Parse.Forms
{
    public partial class frmInvoice
    {
		public InvoiceVAT InnitData()
		{
			this.Invoice.Buyer = this.txtBuyer.Text;
			this.Invoice.CusCode = this.txtCusCode.Text;
			this.Invoice.CusName = this.txtCusName.Text;
			this.Invoice.CusPhone = this.txtCusPhone.Text;
			this.Invoice.CusComName = this.txtCusComName.Text;
			this.Invoice.CusTaxCode = this.txtCusTaxCode.Text;
			this.Invoice.CusAddress = this.txtCusAddress.Text;
			this.Invoice.CusEmail = this.txtCusEmail.Text;
			//this.Invoice.FolioOrigin = this.txtFolioOrigin.Text;
            this.Invoice.InvoiceNoSAP = this.txtInvoiceNoSAP.Text;
            this.Invoice.CusBankName = this.txtCusBankName.Text;
			this.Invoice.CusBankNo = this.txtCusBankNo.Text;
			this.Invoice.DeliveryId = this.txtDeliveryId.Text;
			this.Invoice.StaffId = this.txtStaffId.Text;
			this.Invoice.VATRate = float.Parse(this.cbVATRate.EditValue.ToString());
			this.Invoice.Total = (this.txtTotal.Text != "" ? Convert.ToDecimal(this.txtTotal.Text) : decimal.Zero);
			this.Invoice.VATAmount = (this.txtVATAmount.Text != "" ? Convert.ToDecimal(this.txtVATAmount.Text) : decimal.Zero);
			this.Invoice.DiscountAmount = (this.txtVATAmount.Text != "" ? Convert.ToDecimal(this.txtDiscountAmount.Text) : decimal.Zero);
			this.Invoice.Amount = (this.txtAmountInvoice.Text != "" ? Convert.ToDecimal(this.txtAmountInvoice.Text) : decimal.Zero);
			this.Invoice.AmountInWords = this.txtAmountInWord.Text;
			if (this.Invoice.Id == 0)
			{
				this.Invoice.ArisingDate = DateTime.Now.Date;
			}
			return this.Invoice;
		}

		public void LoadData()
		{
			if (this.Invoice != null)
			{
				this.txtBuyer.Text = this.Invoice.Buyer;
				this.txtCusCode.Text = this.Invoice.CusCode;
				this.txtCusName.Text = this.Invoice.CusName;
				this.txtCusPhone.Text = this.Invoice.CusPhone;
				this.txtCusComName.Text = this.Invoice.CusComName;
				this.txtCusTaxCode.Text = this.Invoice.CusTaxCode;
				this.txtCusAddress.Text = this.Invoice.CusAddress;
				this.txtCusEmail.Text = this.Invoice.CusEmail;
				//this.txtFolioOrigin.Text = this.Invoice.InvoiceNoSAP;
                this.txtInvoiceNoSAP.Text = this.Invoice.InvoiceNoSAP;
                this.txtCusBankName.Text = this.Invoice.CusBankName;
				this.txtCusBankNo.Text = this.Invoice.CusBankNo;
				this.txtStaffId.Text = this.Invoice.StaffId;
				this.txtDeliveryId.Text = this.Invoice.DeliveryId;
				TextEdit str = this.txtTotal;
				decimal total = this.Invoice.Total;
				str.Text = total.ToString();
				TextEdit textEdit = this.txtVATAmount;
				total = this.Invoice.VATAmount;
				textEdit.Text = total.ToString();
				TextEdit str1 = this.txtDiscountAmount;
				total = this.Invoice.DiscountAmount;
				str1.Text = total.ToString();
				TextEdit textEdit1 = this.txtAmountInvoice;
				total = this.Invoice.Amount;
				textEdit1.Text = total.ToString();
				this.txtAmountInWord.Text = this.Invoice.AmountInWords;
				this.btnReleased.Enabled = (this.Invoice.Id > 0 ? true : false);
				this.cbVATRate.EditValue = this.Invoice.VATRate;
				this.Bindingsource.DataSource = this.Invoice.Products;
				this.gridProduct.DataSource = this.Bindingsource;
			}
		}

		public void LoadProTypeCombobox()
		{
			this.cboType.DataSource = Parse.Core.Common.lstProductType;
			this.cboType.DisplayMember = "Value";
			this.cboType.ValueMember = "Key";
			this.cboType.PopulateColumns();
			this.cboType.Columns[0].Visible = false;
		}

		public void LoadTaxCombobox()
		{
			this.cbVATRate.Properties.DataSource = Parse.Core.Common.lstTax;
			this.cbVATRate.Properties.DisplayMember = "Value";
			this.cbVATRate.Properties.ValueMember = "Key";
			this.cbVATRate.Properties.PopulateColumns();
			this.cbVATRate.Properties.DropDownRows = (Parse.Core.Common.lstTax.Count<KeyValuePair<float, string>>() < 10 ? Parse.Core.Common.lstTax.Count : 10);
			this.cbVATRate.Properties.Columns[0].Visible = false;
		}

		private void TextEdit_EditValueChanged(object sender, EventArgs e)
		{
		}

		private void txtAmountInvoice_Leave(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(this.txtAmountInvoice.Text))
			{
				decimal amount = Convert.ToDecimal(this.txtAmountInvoice.EditValue);
				this.txtAmountInWord.Text = NumberToLeter.DocTienBangChu(amount);
			}
		}

		private bool ValidateData()
		{
			if (this.txtInvoiceNoSAP.Text == "")
			{
				return false;
			}
			return true;
		}
		public frmInvoice(IMainForm main, InvoiceVAT inv, bool type)
		{
			this.InitializeComponent();
			this.Main = main;
			this.Invoice = inv;
			if (!type)
			{
				this.Text = "Tạo lập hóa đơn";
				return;
			}
			this.Text = string.Concat("Thông tin chi tiết hóa đơn ", this.Invoice.No);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnDeleteRow_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			try
			{
				this.viewProduct.DeleteRow(this.viewProduct.FocusedRowHandle);
				this.Calculator();
			}
			catch (Exception exception)
			{
				this.log.Error(exception);
			}
		}

		private void btnRecal_Click(object sender, EventArgs e)
		{
			this.Calculator();
		}

		private void btnReleased_Click(object sender, EventArgs e)
		{
			if (XtraMessageBox.Show("Bạn muốn phát hành hóa đơn?", "Thông báo", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
			{
				base.Close();
				ucInvoiceVAT uc = new ucInvoiceVAT(this.Main);
				this.Main.AddUC(uc);
			}
			else
			{
				IBussinessLogService logService = IoC.Resolve<IBussinessLogService>();
				try
				{
                    try
                    {
                        IInvoiceVATService service = IoC.Resolve<IInvoiceVATService>();
                        InvoiceVAT invoice = this.InnitData();
                        string message = "";
                        if (invoice.Id <= 0)
                        {
                            XtraMessageBox.Show("Phát hành hóa đơn thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            if (!service.UpdateNewInvoice(invoice, out message))
                            {
                                throw new Exception(message);
                            }
                            BussinessLog Bussinesslog = new BussinessLog()
                            {
                                FileName = "Phát hành hóa đơn",
                                AppName = "Phát hành",
                                CreateDate = DateTime.Now
                            };

                            /* Check Invoice Exist On Viettel Server */
                            try
                            {
                                APIResultCodeOfTax resultTemp = ViettelAPI.APIHelper.GetCodeOfTax(invoice.Fkey, null);
                                if (resultTemp != null)
                                {
                                    XtraMessageBox.Show("Hóa Đơn Đã Tồn Tại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    return;
                                }
                            }
                            catch (Exception ex) { }
                            /* Check Invoice Exist On Viettel Server */

                            APIResult result = ViettelAPI.APIHelper.SendInvoiceV2(IoC.Resolve<IApiParserService>().MappingToInvoiceModel(invoice));
                            /* Handle CodeOfTax */
                            if (string.IsNullOrEmpty(result.errorCode))
                            {
                                try
                                {
                                    APIResultCodeOfTax resultGetCodeOfTax = ViettelAPI.APIHelper.GetCodeOfTax(invoice.Fkey, null);

                                    string codeOfTax = resultGetCodeOfTax.result.ElementAt(0).codeOfTax;
                                    string exchangeDes = resultGetCodeOfTax.result.ElementAt(0).exchangeDes;
                                    string exchangeStatus = resultGetCodeOfTax.result.ElementAt(0).exchangeStatus;

                                    invoice.ExchangeDes = exchangeDes;
                                    invoice.ExchangeStatus = exchangeStatus;

                                    if (!string.IsNullOrEmpty(codeOfTax))
                                    {
                                        invoice.CodeOfTax = codeOfTax;
                                    }
                                    else
                                    {
                                        if ((exchangeDes != null) && exchangeDes.ToLower().Contains(("Mã Lỗi").ToLower()))
                                        {
                                            invoice.CodeOfTax = "Lỗi";
                                            Bussinesslog.Error += string.Format("Lấy Mã CQT Có Lỗi - {0} \r\n", resultGetCodeOfTax.description);
                                        }
                                    }
                                    Bussinesslog.Error += string.Format("Phát hành hóa đơn thành công - {0}", invoice.InvoiceNoSAP);
                                }
                                catch (Exception ex)
                                {
                                    invoice.CodeOfTax = "";
                                    Bussinesslog.Error += string.Format("Lấy Mã CQT Có Lỗi - {0} \r\n", ex.Message);
                                }
                            }
                            /* Handle CodeOfTax */
                            else
                            {
                                Bussinesslog.Error += string.Format("Phát hành hóa đơn lỗi - {0}", invoice.InvoiceNoSAP);
                            }

                            InvoiceHelper.UpdatePublishResultV2(invoice, result);
                            logService.CreateNew(Bussinesslog);
                            logService.CommitChanges();
                            XtraMessageBox.Show("Đã thực hiện phát hành hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    catch (WebException ex)
                    {
                        var sr = new StreamReader(((WebException)ex).Response.GetResponseStream());
                        var jsData = sr.ReadToEnd();
                        APIErrorResult result = JsonConvert.DeserializeObject<APIErrorResult>(jsData);

                        BussinessLog Bussinesslog = new BussinessLog()
                        {
                            FileName = "Phát hành hóa đơn",
                            AppName = "Phát hành",
                            CreateDate = DateTime.Now,
                            Error = string.Concat("Hóa đơn: ", this.Invoice.InvoiceNoSAP, " + Lỗi: ", result.data)
                        };

                        logService.CreateNew(Bussinesslog);
                        logService.CommitChanges();
                        this.log.Error(ex);
                        XtraMessageBox.Show("Phát hành hóa đơn thất bại. \r\nLỗi : " + result.data, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    catch (Exception ex)
                    {
                        BussinessLog Bussinesslog = new BussinessLog()
                        {
                            FileName = "Phát hành hóa đơn",
                            AppName = "Phát hành",
                            CreateDate = DateTime.Now,
                            Error = string.Concat("Hóa đơn: ", this.Invoice.InvoiceNoSAP, " + Lỗi: ", ex.Message)
                        };
                        logService.CreateNew(Bussinesslog);
                        logService.CommitChanges();
                        this.log.Error(ex);
                        XtraMessageBox.Show("Phát hành hóa đơn thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
				}
				finally
				{
					base.Close();
					ucInvoiceVAT uc = new ucInvoiceVAT(this.Main);
					this.Main.AddUC(uc);
				}
			}
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			bool result;
			try
			{
				if (this.ValidateData())
				{
					IInvoiceVATService service = IoC.Resolve<IInvoiceVATService>();
					InvoiceVAT invoice = this.InnitData();
					string message = "";
					result = (invoice.Id <= 0 ? service.CreateNewInvoice(invoice, out message) : service.UpdateNewInvoice(invoice, out message));
					if (!result)
					{
						throw new Exception(message);
					}
					ucInvoiceVAT uc = new ucInvoiceVAT(this.Main);
					this.Main.AddUC(uc);
					XtraMessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.btnReleased.Enabled = true;
				}
				else
				{
					XtraMessageBox.Show("Số hóa đơn không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			catch (Exception exception)
			{
				Exception ex = exception;
				XtraMessageBox.Show("Cập nhật thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				this.log.Error(ex);
			}
		}

		private void Calculator()
		{
			InvoiceVAT invoice = this.Invoice;
			if (invoice != null)
			{
                decimal num = 0;
                foreach (ProductInv prod in invoice.Products)
                {
                    List<string> itemCode = prod.Name.Split(' ').ToList();
                    if (itemCode.ElementAt(0).Equals("50504") || 
                        itemCode.ElementAt(0).Equals("50506") ||
                        itemCode.ElementAt(0).Equals("50507") ||
                        itemCode.ElementAt(0).Equals("50508"))
                    {
                        if (prod.Total < 0)
                        {
                            num = num + prod.Total;
                        }
                        else
                        {
                            num = num - prod.Total;
                        }
                    }
                    else
                    {
                        num = num + prod.Total;
                    }
                }

				double total = double.Parse(num.ToString());
				num = invoice.Products.Sum<ProductInv>((ProductInv p) => p.DiscountAmount);
				double discount = double.Parse(num.ToString());
				if (invoice.VATRate == -1f)
				{
					invoice.VATRate = 0f;
				}
				double tax = Math.Round((total - discount) * (double)invoice.VATRate / 100, MidpointRounding.AwayFromZero);
				double amount = Math.Round(total - discount + tax);
				this.txtTotal.Text = (total - discount).ToString();
				this.txtVATAmount.Text = tax.ToString();
				this.txtDiscountAmount.Text = discount.ToString();
				this.txtAmountInvoice.Text = amount.ToString();
				this.txtAmountInWord.Text = NumberToLeter.DocTienBangChu((long)amount);
			}
		}

		private void cbVATRate_EditValueChanged(object sender, EventArgs e)
		{
			this.Invoice.VATRate = float.Parse(this.cbVATRate.EditValue.ToString());
			this.Calculator();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmInvoice_Load(object sender, EventArgs e)
		{
			this.LoadProTypeCombobox();
			this.LoadTaxCombobox();
			this.LoadData();
		}

		private void gridProduct_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			if (e.Column.FieldName == "Quantity")
			{
				decimal quantity = (e.Value != null ? decimal.Parse(e.Value.ToString()) : decimal.Zero);
				decimal price = (this.viewProduct.GetFocusedRowCellValue("Price") != null ? decimal.Parse(this.viewProduct.GetFocusedRowCellValue("Price").ToString()) : decimal.Zero);
				this.viewProduct.SetFocusedRowCellValue("Total", ((!(quantity == decimal.Zero) || !(price < decimal.Zero) ? quantity * price : price)).ToString());
				this.Calculator();
			}
			if (e.Column.FieldName == "Price")
			{
				decimal price = (e.Value != null ? decimal.Parse(e.Value.ToString()) : decimal.Zero);
				decimal quantity = (this.viewProduct.GetFocusedRowCellValue("Quantity") != null ? decimal.Parse(this.viewProduct.GetFocusedRowCellValue("Quantity").ToString()) : decimal.Zero);
				this.viewProduct.SetFocusedRowCellValue("Total", ((!(quantity == decimal.Zero) || !(price < decimal.Zero) ? quantity * price : price)).ToString());
				this.Calculator();
			}
			if (e.Column.FieldName == "DiscountAmount")
			{
				this.Calculator();
			}
		}

	}
}
