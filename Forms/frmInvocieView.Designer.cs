using DevExpress.Utils;
using DevExpress.XtraEditors;
using FX.Core;
using log4net;
using Microsoft.Win32;
using Parse.Core.Domain;
using Parse.Core.IService;
using Parse.Forms.CustomUC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using ViettelAPI;
using ViettelAPI.Models;

namespace Parse.Forms
{
	public class frmInvocieView : XtraForm
	{
		private readonly ILog log = LogManager.GetLogger(typeof(frmInvocieView));

		private string XMLData = "";

		private InvoiceVAT Invoice;

		private IMainForm Main;

		private IContainer components;

		private GroupControl groupInvoiceDetail;

		private WebBrowser invBrowser;

		private SimpleButton btnPrint;

		private SimpleButton btnPublishInvoice;

		private SimpleButton btnEditInvocie;

		public frmInvocieView(string xmldata, IMainForm main, InvoiceVAT invoice = null)
		{
			this.InitializeComponent();
			this.Main = main;
			if (invoice != null)
			{
				this.Invoice = invoice;
				this.XMLData = xmldata;
				this.LoadInvoice(this.XMLData);
				this.btnEditInvocie.Visible = string.IsNullOrEmpty(invoice.No);
				this.btnPrint.Visible = false;
				return;
			}
			this.invBrowser.DocumentText = xmldata;
			this.invBrowser.ScriptErrorsSuppressed = true;
			this.btnEditInvocie.Visible = false;
			this.btnPrint.Visible = true;
			this.btnPrint.Location = new Point(367, 626);
		}

		private void btnEditInvocie_Click(object sender, EventArgs e)
		{
			frmInvoice _frmInvoice = new frmInvoice(this.Main, this.Invoice, true);
			base.Hide();
			_frmInvoice.ShowDialog();
			base.Close();
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			this.invBrowser.ShowPrintDialog();
		}

