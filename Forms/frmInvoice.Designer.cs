using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using FX.Core;
using FX.Data;
using log4net;
using Parse.Core;
using Parse.Core.Domain;
using Parse.Core.IService;
using Parse.Forms.CustomUC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ViettelAPI;
using ViettelAPI.Models;

namespace Parse.Forms
{
	public partial class frmInvoice : XtraForm
	{
		public InvoiceVAT Invoice = new InvoiceVAT();

		private BindingSource Bindingsource = new BindingSource();

		private readonly ILog log = LogManager.GetLogger(typeof(frmInvoice));

		private IMainForm Main;

		private IContainer components;

		private GroupControl groupControl1;

		private LabelControl labelControl5;

		private LabelControl labelControl4;

		private LabelControl labelControl3;

		private LabelControl labelControl2;

		private TextEdit txtCusTaxCode;

		private TextEdit txtCusName;

		private TextEdit txtBuyer;

		private LabelControl labelControl9;

		private TextEdit txtFolioOrigin;

        private TextEdit txtInvoiceNoSAP;

        private MemoEdit txtCusAddress;

		private GroupControl groupControl2;

		private GridControl gridProduct;

		private GridView viewProduct;

		private SimpleButton btnClose;

		private SimpleButton btnUpdate;

		private GridColumn colName;

		private GridColumn colQuantity;

		private GridColumn colPrice;

		private GridColumn ColAmount;

		private RepositoryItemTextEdit txtQuantity;

		private RepositoryItemTextEdit txtPrice;

		private RepositoryItemTextEdit txtAmount;

		private LabelControl labelControl1;

		private LabelControl labelControl11;

		private LabelControl labelControl14;

		private LabelControl labelControl15;

		private TextEdit txtTotal;

		private TextEdit txtVATAmount;

		private TextEdit txtAmountInvoice;

		private TextEdit txtAmountInWord;

		private ControlNavigator controlNavigator1;

		private RepositoryItemSpinEdit spinQuantity;

		private SimpleButton btnRecal;

		private GridColumn colDelete;

		private RepositoryItemButtonEdit btnDeleteRow;

		private RepositoryItemLookUpEdit cboType;

		private SimpleButton btnReleased;

		private Label label1;

		private TextEdit txtCusEmail;

		private GridColumn colCode;

		private TextBox txtCusCode;

		private Label label2;

		private GridColumn colNote;

		private Label label6;

		private Label label5;

		private Label label4;

		private Label label3;

		private TextBox txtCusPhone;

		private Label label7;

		private TextBox txtCusBankName;

		private TextBox txtCusBankNo;

		private TextBox txtStaffId;

		private TextBox txtDeliveryId;

		private TextBox txtCusComName;

		private Label label8;

		private GridColumn colType;

		private Label label9;

		private GridColumn ColDiscount;

		private Label label10;

		private TextEdit txtDiscountAmount;

