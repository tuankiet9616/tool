using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using FX.Core;
using Parse.Core.Domain;
using Parse.Core.Implement;
using Parse.Core.IService;
using Parse.Forms.CustomUC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ViettelAPI;
using ViettelAPI.Models;

namespace Parse.Forms
{
    partial class ucInvoiceVAT : UserControl
    {
		private void btnClear_Click(object sender, EventArgs e)
		{
			this.ClearData();
			this.LoadData(1);
		}

		private void btnDeleteAll_Click(object sender, EventArgs e)
		{
			if (XtraMessageBox.Show("Bạn có chắc chắn muốn xóa tất cả hóa đơn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				SplashScreenManager.ShowForm(typeof(ProcessIndicator));
				IoC.Resolve<IBussinessLogService>();
				IInvoiceVATService invService = IoC.Resolve<IInvoiceVATService>();
				try
				{
					List<InvoiceVAT> lstInvoice = invService.GetUnPublish();
					foreach (InvoiceVAT invoice in lstInvoice)
					{
						List<ProductInv> lstprod = IoC.Resolve<IProductInvService>().GetByInvoiceID(invoice.Id);
						invoice.Products = lstprod;
					}
					if (lstInvoice.Count <= 0)
					{
						SplashScreenManager.CloseForm();
						XtraMessageBox.Show("Không tồn tại hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else
					{
						string message = "";
						if (IoC.Resolve<IInvoiceVATService>().DeleteInvoices(lstInvoice, out message))
						{
							SplashScreenManager.CloseForm();
							XtraMessageBox.Show("Đã thực hiện xóa hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							ucInvoiceVAT uc = new ucInvoiceVAT(this.Main);
							this.Main.AddUC(uc);
						}
						else
						{
							SplashScreenManager.CloseForm();
							XtraMessageBox.Show("Có lỗi xảy ra, vui lòng thực hiện lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
					}
				}
				catch (Exception exception)
				{
					Exception ex = exception;
					SplashScreenManager.CloseForm();
					XtraMessageBox.Show("Có lỗi xảy ra, vui lòng thực hiện lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					this.log.Error(ex);
				}
			}
		}

		private void btnDeleteSelected_Click(object sender, EventArgs e)
		{
			if (this.viewInvoiceVAT.SelectedRowsCount <= 0)
			{
				XtraMessageBox.Show("Chưa chọn hóa đơn xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (XtraMessageBox.Show("Bạn có chắc chắn muốn xóa các hóa đơn này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				SplashScreenManager.ShowForm(typeof(ProcessIndicator));
				try
				{
					List<InvoiceVAT> lstInvoice = new List<InvoiceVAT>();
					int[] selectedRows = this.viewInvoiceVAT.GetSelectedRows();
					for (int i = 0; i < (int)selectedRows.Length; i++)
					{
						int rowHandle = selectedRows[i];
						InvoiceVAT entity = (InvoiceVAT)this.viewInvoiceVAT.GetRow(rowHandle);
						if (entity != null)
						{
							List<ProductInv> lstprod = IoC.Resolve<IProductInvService>().GetByInvoiceID(entity.Id);
							entity.Products = lstprod;
							lstInvoice.Add(entity);
						}
					}
					string message = "";
					if (IoC.Resolve<IInvoiceVATService>().DeleteInvoices(lstInvoice, out message))
					{
						SplashScreenManager.CloseForm();
						XtraMessageBox.Show("Đã thực hiện xóa hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						if (this.isFind)
						{
							this.FindData(this.ucPaging.PageIndex);
						}
						else
						{
							this.LoadData(this.ucPaging.PageIndex);
						}
					}
					else
					{
						SplashScreenManager.CloseForm();
						XtraMessageBox.Show("Có lỗi xảy ra, vui lòng thực hiện lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				catch (Exception exception)
				{
					Exception ex = exception;
					SplashScreenManager.CloseForm();
					XtraMessageBox.Show("Có lỗi xảy ra, vui lòng thực hiện lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					this.log.Error(ex);
				}
			}
		}

		private void btnFind_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.txtFind.Text) && string.IsNullOrEmpty(this.dtTuNgay.Text) && string.IsNullOrEmpty(this.dtDenNgay.Text))
			{
				this.ClearData();
				this.LoadData(1);
				return;
			}
			this.strFind = this.txtFind.Text;
			if (string.IsNullOrEmpty(this.dtTuNgay.Text))
			{
				this.tuNgay = null;
			}
			else
			{
				this.tuNgay = new DateTime?(this.dtTuNgay.DateTime);
			}
			if (string.IsNullOrEmpty(this.dtDenNgay.Text))
			{
				this.denNgay = null;
			}
			else
			{
				this.denNgay = new DateTime?(this.dtDenNgay.DateTime);
			}
			this.isFind = true;
			this.FindData(1);
		}

		private void btnPublicAll_Click(object sender, EventArgs e)
		{
			SplashScreenManager.ShowForm(typeof(ProcessIndicator));
			IBussinessLogService logService = IoC.Resolve<IBussinessLogService>();
			IInvoiceVATService invService = IoC.Resolve<IInvoiceVATService>();
			try
			{
				List<InvoiceVAT> lstInvoice = invService.GetUnPublish();
				foreach (InvoiceVAT invoice in lstInvoice)
				{
					List<ProductInv> lstprod = IoC.Resolve<IProductInvService>().GetByInvoiceID(invoice.Id);
					invoice.Products = lstprod;
				}
				if (lstInvoice.Count <= 0)
				{
					SplashScreenManager.CloseForm();
					XtraMessageBox.Show("Không tồn tại hóa đơn chưa phát hành", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					string strlstFolio = string.Concat("Danh sách hóa đơn: ", lstInvoice[0].InvoiceNoSAP);
					for (int i = 1; i < lstInvoice.Count; i++)
					{
						strlstFolio = string.Concat(strlstFolio, " - ", lstInvoice[i].InvoiceNoSAP);
					}
					try
					{
						APIResults results = ViettelAPI.APIHelper.SendInvoicesV2(IoC.Resolve<IApiParserService>().ConvertToAPIModel(lstInvoice));
						InvoiceHelper.UpdatePublishResult(lstInvoice, results);
						List<APIResult> successResults = (
							from p in results.createInvoiceOutputs
							where (string.IsNullOrEmpty(p.errorCode) || p.errorCode == "200")
							select p).ToList<APIResult>();
						List<APIResult> failResults = (
							from p in results.createInvoiceOutputs
							where (!string.IsNullOrEmpty(p.errorCode) && p.errorCode != "200")
                            select p).ToList<APIResult>();
						string lstFail = "";
						if (failResults.Count > 0)
						{
							foreach (APIResult item in failResults)
							{
								lstFail = string.Concat(lstFail, " - ", item.transactionUuid.Split(new char[] { '-' }).ToList<string>()[item.transactionUuid.Split(new char[] { '-' }).ToList<string>().Count<string>() - 2]);
							}
						}
						BussinessLog Bussinesslog = new BussinessLog()
						{
							FileName = "Phát hành hóa đơn",
							AppName = "Phát hành",
							CreateDate = DateTime.Now
						};
						if (failResults.Count<APIResult>() != 0)
						{
							Bussinesslog.Error = string.Format("Phát hành hóa đơn thành công {0}/{1} - {2} - Hóa đơn lỗi: {3}", new object[] { successResults.Count<APIResult>(), lstInvoice.Count, strlstFolio, lstFail });
						}
						else
						{
							Bussinesslog.Error = string.Format("Phát hành hóa đơn thành công - {0}", strlstFolio);
						}
						logService.CreateNew(Bussinesslog);
						logService.CommitChanges();
						SplashScreenManager.CloseForm();
						XtraMessageBox.Show("Đã thực hiện phát hành hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						ucInvoiceVAT uc = new ucInvoiceVAT(this.Main);
						this.Main.AddUC(uc);
					}
					catch (Exception exception)
					{
						Exception ex = exception;
						BussinessLog Bussinesslog = new BussinessLog()
						{
							FileName = "Phát hành hóa đơn",
							AppName = "Phát hành",
							CreateDate = DateTime.Now,
							Error = string.Concat(strlstFolio, " + Lỗi: ", ex.Message)
						};
						logService.CreateNew(Bussinesslog);
						logService.CommitChanges();
						this.log.Error(ex);
						SplashScreenManager.CloseForm();
						XtraMessageBox.Show("Phát hành hóa đơn thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						this.log.Error(ex);
					}
				}
			}
			catch (Exception exception1)
			{
				Exception ex = exception1;
				BussinessLog Bussinesslog = new BussinessLog()
				{
					FileName = "Phát hành hóa đơn",
					AppName = "Phát hành",
					CreateDate = DateTime.Now,
					Error = string.Concat("Lỗi: ", ex.Message)
				};
				logService.CreateNew(Bussinesslog);
				logService.CommitChanges();
				this.log.Error(ex);
				SplashScreenManager.CloseForm();
				XtraMessageBox.Show("Phát hành hóa đơn thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void btnPublicInvoice_Click(object sender, EventArgs e)
		{
            InvoiceVATService.log.Error(string.Format("--------- Start Public Invoice -------------"));
            if (this.viewInvoiceVAT.SelectedRowsCount <= 0)
			{
				XtraMessageBox.Show("Chưa chọn hóa đơn phát hành", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				SplashScreenManager.ShowForm(typeof(ProcessIndicator));
				IBussinessLogService logService = IoC.Resolve<IBussinessLogService>();
				try
				{
					List<InvoiceVAT> lstInvoice = new List<InvoiceVAT>();
					int[] selectedRows = this.viewInvoiceVAT.GetSelectedRows();
                    List<string> invExisted = new List<string>(); 

                    for (int num = 0; num < (int)selectedRows.Length; num++)
					{
						int rowHandle = selectedRows[num];
						InvoiceVAT entity = (InvoiceVAT)this.viewInvoiceVAT.GetRow(rowHandle);
						if (entity != null)
						{
                            /* Check Invoice Exist On Viettel Server */
                            try
                            {
                                APIResultCodeOfTax resultTemp = APIHelper.GetCodeOfTax(entity.Fkey, null);
                                if (resultTemp != null)
                                {
                                    invExisted.Add(entity.Fkey);
                                    continue;
                                }
                            }
                            catch (Exception ex){}
                            /* Check Invoice Exist On Viettel Server */

                            List<ProductInv> lstprod = IoC.Resolve<IProductInvService>().GetByInvoiceID(entity.Id);
							entity.Products = lstprod;
							lstInvoice.Add(entity);
						}
					}

					try
					{
                        List<string> messageLines = new List<string>();
                        APIResults results = ViettelAPI.APIHelper.SendInvoicesV2(IoC.Resolve<IApiParserService>().ConvertToAPIModel(lstInvoice));
                        InvoiceVATService.log.Error(string.Format("----- Result After Send Invoices : {0}", results.createInvoiceOutputs.Count));

                        List<APIResult> successResults = (
							from p in results.createInvoiceOutputs
							where (string.IsNullOrEmpty(p.errorCode) || p.errorCode == "200")
							select p).ToList<APIResult>();
						List<APIResult> failResults = (
							from p in results.createInvoiceOutputs
							where (!string.IsNullOrEmpty(p.errorCode) && p.errorCode != "200")
                            select p).ToList<APIResult>();
						string lstFail = "";

                        List<string> scs = (from p in results.createInvoiceOutputs
                                            where (string.IsNullOrEmpty(p.errorCode) || p.errorCode == "200")
                                            select p.transactionUuid).ToList<string>();

                        List<string> fls = (from p in results.createInvoiceOutputs
                                            where (!string.IsNullOrEmpty(p.errorCode) && p.errorCode != "200")
                                            select p.transactionUuid).ToList<string>();

                        int invUnablePubCount = selectedRows.Length - successResults.Count - invExisted.Count - failResults.Count;
                        List<string> invUnablePublish = new List<string>();

                        foreach (InvoiceVAT invoice in lstInvoice) {
                            if (!scs.Contains(invoice.InvoiceNoSAP) && !fls.Contains(invoice.InvoiceNoSAP)) {
                                invUnablePublish.Add(invoice.InvoiceNoSAP);
                            }
                        }

                        InvoiceVATService.log.Error(string.Format("----- Success Result : {0}", successResults.Count));
                        InvoiceVATService.log.Error(string.Format("----- Fail Result : {0}", failResults.Count));

                        if (failResults.Count > 0)
						{
							foreach (APIResult item in failResults)
							{
								lstFail = string.Concat(lstFail, " - ", item.transactionUuid.Split(new char[] { '-' }).ToList<string>()[item.transactionUuid.Split(new char[] { '-' }).ToList<string>().Count<string>() - 2]);
							}
						}

						BussinessLog Bussinesslog = new BussinessLog()
						{
							FileName = "Phát hành hóa đơn",
							AppName = "Phát hành",
							CreateDate = DateTime.Now
						};
                        if (successResults.Count > 0)
                        {
                            string fkeys = string.Empty;
                            for (int i = 0; i < successResults.Count; i++)
                            {
                                Console.WriteLine(i);

                                InvoiceVAT invoice = (from t in lstInvoice where t.Fkey.Equals(successResults.ElementAt(i).transactionUuid) select t).FirstOrDefault<InvoiceVAT>();
                                try
                                {
                                    InvoiceVATService.log.Error(string.Format("--------- Start Get Code Of Tax : {0}", invoice.Fkey));
                                    APIResultCodeOfTax resultGetCodeOfTax = ViettelAPI.APIHelper.GetCodeOfTax(invoice.Fkey, null);
                                    InvoiceVATService.log.Error(string.Format("--------- Finish Get Code Of Tax : {0}", invoice.Fkey));

                                    string codeOfTax = resultGetCodeOfTax.result.ElementAt(0).codeOfTax;
                                    string invoiceNo = resultGetCodeOfTax.result.ElementAt(0).invoiceNo;
                                    string exchangeStatus = resultGetCodeOfTax.result.ElementAt(0).exchangeStatus;
                                    string exchangeDes = resultGetCodeOfTax.result.ElementAt(0).exchangeDes;

                                    invoice.ExchangeStatus = exchangeStatus;
                                    invoice.ExchangeDes = exchangeDes;

                                    if (!string.IsNullOrEmpty(invoiceNo))
                                    {
                                        invoice.No = invoiceNo;
                                        invoice.Serial = invoiceNo.Substring(0, 6);
                                    }

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
                                }
                                catch (Exception ex)
                                {
                                    invoice.CodeOfTax = "";
                                    Bussinesslog.Error += string.Format("Lấy Mã CQT Có Lỗi - {0} \r\n", ex.Message);
                                }
                            }
                        }

                        InvoiceVATService.log.Error(string.Format("--------- Start Update DB ---------------"));
                        InvoiceHelper.UpdatePublishResult(lstInvoice, results);
                        InvoiceVATService.log.Error(string.Format("--------- Finish Update DB ---------------"));

                        /* Write Log */
                        string logStr = string.Empty;

                        /* Write Log [Hóa Đơn Phát Hành Thành Công] */
                        if (successResults.Count > 0)
                        {
                            string invSuccessLog = "Hóa đơn phát hành thành công : " + successResults.ElementAt(0).transactionUuid;
                            for (int i = 1; i < successResults.Count; i++)
                            {
                                invSuccessLog += " - " + successResults.ElementAt(i).transactionUuid;
                            }

                            invSuccessLog += " \r\n";
                            logStr += invSuccessLog;
                        }
                        /* Write Log [Hóa Đơn Phát Hành Thành Công] */

                        /* Write Log [Hóa Đơn Phát Hành Thất Bại] */
                        if (failResults.Count > 0)
                        {
                            string invFailLog = "Hóa đơn phát hành thất bại : " + failResults.ElementAt(0).transactionUuid;
                            for (int i = 1; i < failResults.Count; i++)
                            {
                                invFailLog += " - " + failResults.ElementAt(i).transactionUuid;
                            }

                            invFailLog += " \r\n";
                            logStr += invFailLog;
                        }
                        /* Write Log [Hóa Đơn Phát Hành Thất Bại] */

                        /* Write Log [Hóa Đơn Đã Tồn Tại] */
                        if (invExisted.Count > 0)
                        {
                            string invExistedLog = "Hóa đơn đã tồn tại trên Server Viettel : " + invExisted[0];
                            for (int i = 1; i < invExisted.Count; i++)
                            {
                                invExistedLog += " - " + invExisted[i];
                            }

                            invExistedLog += " \r\n";
                            logStr += invExistedLog;
                        }
                        /* Write Log [Hóa Đơn Đã Tồn Tại] */

                        /* Write Log [Hóa Đơn Không Thể Phát Hành] */
                        if (invUnablePublish.Count > 0)
                        {
                            string invUnablePublishLog = "Hóa đơn không thể phát hành : " + invUnablePublish[0];
                            for (int i = 1; i < invUnablePublish.Count; i++)
                            {
                                invUnablePublishLog += " - " + invUnablePublish[i];
                            }

                            invUnablePublishLog += " \r\n";
                            logStr += invUnablePublishLog;
                        }
                        /* Write Log [Hóa Đơn Không Thể Phát Hành] */
                        /* Write Log */

                        Bussinesslog.Error += logStr;
                        logService.CreateNew(Bussinesslog);
						logService.CommitChanges();

                        messageLines.Add(string.Format("Hóa đơn phát hành thành công : {0}/{1}", successResults.Count, selectedRows.Length));
                        messageLines.Add(string.Format("Hóa đơn phát hành lỗi từ Viettel : {0}/{1}", failResults.Count, selectedRows.Length));
                        messageLines.Add(string.Format("Hóa đơn đã tồn tại trên Server Viettel : {0}/{1}", invExisted.Count, selectedRows.Length));
                        messageLines.Add(string.Format("Hóa đơn không thể phát hành : {0}/{1}", invUnablePubCount, selectedRows.Length));
                        
                        /* Handle List Map Error */
                        Dictionary<string, string> dictError = new Dictionary<string, string>();
                        if (results.lstMapError.Count > 0)
                        {
                            messageLines.Add(string.Format("-----------------------------------------------"));
                            for (int i = 0; i < results.lstMapError.Count; i++)
                            {
                                if (!dictError.ContainsKey(results.lstMapError[i].errorCode))
                                {
                                    if ("INVOICE_SERIAL_NOT_FOUND".Equals(results.lstMapError[i].errorCode))
                                    {
                                        dictError.Add(results.lstMapError[i].errorCode, results.lstMapError[i].invoiceSeri + " - " + results.lstMapError[i].msg);
                                    }
                                    else
                                    {
                                        dictError.Add(results.lstMapError[i].errorCode, results.lstMapError[i].msg);
                                    }
                                }
                            }
                        }
                        /* Handle List Map Error */

                        foreach (var x in dictError.Select((Entry, Index) => new { Entry, Index }))
                        {
                            messageLines.Add(string.Format("Lỗi {0} : {1} ", x.Index + 1, x.Entry.Value));
                        }

                        SplashScreenManager.CloseForm();
                        XtraMessageBox.Show(string.Join(Environment.NewLine, messageLines.ToArray()), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        if (this.isFind)
						{
							this.FindData(this.ucPaging.PageIndex);
						}
						else
						{
							this.LoadData(this.ucPaging.PageIndex);
						}
					}
					catch (Exception exception)
					{
                        InvoiceVATService.log.Error(string.Format(">>>> --- Exception : {0}", exception.Message));
                        Exception ex = exception;
						BussinessLog Bussinesslog = new BussinessLog()
						{
							FileName = "Phát hành hóa đơn",
							AppName = "Phát hành",
							CreateDate = DateTime.Now,
							Error = string.Concat("Lỗi: ", ex.Message)
						};
						logService.CreateNew(Bussinesslog);
						logService.CommitChanges();
						this.log.Error(ex);
						SplashScreenManager.CloseForm();
						XtraMessageBox.Show("Phát hành hóa đơn thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				catch (Exception exception1)
				{
                    InvoiceVATService.log.Error(string.Format(">>>> --- Exception : {0}", exception1.Message));
                    Exception ex = exception1;
					BussinessLog Bussinesslog = new BussinessLog()
					{
						FileName = "Phát hành hóa đơn",
						AppName = "Phát hành",
						CreateDate = DateTime.Now,
						Error = string.Concat("Lỗi: ", ex.Message)
					};
					logService.CreateNew(Bussinesslog);
					logService.CommitChanges();
					this.log.Error(ex);
					SplashScreenManager.CloseForm();
					XtraMessageBox.Show("Phát hành hóa đơn thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
            InvoiceVATService.log.Error(string.Format("--------- Finish Public Invoice -------------"));
        }

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			this.ClearData();
			this.LoadData(1);
		}

		public void ClearData()
		{
			this.txtFind.Text = "";
			this.strFind = "";
			this.dtTuNgay.Text = "";
			this.tuNgay = null;
			this.dtDenNgay.Text = "";
			this.denNgay = null;
			this.isFind = false;
		}

		private void cmdTaoLap_Click(object sender, EventArgs e)
		{
			InvoiceVAT invoice = new InvoiceVAT()
			{
				Products = new List<ProductInv>(),
				VATRate = 10f
			};
			(new frmInvoice(this.Main, invoice, false)).ShowDialog();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		public void FindData(int PageIndex)
		{
			int total = 0;
			List<InvoiceVAT> list = IoC.Resolve<IInvoiceVATService>().FindUnPublish(this.tuNgay, this.denNgay, this.strFind, ref PageIndex, this.ucPaging.PageSize, out total);
			this.gridInvoiceVAT.DataSource = list;
			this.gridInvoiceVAT.Focus();
			this.lblInvoicesNumber.Text = total.ToString();
			this.ucPaging.PageIndex = PageIndex;
			this.ucPaging.Total = new int?(total);
			this.ucPaging.UpdatePagingState();
		}

		private void ucInvoiceVAT_Load(object sender, EventArgs e)
		{
			this.LoadData(1);
		}

		private void UCPaging_Click(object sender, PagingEventArgs e)
		{
			SimpleButton button = (SimpleButton)sender;
			if (this.isFind)
			{
				if (button.Name == "btnFirst")
				{
					this.FindData(1);
					return;
				}
				if (button.Name == "btnPrev")
				{
					this.FindData(e.NextPageIndex);
					return;
				}
				if (button.Name == "btnNext")
				{
					this.FindData(e.NextPageIndex);
					return;
				}
				if (button.Name == "btnLast")
				{
					this.FindData(e.NextPageIndex);
				}
			}
			else
			{
				if (button.Name == "btnFirst")
				{
					this.LoadData(1);
					return;
				}
				if (button.Name == "btnPrev")
				{
					this.LoadData(e.NextPageIndex);
					return;
				}
				if (button.Name == "btnNext")
				{
					this.LoadData(e.NextPageIndex);
					return;
				}
				if (button.Name == "btnLast")
				{
					this.LoadData(e.NextPageIndex);
					return;
				}
			}
		}

		private void viewInvoiceVAT_RowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (e.Column.FieldName == "ViewInv")
			{
				try
				{
					InvoiceVAT entity = (InvoiceVAT)this.viewInvoiceVAT.GetRow(this.viewInvoiceVAT.FocusedRowHandle);
					if (entity != null)
					{
						List<ProductInv> lstprod = IoC.Resolve<IProductInvService>().GetByInvoiceID(entity.Id);
						entity.Products = lstprod;
						(new frmInvocieView(entity.GetXMLData(Parse.Core.AppContext.Current.company), this.Main, entity)).ShowDialog();
					}
				}
				catch (Exception exception)
				{
					this.log.Error(exception);
				}
			}
			if (e.Column.FieldName == "EditInv")
			{
				try
				{
					InvoiceVAT entity = (InvoiceVAT)this.viewInvoiceVAT.GetRow(this.viewInvoiceVAT.FocusedRowHandle);
					if (entity != null)
					{
						List<ProductInv> lstprod = IoC.Resolve<IProductInvService>().GetByInvoiceID(entity.Id);
						entity.Products = lstprod;
						(new frmInvoice(this.Main, entity, true)).ShowDialog();
					}
				}
				catch (Exception exception1)
				{
					this.log.Error(exception1);
				}
			}
		}
	}
}
