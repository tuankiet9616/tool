using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Controls;
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace Parse.Forms
{
	public class frmSetting : XtraForm
	{
		private Setup entity = new Setup();

		private readonly ILog log = LogManager.GetLogger(typeof(frmSetting));

		private BindingSource sourse = new BindingSource();

		private frmMain Main;

		private IContainer components;

		private GroupControl groupControl1;

		private TextEdit txtFilePath;

		private LabelControl labelControl1;

		private SimpleButton btnClose;

		private SimpleButton btnUpdate;

		private LabelControl labelControl3;

		private TextEdit txtCode;

		private GridControl gridSetup;

		private GridView viewSetup;

		private GridColumn ColId;

		private GridColumn ColCode;

		private GridColumn ColFilePath;

		private TextEdit txtId;

		private SimpleButton btnAdd;

		private LabelControl labelControl5;

		private LabelControl labelControl6;

		private LabelControl labelControl2;

		private LabelControl labelControl4;

		private SimpleButton btnChooseFolder;

		private FolderBrowserDialog folderBrowserDialog;

		private GridColumn colDelete;

		private RepositoryItemButtonEdit btnDelete;

		private RepositoryItemCheckEdit chkCheckKeyGrid;

		private LabelControl labelControl7;

		public frmSetting(frmMain main)
		{
			this.InitializeComponent();
			this.Main = main;
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			this.viewSetup.FocusedRowHandle = -1;
			this.txtId.Text = "0";
			this.txtCode.Text = "";
			this.txtFilePath.Text = "";
		}

		private void btnChooseFolder_Click(object sender, EventArgs e)
		{
			if (this.folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.txtFilePath.Text = this.folderBrowserDialog.SelectedPath;
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnDelete_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			try
			{
				Setup entity = (Setup)this.viewSetup.GetRow(this.viewSetup.FocusedRowHandle);
				if (entity != null && XtraMessageBox.Show("Bạn có chắc chắn muốn xóa dữ liệu này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
				{
					ISetupService setupService = IoC.Resolve<ISetupService>();
					setupService.Delete(entity.Id);
					setupService.CommitChanges();
					this.LoadData();
				}
			}
			catch (Exception exception)
			{
				this.log.Error(exception.Message);
			}
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.Validation())
				{
					XtraMessageBox.Show("Trường đánh dấu (*) bắt buộc nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else if (this.CheckExit())
				{
					ISetupService service = IoC.Resolve<ISetupService>();
					if (int.Parse(this.txtId.Text) <= 0)
					{
						Setup obj = new Setup()
						{
							Code = this.txtCode.Text,
							FilePath = this.txtFilePath.Text
						};
						service.CreateNew(obj);
					}
					else
					{
						this.entity.Code = this.txtCode.Text;
						this.entity.FilePath = this.txtFilePath.Text;
						service.Update(this.entity);
					}
					service.CommitChanges();
					XtraMessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.LoadData();
				}
				else
				{
					XtraMessageBox.Show(string.Concat("Mã ", this.txtCode.Text.ToUpper(), " đã tồn tại."), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			catch (Exception exception)
			{
				this.log.Error(exception);
			}
		}

		private bool CheckExit()
		{
			if (IoC.Resolve<ISetupService>().GetbyCode(this.txtCode.Text) != null && int.Parse(this.txtId.Text) == 0)
			{
				return false;
			}
			return true;
		}

		private void CreateDefaultSetting()
		{
			ISetupService service = IoC.Resolve<ISetupService>();
			try
			{
				service.BeginTran();
				foreach (MenuModel menu in MenuModel.MenuItems)
				{
					Setup setup = new Setup()
					{
						Code = menu.Code,
						FilePath = string.Concat("C:\\", menu.Code)
					};
					service.CreateNew(setup);
				}
				service.CommitTran();
			}
			catch (Exception exception)
			{
				Exception ex = exception;
				service.RolbackTran();
				this.log.Error(ex.Message);
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

		private void frmSetting_Load(object sender, EventArgs e)
		{
			ISetupService service = IoC.Resolve<ISetupService>();
			if (service.GetAll().Count<Setup>() == 0)
			{
				this.CreateDefaultSetting();
				this.sourse.DataSource = service.GetAll();
			}
			this.LoadData();
		}

		private void InitializeComponent()
		{
			ComponentResourceManager resources = new ComponentResourceManager(typeof(frmSetting));
			EditorButtonImageOptions editorButtonImageOptions1 = new EditorButtonImageOptions();
			SerializableAppearanceObject serializableAppearanceObject1 = new SerializableAppearanceObject();
			SerializableAppearanceObject serializableAppearanceObject2 = new SerializableAppearanceObject();
			SerializableAppearanceObject serializableAppearanceObject3 = new SerializableAppearanceObject();
			SerializableAppearanceObject serializableAppearanceObject4 = new SerializableAppearanceObject();
			this.groupControl1 = new GroupControl();
			this.labelControl7 = new LabelControl();
			this.btnAdd = new SimpleButton();
			this.btnChooseFolder = new SimpleButton();
			this.btnClose = new SimpleButton();
			this.btnUpdate = new SimpleButton();
			this.labelControl5 = new LabelControl();
			this.labelControl6 = new LabelControl();
			this.labelControl2 = new LabelControl();
			this.labelControl4 = new LabelControl();
			this.txtId = new TextEdit();
			this.gridSetup = new GridControl();
			this.viewSetup = new GridView();
			this.ColId = new GridColumn();
			this.ColCode = new GridColumn();
			this.ColFilePath = new GridColumn();
			this.colDelete = new GridColumn();
			this.btnDelete = new RepositoryItemButtonEdit();
			this.chkCheckKeyGrid = new RepositoryItemCheckEdit();
			this.txtCode = new TextEdit();
			this.labelControl3 = new LabelControl();
			this.txtFilePath = new TextEdit();
			this.labelControl1 = new LabelControl();
			this.folderBrowserDialog = new FolderBrowserDialog();
			((ISupportInitialize)this.groupControl1).BeginInit();
			this.groupControl1.SuspendLayout();
			((ISupportInitialize)this.txtId.Properties).BeginInit();
			((ISupportInitialize)this.gridSetup).BeginInit();
			((ISupportInitialize)this.viewSetup).BeginInit();
			((ISupportInitialize)this.btnDelete).BeginInit();
			((ISupportInitialize)this.chkCheckKeyGrid).BeginInit();
			((ISupportInitialize)this.txtCode.Properties).BeginInit();
			((ISupportInitialize)this.txtFilePath.Properties).BeginInit();
			base.SuspendLayout();
			this.groupControl1.Controls.Add(this.labelControl7);
			this.groupControl1.Controls.Add(this.btnAdd);
			this.groupControl1.Controls.Add(this.btnChooseFolder);
			this.groupControl1.Controls.Add(this.btnClose);
			this.groupControl1.Controls.Add(this.btnUpdate);
			this.groupControl1.Controls.Add(this.labelControl5);
			this.groupControl1.Controls.Add(this.labelControl6);
			this.groupControl1.Controls.Add(this.labelControl2);
			this.groupControl1.Controls.Add(this.labelControl4);
			this.groupControl1.Controls.Add(this.txtId);
			this.groupControl1.Controls.Add(this.gridSetup);
			this.groupControl1.Controls.Add(this.txtCode);
			this.groupControl1.Controls.Add(this.labelControl3);
			this.groupControl1.Controls.Add(this.txtFilePath);
			this.groupControl1.Controls.Add(this.labelControl1);
			this.groupControl1.Location = new Point(2, 2);
			this.groupControl1.Name = "groupControl1";
			this.groupControl1.Size = new System.Drawing.Size(697, 309);
			this.groupControl1.TabIndex = 0;
			this.groupControl1.Text = "Cấu hình thư mục lưu trữ file";
			this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Italic);
			this.labelControl7.Appearance.ForeColor = Color.Red;
			this.labelControl7.Appearance.Options.UseFont = true;
			this.labelControl7.Appearance.Options.UseForeColor = true;
			this.labelControl7.Location = new Point(207, 86);
			this.labelControl7.Name = "labelControl7";
			this.labelControl7.Size = new System.Drawing.Size(247, 13);
			this.labelControl7.TabIndex = 67;
			this.labelControl7.Text = "Khởi động lại chương trình để áp dụng cấu hình mới.";
			this.btnAdd.Cursor = Cursors.Hand;
			this.btnAdd.ImageOptions.Image = (Image)resources.GetObject("btnAdd.ImageOptions.Image");
			this.btnAdd.Location = new Point(203, 111);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(87, 23);
			this.btnAdd.TabIndex = 18;
			this.btnAdd.Text = "Thêm mới";
			this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
			this.btnChooseFolder.Cursor = Cursors.Hand;
			this.btnChooseFolder.ImageOptions.Image = (Image)resources.GetObject("btnChooseFolder.ImageOptions.Image");
			this.btnChooseFolder.Location = new Point(556, 51);
			this.btnChooseFolder.Name = "btnChooseFolder";
			this.btnChooseFolder.Size = new System.Drawing.Size(92, 23);
			this.btnChooseFolder.TabIndex = 66;
			this.btnChooseFolder.Text = "Chọn Folder";
			this.btnChooseFolder.Click += new EventHandler(this.btnChooseFolder_Click);
			this.btnClose.Cursor = Cursors.Hand;
			this.btnClose.ImageOptions.Image = (Image)resources.GetObject("btnClose.ImageOptions.Image");
			this.btnClose.Location = new Point(397, 111);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(83, 23);
			this.btnClose.TabIndex = 17;
			this.btnClose.Text = "Hủy";
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			this.btnUpdate.Cursor = Cursors.Hand;
			this.btnUpdate.ImageOptions.Image = (Image)resources.GetObject("btnUpdate.ImageOptions.Image");
			this.btnUpdate.Location = new Point(300, 111);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(87, 23);
			this.btnUpdate.TabIndex = 16;
			this.btnUpdate.Text = "Cập nhật";
			this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
			this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.labelControl5.Appearance.ForeColor = Color.Red;
			this.labelControl5.Appearance.Options.UseFont = true;
			this.labelControl5.Appearance.Options.UseForeColor = true;
			this.labelControl5.Location = new Point(194, 59);
			this.labelControl5.Name = "labelControl5";
			this.labelControl5.Size = new System.Drawing.Size(6, 13);
			this.labelControl5.TabIndex = 65;
			this.labelControl5.Text = "*";
			this.labelControl6.Location = new Point(190, 56);
			this.labelControl6.Name = "labelControl6";
			this.labelControl6.Size = new System.Drawing.Size(14, 13);
			this.labelControl6.TabIndex = 64;
			this.labelControl6.Text = "(  )";
			this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.labelControl2.Appearance.ForeColor = Color.Red;
			this.labelControl2.Appearance.Options.UseFont = true;
			this.labelControl2.Appearance.Options.UseForeColor = true;
			this.labelControl2.Location = new Point(193, 32);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(6, 13);
			this.labelControl2.TabIndex = 63;
			this.labelControl2.Text = "*";
			this.labelControl4.Location = new Point(189, 29);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(14, 13);
			this.labelControl4.TabIndex = 62;
			this.labelControl4.Text = "(  )";
			this.txtId.Location = new Point(549, 27);
			this.txtId.Name = "txtId";
			this.txtId.Size = new System.Drawing.Size(10, 20);
			this.txtId.TabIndex = 18;
			this.txtId.Visible = false;
			this.gridSetup.Location = new Point(5, 149);
			this.gridSetup.MainView = this.viewSetup;
			this.gridSetup.Name = "gridSetup";
			this.gridSetup.RepositoryItems.AddRange(new RepositoryItem[] { this.btnDelete, this.chkCheckKeyGrid });
			this.gridSetup.Size = new System.Drawing.Size(687, 179);
			this.gridSetup.TabIndex = 17;
			this.gridSetup.ViewCollection.AddRange(new BaseView[] { this.viewSetup });
			this.viewSetup.Columns.AddRange(new GridColumn[] { this.ColId, this.ColCode, this.ColFilePath, this.colDelete });
			this.viewSetup.GridControl = this.gridSetup;
			this.viewSetup.Name = "viewSetup";
			this.viewSetup.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
			this.viewSetup.OptionsView.ShowGroupPanel = false;
			this.viewSetup.FocusedRowChanged += new FocusedRowChangedEventHandler(this.viewSetup_FocusedRowChanged);
			this.ColId.AppearanceHeader.Options.UseTextOptions = true;
			this.ColId.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.ColId.Caption = "Id";
			this.ColId.FieldName = "Id";
			this.ColId.Name = "ColId";
			this.ColCode.AppearanceHeader.Options.UseTextOptions = true;
			this.ColCode.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.ColCode.Caption = "Mã";
			this.ColCode.FieldName = "Code";
			this.ColCode.Name = "ColCode";
			this.ColCode.OptionsColumn.AllowEdit = false;
			this.ColCode.OptionsColumn.ReadOnly = true;
			this.ColCode.Visible = true;
			this.ColCode.VisibleIndex = 0;
			this.ColCode.Width = 120;
			this.ColFilePath.AppearanceHeader.Options.UseTextOptions = true;
			this.ColFilePath.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.ColFilePath.Caption = "Thư mục lưu trữ";
			this.ColFilePath.FieldName = "FilePath";
			this.ColFilePath.Name = "ColFilePath";
			this.ColFilePath.OptionsColumn.AllowEdit = false;
			this.ColFilePath.OptionsColumn.ReadOnly = true;
			this.ColFilePath.Visible = true;
			this.ColFilePath.VisibleIndex = 1;
			this.ColFilePath.Width = 549;
			this.colDelete.AppearanceHeader.Options.UseTextOptions = true;
			this.colDelete.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.colDelete.Caption = "Xóa";
			this.colDelete.ColumnEdit = this.btnDelete;
			this.colDelete.Name = "colDelete";
			this.colDelete.Visible = true;
			this.colDelete.VisibleIndex = 2;
			this.colDelete.Width = 80;
			this.btnDelete.AutoHeight = false;
			editorButtonImageOptions1.Image = (Image)resources.GetObject("editorButtonImageOptions1.Image");
			this.btnDelete.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new KeyShortcut(Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, ToolTipAnchor.Default) });
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.TextEditStyle = TextEditStyles.HideTextEditor;
			this.btnDelete.ButtonClick += new ButtonPressedEventHandler(this.btnDelete_ButtonClick);
			this.chkCheckKeyGrid.AutoHeight = false;
			this.chkCheckKeyGrid.Name = "chkCheckKeyGrid";
			this.txtCode.Enabled = false;
			this.txtCode.Location = new Point(207, 27);
			this.txtCode.Name = "txtCode";
			this.txtCode.Size = new System.Drawing.Size(343, 20);
			this.txtCode.TabIndex = 1;
			this.labelControl3.Location = new Point(172, 29);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(14, 13);
			this.labelControl3.TabIndex = 13;
			this.labelControl3.Text = "Mã";
			this.txtFilePath.Location = new Point(207, 53);
			this.txtFilePath.Name = "txtFilePath";
			this.txtFilePath.Size = new System.Drawing.Size(343, 20);
			this.txtFilePath.TabIndex = 2;
			this.labelControl1.Location = new Point(39, 56);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(147, 13);
			this.labelControl1.TabIndex = 0;
			this.labelControl1.Text = "Đường dẫn tới thư mục lưu trữ";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(701, 346);
			base.Controls.Add(this.groupControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmSetting";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Cấu hình hệ thống";
			base.Load += new EventHandler(this.frmSetting_Load);
			((ISupportInitialize)this.groupControl1).EndInit();
			this.groupControl1.ResumeLayout(false);
			this.groupControl1.PerformLayout();
			((ISupportInitialize)this.txtId.Properties).EndInit();
			((ISupportInitialize)this.gridSetup).EndInit();
			((ISupportInitialize)this.viewSetup).EndInit();
			((ISupportInitialize)this.btnDelete).EndInit();
			((ISupportInitialize)this.chkCheckKeyGrid).EndInit();
			((ISupportInitialize)this.txtCode.Properties).EndInit();
			((ISupportInitialize)this.txtFilePath.Properties).EndInit();
			base.ResumeLayout(false);
		}

		public void LoadData()
		{
			List<Setup> lstSetup = IoC.Resolve<ISetupService>().GetAll();
			this.sourse.DataSource = lstSetup;
			this.gridSetup.DataSource = this.sourse;
		}

		private bool Validation()
		{
			if (!(this.txtCode.Text == "") && !(this.txtFilePath.Text == ""))
			{
				return true;
			}
			return false;
		}

		private void viewSetup_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			this.entity = (Setup)this.viewSetup.GetRow(this.viewSetup.FocusedRowHandle);
			if (this.entity != null)
			{
				this.txtId.Text = this.entity.Id.ToString();
				this.txtCode.Text = this.entity.Code;
				this.txtFilePath.Text = this.entity.FilePath;
			}
		}
	}
}