using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using FX.Core;
using FX.Data;
using log4net;
using Parse.Core;
using Parse.Core.Domain;
using Parse.Core.IService;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Parse.Core.Utils;

namespace Parse.Forms
{
    public class frmCompany : XtraForm
    {
        public Company COM = new Company();

        private readonly ILog log = LogManager.GetLogger(typeof(frmCompany));

        private IContainer components;

        private GroupControl groupControl1;

        private SimpleButton btnClose;

        private SimpleButton btnUpdate;

        private LabelControl labelControl1;

        private LabelControl labelControl10;

        private LabelControl labelControl9;

        private LabelControl labelControl8;

        private LabelControl labelControl7;

        private LabelControl labelControl6;

        private LabelControl labelControl5;

        private LabelControl labelControl4;

        private LabelControl labelControl3;

        private LabelControl labelControl2;

        private TextEdit txtBankNumber;

        private TextEdit txtBankName;

        private TextEdit txtName;

        private TextEdit txtTaxCode;

        private TextEdit txtAddress;

        private TextEdit txtPhone;

        private TextEdit txtEmail;

        private TextEdit txtPassWord;

        private TextEdit txtUserName;

        private TextEdit txtInvPattern;

        private LabelControl labelControl11;

        private GroupControl groupControl2;

        private TextEdit txtFax;

        private LabelControl labelControl12;

        private LabelControl labelControl13;

        private CheckEdit chkPubInv;

        private LabelControl labelControl14;

        private TextEdit txtPublishTime;

        private LabelControl labelControl15;

        private TextBox txtInvSerial;

        private Label label1;