		private void btnPublishInvoice_Click(object sender, EventArgs e)
		{
			try
			{
				List<InvoiceVAT> lstInvoice = new List<InvoiceVAT>()
				{
					this.Invoice
				};
				APIResults results = APIHelper.SendInvoices(IoC.Resolve<IApiParserService>().ConvertToAPIModel(lstInvoice));
				InvoiceHelper.UpdatePublishResult(lstInvoice, results);
				XtraMessageBox.Show("Đã thực hiện phát hành hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch (Exception exception)
			{
				this.log.Error(exception);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(frmInvocieView));
			this.groupInvoiceDetail = new GroupControl();
			this.invBrowser = new WebBrowser();
			this.btnPrint = new SimpleButton();
			this.btnPublishInvoice = new SimpleButton();
			this.btnEditInvocie = new SimpleButton();
			((ISupportInitialize)this.groupInvoiceDetail).BeginInit();
			this.groupInvoiceDetail.SuspendLayout();
			base.SuspendLayout();
			this.groupInvoiceDetail.Controls.Add(this.invBrowser);
			this.groupInvoiceDetail.Location = new Point(0, 1);
			this.groupInvoiceDetail.Name = "groupInvoiceDetail";
			this.groupInvoiceDetail.Size = new System.Drawing.Size(836, 619);
			this.groupInvoiceDetail.TabIndex = 6;
			this.invBrowser.Dock = DockStyle.Fill;
			this.invBrowser.Location = new Point(2, 20);
			this.invBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.invBrowser.Name = "invBrowser";
			this.invBrowser.Size = new System.Drawing.Size(832, 597);
			this.invBrowser.TabIndex = 0;
			this.invBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.invBrowser_DocumentCompleted);
			this.btnPrint.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.btnPrint.Cursor = Cursors.Hand;
			this.btnPrint.ImageOptions.Image = (Image)resources.GetObject("btnPrint.ImageOptions.Image");
			this.btnPrint.Location = new Point(446, 626);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(95, 23);
			this.btnPrint.TabIndex = 19;
			this.btnPrint.Text = "In hóa đơn";
			this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
			this.btnPublishInvoice.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.btnPublishInvoice.Cursor = Cursors.Hand;
			this.btnPublishInvoice.ImageOptions.Image = (Image)resources.GetObject("btnPublishInvoice.ImageOptions.Image");
			this.btnPublishInvoice.Location = new Point(211, 626);
			this.btnPublishInvoice.Name = "btnPublishInvoice";
			this.btnPublishInvoice.Size = new System.Drawing.Size(128, 23);
			this.btnPublishInvoice.TabIndex = 20;
			this.btnPublishInvoice.Text = "Phát hành hóa đơn";
			this.btnPublishInvoice.Visible = false;
			this.btnPublishInvoice.Click += new EventHandler(this.btnPublishInvoice_Click);
			this.btnEditInvocie.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.btnEditInvocie.Cursor = Cursors.Hand;
			this.btnEditInvocie.ImageOptions.Image = (Image)resources.GetObject("btnEditInvocie.ImageOptions.Image");
			this.btnEditInvocie.Location = new Point(345, 626);
			this.btnEditInvocie.Name = "btnEditInvocie";
			this.btnEditInvocie.Size = new System.Drawing.Size(95, 23);
			this.btnEditInvocie.TabIndex = 21;
			this.btnEditInvocie.Text = "Sửa hóa đơn";
			this.btnEditInvocie.Click += new EventHandler(this.btnEditInvocie_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(836, 655);
			base.Controls.Add(this.btnEditInvocie);
			base.Controls.Add(this.btnPublishInvoice);
			base.Controls.Add(this.btnPrint);
			base.Controls.Add(this.groupInvoiceDetail);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmInvocieView";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Thông tin chi tiết hóa đơn";
			((ISupportInitialize)this.groupInvoiceDetail).EndInit();
			this.groupInvoiceDetail.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void invBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			try
			{
				RegistryKey registryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.CurrentUser, "").OpenSubKey("Software\\Microsoft\\Internet Explorer\\PageSetup", RegistryKeyPermissionCheck.ReadWriteSubTree);
				registryKey.SetValue("(Default)", "");
				registryKey.SetValue("font", "");
				registryKey.SetValue("footer", "");
				registryKey.SetValue("header", "");
				registryKey.SetValue("margin_bottom", "0.750000");
				registryKey.SetValue("margin_left", "0.750000");
				registryKey.SetValue("margin_right", "0.750000");
				registryKey.SetValue("margin_top", "0.750000");
				registryKey.SetValue("Print_Background", "yes");
				registryKey.SetValue("Shrink_To_Fit", "yes");
			}
			catch (Exception exception)
			{
				MessageBox.Show("Có lỗi xảy ra, vui lòng thực hiện lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		public void LoadInvoice(string xmlData)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(xmlData))
				{
					int products = 1;
					string html = DsigViewer.GetHtml(xmlData, out products);
					if (!string.IsNullOrWhiteSpace(html))
					{
						string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
						string java = string.Concat("<script src=\"", path, "\\Content\\viewer\\jquery.min.js\"></script>");
						java = string.Concat(java, "<script src=\"", path, "\\Content\\viewer\\main.js\"></script>");
						java = string.Concat(java, "<link href=\"", path, "\\Content\\viewer\\styles.css\" rel=\"stylesheet\" type=\"text/css\">");
						html = html.Replace("<head>", string.Concat("<head>", java));
						if (products > 1)
						{
							StringBuilder replace = new StringBuilder();
							replace.Append("<div class='pagination'><a href='#' class='number' id='ap0' onclick='showPageContent(1); return false;'><<</a><a href='#' id='prev' class='prevnext' onclick='previewPage()'><</a>");
							for (int i = 1; i <= products; i++)
							{
								replace.Append(string.Concat(new object[] { "<a href='#' class='number' id='ap", i, "' onclick='showPageContent(", i, "); return false;'>", i, "</a>" }));
							}
							replace.Append("<a href='#' id='next' class='prevnext' onclick='nextPage()'>></a></div>");
							replace.Append("<script type='text/javascript'>$(document).ready(function () {$('.VATTEMP .invtable .prds').ProductNumberPagination({ number: 10 });});</script></body>");
							html = html.Replace("</body>", replace.ToString());
						}
						this.invBrowser.DocumentText = html;
						this.invBrowser.ScriptErrorsSuppressed = true;
					}
					else
					{
						MessageBox.Show("Lỗi dữ liệu, vui lòng thực hiện lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				else
				{
					MessageBox.Show("Lỗi dữ liệu, vui lòng thực hiện lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			catch (UnauthorizedAccessException unauthorizedAccessException)
			{
				MessageBox.Show("Bạn cần sử dụng với quyền Administartor, vui lòng thực hiện lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Application.Exit();
			}
			catch (Exception exception)
			{
				MessageBox.Show("Có lỗi xảy ra, vui lòng thực hiện lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}
}