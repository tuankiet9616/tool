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
using DevExpress.XtraSplashScreen;
using FX.Core;
using FX.Data;
using log4net;
using Newtonsoft.Json;
using Parse.Core;
using Parse.Core.Domain;
using Parse.Core.Implement;
using Parse.Core.IService;
using Parse.Forms.CustomUC;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using ViettelAPI;
using ViettelAPI.Models;

namespace Parse.Forms
{
	public class ucInvoiceList : UserControl
	{
		private readonly IInvoiceVATService invSrc;

		private readonly ILog log = LogManager.GetLogger(typeof(ucInvoiceList));

		private IMainForm Main;

		private bool isFind;

		private string strFind = "";

		private DateTime? tuNgay;

		private DateTime? denNgay;

		private IContainer components;

		private PanelControl panelControl1;

		private GroupControl groupListInvoice;

		private UCPaging ucPaging;

		private GridControl gridListInv;

		private GridView viewListInv;

		private GridColumn ColId;

		private GridColumn colNo;

		private GridColumn ColSerial;

		private GridColumn ColPattern;

		private GridColumn ColFKey;

		private GridColumn colName;

		private GridColumn colCusName;

		private GridColumn colCusCode;

		private GridColumn colCusAddress;

		private GridColumn ColCusTaxCode;

		private GridColumn ColTotal;

		private RepositoryItemTextEdit txtTotal;

		private GridColumn ColVATRate;

		private GridColumn ColVATAmount;

		private RepositoryItemTextEdit txtVATAmount;

		private GridColumn ColAmount;

		private RepositoryItemTextEdit txtAmount;

		private GridColumn ColArisingDate;

		private RepositoryItemDateEdit dtArisingDate;

		private GridColumn ColConverted;

		private GridColumn ColConvert;

		private RepositoryItemButtonEdit btnConvert;

		private LabelControl labelControl1;

		private GridColumn colBuyer;

		private TextEdit txtPublishFind;

		private SimpleButton btnPublishFind;

		private SimpleButton btnPublishClear;

		private GridColumn colFolioNo;

		private LabelControl lblInvoiceNumber;

		private LabelControl labelControl2;

		private DateEdit dtDenNgay;

		private DateEdit dtTuNgay;

		private LabelControl labelControl4;

		private LabelControl labelControl3;

        private GridColumn ColCodeOfTax;

        public ucInvoiceList(IMainForm main)
		{
			this.InitializeComponent();
			this.Main = main;
			this.invSrc = IoC.Resolve<IInvoiceVATService>();
			this.ucPaging.PageSize = 35;
		}

