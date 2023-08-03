using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using log4net;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Parse.Forms
{
	public class frmProxy : XtraForm
	{
		private readonly ILog log = LogManager.GetLogger(typeof(frmProxy));

		private IContainer components;

		private RadioGroup proxySetting;

		private GroupControl pnlProfile;

		private TextEdit txtProxyPort;

		private TextEdit txtProxyHost;

		private LabelControl labelControl2;

		private LabelControl labelControl1;

		private TextEdit txtProxyPass;

		private TextEdit txtProxyUser;

		private LabelControl labelControl5;

		private LabelControl labelControl4;

		private LabelControl labelControl3;

		private CheckEdit chkAuthen;

		private SimpleButton btnApply;

		private SimpleButton btnCancel;

		public frmProxy()
		{
			this.InitializeComponent();
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			try
			{
				ProxyConfig config = new ProxyConfig();
				string proxyMode = (string)this.proxySetting.EditValue;
				if (proxyMode == "0")
				{
					config.NoneProxy = true;
				}
				else if (proxyMode == "1")
				{
					config.UseIEProxy = true;
				}
				else if (proxyMode == "2")
				{
					config.UseCustomProxy = true;
				}
				config.ProxyAuthen = this.chkAuthen.Checked;
				config.ProxyHost = this.txtProxyHost.Text;
				config.ProxyPort = (!string.IsNullOrEmpty(this.txtProxyPort.Text) ? int.Parse(this.txtProxyPort.Text.Trim()) : 0);
				config.ProxyUser = this.txtProxyUser.Text;
				config.ProxyPass = this.txtProxyPass.Text;
				config.SaveConfig();
				config.SetProxy();
				base.Close();
				XtraMessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch (Exception exception)
			{
				Exception ex = exception;
				this.log.Error(ex);
				XtraMessageBox.Show(string.Concat("Lỗi:", ex.Message), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmProxy_Load(object sender, EventArgs e)
		{
			try
			{
				ProxyConfig config = ProxyConfig.GetConfig();
				if (config != null)
				{
					string editValue = (string)this.proxySetting.EditValue;
					if (config.NoneProxy)
					{
						this.proxySetting.EditValue = "0";
					}
					else if (config.UseIEProxy)
					{
						this.proxySetting.EditValue = "1";
					}
					else if (!config.UseCustomProxy)
					{
						this.proxySetting.EditValue = "2";
					}
					else
					{
						this.proxySetting.EditValue = "2";
					}
					this.chkAuthen.Checked = config.ProxyAuthen;
					this.txtProxyHost.Text = config.ProxyHost;
					this.txtProxyPort.Text = config.ProxyPort.ToString();
					this.txtProxyUser.Text = config.ProxyUser;
					this.txtProxyPass.Text = config.ProxyPass;
				}
			}
			catch (Exception exception)
			{
				this.log.Error(exception);
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(frmProxy));
			this.proxySetting = new RadioGroup();
			this.pnlProfile = new GroupControl();
			this.txtProxyPass = new TextEdit();
			this.txtProxyUser = new TextEdit();
			this.labelControl5 = new LabelControl();
			this.labelControl4 = new LabelControl();
			this.labelControl3 = new LabelControl();
			this.chkAuthen = new CheckEdit();
			this.txtProxyPort = new TextEdit();
			this.txtProxyHost = new TextEdit();
			this.labelControl2 = new LabelControl();
			this.labelControl1 = new LabelControl();
			this.btnApply = new SimpleButton();
			this.btnCancel = new SimpleButton();
			((ISupportInitialize)this.proxySetting.Properties).BeginInit();
			((ISupportInitialize)this.pnlProfile).BeginInit();
			this.pnlProfile.SuspendLayout();
			((ISupportInitialize)this.txtProxyPass.Properties).BeginInit();
			((ISupportInitialize)this.txtProxyUser.Properties).BeginInit();
			((ISupportInitialize)this.chkAuthen.Properties).BeginInit();
			((ISupportInitialize)this.txtProxyPort.Properties).BeginInit();
			((ISupportInitialize)this.txtProxyHost.Properties).BeginInit();
			base.SuspendLayout();
			this.proxySetting.EditValue = "0";
			this.proxySetting.Location = new Point(12, 3);
			this.proxySetting.Name = "proxySetting";
			this.proxySetting.Properties.Appearance.BackColor = Color.Transparent;
			this.proxySetting.Properties.Appearance.Options.UseBackColor = true;
			this.proxySetting.Properties.Columns = 3;
			this.proxySetting.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem("0", "Không sử dụng"), new RadioGroupItem("1", "Hệ thống"), new RadioGroupItem("2", "Cấu hình proxy") });
			this.proxySetting.Size = new System.Drawing.Size(361, 35);
			this.proxySetting.TabIndex = 1;
			this.proxySetting.EditValueChanged += new EventHandler(this.proxySetting_EditValueChanged);
			this.pnlProfile.Controls.Add(this.txtProxyPass);
			this.pnlProfile.Controls.Add(this.txtProxyUser);
			this.pnlProfile.Controls.Add(this.labelControl5);
			this.pnlProfile.Controls.Add(this.labelControl4);
			this.pnlProfile.Controls.Add(this.labelControl3);
			this.pnlProfile.Controls.Add(this.chkAuthen);
			this.pnlProfile.Controls.Add(this.txtProxyPort);
			this.pnlProfile.Controls.Add(this.txtProxyHost);
			this.pnlProfile.Controls.Add(this.labelControl2);
			this.pnlProfile.Controls.Add(this.labelControl1);
			this.pnlProfile.Enabled = false;
			this.pnlProfile.Location = new Point(12, 55);
			this.pnlProfile.Name = "pnlProfile";
			this.pnlProfile.Size = new System.Drawing.Size(361, 164);
			this.pnlProfile.TabIndex = 2;
			this.pnlProfile.Text = "Cấu hình proxy server";
			this.txtProxyPass.Location = new Point(89, 126);
			this.txtProxyPass.Name = "txtProxyPass";
			this.txtProxyPass.Properties.PasswordChar = '*';
			this.txtProxyPass.Properties.UseSystemPasswordChar = true;
			this.txtProxyPass.Size = new System.Drawing.Size(251, 20);
			this.txtProxyPass.TabIndex = 9;
			this.txtProxyUser.Location = new Point(89, 91);
			this.txtProxyUser.Name = "txtProxyUser";
			this.txtProxyUser.Size = new System.Drawing.Size(251, 20);
			this.txtProxyUser.TabIndex = 8;
			this.labelControl5.Location = new Point(38, 129);
			this.labelControl5.Name = "labelControl5";
			this.labelControl5.Size = new System.Drawing.Size(44, 13);
			this.labelControl5.TabIndex = 7;
			this.labelControl5.Text = "Mật khẩu";
			this.labelControl4.Location = new Point(10, 93);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(72, 13);
			this.labelControl4.TabIndex = 6;
			this.labelControl4.Text = "Tên đăng nhập";
			this.labelControl3.Location = new Point(112, 72);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(91, 13);
			this.labelControl3.TabIndex = 5;
			this.labelControl3.Text = "Cấu hình Tài khoản";
			this.chkAuthen.Location = new Point(89, 68);
			this.chkAuthen.Name = "chkAuthen";
			this.chkAuthen.Properties.Caption = "";
			this.chkAuthen.Size = new System.Drawing.Size(17, 19);
			this.chkAuthen.TabIndex = 4;
			this.txtProxyPort.Location = new Point(274, 32);
			this.txtProxyPort.Name = "txtProxyPort";
			this.txtProxyPort.Properties.DisplayFormat.FormatString = "d";
			this.txtProxyPort.Properties.DisplayFormat.FormatType = FormatType.Numeric;
			this.txtProxyPort.Properties.EditFormat.FormatString = "d";
			this.txtProxyPort.Properties.EditFormat.FormatType = FormatType.Numeric;
			this.txtProxyPort.Properties.Mask.EditMask = "d";
			this.txtProxyPort.Properties.Mask.MaskType = MaskType.Numeric;
			this.txtProxyPort.Properties.MaxLength = 4;
			this.txtProxyPort.Size = new System.Drawing.Size(66, 20);
			this.txtProxyPort.TabIndex = 3;
			this.txtProxyHost.Location = new Point(88, 32);
			this.txtProxyHost.Name = "txtProxyHost";
			this.txtProxyHost.Size = new System.Drawing.Size(144, 20);
			this.txtProxyHost.TabIndex = 2;
			this.labelControl2.Location = new Point(243, 35);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(25, 13);
			this.labelControl2.TabIndex = 1;
			this.labelControl2.Text = "Cổng";
			this.labelControl1.Location = new Point(50, 35);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(32, 13);
			this.labelControl1.TabIndex = 0;
			this.labelControl1.Text = "Địa chỉ";
			this.btnApply.Cursor = Cursors.Hand;
			this.btnApply.ImageOptions.Image = (Image)resources.GetObject("btnApply.ImageOptions.Image");
			this.btnApply.ImageOptions.Location = ImageLocation.MiddleLeft;
			this.btnApply.Location = new Point(115, 235);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 14;
			this.btnApply.Text = "Áp dụng";
			this.btnApply.Click += new EventHandler(this.btnApply_Click);
			this.btnCancel.Cursor = Cursors.Hand;
			this.btnCancel.ImageOptions.Image = (Image)resources.GetObject("btnCancel.ImageOptions.Image");
			this.btnCancel.ImageOptions.Location = ImageLocation.MiddleLeft;
			this.btnCancel.Location = new Point(202, 235);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 15;
			this.btnCancel.Text = "Thoát";
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			base.ClientSize = new System.Drawing.Size(385, 271);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnApply);
			base.Controls.Add(this.pnlProfile);
			base.Controls.Add(this.proxySetting);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmProxy";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Cấu hình Proxy";
			base.Load += new EventHandler(this.frmProxy_Load);
			((ISupportInitialize)this.proxySetting.Properties).EndInit();
			((ISupportInitialize)this.pnlProfile).EndInit();
			this.pnlProfile.ResumeLayout(false);
			this.pnlProfile.PerformLayout();
			((ISupportInitialize)this.txtProxyPass.Properties).EndInit();
			((ISupportInitialize)this.txtProxyUser.Properties).EndInit();
			((ISupportInitialize)this.chkAuthen.Properties).EndInit();
			((ISupportInitialize)this.txtProxyPort.Properties).EndInit();
			((ISupportInitialize)this.txtProxyHost.Properties).EndInit();
			base.ResumeLayout(false);
		}

		private void proxySetting_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				this.pnlProfile.Enabled = (string)this.proxySetting.EditValue == "2";
			}
			catch (Exception exception)
			{
				this.log.Error(exception);
			}
		}
	}
}