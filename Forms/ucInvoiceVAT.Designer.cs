using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
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
using System.Threading;
using System.Windows.Forms;
using ViettelAPI;
using ViettelAPI.Models;

namespace Parse.Forms
{
	partial class ucInvoiceVAT
	{
		private readonly ILog log = LogManager.GetLogger(typeof(ucInvoiceVAT));

		private IMainForm Main;

		private bool isFind;

		private string strFind = "";

		private DateTime? tuNgay;

		private DateTime? denNgay;

		private IContainer components;

		private PanelControl panelControl1;

		private LabelControl labelControl1;

		private GroupControl groupControl1;

		private GridControl gridInvoiceVAT;

		private GridView viewInvoiceVAT;

		private GridColumn colId;

		private GridColumn colArisingDate;

		private GridColumn colCusCode;

		private GridColumn colCusComName;

		private GridColumn colBuyer;

		private GridColumn colCusAddress;

		private GridColumn colCusPhone;

		private GridColumn colCusTaxCode;

		private GridColumn colPaymentMethod;

		private GridColumn colFolioNo;

		private GridColumn colTotal;

		private GridColumn colVATRate;

		private GridColumn colVATAmount;

		private GridColumn colAmount;

		private GridColumn colAmountInWords;

		private GridColumn colViewInv;

		private UCPaging ucPaging;

		private SimpleButton btnPublicInvoice;

		private GridColumn colEditInv;

		private GridColumn colAppName;

		private GridColumn colPublish;

		private GridColumn colFkey;

		private GridColumn colPattern;

		private GridColumn colDeleteInv;

		private SimpleButton cmdTaoLap;

		private SimpleButton btnRefresh;

		private GridColumn colMessageError;

		//private SimpleButton btnPublicAll;

		private RepositoryItemButtonEdit btnViewInv;

		private RepositoryItemButtonEdit btnEditInv;

		private RepositoryItemDateEdit dtArisingDate;

		private RepositoryItemTextEdit txtTotal;

		private RepositoryItemTextEdit txtVATAmount;

		private RepositoryItemTextEdit txtAmount;

		private RepositoryItemCheckEdit chkPublish;

		private RepositoryItemButtonEdit btnDeleteInv;

		private SimpleButton btnDeleteAll;

		private SimpleButton btnDeleteSelected;

		private LabelControl lblInvoicesNumber;

		private LabelControl labelControl2;

		private TextEdit txtFind;

		private SimpleButton btnFind;

		private SimpleButton btnClear;

		private LabelControl labelControl4;

		private LabelControl labelControl3;

		private DateEdit dtDenNgay;

		private DateEdit dtTuNgay;

		public ucInvoiceVAT(IMainForm main)
		{
			this.InitializeComponent();
			this.Main = main;
			this.ucPaging.PageSize = 35;
            this.LoadData(this.ucPaging.PageIndex);
        }

	