		private void btnConvert_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			try
			{
                IBussinessLogService logService = IoC.Resolve<IBussinessLogService>();
                try
				{
                    SplashScreenManager.ShowForm(typeof(ProcessIndicator));
					InvoiceVAT entity = (InvoiceVAT)this.viewListInv.GetRow(this.viewListInv.FocusedRowHandle);
					string folder = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "PRINT_INVOICE");
                    InvoiceVATService.log.Error(string.Format("Folder PDF : {0}", folder));
                    if (!Directory.Exists(folder))
					{
						Directory.CreateDirectory(folder);
					}
					string fileName = string.Format("{0}_{1}_{2}", entity.Pattern.Replace("/", ""), entity.Serial.Replace("/", ""), entity.No.Replace("/", ""));
					string path = string.Format("{0}\\{1}.pdf", folder, fileName);
					if (!File.Exists(path))
					{
						PDFFileResponse data = ViettelAPI.APIHelper.GetInvoicePdfV2(entity.No, entity.Pattern, entity.Fkey);
						if (!string.IsNullOrEmpty(data.errorCode) && (data.errorCode != "200"))
						{
							throw new Exception(string.Concat(data.errorCode, ":", data.description));
						}
						File.WriteAllBytes(path, data.fileToBytes);
					}
					(new frmInvoicePdf(path, this.Main)).ShowDialog();
				}
				catch (Exception exception)
				{
					this.log.Error(exception);
                    BussinessLog Bussinesslog = new BussinessLog()
                    {
                        FileName = "Tải File Hóa Đơn PDF",
                        AppName = "Tải File",
                        CreateDate = DateTime.Now,
                        Error = string.Concat("Lỗi: ", exception.Message)
                    };
                    logService.CreateNew(Bussinesslog);
                    logService.CommitChanges();
                    XtraMessageBox.Show("Có lỗi xảy ra, vui lòng thực hiện lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			finally
			{
				SplashScreenManager.CloseForm();
			}
		}

		private void btnESCConvert_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			InvoiceVAT entity = (InvoiceVAT)this.viewListInv.GetRow(this.viewListInv.FocusedRowHandle);
			try
			{
				if (ConfigurationManager.AppSettings["POPULAR_API"] == "1")
				{
					List<ProductInv> lstprod = IoC.Resolve<IProductInvService>().GetByInvoiceID(entity.Id);
					entity.Products = lstprod;
					(new frmInvocieView(entity.GetXMLData(Parse.Core.AppContext.Current.company), this.Main, entity)).ShowDialog();
				}
				else if (entity != null)
				{
					string xmlResult = Parse.Core.APIHelper.ConvertInv(entity.Fkey, entity.Pattern, entity.Serial, entity.No);
					if (!xmlResult.Contains("ERR:"))
					{
						entity.Converted = true;
						this.invSrc.Update(entity);
						this.invSrc.CommitChanges();
						XtraMessageBox.Show("In chuyển đổi hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						if (this.isFind)
						{
							this.FindData(this.ucPaging.PageIndex);
						}
						else
						{
							this.LoadData(1);
						}
						(new frmInvocieView(JsonConvert.DeserializeObject<string>(xmlResult), this.Main, null)).ShowDialog();
					}
					else
					{
						XtraMessageBox.Show("", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
				}
			}
			catch (Exception exception)
			{
				this.log.Error(exception);
				XtraMessageBox.Show("Có lỗi xảy ra, vui lòng thực hiện lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void btnPublishClear_Click(object sender, EventArgs e)
		{
			this.ClearData();
			this.LoadData(1);
		}

		private void btnPublishFind_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.txtPublishFind.Text) && string.IsNullOrEmpty(this.dtTuNgay.Text) && string.IsNullOrEmpty(this.dtDenNgay.Text))
			{
				this.ClearData();
				this.LoadData(1);
				return;
			}
			this.strFind = this.txtPublishFind.Text;
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

		public void ClearData()
		{
			this.txtPublishFind.Text = "";
			this.strFind = "";
			this.dtTuNgay.Text = "";
			this.tuNgay = null;
			this.dtDenNgay.Text = "";
			this.denNgay = null;
			this.isFind = false;
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
			this.gridListInv.DataSource = this.invSrc.FindPublishSuccess(this.tuNgay, this.denNgay, this.strFind, ref PageIndex, this.ucPaging.PageSize, out total);
			this.lblInvoiceNumber.Text = total.ToString();
			this.ucPaging.PageIndex = PageIndex;
			this.ucPaging.Total = new int?(total);
			this.ucPaging.UpdatePagingState();
		}

		private void InitializeComponent()
		{
			EditorButtonImageOptions editorButtonImageOptions2 = new EditorButtonImageOptions();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(ucInvoiceList));
			SerializableAppearanceObject serializableAppearanceObject5 = new SerializableAppearanceObject();
			SerializableAppearanceObject serializableAppearanceObject6 = new SerializableAppearanceObject();
			SerializableAppearanceObject serializableAppearanceObject7 = new SerializableAppearanceObject();
			SerializableAppearanceObject serializableAppearanceObject8 = new SerializableAppearanceObject();
			this.panelControl1 = new PanelControl();
			this.labelControl1 = new LabelControl();
			this.groupListInvoice = new GroupControl();
			this.dtDenNgay = new DateEdit();
			this.dtTuNgay = new DateEdit();
			this.labelControl4 = new LabelControl();
			this.labelControl3 = new LabelControl();
			this.labelControl2 = new LabelControl();
			this.lblInvoiceNumber = new LabelControl();
			this.btnPublishClear = new SimpleButton();
			this.btnPublishFind = new SimpleButton();
			this.txtPublishFind = new TextEdit();
			this.ucPaging = new UCPaging();
			this.gridListInv = new GridControl();
			this.viewListInv = new GridView();
			this.ColId = new GridColumn();
			this.ColSerial = new GridColumn();
			this.ColPattern = new GridColumn();
			this.ColFKey = new GridColumn();
			this.colNo = new GridColumn();
			this.colFolioNo = new GridColumn();
            this.ColCodeOfTax = new GridColumn();
            this.colName = new GridColumn();
			this.colBuyer = new GridColumn();
			this.colCusName = new GridColumn();
			this.colCusCode = new GridColumn();
			this.colCusAddress = new GridColumn();
			this.ColCusTaxCode = new GridColumn();
			this.ColTotal = new GridColumn();
			this.txtTotal = new RepositoryItemTextEdit();
			this.ColVATRate = new GridColumn();
			this.ColVATAmount = new GridColumn();
			this.txtVATAmount = new RepositoryItemTextEdit();
			this.ColAmount = new GridColumn();
			this.txtAmount = new RepositoryItemTextEdit();
			this.ColArisingDate = new GridColumn();
			this.dtArisingDate = new RepositoryItemDateEdit();
			this.ColConverted = new GridColumn();
			this.ColConvert = new GridColumn();
			this.btnConvert = new RepositoryItemButtonEdit();
			((ISupportInitialize)this.panelControl1).BeginInit();
			this.panelControl1.SuspendLayout();
			((ISupportInitialize)this.groupListInvoice).BeginInit();
			this.groupListInvoice.SuspendLayout();
			((ISupportInitialize)this.dtDenNgay.Properties.CalendarTimeProperties).BeginInit();
			((ISupportInitialize)this.dtDenNgay.Properties).BeginInit();
			((ISupportInitialize)this.dtTuNgay.Properties.CalendarTimeProperties).BeginInit();
			((ISupportInitialize)this.dtTuNgay.Properties).BeginInit();
			((ISupportInitialize)this.txtPublishFind.Properties).BeginInit();
			((ISupportInitialize)this.gridListInv).BeginInit();
			((ISupportInitialize)this.viewListInv).BeginInit();
			((ISupportInitialize)this.txtTotal).BeginInit();
			((ISupportInitialize)this.txtVATAmount).BeginInit();
			((ISupportInitialize)this.txtAmount).BeginInit();
			((ISupportInitialize)this.dtArisingDate).BeginInit();
			((ISupportInitialize)this.dtArisingDate.CalendarTimeProperties).BeginInit();
			((ISupportInitialize)this.btnConvert).BeginInit();
			base.SuspendLayout();
			this.panelControl1.Controls.Add(this.labelControl1);
			this.panelControl1.Dock = DockStyle.Top;
			this.panelControl1.Location = new Point(0, 0);
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Size = new System.Drawing.Size(951, 30);
			this.panelControl1.TabIndex = 0;
			this.labelControl1.Anchor = AnchorStyles.Top;
			this.labelControl1.Appearance.Font = new System.Drawing.Font("Times New Roman", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.labelControl1.Appearance.Options.UseFont = true;
			this.labelControl1.Location = new Point(403, 8);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(135, 15);
			this.labelControl1.TabIndex = 1;
			this.labelControl1.Text = "DANH SÁCH HÓA ĐƠN";
			this.groupListInvoice.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.groupListInvoice.Controls.Add(this.dtDenNgay);
			this.groupListInvoice.Controls.Add(this.dtTuNgay);
			this.groupListInvoice.Controls.Add(this.labelControl4);
			this.groupListInvoice.Controls.Add(this.labelControl3);
			this.groupListInvoice.Controls.Add(this.labelControl2);
			this.groupListInvoice.Controls.Add(this.lblInvoiceNumber);
			this.groupListInvoice.Controls.Add(this.btnPublishClear);
			this.groupListInvoice.Controls.Add(this.btnPublishFind);
			this.groupListInvoice.Controls.Add(this.txtPublishFind);
			this.groupListInvoice.Controls.Add(this.ucPaging);
			this.groupListInvoice.Controls.Add(this.gridListInv);
			this.groupListInvoice.Location = new Point(0, 30);
			this.groupListInvoice.Name = "groupListInvoice";
			this.groupListInvoice.Size = new System.Drawing.Size(951, 566);
			this.groupListInvoice.TabIndex = 21;
			this.groupListInvoice.Text = "Chi tiết";
			this.dtDenNgay.EditValue = null;
			this.dtDenNgay.Location = new Point(164, 23);
			this.dtDenNgay.Name = "dtDenNgay";
			this.dtDenNgay.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
			this.dtDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
			this.dtDenNgay.Size = new System.Drawing.Size(100, 20);
			this.dtDenNgay.TabIndex = 10;
			this.dtTuNgay.EditValue = null;
			this.dtTuNgay.Location = new Point(28, 23);
			this.dtTuNgay.Name = "dtTuNgay";
			this.dtTuNgay.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
			this.dtTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
			this.dtTuNgay.Size = new System.Drawing.Size(100, 20);
			this.dtTuNgay.TabIndex = 9;
			this.labelControl4.Location = new Point(134, 26);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(24, 13);
			this.labelControl4.TabIndex = 8;
			this.labelControl4.Text = "Đến:";
			this.labelControl3.Location = new Point(5, 26);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(17, 13);
			this.labelControl3.TabIndex = 7;
			this.labelControl3.Text = "Từ:";
			this.labelControl2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.labelControl2.Appearance.Options.UseFont = true;
			this.labelControl2.Location = new Point(796, 26);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(100, 13);
			this.labelControl2.TabIndex = 6;
			this.labelControl2.Text = "Số lượng hóa đơn:";
			this.lblInvoiceNumber.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.lblInvoiceNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblInvoiceNumber.Appearance.Options.UseFont = true;
			this.lblInvoiceNumber.Location = new Point(902, 26);
			this.lblInvoiceNumber.Name = "lblInvoiceNumber";
			this.lblInvoiceNumber.Size = new System.Drawing.Size(44, 13);
			this.lblInvoiceNumber.TabIndex = 5;
			this.lblInvoiceNumber.Text = "Number";
			this.btnPublishClear.Location = new Point(554, 21);
			this.btnPublishClear.Name = "btnPublishClear";
			this.btnPublishClear.Size = new System.Drawing.Size(75, 23);
			this.btnPublishClear.TabIndex = 4;
			this.btnPublishClear.Text = "Hủy";
			this.btnPublishClear.Click += new EventHandler(this.btnPublishClear_Click);
			this.btnPublishFind.Location = new Point(473, 21);
			this.btnPublishFind.Name = "btnPublishFind";
			this.btnPublishFind.Size = new System.Drawing.Size(75, 23);
			this.btnPublishFind.TabIndex = 3;
			this.btnPublishFind.Text = "Tìm kiếm";
			this.btnPublishFind.Click += new EventHandler(this.btnPublishFind_Click);
			this.txtPublishFind.Location = new Point(270, 23);
			this.txtPublishFind.Name = "txtPublishFind";
			this.txtPublishFind.Size = new System.Drawing.Size(197, 20);
			this.txtPublishFind.TabIndex = 2;
			this.ucPaging.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.ucPaging.Location = new Point(5, 535);
			this.ucPaging.Name = "ucPaging";
			this.ucPaging.PageIndex = 1;
			this.ucPaging.PageSize = 30;
			this.ucPaging.Size = new System.Drawing.Size(200, 26);
			this.ucPaging.TabIndex = 1;
			this.ucPaging.Total = null;
			this.ucPaging.NextClick += new PagingEventHandler(this.UCPaging_Click);
			this.ucPaging.PrevClick += new PagingEventHandler(this.UCPaging_Click);
			this.ucPaging.FirstClick += new PagingEventHandler(this.UCPaging_Click);
			this.ucPaging.LastClick += new PagingEventHandler(this.UCPaging_Click);
			this.gridListInv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.gridListInv.Location = new Point(2, 49);
			this.gridListInv.MainView = this.viewListInv;
			this.gridListInv.Name = "gridListInv";
			this.gridListInv.RepositoryItems.AddRange(new RepositoryItem[] { this.btnConvert, this.txtTotal, this.txtVATAmount, this.txtAmount, this.dtArisingDate });
			this.gridListInv.Size = new System.Drawing.Size(947, 515);
			this.gridListInv.TabIndex = 0;
			this.gridListInv.ViewCollection.AddRange(new BaseView[] { this.viewListInv });
			this.viewListInv.Columns.AddRange(new GridColumn[] { this.ColId, this.ColSerial, this.ColPattern, this.ColFKey, this.colNo, this.colFolioNo, this.colName, this.colBuyer, this.colCusName, this.colCusCode, this.colCusAddress, this.ColCusTaxCode, this.ColTotal, this.ColVATRate, this.ColVATAmount, this.ColAmount, this.ColArisingDate, this.ColConverted, this.ColConvert, this.ColCodeOfTax });
			this.viewListInv.GridControl = this.gridListInv;
			this.viewListInv.GroupPanelText = "Kéo một cột vào đây để xem dữ liệu theo nhóm";
			this.viewListInv.Name = "viewListInv";
			this.viewListInv.OptionsFind.FindNullPrompt = "Nhập dữ liệu cần tìm...";
			this.viewListInv.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
			this.viewListInv.OptionsView.ShowFooter = true;
			this.ColId.AppearanceHeader.Options.UseTextOptions = true;
			this.ColId.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.ColId.Caption = "Id";
			this.ColId.FieldName = "Id";
			this.ColId.Name = "ColId";
			this.ColId.OptionsColumn.AllowEdit = false;
			this.ColId.OptionsColumn.ReadOnly = true;
			this.ColSerial.AppearanceHeader.Options.UseTextOptions = true;
			this.ColSerial.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.ColSerial.Caption = "Ký hiệu";
			this.ColSerial.FieldName = "Serial";
			this.ColSerial.Name = "ColSerial";
			this.ColSerial.OptionsColumn.AllowEdit = false;
			this.ColSerial.OptionsColumn.ReadOnly = true;
			this.ColSerial.Visible = true;
			this.ColSerial.VisibleIndex = 0;
			this.ColSerial.Width = 100;
			this.ColPattern.AppearanceHeader.Options.UseTextOptions = true;
			this.ColPattern.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.ColPattern.Caption = "Mẫu số";
			this.ColPattern.FieldName = "Pattern";
			this.ColPattern.Name = "ColPattern";
			this.ColPattern.OptionsColumn.AllowEdit = false;
			this.ColPattern.OptionsColumn.ReadOnly = true;
			this.ColPattern.Visible = true;
			this.ColPattern.VisibleIndex = 1;
			this.ColPattern.Width = 100;
			this.ColFKey.AppearanceHeader.Options.UseTextOptions = true;
			this.ColFKey.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.ColFKey.Caption = "Key";
			this.ColFKey.FieldName = "FKey";
			this.ColFKey.Name = "ColFKey";
			this.ColFKey.OptionsColumn.AllowEdit = false;
			this.ColFKey.OptionsColumn.ReadOnly = true;
			this.ColFKey.Width = 100;
			this.colNo.AppearanceHeader.Options.UseTextOptions = true;
			this.colNo.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.colNo.Caption = "Số hóa đơn";
			this.colNo.FieldName = "No";
			this.colNo.Name = "colNo";
			this.colNo.OptionsColumn.AllowEdit = false;
			this.colNo.OptionsColumn.ReadOnly = true;
			this.colNo.Visible = true;
			this.colNo.VisibleIndex = 2;
			this.colNo.Width = 100;
			this.colFolioNo.Caption = "Số tham chiếu";
			this.colFolioNo.FieldName = "InvoiceNoSAP";
			this.colFolioNo.Name = "colFolioNo";
			this.colFolioNo.Visible = true;
			this.colFolioNo.VisibleIndex = 3;
            this.colFolioNo.OptionsColumn.AllowEdit = false;
            this.colFolioNo.OptionsColumn.ReadOnly = true;
            this.colName.AppearanceHeader.Options.UseTextOptions = true;
			this.colName.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.colName.Caption = "Phần mềm";
			this.colName.FieldName = "Name";
			this.colName.Name = "colName";
			this.colName.OptionsColumn.AllowEdit = false;
			this.colName.OptionsColumn.ReadOnly = true;
			this.colName.Width = 100;
			this.colBuyer.AppearanceHeader.Options.UseTextOptions = true;
			this.colBuyer.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.colBuyer.Caption = "Người mua hàng";
			this.colBuyer.FieldName = "Buyer";
			this.colBuyer.Name = "colBuyer";
			this.colBuyer.OptionsColumn.AllowEdit = false;
			this.colBuyer.OptionsColumn.ReadOnly = true;
			this.colBuyer.Visible = true;
			this.colBuyer.VisibleIndex = 4;
			this.colBuyer.Width = 100;
			this.colCusName.AppearanceHeader.Options.UseTextOptions = true;
			this.colCusName.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.colCusName.Caption = "Tên đơn vị";
			this.colCusName.FieldName = "CusComName";
			this.colCusName.Name = "colCusName";
			this.colCusName.OptionsColumn.AllowEdit = false;
			this.colCusName.OptionsColumn.ReadOnly = true;
			this.colCusName.Visible = true;
			this.colCusName.VisibleIndex = 5;
			this.colCusName.Width = 150;
			this.colCusCode.AppearanceHeader.Options.UseTextOptions = true;
			this.colCusCode.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.colCusCode.Caption = "Mã khách hàng";
			this.colCusCode.FieldName = "CusCode";
			this.colCusCode.Name = "colCusCode";
			this.colCusCode.OptionsColumn.AllowEdit = false;
			this.colCusCode.OptionsColumn.ReadOnly = true;
			this.colCusCode.Width = 100;
			this.colCusAddress.AppearanceHeader.Options.UseTextOptions = true;
			this.colCusAddress.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.colCusAddress.Caption = "Địa chỉ";
			this.colCusAddress.FieldName = "CusAddress";
			this.colCusAddress.Name = "colCusAddress";
			this.colCusAddress.OptionsColumn.AllowEdit = false;
			this.colCusAddress.OptionsColumn.ReadOnly = true;
			this.colCusAddress.Visible = true;
			this.colCusAddress.VisibleIndex = 6;
			this.colCusAddress.Width = 250;
			this.ColCusTaxCode.AppearanceHeader.Options.UseTextOptions = true;
			this.ColCusTaxCode.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.ColCusTaxCode.Caption = "Mã số thuế";
			this.ColCusTaxCode.FieldName = "CusTaxCode";
			this.ColCusTaxCode.Name = "ColCusTaxCode";
			this.ColCusTaxCode.OptionsColumn.AllowEdit = false;
			this.ColCusTaxCode.OptionsColumn.ReadOnly = true;
			this.ColCusTaxCode.Visible = true;
			this.ColCusTaxCode.VisibleIndex = 7;
			this.ColCusTaxCode.Width = 130;
			this.ColTotal.AppearanceHeader.Options.UseTextOptions = true;
			this.ColTotal.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.ColTotal.Caption = "Cộng tiền";
			this.ColTotal.ColumnEdit = this.txtTotal;
			this.ColTotal.FieldName = "Total";
			this.ColTotal.Name = "ColTotal";
			this.ColTotal.OptionsColumn.AllowEdit = false;
			this.ColTotal.OptionsColumn.ReadOnly = true;
			this.ColTotal.Visible = true;
			this.ColTotal.VisibleIndex = 8;
			this.ColTotal.Width = 130;
			this.txtTotal.AutoHeight = false;
			this.txtTotal.DisplayFormat.FormatString = "n0";
			this.txtTotal.DisplayFormat.FormatType = FormatType.Numeric;
			this.txtTotal.EditFormat.FormatString = "n0";
			this.txtTotal.EditFormat.FormatType = FormatType.Numeric;
			this.txtTotal.Mask.EditMask = "n0";
			this.txtTotal.Mask.MaskType = MaskType.Numeric;
			this.txtTotal.Name = "txtTotal";
			this.ColVATRate.AppearanceHeader.Options.UseTextOptions = true;
			this.ColVATRate.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.ColVATRate.Caption = "Thuế GTGT";
			this.ColVATRate.FieldName = "VATRate";
			this.ColVATRate.Name = "ColVATRate";
			this.ColVATRate.OptionsColumn.AllowEdit = false;
			this.ColVATRate.OptionsColumn.ReadOnly = true;
			this.ColVATRate.Visible = true;
			this.ColVATRate.VisibleIndex = 9;
			this.ColVATRate.Width = 100;
			this.ColVATAmount.AppearanceHeader.Options.UseTextOptions = true;
			this.ColVATAmount.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.ColVATAmount.Caption = "Tiền thuế GTGT";
			this.ColVATAmount.ColumnEdit = this.txtVATAmount;
			this.ColVATAmount.FieldName = "VATAmount";
			this.ColVATAmount.Name = "ColVATAmount";
			this.ColVATAmount.OptionsColumn.AllowEdit = false;
			this.ColVATAmount.OptionsColumn.ReadOnly = true;
			this.ColVATAmount.Visible = true;
			this.ColVATAmount.VisibleIndex = 10;
			this.ColVATAmount.Width = 130;
			this.txtVATAmount.AutoHeight = false;
			this.txtVATAmount.DisplayFormat.FormatString = "n0";
			this.txtVATAmount.DisplayFormat.FormatType = FormatType.Numeric;
			this.txtVATAmount.EditFormat.FormatString = "n0";
			this.txtVATAmount.EditFormat.FormatType = FormatType.Numeric;
			this.txtVATAmount.Mask.EditMask = "n0";
			this.txtVATAmount.Mask.MaskType = MaskType.Numeric;
			this.txtVATAmount.Name = "txtVATAmount";
			this.ColAmount.AppearanceHeader.Options.UseTextOptions = true;
			this.ColAmount.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.ColAmount.Caption = "Tổng tiền";
			this.ColAmount.ColumnEdit = this.txtAmount;
			this.ColAmount.FieldName = "Amount";
			this.ColAmount.Name = "ColAmount";
			this.ColAmount.OptionsColumn.AllowEdit = false;
			this.ColAmount.OptionsColumn.ReadOnly = true;
			this.ColAmount.Visible = true;
			this.ColAmount.VisibleIndex = 11;
			this.ColAmount.Width = 130;
			this.txtAmount.AutoHeight = false;
			this.txtAmount.DisplayFormat.FormatString = "n0";
			this.txtAmount.DisplayFormat.FormatType = FormatType.Numeric;
			this.txtAmount.EditFormat.FormatString = "n0";
			this.txtAmount.EditFormat.FormatType = FormatType.Numeric;
			this.txtAmount.Mask.EditMask = "n0";
			this.txtAmount.Mask.MaskType = MaskType.Numeric;
			this.txtAmount.Name = "txtAmount";
			this.ColArisingDate.AppearanceHeader.Options.UseTextOptions = true;
			this.ColArisingDate.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.ColArisingDate.Caption = "Ngày hóa đơn";
			this.ColArisingDate.ColumnEdit = this.dtArisingDate;
			this.ColArisingDate.FieldName = "ArisingDate";
			this.ColArisingDate.Name = "ColArisingDate";
			this.ColArisingDate.OptionsColumn.AllowEdit = false;
			this.ColArisingDate.OptionsColumn.ReadOnly = true;
			this.ColArisingDate.Visible = true;
			this.ColArisingDate.VisibleIndex = 12;
			this.ColArisingDate.Width = 100;
			this.dtArisingDate.AutoHeight = false;
			this.dtArisingDate.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
			this.dtArisingDate.CalendarTimeProperties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
			this.dtArisingDate.DisplayFormat.FormatString = "dd\\/MM\\/yyyy";
			this.dtArisingDate.DisplayFormat.FormatType = FormatType.DateTime;
			this.dtArisingDate.EditFormat.FormatString = "dd\\/MM\\/yyyy";
			this.dtArisingDate.EditFormat.FormatType = FormatType.DateTime;
			this.dtArisingDate.Mask.EditMask = "dd\\/MM\\/yyyy";
			this.dtArisingDate.Mask.UseMaskAsDisplayFormat = true;
			this.dtArisingDate.Name = "dtArisingDate";
			this.dtArisingDate.NullDate = "";
            this.ColCodeOfTax.AppearanceHeader.Options.UseTextOptions = true;
            this.ColCodeOfTax.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            this.ColCodeOfTax.Caption = "Mã CQT";
            this.ColCodeOfTax.FieldName = "CodeOfTax";
            this.ColCodeOfTax.Name = "ColCodeOfTax";
            this.ColCodeOfTax.OptionsColumn.AllowEdit = false;
            this.ColCodeOfTax.OptionsColumn.ReadOnly = true;
            this.ColCodeOfTax.Visible = true;
            this.ColCodeOfTax.VisibleIndex = 0;
            this.ColCodeOfTax.Width = 100;
            this.ColConverted.AppearanceHeader.Options.UseTextOptions = true;
			this.ColConverted.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.ColConverted.Caption = "Đã chuyển đổi";
			this.ColConverted.FieldName = "Converted";
			this.ColConverted.Name = "ColConverted";
			this.ColConverted.OptionsColumn.AllowEdit = false;
			this.ColConverted.OptionsColumn.ReadOnly = true;
			this.ColConverted.Width = 80;
			this.ColConvert.AppearanceHeader.Options.UseTextOptions = true;
			this.ColConvert.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.ColConvert.Caption = "In hóa đơn";
			this.ColConvert.ColumnEdit = this.btnConvert;
			this.ColConvert.Fixed = FixedStyle.Right;
			this.ColConvert.Name = "ColConvert";
			this.ColConvert.Visible = true;
			this.ColConvert.VisibleIndex = 13;
			this.btnConvert.AutoHeight = false;
			editorButtonImageOptions2.Image = (Image)resources.GetObject("editorButtonImageOptions2.Image");
			this.btnConvert.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new KeyShortcut(Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, ToolTipAnchor.Default) });
			this.btnConvert.Name = "btnConvert";
			this.btnConvert.TextEditStyle = TextEditStyles.HideTextEditor;
			this.btnConvert.ButtonClick += new ButtonPressedEventHandler(this.btnConvert_ButtonClick);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.groupListInvoice);
			base.Controls.Add(this.panelControl1);
			base.Name = "ucInvoiceList";
			base.Size = new System.Drawing.Size(951, 596);
			base.Load += new EventHandler(this.ucInvoiceList_Load);
			((ISupportInitialize)this.panelControl1).EndInit();
			this.panelControl1.ResumeLayout(false);
			this.panelControl1.PerformLayout();
			((ISupportInitialize)this.groupListInvoice).EndInit();
			this.groupListInvoice.ResumeLayout(false);
			this.groupListInvoice.PerformLayout();
			((ISupportInitialize)this.dtDenNgay.Properties.CalendarTimeProperties).EndInit();
			((ISupportInitialize)this.dtDenNgay.Properties).EndInit();
			((ISupportInitialize)this.dtTuNgay.Properties.CalendarTimeProperties).EndInit();
			((ISupportInitialize)this.dtTuNgay.Properties).EndInit();
			((ISupportInitialize)this.txtPublishFind.Properties).EndInit();
			((ISupportInitialize)this.gridListInv).EndInit();
			((ISupportInitialize)this.viewListInv).EndInit();
			((ISupportInitialize)this.txtTotal).EndInit();
			((ISupportInitialize)this.txtVATAmount).EndInit();
			((ISupportInitialize)this.txtAmount).EndInit();
			((ISupportInitialize)this.dtArisingDate.CalendarTimeProperties).EndInit();
			((ISupportInitialize)this.dtArisingDate).EndInit();
			((ISupportInitialize)this.btnConvert).EndInit();
			base.ResumeLayout(false);
		}

		public void LoadData(int PageIndex)
		{
			int total = 0;
			this.gridListInv.DataSource = this.invSrc.GetPublishSuccess(ref PageIndex, this.ucPaging.PageSize, out total);
			this.lblInvoiceNumber.Text = total.ToString();
			this.ucPaging.PageIndex = PageIndex;
			this.ucPaging.Total = new int?(total);
			this.ucPaging.UpdatePagingState();
		}

		private void ucInvoiceList_Load(object sender, EventArgs e)
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
	}
}