        public frmCompany()
        {
            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ICompanyService service = IoC.Resolve<ICompanyService>();
            service.BeginTran();
            try
            {
                /** Get Token */
                string username = this.txtUserName.Text.Trim();
                string password = this.txtPassWord.Text;
                string accessToken = ViettelAPI.APIHelper.GetToken(username, password);
                /** Get Token */

                if (!string.IsNullOrEmpty(accessToken))
                {
                    this.COM = this.InnitData();
                    this.COM.Token = accessToken;
                    this.COM.Token_Update_At = Convert.ToString(NumberUtil.ConvertToUnixTime(DateTime.Now));
                    if (this.COM.Id <= 0)
                    {
                        service.CreateNew(this.COM);
                    }
                    else
                    {
                        service.Update(this.COM);
                    }
                    service.CommitTran();
                    AppContext.InitContext(this.COM);
                    XtraMessageBox.Show("Đăng Nhập Thành Công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception exception)
            {
                Exception ex = exception;
                service.RolbackTran();
                this.log.Error(ex);
                XtraMessageBox.Show("Đăng Nhập Thất Bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

        private void frmCompany_Load(object sender, EventArgs e)
        {
            this.LoadData();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(frmCompany));

            this.txtFax = new TextEdit();
            this.labelControl12 = new LabelControl();
            this.txtAddress = new TextEdit();
            this.txtPhone = new TextEdit();
            this.txtEmail = new TextEdit();
            this.txtTaxCode = new TextEdit();
            this.txtBankNumber = new TextEdit();
            this.txtBankName = new TextEdit();
            this.txtName = new TextEdit();
            this.labelControl7 = new LabelControl();
            this.labelControl6 = new LabelControl();
            this.labelControl5 = new LabelControl();
            this.labelControl4 = new LabelControl();
            this.labelControl3 = new LabelControl();
            this.labelControl2 = new LabelControl();
            this.labelControl1 = new LabelControl();
            this.labelControl11 = new LabelControl();
            this.txtInvPattern = new TextEdit();
            this.txtPassWord = new TextEdit();
            this.txtUserName = new TextEdit();
            this.labelControl10 = new LabelControl();
            this.labelControl9 = new LabelControl();
            this.labelControl8 = new LabelControl();
            this.btnClose = new SimpleButton();
            this.btnUpdate = new SimpleButton();
            this.groupControl2 = new GroupControl();
            this.txtPublishTime = new TextEdit();
            this.labelControl15 = new LabelControl();
            this.chkPubInv = new CheckEdit();
            this.labelControl14 = new LabelControl();
            this.labelControl13 = new LabelControl();
            this.label1 = new Label();
            this.txtInvSerial = new TextBox();

            ((ISupportInitialize)this.txtFax.Properties).BeginInit();
            ((ISupportInitialize)this.txtAddress.Properties).BeginInit();
            ((ISupportInitialize)this.txtPhone.Properties).BeginInit();
            ((ISupportInitialize)this.txtEmail.Properties).BeginInit();
            ((ISupportInitialize)this.txtTaxCode.Properties).BeginInit();
            ((ISupportInitialize)this.txtBankNumber.Properties).BeginInit();
            ((ISupportInitialize)this.txtBankName.Properties).BeginInit();
            ((ISupportInitialize)this.txtName.Properties).BeginInit();
            ((ISupportInitialize)this.txtInvPattern.Properties).BeginInit();
            ((ISupportInitialize)this.txtPassWord.Properties).BeginInit();
            ((ISupportInitialize)this.txtUserName.Properties).BeginInit();
            ((ISupportInitialize)this.groupControl2).BeginInit();
            this.groupControl2.SuspendLayout();
            ((ISupportInitialize)this.txtPublishTime.Properties).BeginInit();
            ((ISupportInitialize)this.chkPubInv.Properties).BeginInit();
            base.SuspendLayout();
            
            this.txtFax.Location = new Point(107, 113);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new Size(242, 20);
            this.txtFax.TabIndex = 6;
            this.labelControl12.Location = new Point(83, 117);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new Size(18, 13);
            this.labelControl12.TabIndex = 9;
            this.labelControl12.Text = "Fax";
            this.txtAddress.Location = new Point(107, 139);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new Size(622, 20);
            this.txtAddress.TabIndex = 8;
            this.txtPhone.Location = new Point(495, 114);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new Size(234, 20);
            this.txtPhone.TabIndex = 7;
            this.txtEmail.Location = new Point(495, 86);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new Size(234, 20);
            this.txtEmail.TabIndex = 5;
            this.txtTaxCode.Location = new Point(107, 87);
            this.txtTaxCode.Name = "txtTaxCode";
            this.txtTaxCode.Size = new Size(242, 20);
            this.txtTaxCode.TabIndex = 4;
            this.txtBankNumber.Location = new Point(495, 59);
            this.txtBankNumber.Name = "txtBankNumber";
            this.txtBankNumber.Size = new Size(234, 20);
            this.txtBankNumber.TabIndex = 3;
            this.txtBankName.Location = new Point(107, 59);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new Size(242, 20);
            this.txtBankName.TabIndex = 2;
            this.txtName.Location = new Point(107, 32);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(622, 20);
            this.txtName.TabIndex = 1;
            this.labelControl7.Location = new Point(69, 142);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new Size(32, 13);
            this.labelControl7.TabIndex = 6;
            this.labelControl7.Text = "Địa chỉ";
            this.labelControl6.Location = new Point(440, 116);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new Size(49, 13);
            this.labelControl6.TabIndex = 5;
            this.labelControl6.Text = "Điện thoại";
            this.labelControl5.Location = new Point(465, 88);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new Size(24, 13);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "Email";
            this.labelControl4.Location = new Point(48, 89);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new Size(53, 13);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Mã số thuế";
            this.labelControl3.Location = new Point(389, 62);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new Size(100, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Tài khoản ngân hàng";
            this.labelControl2.Location = new Point(29, 61);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new Size(72, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Tên ngân hàng";
            this.labelControl1.Location = new Point(51, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new Size(50, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Tên đơn vị";
            this.labelControl11.Location = new Point(65, 34);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new Size(36, 13);
            this.labelControl11.TabIndex = 21;
            this.labelControl11.Text = "Pattern";
            this.txtInvPattern.Location = new Point(107, 31);
            this.txtInvPattern.Name = "txtInvPattern";
            this.txtInvPattern.Size = new Size(242, 20);
            this.txtInvPattern.TabIndex = 12;
            this.txtPassWord.Location = new Point(495, 53);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.Properties.PasswordChar = '*';
            this.txtPassWord.Properties.UseSystemPasswordChar = true;
            this.txtPassWord.Size = new Size(234, 20);
            this.txtPassWord.TabIndex = 10;
            this.txtUserName.Location = new Point(495, 27);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new Size(234, 20);
            this.txtUserName.TabIndex = 9;
            this.labelControl9.Location = new Point(445, 56);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new Size(44, 13);
            this.labelControl9.TabIndex = 8;
            this.labelControl9.Text = "Mật khẩu";
            this.labelControl8.Location = new Point(417, 30);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new Size(72, 13);
            this.labelControl8.TabIndex = 7;
            this.labelControl8.Text = "Tên đăng nhập";
            this.btnClose.Cursor = Cursors.Hand;
            this.btnClose.ImageOptions.Image = (Image)resources.GetObject("btnClose.ImageOptions.Image");
            this.btnClose.Location = new Point(385, 149);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(87, 23);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.btnUpdate.Cursor = Cursors.Hand;
            this.btnUpdate.DialogResult = DialogResult.OK;
            this.btnUpdate.ImageOptions.Image = (Image)resources.GetObject("btnUpdate.ImageOptions.Image");
            this.btnUpdate.Location = new Point(286, 149);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new Size(88, 23);
            this.btnUpdate.TabIndex = 12;
            this.btnUpdate.Text = "Đăng Nhập";
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
            this.groupControl2.Controls.Add(this.txtInvSerial);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Controls.Add(this.txtPublishTime);
            this.groupControl2.Controls.Add(this.labelControl15);
            this.groupControl2.Controls.Add(this.chkPubInv);
            this.groupControl2.Controls.Add(this.labelControl14);
            this.groupControl2.Controls.Add(this.labelControl11);
            this.groupControl2.Controls.Add(this.txtPassWord);
            this.groupControl2.Controls.Add(this.txtInvPattern);
            this.groupControl2.Controls.Add(this.labelControl8);
            this.groupControl2.Controls.Add(this.labelControl9);
            this.groupControl2.Controls.Add(this.labelControl10);
            this.groupControl2.Controls.Add(this.txtUserName);
            this.groupControl2.Location = new Point(8, 5);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new Size(749, 110);
            this.groupControl2.TabIndex = 14;
            this.groupControl2.Text = "Thông tin cấu hình";
            this.txtPublishTime.Location = new Point(495, 81);
            this.txtPublishTime.Name = "txtPublishTime";
            this.txtPublishTime.Size = new Size(103, 20);
            this.txtPublishTime.TabIndex = 25;
            this.labelControl15.Location = new Point(415, 84);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new Size(74, 13);
            this.labelControl15.TabIndex = 24;
            this.labelControl15.Text = "Thời gian (giây)";
            this.chkPubInv.Location = new Point(713, 81);
            this.chkPubInv.Name = "chkPubInv";
            this.chkPubInv.Properties.Caption = "";
            this.chkPubInv.Size = new Size(16, 19);
            this.chkPubInv.TabIndex = 23;
            this.labelControl14.Location = new Point(617, 84);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new Size(90, 13);
            this.labelControl14.TabIndex = 22;
            this.labelControl14.Text = "Phát hành tự động";
            this.labelControl13.Appearance.Font = new Font("Tahoma", 8.25f, FontStyle.Italic);
            this.labelControl13.Appearance.ForeColor = Color.Red;
            this.labelControl13.Appearance.Options.UseFont = true;
            this.labelControl13.Appearance.Options.UseForeColor = true;
            this.labelControl13.Location = new Point(261, 124);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new Size(247, 13);
            this.labelControl13.TabIndex = 68;
            this.labelControl13.Text = "Khởi động lại chương trình để áp dụng cấu hình mới.";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(65, 60);
            this.label1.Name = "label1";
            this.label1.Size = new Size(33, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Serial";
            this.txtInvSerial.Location = new Point(107, 57);
            this.txtInvSerial.Name = "txtInvSerial";
            this.txtInvSerial.Size = new Size(242, 21);
            this.txtInvSerial.TabIndex = 27;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(764, 178);
            base.Controls.Add(this.labelControl13);
            base.Controls.Add(this.groupControl2);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnUpdate);

            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmCompany";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Thông tin đơn vị";
            base.Load += new EventHandler(this.frmCompany_Load);

            ((ISupportInitialize)this.txtFax.Properties).EndInit();
            ((ISupportInitialize)this.txtAddress.Properties).EndInit();
            ((ISupportInitialize)this.txtPhone.Properties).EndInit();
            ((ISupportInitialize)this.txtEmail.Properties).EndInit();
            ((ISupportInitialize)this.txtTaxCode.Properties).EndInit();
            ((ISupportInitialize)this.txtBankNumber.Properties).EndInit();
            ((ISupportInitialize)this.txtBankName.Properties).EndInit();
            ((ISupportInitialize)this.txtName.Properties).EndInit();
            ((ISupportInitialize)this.txtInvPattern.Properties).EndInit();
            ((ISupportInitialize)this.txtPassWord.Properties).EndInit();
            ((ISupportInitialize)this.txtUserName.Properties).EndInit();
            ((ISupportInitialize)this.groupControl2).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((ISupportInitialize)this.txtPublishTime.Properties).EndInit();
            ((ISupportInitialize)this.chkPubInv.Properties).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public Company InnitData()
        {
            string lower;
            if (this.COM == null)
            {
                this.COM = new Company();
            }
            this.COM.InvPattern = this.txtInvPattern.Text;
            this.COM.InvSerial = this.txtInvSerial.Text;
            this.COM.Name = this.txtName.Text;
            this.COM.BankName = this.txtBankName.Text;
            this.COM.BankNumber = this.txtBankNumber.Text;
            this.COM.Email = this.txtEmail.Text;
            this.COM.TaxCode = this.txtTaxCode.Text;
            this.COM.Phone = this.txtPhone.Text;
            this.COM.Address = this.txtAddress.Text;
            this.COM.Fax = this.txtFax.Text;
            this.COM.UserName = this.txtUserName.Text.Trim();
            this.COM.PassWord = this.txtPassWord.Text;
            Company cOM = this.COM;
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["COM_CODE"]))
            {
                lower = ConfigurationManager.AppSettings["COM_CODE"].ToLower();
            }
            else
            {
                lower = null;
            }
            cOM.Code = lower;
            this.COM.Config["PUBLISH_TASK_AUTO_START"] = (this.chkPubInv.Checked ? "1" : "0");
            this.COM.Config["PUBLISH_TASK_DURATION"] = this.txtPublishTime.Text;
            return this.COM;
        }

        public void LoadData()
        {
            bool flag;
            this.COM = AppContext.Current.company;
            if (this.COM != null)
            {
                this.txtName.Text = this.COM.Name;
                this.txtBankName.Text = this.COM.BankName;
                this.txtBankNumber.Text = this.COM.BankNumber;
                this.txtEmail.Text = this.COM.Email;
                this.txtTaxCode.Text = this.COM.TaxCode;
                this.txtPhone.Text = this.COM.Phone;
                this.txtAddress.Text = this.COM.Address;
                this.txtFax.Text = this.COM.Fax;
                this.txtUserName.Text = this.COM.UserName;
                this.txtPassWord.Text = this.COM.PassWord;
                this.txtInvPattern.Text = this.COM.InvPattern;
                this.txtInvSerial.Text = this.COM.InvSerial;
                CheckEdit checkEdit = this.chkPubInv;
                if (this.COM.Config.ContainsKey("PUBLISH_TASK_AUTO_START"))
                {
                    flag = (Convert.ToInt32(this.COM.Config["PUBLISH_TASK_AUTO_START"]) == 1 ? true : false);
                }
                else
                {
                    flag = false;
                }
                checkEdit.Checked = flag;
                this.txtPublishTime.Text = (this.COM.Config.ContainsKey("PUBLISH_TASK_DURATION") ? this.COM.Config["PUBLISH_TASK_DURATION"].ToString() : "");
            }
        }
    }
}