		private void InitializeComponent()
		{
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucInvoiceVAT));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.btnViewInv = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnEditInv = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.dtArisingDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.txtTotal = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.txtVATAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.txtAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.chkPublish = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.btnDeleteInv = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            //this.ucPaging = new UCPaging();
            this.dtDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.dtTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnFind = new DevExpress.XtraEditors.SimpleButton();
            this.txtFind = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblInvoicesNumber = new DevExpress.XtraEditors.LabelControl();
            this.btnDeleteSelected = new DevExpress.XtraEditors.SimpleButton();
            //this.btnPublicAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.cmdTaoLap = new DevExpress.XtraEditors.SimpleButton();
            this.btnPublicInvoice = new DevExpress.XtraEditors.SimpleButton();
            this.ucPaging = new UCPaging();
            this.gridInvoiceVAT = new DevExpress.XtraGrid.GridControl();
            this.viewInvoiceVAT = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colViewInv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEditInv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMessageError = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colArisingDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCusCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCusComName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCusAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCusPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCusTaxCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPaymentMethod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFolioNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVATRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVATAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmountInWords = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAppName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPublish = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFkey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPattern = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeleteInv = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewInv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditInv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtArisingDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtArisingDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVATAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPublish)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteInv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoiceVAT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewInvoiceVAT)).BeginInit();
            this.SuspendLayout();
            // 
            // btnViewInv
            // 
            this.btnViewInv.AutoHeight = false;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.btnViewInv.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnViewInv.Name = "btnViewInv";
            this.btnViewInv.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // btnEditInv
            // 
            this.btnEditInv.AutoHeight = false;
            editorButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions2.Image")));
            this.btnEditInv.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btnEditInv.Name = "btnEditInv";
            this.btnEditInv.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // dtArisingDate
            // 
            this.dtArisingDate.AutoHeight = false;
            this.dtArisingDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtArisingDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtArisingDate.DisplayFormat.FormatString = "dd\\/MM\\/yyyy";
            this.dtArisingDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtArisingDate.EditFormat.FormatString = "dd\\/MM\\/yyyy";
            this.dtArisingDate.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtArisingDate.Mask.EditMask = "dd\\/MM\\/yyyy";
            this.dtArisingDate.Mask.UseMaskAsDisplayFormat = true;
            this.dtArisingDate.Name = "dtArisingDate";
            this.dtArisingDate.NullDate = "";
            // 
            // txtTotal
            // 
            this.txtTotal.AutoHeight = false;
            this.txtTotal.DisplayFormat.FormatString = "0.0";
            this.txtTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtTotal.EditFormat.FormatString = "0.0";
            this.txtTotal.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtTotal.Mask.EditMask = "0.0";
            this.txtTotal.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtTotal.Name = "txtTotal";
            // 
            // txtVATAmount
            // 
            this.txtVATAmount.AutoHeight = false;
            this.txtVATAmount.DisplayFormat.FormatString = "n0";
            this.txtVATAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtVATAmount.EditFormat.FormatString = "n0";
            this.txtVATAmount.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtVATAmount.Mask.EditMask = "n0";
            this.txtVATAmount.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtVATAmount.Name = "txtVATAmount";
            // 
            // txtAmount
            // 
            this.txtAmount.AutoHeight = false;
            this.txtAmount.DisplayFormat.FormatString = "n0";
            this.txtAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAmount.EditFormat.FormatString = "n0";
            this.txtAmount.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAmount.Mask.EditMask = "n0";
            this.txtAmount.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtAmount.Name = "txtAmount";
            // 
            // chkPublish
            // 
            this.chkPublish.AutoHeight = false;
            this.chkPublish.Name = "chkPublish";
            // 
            // btnDeleteInv
            // 
            this.btnDeleteInv.AutoHeight = false;
            this.btnDeleteInv.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.btnDeleteInv.Name = "btnDeleteInv";
            this.btnDeleteInv.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1233, 37);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(500, 10);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(210, 19);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "HÓA ĐƠN CHỜ PHÁT HÀNH";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.dtDenNgay);
            this.groupControl1.Controls.Add(this.dtTuNgay);
            this.groupControl1.Controls.Add(this.btnClear);
            this.groupControl1.Controls.Add(this.btnFind);
            this.groupControl1.Controls.Add(this.txtFind);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.lblInvoicesNumber);
            this.groupControl1.Controls.Add(this.btnDeleteSelected);
            //this.groupControl1.Controls.Add(this.btnPublicAll);
            this.groupControl1.Controls.Add(this.btnDeleteAll);
            this.groupControl1.Controls.Add(this.btnRefresh);
            this.groupControl1.Controls.Add(this.cmdTaoLap);
            this.groupControl1.Controls.Add(this.btnPublicInvoice);
            this.groupControl1.Controls.Add(this.gridInvoiceVAT);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 37);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1233, 703);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Chi tiết";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(179, 68);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(30, 17);
            this.labelControl4.TabIndex = 31;
            this.labelControl4.Text = "Đến:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(7, 68);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(22, 17);
            this.labelControl3.TabIndex = 30;
            this.labelControl3.Text = "Từ:";
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.EditValue = null;
            this.dtDenNgay.Location = new System.Drawing.Point(219, 64);
            this.dtDenNgay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDenNgay.Size = new System.Drawing.Size(133, 22);
            this.dtDenNgay.TabIndex = 29;
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.EditValue = null;
            this.dtTuNgay.Location = new System.Drawing.Point(37, 64);
            this.dtTuNgay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTuNgay.Properties.NullValuePromptShowForEmptyValue = true;
            this.dtTuNgay.Size = new System.Drawing.Size(133, 22);
            this.dtTuNgay.TabIndex = 28;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(725, 62);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 28);
            this.btnClear.TabIndex = 27;
            this.btnClear.Text = "Hủy";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(617, 62);
            this.btnFind.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(100, 28);
            this.btnFind.TabIndex = 26;
            this.btnFind.Text = "Tìm kiếm";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtFind
            // 
            this.txtFind.Location = new System.Drawing.Point(360, 64);
            this.txtFind.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(249, 22);
            this.txtFind.TabIndex = 25;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(1027, 68);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(128, 17);
            this.labelControl2.TabIndex = 24;
            this.labelControl2.Text = "Số lượng hóa đơn:";
            // 
            // lblInvoicesNumber
            // 
            this.lblInvoicesNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInvoicesNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoicesNumber.Appearance.Options.UseFont = true;
            this.lblInvoicesNumber.Location = new System.Drawing.Point(1168, 68);
            this.lblInvoicesNumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblInvoicesNumber.Name = "lblInvoicesNumber";
            this.lblInvoicesNumber.Size = new System.Drawing.Size(56, 17);
            this.lblInvoicesNumber.TabIndex = 23;
            this.lblInvoicesNumber.Text = "Number";
            // 
            // btnDeleteSelected
            // 
            this.btnDeleteSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteSelected.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteSelected.ImageOptions.Image")));
            this.btnDeleteSelected.Location = new System.Drawing.Point(579, 28);
            this.btnDeleteSelected.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDeleteSelected.Name = "btnDeleteSelected";
            this.btnDeleteSelected.Size = new System.Drawing.Size(183, 28);
            this.btnDeleteSelected.TabIndex = 22;
            this.btnDeleteSelected.Text = "Xóa hóa đơn đã chọn";
            this.btnDeleteSelected.Click += new System.EventHandler(this.btnDeleteSelected_Click);
            // 
            // btnPublicAll
            // 
            //this.btnPublicAll.Cursor = System.Windows.Forms.Cursors.Hand;
            //this.btnPublicAll.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPublicAll.ImageOptions.Image")));
            //this.btnPublicAll.Location = new System.Drawing.Point(5, 28);
            //this.btnPublicAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            //this.btnPublicAll.Name = "btnPublicAll";
            //this.btnPublicAll.Size = new System.Drawing.Size(208, 28);
            //this.btnPublicAll.TabIndex = 21;
            //this.btnPublicAll.Text = "Phát hành tất cả hóa đơn";
            //this.btnPublicAll.Click += new System.EventHandler(this.btnPublicAll_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteAll.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteAll.ImageOptions.Image")));
            this.btnDeleteAll.Location = new System.Drawing.Point(769, 28);
            this.btnDeleteAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(192, 28);
            this.btnDeleteAll.TabIndex = 20;
            this.btnDeleteAll.Text = "Xóa tất cả hóa đơn";
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.ImageOptions.Image")));
            this.btnRefresh.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(969, 28);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(108, 28);
            this.btnRefresh.TabIndex = 19;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cmdTaoLap
            // 
            this.cmdTaoLap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTaoLap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdTaoLap.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("cmdTaoLap.ImageOptions.Image")));
            this.cmdTaoLap.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.cmdTaoLap.Location = new System.Drawing.Point(1085, 28);
            this.cmdTaoLap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdTaoLap.Name = "cmdTaoLap";
            this.cmdTaoLap.Size = new System.Drawing.Size(141, 28);
            this.cmdTaoLap.TabIndex = 18;
            this.cmdTaoLap.Text = "Tạo lập hóa đơn";
            this.cmdTaoLap.Click += new System.EventHandler(this.cmdTaoLap_Click);
            // 
            // btnPublicInvoice
            // 
            this.btnPublicInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPublicInvoice.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPublicInvoice.ImageOptions.Image")));
            this.btnPublicInvoice.Location = new System.Drawing.Point(5, 28);
            this.btnPublicInvoice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPublicInvoice.Name = "btnPublicInvoice";
            this.btnPublicInvoice.Size = new System.Drawing.Size(220, 28);
            this.btnPublicInvoice.TabIndex = 17;
            this.btnPublicInvoice.Text = "Phát hành hóa đơn đã chọn";
            this.btnPublicInvoice.Click += new System.EventHandler(this.btnPublicInvoice_Click);
            // 
            // gridInvoiceVAT
            // 
            this.gridInvoiceVAT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridInvoiceVAT.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridInvoiceVAT.Location = new System.Drawing.Point(3, 96);
            this.gridInvoiceVAT.MainView = this.viewInvoiceVAT;
            this.gridInvoiceVAT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gridInvoiceVAT.Name = "gridInvoiceVAT";
            this.gridInvoiceVAT.Size = new System.Drawing.Size(1228, 604);
            this.gridInvoiceVAT.TabIndex = 0;
            this.gridInvoiceVAT.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewInvoiceVAT});
            // 
            // Paging Invoice
            // 
            this.ucPaging.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.ucPaging.BackColor = Color.FromArgb(235, 236, 239);
            this.ucPaging.Location = new Point(2, 707);
            this.ucPaging.Name = "ucPaging";
            this.ucPaging.PageIndex = 1;
            this.ucPaging.PageSize = 30;
            this.ucPaging.Size = new System.Drawing.Size(200, 28);
            this.ucPaging.TabIndex = 3;
            this.ucPaging.Total = null;
            this.ucPaging.NextClick += new PagingEventHandler(this.UCPaging_Click);
            this.ucPaging.PrevClick += new PagingEventHandler(this.UCPaging_Click);
            this.ucPaging.FirstClick += new PagingEventHandler(this.UCPaging_Click);
            this.ucPaging.LastClick += new PagingEventHandler(this.UCPaging_Click);
            // 
            // viewInvoiceVAT
            // 
            this.viewInvoiceVAT.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colViewInv,
            this.colEditInv,
            this.colId,
            this.colMessageError,
            this.colArisingDate,
            this.colCusCode,
            this.colBuyer,
            this.colCusComName,
            this.colCusAddress,
            this.colCusPhone,
            this.colCusTaxCode,
            this.colPaymentMethod,
            this.colFolioNo,
            this.colTotal,
            this.colVATRate,
            this.colVATAmount,
            this.colAmount,
            this.colAmountInWords,
            this.colAppName,
            this.colPublish,
            this.colFkey,
            this.colPattern,
            this.colDeleteInv});
            this.viewInvoiceVAT.GridControl = this.gridInvoiceVAT;
            this.viewInvoiceVAT.GroupPanelText = "Kéo một cột vào đây để xem dữ liệu theo nhóm";
            this.viewInvoiceVAT.Name = "viewInvoiceVAT";
            this.viewInvoiceVAT.OptionsFind.AllowFindPanel = false;
            this.viewInvoiceVAT.OptionsFind.FindNullPrompt = "Nhập dữ liệu cần tìm...";
            this.viewInvoiceVAT.OptionsSelection.CheckBoxSelectorColumnWidth = 25;
            this.viewInvoiceVAT.OptionsSelection.MultiSelect = true;
            this.viewInvoiceVAT.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.viewInvoiceVAT.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.viewInvoiceVAT.OptionsView.ShowFooter = true;
            this.viewInvoiceVAT.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.viewInvoiceVAT_RowCellClick);
            // 
            // colViewInv
            // 
            this.colViewInv.AppearanceHeader.Options.UseTextOptions = true;
            this.colViewInv.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colViewInv.Caption = "Xem";
            this.colViewInv.ColumnEdit = this.btnViewInv;
            this.colViewInv.FieldName = "ViewInv";
            this.colViewInv.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colViewInv.Name = "colViewInv";
            this.colViewInv.Width = 40;
            // 
            // colEditInv
            // 
            this.colEditInv.AppearanceHeader.Options.UseTextOptions = true;
            this.colEditInv.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEditInv.Caption = "Sửa";
            this.colEditInv.ColumnEdit = this.btnEditInv;
            this.colEditInv.FieldName = "EditInv";
            this.colEditInv.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colEditInv.Name = "colEditInv";
            this.colEditInv.Visible = true;
            this.colEditInv.VisibleIndex = 1;
            this.colEditInv.Width = 40;
            // 
            // colId
            // 
            this.colId.AppearanceHeader.Options.UseTextOptions = true;
            this.colId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colId.Caption = "Id";
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.OptionsColumn.AllowEdit = false;
            this.colId.OptionsColumn.ReadOnly = true;
            // 
            // colMessageError
            // 
            this.colMessageError.AppearanceCell.ForeColor = System.Drawing.Color.Red;
            this.colMessageError.AppearanceCell.Options.UseForeColor = true;
            this.colMessageError.AppearanceHeader.Options.UseTextOptions = true;
            this.colMessageError.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMessageError.Caption = "Lỗi Phát hành";
            this.colMessageError.FieldName = "MessageError";
            this.colMessageError.Name = "colMessageError";
            this.colMessageError.OptionsColumn.ReadOnly = true;
            this.colMessageError.Visible = true;
            this.colMessageError.VisibleIndex = 2;
            this.colMessageError.Width = 85;
            // 
            // colArisingDate
            // 
            this.colArisingDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colArisingDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colArisingDate.Caption = "Ngày hóa đơn";
            this.colArisingDate.ColumnEdit = this.dtArisingDate;
            this.colArisingDate.FieldName = "ArisingDate";
            this.colArisingDate.Name = "colArisingDate";
            this.colArisingDate.OptionsColumn.AllowEdit = false;
            this.colArisingDate.OptionsColumn.ReadOnly = true;
            this.colArisingDate.Visible = true;
            this.colArisingDate.VisibleIndex = 3;
            this.colArisingDate.Width = 100;
            // 
            // colCusCode
            // 
            this.colCusCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colCusCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCusCode.Caption = "Mã Khách hàng";
            this.colCusCode.FieldName = "CusCode";
            this.colCusCode.Name = "colCusCode";
            this.colCusCode.OptionsColumn.AllowEdit = false;
            this.colCusCode.OptionsColumn.ReadOnly = true;
            this.colCusCode.Width = 100;
            // 
            // colBuyer
            // 
            this.colBuyer.AppearanceHeader.Options.UseTextOptions = true;
            this.colBuyer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBuyer.Caption = "Người mua hàng";
            this.colBuyer.FieldName = "Buyer";
            this.colBuyer.Name = "colBuyer";
            this.colBuyer.OptionsColumn.AllowEdit = false;
            this.colBuyer.OptionsColumn.ReadOnly = true;
            this.colBuyer.Visible = true;
            this.colBuyer.VisibleIndex = 4;
            this.colBuyer.Width = 139;
            // 
            // colCusComName
            // 
            this.colCusComName.AppearanceHeader.Options.UseTextOptions = true;
            this.colCusComName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCusComName.Caption = "Tên đơn vị";
            this.colCusComName.FieldName = "CusComName";
            this.colCusComName.Name = "colCusComName";
            this.colCusComName.OptionsColumn.AllowEdit = false;
            this.colCusComName.OptionsColumn.ReadOnly = true;
            this.colCusComName.Visible = true;
            this.colCusComName.VisibleIndex = 5;
            this.colCusComName.Width = 200;
            // 
            // colCusAddress
            // 
            this.colCusAddress.AppearanceHeader.Options.UseTextOptions = true;
            this.colCusAddress.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCusAddress.Caption = "Địa chỉ";
            this.colCusAddress.FieldName = "CusAddress";
            this.colCusAddress.Name = "colCusAddress";
            this.colCusAddress.OptionsColumn.AllowEdit = false;
            this.colCusAddress.OptionsColumn.ReadOnly = true;
            this.colCusAddress.Visible = true;
            this.colCusAddress.VisibleIndex = 6;
            this.colCusAddress.Width = 150;
            // 
            // colCusPhone
            // 
            this.colCusPhone.AppearanceHeader.Options.UseTextOptions = true;
            this.colCusPhone.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCusPhone.Caption = "Điện thoại";
            this.colCusPhone.FieldName = "CusPhone";
            this.colCusPhone.Name = "colCusPhone";
            this.colCusPhone.OptionsColumn.AllowEdit = false;
            this.colCusPhone.OptionsColumn.ReadOnly = true;
            this.colCusPhone.Width = 100;
            // 
            // colCusTaxCode
            // 
            this.colCusTaxCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colCusTaxCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCusTaxCode.Caption = "Mã số thuế";
            this.colCusTaxCode.FieldName = "CusTaxCode";
            this.colCusTaxCode.Name = "colCusTaxCode";
            this.colCusTaxCode.OptionsColumn.AllowEdit = false;
            this.colCusTaxCode.OptionsColumn.ReadOnly = true;
            this.colCusTaxCode.Visible = true;
            this.colCusTaxCode.VisibleIndex = 7;
            this.colCusTaxCode.Width = 100;
            // 
            // colPaymentMethod
            // 
            this.colPaymentMethod.AppearanceHeader.Options.UseTextOptions = true;
            this.colPaymentMethod.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPaymentMethod.Caption = "Phương thức TT";
            this.colPaymentMethod.FieldName = "PaymentMethod";
            this.colPaymentMethod.Name = "colPaymentMethod";
            this.colPaymentMethod.OptionsColumn.AllowEdit = false;
            this.colPaymentMethod.OptionsColumn.ReadOnly = true;
            this.colPaymentMethod.Width = 100;
            // 
            // colFolioNo
            // 
            this.colFolioNo.AppearanceHeader.Options.UseTextOptions = true;
            this.colFolioNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFolioNo.Caption = "Số tham chiếu";
            this.colFolioNo.FieldName = "InvoiceNoSAP";
            this.colFolioNo.Name = "colFolioNo";
            this.colFolioNo.OptionsColumn.AllowEdit = false;
            this.colFolioNo.OptionsColumn.ReadOnly = true;
            this.colFolioNo.Visible = true;
            this.colFolioNo.VisibleIndex = 8;
            this.colFolioNo.Width = 100;
            // 
            // colTotal
            // 
            this.colTotal.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotal.Caption = "Cộng hàng (VNĐ)";
            this.colTotal.ColumnEdit = this.txtTotal;
            this.colTotal.FieldName = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.OptionsColumn.AllowEdit = false;
            this.colTotal.OptionsColumn.ReadOnly = true;
            this.colTotal.Visible = true;
            this.colTotal.VisibleIndex = 9;
            this.colTotal.Width = 120;
            // 
            // colVATRate
            // 
            this.colVATRate.AppearanceHeader.Options.UseTextOptions = true;
            this.colVATRate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colVATRate.Caption = "Thuế GTGT (%)";
            this.colVATRate.FieldName = "VATRate";
            this.colVATRate.Name = "colVATRate";
            this.colVATRate.OptionsColumn.AllowEdit = false;
            this.colVATRate.OptionsColumn.ReadOnly = true;
            this.colVATRate.Visible = true;
            this.colVATRate.VisibleIndex = 10;
            this.colVATRate.Width = 100;
            // 
            // colVATAmount
            // 
            this.colVATAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colVATAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colVATAmount.Caption = "Tiền thuế GTGT (VNĐ)";
            this.colVATAmount.ColumnEdit = this.txtVATAmount;
            this.colVATAmount.FieldName = "VATAmount";
            this.colVATAmount.Name = "colVATAmount";
            this.colVATAmount.OptionsColumn.AllowEdit = false;
            this.colVATAmount.OptionsColumn.ReadOnly = true;
            this.colVATAmount.Visible = true;
            this.colVATAmount.VisibleIndex = 11;
            this.colVATAmount.Width = 120;
            // 
            // colAmount
            // 
            this.colAmount.AppearanceHeader.Options.UseTextOptions = true;
            this.colAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAmount.Caption = "Tổng tiền (VNĐ)";
            this.colAmount.ColumnEdit = this.txtAmount;
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.OptionsColumn.ReadOnly = true;
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 12;
            this.colAmount.Width = 120;
            // 
            // colAmountInWords
            // 
            this.colAmountInWords.AppearanceHeader.Options.UseTextOptions = true;
            this.colAmountInWords.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAmountInWords.Caption = "Số tiền bằng chữ";
            this.colAmountInWords.FieldName = "AmountInWords";
            this.colAmountInWords.Name = "colAmountInWords";
            this.colAmountInWords.OptionsColumn.AllowEdit = false;
            this.colAmountInWords.OptionsColumn.ReadOnly = true;
            this.colAmountInWords.Visible = true;
            this.colAmountInWords.VisibleIndex = 13;
            this.colAmountInWords.Width = 200;
            // 
            // colAppName
            // 
            this.colAppName.Caption = "Loại dữ liệu";
            this.colAppName.FieldName = "AppName";
            this.colAppName.Name = "colAppName";
            // 
            // colPublish
            // 
            this.colPublish.Caption = "Phát hành";
            this.colPublish.ColumnEdit = this.chkPublish;
            this.colPublish.FieldName = "Publish";
            this.colPublish.Name = "colPublish";
            // 
            // colFkey
            // 
            this.colFkey.Caption = "Key";
            this.colFkey.FieldName = "Fkey";
            this.colFkey.Name = "colFkey";
            // 
            // colPattern
            // 
            this.colPattern.Caption = "Mẫu số";
            this.colPattern.FieldName = "Pattern";
            this.colPattern.Name = "colPattern";
            // 
            // colDeleteInv
            // 
            this.colDeleteInv.AppearanceHeader.Options.UseTextOptions = true;
            this.colDeleteInv.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDeleteInv.Caption = "Xóa";
            this.colDeleteInv.ColumnEdit = this.btnDeleteInv;
            this.colDeleteInv.FieldName = "DeleteInv";
            this.colDeleteInv.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.colDeleteInv.Name = "colDeleteInv";
            this.colDeleteInv.Width = 40;
            // 
            // ucInvoiceVAT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.Controls.Add(this.ucPaging);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ucInvoiceVAT";
            this.Size = new System.Drawing.Size(1233, 740);
            this.Load += new System.EventHandler(this.ucInvoiceVAT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnViewInv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditInv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtArisingDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtArisingDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVATAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPublish)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteInv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoiceVAT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewInvoiceVAT)).EndInit();
            this.ResumeLayout(false);

		}

		public void LoadData(int PageIndex)
		{
			int total = 0;
			List<InvoiceVAT> list = IoC.Resolve<IInvoiceVATService>().GetUnPublish(ref PageIndex, this.ucPaging.PageSize, out total);
			this.gridInvoiceVAT.DataSource = list;
			this.gridInvoiceVAT.Focus();
			this.lblInvoicesNumber.Text = total.ToString();
			this.ucPaging.PageIndex = PageIndex;
			this.ucPaging.Total = new int?(total);
			this.ucPaging.UpdatePagingState();
		}
    }
}