		private LookUpEdit cbVATRate;

		
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInvoice));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtCusComName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtStaffId = new System.Windows.Forms.TextBox();
            this.txtDeliveryId = new System.Windows.Forms.TextBox();
            this.txtCusBankName = new System.Windows.Forms.TextBox();
            this.txtCusBankNo = new System.Windows.Forms.TextBox();
            this.txtCusPhone = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCusCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCusAddress = new DevExpress.XtraEditors.MemoEdit();
            //this.txtFolioOrigin = new DevExpress.XtraEditors.TextEdit();
            this.txtInvoiceNoSAP = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtCusEmail = new DevExpress.XtraEditors.TextEdit();
            this.txtCusTaxCode = new DevExpress.XtraEditors.TextEdit();
            this.txtCusName = new DevExpress.XtraEditors.TextEdit();
            this.txtBuyer = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.controlNavigator1 = new DevExpress.XtraEditors.ControlNavigator();
            this.gridProduct = new DevExpress.XtraGrid.GridControl();
            this.viewProduct = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cboType = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtQuantity = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtPrice = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ColAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ColDiscount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDeleteRow = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.spinQuantity = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.txtTotal = new DevExpress.XtraEditors.TextEdit();
            this.txtVATAmount = new DevExpress.XtraEditors.TextEdit();
            this.txtAmountInvoice = new DevExpress.XtraEditors.TextEdit();
            this.txtAmountInWord = new DevExpress.XtraEditors.TextEdit();
            this.btnRecal = new DevExpress.XtraEditors.SimpleButton();
            this.btnReleased = new DevExpress.XtraEditors.SimpleButton();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDiscountAmount = new DevExpress.XtraEditors.TextEdit();
            this.cbVATRate = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCusAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceNoSAP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCusEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCusTaxCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCusName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuyer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVATAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmountInvoice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmountInWord.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscountAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbVATRate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtCusComName);
            this.groupControl1.Controls.Add(this.label8);
            this.groupControl1.Controls.Add(this.txtStaffId);
            this.groupControl1.Controls.Add(this.txtDeliveryId);
            this.groupControl1.Controls.Add(this.txtCusBankName);
            this.groupControl1.Controls.Add(this.txtCusBankNo);
            this.groupControl1.Controls.Add(this.txtCusPhone);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.label6);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.txtCusCode);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.txtCusAddress);
            this.groupControl1.Controls.Add(this.txtInvoiceNoSAP);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.txtCusEmail);
            this.groupControl1.Controls.Add(this.txtCusTaxCode);
            this.groupControl1.Controls.Add(this.txtCusName);
            this.groupControl1.Controls.Add(this.txtBuyer);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(915, 304);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Thông tin khách hàng";
            // 
            // txtCusComName
            // 
            this.txtCusComName.Location = new System.Drawing.Point(141, 70);
            this.txtCusComName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCusComName.Name = "txtCusComName";
            this.txtCusComName.Size = new System.Drawing.Size(405, 23);
            this.txtCusComName.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 17);
            this.label8.TabIndex = 30;
            this.label8.Text = "Tên trên hóa đơn";
            // 
            // txtStaffId
            // 
            this.txtStaffId.Location = new System.Drawing.Point(141, 266);
            this.txtStaffId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStaffId.Name = "txtStaffId";
            this.txtStaffId.Size = new System.Drawing.Size(405, 23);
            this.txtStaffId.TabIndex = 29;
            // 
            // txtDeliveryId
            // 
            this.txtDeliveryId.Location = new System.Drawing.Point(141, 233);
            this.txtDeliveryId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDeliveryId.Name = "txtDeliveryId";
            this.txtDeliveryId.Size = new System.Drawing.Size(405, 23);
            this.txtDeliveryId.TabIndex = 28;
            // 
            // txtCusBankName
            // 
            this.txtCusBankName.Location = new System.Drawing.Point(141, 196);
            this.txtCusBankName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCusBankName.Name = "txtCusBankName";
            this.txtCusBankName.Size = new System.Drawing.Size(405, 23);
            this.txtCusBankName.TabIndex = 27;
            // 
            // txtCusBankNo
            // 
            this.txtCusBankNo.Location = new System.Drawing.Point(692, 199);
            this.txtCusBankNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCusBankNo.Name = "txtCusBankNo";
            this.txtCusBankNo.Size = new System.Drawing.Size(210, 23);
            this.txtCusBankNo.TabIndex = 26;
            // 
            // txtCusPhone
            // 
            this.txtCusPhone.Location = new System.Drawing.Point(692, 70);
            this.txtCusPhone.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCusPhone.Name = "txtCusPhone";
            this.txtCusPhone.Size = new System.Drawing.Size(210, 23);
            this.txtCusPhone.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 236);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 17);
            this.label7.TabIndex = 24;
            this.label7.Text = "Nhân viên giao hàng";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 270);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 17);
            this.label6.TabIndex = 23;
            this.label6.Text = "Nhân viên bán hàng";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(608, 203);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 17);
            this.label5.TabIndex = 22;
            this.label5.Text = "Số tài khoản";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 17);
            this.label4.TabIndex = 21;
            this.label4.Text = "Ngân hàng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(608, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 20;
            this.label3.Text = "Số điện thoại";
            // 
            // txtCusCode
            // 
            this.txtCusCode.Location = new System.Drawing.Point(692, 34);
            this.txtCusCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCusCode.Name = "txtCusCode";
            this.txtCusCode.Size = new System.Drawing.Size(210, 23);
            this.txtCusCode.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(593, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 17);
            this.label2.TabIndex = 18;
            this.label2.Text = "Mã khách hàng";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(649, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "Email";
            // 
            // txtCusAddress
            // 
            this.txtCusAddress.Location = new System.Drawing.Point(141, 135);
            this.txtCusAddress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCusAddress.Name = "txtCusAddress";
            this.txtCusAddress.Size = new System.Drawing.Size(406, 53);
            this.txtCusAddress.TabIndex = 3;
            // 
            // txtInvoiceNoSAP
            // 
            this.txtInvoiceNoSAP.Location = new System.Drawing.Point(693, 167);
            this.txtInvoiceNoSAP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtInvoiceNoSAP.Name = "txtInvoiceNoSAP";
            this.txtInvoiceNoSAP.Size = new System.Drawing.Size(210, 22);
            this.txtInvoiceNoSAP.TabIndex = 5;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(586, 171);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(113, 17);
            this.labelControl9.TabIndex = 9;
            this.labelControl9.Text = "Số hóa đơn nội bộ";
            // 
            // txtCusEmail
            // 
            this.txtCusEmail.Location = new System.Drawing.Point(693, 135);
            this.txtCusEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCusEmail.Name = "txtCusEmail";
            this.txtCusEmail.Size = new System.Drawing.Size(210, 22);
            this.txtCusEmail.TabIndex = 4;
            // 
            // txtCusTaxCode
            // 
            this.txtCusTaxCode.Location = new System.Drawing.Point(692, 103);
            this.txtCusTaxCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCusTaxCode.Name = "txtCusTaxCode";
            this.txtCusTaxCode.Size = new System.Drawing.Size(211, 22);
            this.txtCusTaxCode.TabIndex = 4;
            // 
            // txtCusName
            // 
            this.txtCusName.Location = new System.Drawing.Point(141, 103);
            this.txtCusName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCusName.Name = "txtCusName";
            this.txtCusName.Size = new System.Drawing.Size(406, 22);
            this.txtCusName.TabIndex = 2;
            // 
            // txtBuyer
            // 
            this.txtBuyer.Location = new System.Drawing.Point(141, 36);
            this.txtBuyer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBuyer.Name = "txtBuyer";
            this.txtBuyer.Size = new System.Drawing.Size(406, 22);
            this.txtBuyer.TabIndex = 1;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(623, 107);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(67, 17);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "Mã số thuế";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(97, 138);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(40, 17);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Địa chỉ";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(76, 74);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(65, 17);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Tên đơn vị";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(43, 38);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(102, 17);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Người mua hàng";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.controlNavigator1);
            this.groupControl2.Controls.Add(this.gridProduct);
            this.groupControl2.Location = new System.Drawing.Point(2, 313);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(915, 293);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Thông tin sản phẩm";
            // 
            // controlNavigator1
            // 
            this.controlNavigator1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.controlNavigator1.Appearance.Options.UseBackColor = true;
            this.controlNavigator1.Buttons.Append.Visible = false;
            this.controlNavigator1.Buttons.CancelEdit.Visible = false;
            this.controlNavigator1.Buttons.Edit.Visible = false;
            this.controlNavigator1.Buttons.EndEdit.Visible = false;
            this.controlNavigator1.Buttons.NextPage.Visible = false;
            this.controlNavigator1.Buttons.PrevPage.Visible = false;
            this.controlNavigator1.Buttons.Remove.Visible = false;
            this.controlNavigator1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.controlNavigator1.Location = new System.Drawing.Point(3, 254);
            this.controlNavigator1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.controlNavigator1.Name = "controlNavigator1";
            this.controlNavigator1.NavigatableControl = this.gridProduct;
            this.controlNavigator1.Size = new System.Drawing.Size(227, 36);
            this.controlNavigator1.TabIndex = 1;
            this.controlNavigator1.Text = "controlNavigator1";
            this.controlNavigator1.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.Center;
            this.controlNavigator1.TextStringFormat = "Record {0} / {1}";
            // 
            // gridProduct
            // 
            this.gridProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProduct.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridProduct.Location = new System.Drawing.Point(2, 25);
            this.gridProduct.MainView = this.viewProduct;
            this.gridProduct.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridProduct.Name = "gridProduct";
            this.gridProduct.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.txtAmount,
            this.txtQuantity,
            this.txtPrice,
            this.spinQuantity,
            this.btnDeleteRow,
            this.cboType});
            this.gridProduct.Size = new System.Drawing.Size(911, 266);
            this.gridProduct.TabIndex = 0;
            this.gridProduct.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewProduct});
            // 
            // viewProduct
            // 
            this.viewProduct.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCode,
            this.colName,
            this.colType,
            this.colQuantity,
            this.colPrice,
            this.ColAmount,
            this.ColDiscount,
            this.colNote,
            this.colDelete});
            this.viewProduct.GridControl = this.gridProduct;
            this.viewProduct.Name = "viewProduct";
            this.viewProduct.NewItemRowText = "Thêm mới sản phẩm";
            this.viewProduct.OptionsFind.FindNullPrompt = "Nhập thông tin cần tìm...";
            this.viewProduct.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.viewProduct.OptionsView.ShowFooter = true;
            this.viewProduct.OptionsView.ShowGroupPanel = false;
            this.viewProduct.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridProduct_CellValueChanged);
            // 
            // colCode
            // 
            this.colCode.Caption = "Mã";
            this.colCode.FieldName = "Code";
            this.colCode.Name = "colCode";
            this.colCode.Visible = true;
            this.colCode.VisibleIndex = 0;
            this.colCode.Width = 42;
            // 
            // colName
            // 
            this.colName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colName.AppearanceCell.Options.UseFont = true;
            this.colName.AppearanceHeader.Options.UseTextOptions = true;
            this.colName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colName.Caption = "Tên hàng";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 300;
            // 
            // colType
            // 
            this.colType.Caption = "Loại hàng";
            this.colType.ColumnEdit = this.cboType;
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 2;
            this.colType.Width = 62;
            // 
            // cboType
            // 
            this.cboType.AutoHeight = false;
            this.cboType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboType.ImmediatePopup = true;
            this.cboType.Name = "cboType";
            this.cboType.NullText = "";
            this.cboType.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.cboType.ShowFooter = false;
            this.cboType.ShowHeader = false;
            this.cboType.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            // 
            // colQuantity
            // 
            this.colQuantity.AppearanceCell.Options.UseTextOptions = true;
            this.colQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuantity.AppearanceHeader.Options.UseTextOptions = true;
            this.colQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuantity.Caption = "Số lượng";
            this.colQuantity.ColumnEdit = this.txtQuantity;
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 3;
            this.colQuantity.Width = 34;
            // 
            // txtQuantity
            // 
            this.txtQuantity.AutoHeight = false;
            this.txtQuantity.DisplayFormat.FormatString = "d";
            this.txtQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtQuantity.EditFormat.FormatString = "d";
            this.txtQuantity.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtQuantity.Mask.EditMask = "d";
            this.txtQuantity.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.NullText = "1";
            this.txtQuantity.NullValuePrompt = "1";
            // 
            // colPrice
            // 
            this.colPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPrice.Caption = "Đơn giá (VNĐ)";
            this.colPrice.ColumnEdit = this.txtPrice;
            this.colPrice.FieldName = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 4;
            this.colPrice.Width = 55;
            // 
            // txtPrice
            // 
            this.txtPrice.AutoHeight = false;
            this.txtPrice.Name = "txtPrice";
            // 
            // ColAmount
            // 
            this.ColAmount.AppearanceCell.Options.UseTextOptions = true;
            this.ColAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.ColAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.ColAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ColAmount.Caption = "Thành tiền (VNĐ)";
            this.ColAmount.ColumnEdit = this.txtAmount;
            this.ColAmount.FieldName = "Total";
            this.ColAmount.Name = "ColAmount";
            this.ColAmount.Visible = true;
            this.ColAmount.VisibleIndex = 5;
            this.ColAmount.Width = 90;
            // 
            // txtAmount
            // 
            this.txtAmount.AutoHeight = false;
            this.txtAmount.Name = "txtAmount";
            // 
            // ColDiscount
            // 
            this.ColDiscount.Caption = "Chiết khấu trước thuế";
            this.ColDiscount.FieldName = "DiscountAmount";
            this.ColDiscount.Name = "ColDiscount";
            this.ColDiscount.Visible = true;
            this.ColDiscount.VisibleIndex = 6;
            this.ColDiscount.Width = 90;
            // 
            // colNote
            // 
            this.colNote.Caption = "Ghi chú";
            this.colNote.FieldName = "Remark";
            this.colNote.Name = "colNote";
            this.colNote.Visible = true;
            this.colNote.VisibleIndex = 7;
            this.colNote.Width = 61;
            // 
            // colDelete
            // 
            this.colDelete.AppearanceCell.Options.UseTextOptions = true;
            this.colDelete.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDelete.AppearanceHeader.Options.UseTextOptions = true;
            this.colDelete.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDelete.Caption = "Xóa";
            this.colDelete.ColumnEdit = this.btnDeleteRow;
            this.colDelete.FieldName = "DeleteRow";
            this.colDelete.Name = "colDelete";
            this.colDelete.OptionsColumn.FixedWidth = true;
            this.colDelete.OptionsColumn.ReadOnly = true;
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 8;
            this.colDelete.Width = 30;
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.AutoHeight = false;
            this.btnDeleteRow.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnDeleteRow.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnDeleteRow_ButtonClick);
            // 
            // spinQuantity
            // 
            this.spinQuantity.AutoHeight = false;
            this.spinQuantity.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinQuantity.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spinQuantity.Name = "spinQuantity";
            this.spinQuantity.NullText = "0";
            this.spinQuantity.NullValuePrompt = "0";
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.ImageOptions.Image")));
            this.btnClose.Location = new System.Drawing.Point(419, 832);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(101, 28);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.ImageOptions.Image")));
            this.btnUpdate.Location = new System.Drawing.Point(303, 832);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(103, 28);
            this.btnUpdate.TabIndex = 14;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 794);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(131, 17);
            this.labelControl1.TabIndex = 20;
            this.labelControl1.Text = "Số tiền viết bằng chữ";
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(66, 759);
            this.labelControl11.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(67, 17);
            this.labelControl11.TabIndex = 19;
            this.labelControl11.Text = "Tổng cộng";
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(40, 625);
            this.labelControl14.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(94, 17);
            this.labelControl14.TabIndex = 16;
            this.labelControl14.Text = "Cộng tiền hàng";
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(63, 689);
            this.labelControl15.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(69, 17);
            this.labelControl15.TabIndex = 21;
            this.labelControl15.Text = "Thuế GTGT";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(132, 622);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTotal.Properties.ReadOnly = true;
            this.txtTotal.Properties.UseReadOnlyAppearance = false;
            this.txtTotal.Size = new System.Drawing.Size(722, 22);
            this.txtTotal.TabIndex = 22;
            // 
            // txtVATAmount
            // 
            this.txtVATAmount.Location = new System.Drawing.Point(132, 686);
            this.txtVATAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtVATAmount.Name = "txtVATAmount";
            this.txtVATAmount.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtVATAmount.Properties.ReadOnly = true;
            this.txtVATAmount.Properties.UseReadOnlyAppearance = false;
            this.txtVATAmount.Size = new System.Drawing.Size(618, 22);
            this.txtVATAmount.TabIndex = 25;
            // 
            // txtAmountInvoice
            // 
            this.txtAmountInvoice.Location = new System.Drawing.Point(132, 756);
            this.txtAmountInvoice.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAmountInvoice.Name = "txtAmountInvoice";
            this.txtAmountInvoice.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtAmountInvoice.Properties.ReadOnly = true;
            this.txtAmountInvoice.Properties.UseReadOnlyAppearance = false;
            this.txtAmountInvoice.Size = new System.Drawing.Size(618, 22);
            this.txtAmountInvoice.TabIndex = 26;
            this.txtAmountInvoice.Leave += new System.EventHandler(this.txtAmountInvoice_Leave);
            // 
            // txtAmountInWord
            // 
            this.txtAmountInWord.Enabled = false;
            this.txtAmountInWord.Location = new System.Drawing.Point(132, 790);
            this.txtAmountInWord.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtAmountInWord.Name = "txtAmountInWord";
            this.txtAmountInWord.Properties.ReadOnly = true;
            this.txtAmountInWord.Properties.UseReadOnlyAppearance = false;
            this.txtAmountInWord.Size = new System.Drawing.Size(722, 22);
            this.txtAmountInWord.TabIndex = 27;
            // 
            // btnRecal
            // 
            this.btnRecal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecal.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRecal.ImageOptions.Image")));
            this.btnRecal.Location = new System.Drawing.Point(769, 754);
            this.btnRecal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRecal.Name = "btnRecal";
            this.btnRecal.Size = new System.Drawing.Size(85, 25);
            this.btnRecal.TabIndex = 14;
            this.btnRecal.Text = "Tính lại";
            this.btnRecal.Click += new System.EventHandler(this.btnRecal_Click);
            // 
            // btnReleased
            // 
            this.btnReleased.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReleased.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnReleased.ImageOptions.Image")));
            this.btnReleased.Location = new System.Drawing.Point(535, 832);
            this.btnReleased.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReleased.Name = "btnReleased";
            this.btnReleased.Size = new System.Drawing.Size(101, 28);
            this.btnReleased.TabIndex = 15;
            this.btnReleased.Text = "Phát hành";
            this.btnReleased.Click += new System.EventHandler(this.btnReleased_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(57, 721);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 17);
            this.label9.TabIndex = 29;
            this.label9.Text = "Chiết khấu";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(61, 657);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 17);
            this.label10.TabIndex = 30;
            this.label10.Text = "Thuế suất";
            // 
            // txtDiscountAmount
            // 
            this.txtDiscountAmount.Location = new System.Drawing.Point(132, 718);
            this.txtDiscountAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDiscountAmount.Name = "txtDiscountAmount";
            this.txtDiscountAmount.Properties.ReadOnly = true;
            this.txtDiscountAmount.Properties.UseReadOnlyAppearance = false;
            this.txtDiscountAmount.Size = new System.Drawing.Size(618, 22);
            this.txtDiscountAmount.TabIndex = 32;
            // 
            // cbVATRate
            // 
            this.cbVATRate.Location = new System.Drawing.Point(132, 654);
            this.cbVATRate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbVATRate.Name = "cbVATRate";
            this.cbVATRate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbVATRate.Properties.NullText = "";
            this.cbVATRate.Properties.ShowFooter = false;
            this.cbVATRate.Properties.ShowHeader = false;
            this.cbVATRate.Properties.ValueMember = "Value";
            this.cbVATRate.Size = new System.Drawing.Size(117, 22);
            this.cbVATRate.TabIndex = 33;
            this.cbVATRate.EditValueChanged += new System.EventHandler(this.cbVATRate_EditValueChanged);
            // 
            // frmInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(919, 874);
            this.Controls.Add(this.cbVATRate);
            this.Controls.Add(this.txtDiscountAmount);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtAmountInWord);
            this.Controls.Add(this.txtAmountInvoice);
            this.Controls.Add(this.txtVATAmount);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.labelControl15);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.labelControl14);
            this.Controls.Add(this.btnReleased);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRecal);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInvoice";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thông tin chi tiết hóa đơn";
            this.Load += new System.EventHandler(this.frmInvoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCusAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceNoSAP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCusEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCusTaxCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCusName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuyer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVATAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmountInvoice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmountInWord.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDiscountAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbVATRate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

	
	}
}