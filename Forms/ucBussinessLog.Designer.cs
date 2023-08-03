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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using FX.Core;
using Parse.Core.IService;
using Parse.Forms.CustomUC;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Parse.Forms
{
	public partial class ucBussinessLog : UserControl
	{
		private IContainer components;

		private PanelControl panelControl1;

		private GridControl gridBussinessLog;

		private GridView viewBussinessLog;

		private LabelControl labelControl1;

		private GridColumn colId;

		private GridColumn colFileName;

		private GridColumn colAppName;

		private GridColumn colCreateDate;

		private RepositoryItemDateEdit dtCreateDate;

		private GridColumn colError;

		private UCPaging ucPaging;

		private RepositoryItemMemoEdit memoError;

		public ucBussinessLog()
		{
			this.InitializeComponent();
			this.ucPaging.PageSize = 30;
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
			EditorButtonImageOptions editorButtonImageOptions1 = new EditorButtonImageOptions();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(ucBussinessLog));
			SerializableAppearanceObject serializableAppearanceObject1 = new SerializableAppearanceObject();
			SerializableAppearanceObject serializableAppearanceObject2 = new SerializableAppearanceObject();
			SerializableAppearanceObject serializableAppearanceObject3 = new SerializableAppearanceObject();
			SerializableAppearanceObject serializableAppearanceObject4 = new SerializableAppearanceObject();
			this.panelControl1 = new PanelControl();
			this.labelControl1 = new LabelControl();
			this.gridBussinessLog = new GridControl();
			this.viewBussinessLog = new GridView();
			this.colId = new GridColumn();
			this.colFileName = new GridColumn();
			this.colAppName = new GridColumn();
			this.colCreateDate = new GridColumn();
			this.dtCreateDate = new RepositoryItemDateEdit();
			this.colError = new GridColumn();
			this.memoError = new RepositoryItemMemoEdit();
			this.ucPaging = new UCPaging();
			((ISupportInitialize)this.panelControl1).BeginInit();
			this.panelControl1.SuspendLayout();
			((ISupportInitialize)this.gridBussinessLog).BeginInit();
			((ISupportInitialize)this.viewBussinessLog).BeginInit();
			((ISupportInitialize)this.dtCreateDate).BeginInit();
			((ISupportInitialize)this.dtCreateDate.CalendarTimeProperties).BeginInit();
			((ISupportInitialize)this.memoError).BeginInit();
			base.SuspendLayout();
			this.panelControl1.Controls.Add(this.labelControl1);
			this.panelControl1.Dock = DockStyle.Top;
			this.panelControl1.Location = new Point(0, 0);
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Size = new System.Drawing.Size(877, 30);
			this.panelControl1.TabIndex = 0;
			this.labelControl1.Anchor = AnchorStyles.Top;
			this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.labelControl1.Appearance.Options.UseFont = true;
			this.labelControl1.Location = new Point(387, 8);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(89, 14);
			this.labelControl1.TabIndex = 0;
			this.labelControl1.Text = "BUSSINESS LOG";
			this.gridBussinessLog.Dock = DockStyle.Fill;
			this.gridBussinessLog.Location = new Point(0, 30);
			this.gridBussinessLog.MainView = this.viewBussinessLog;
			this.gridBussinessLog.Name = "gridBussinessLog";
			this.gridBussinessLog.RepositoryItems.AddRange(new RepositoryItem[] { this.dtCreateDate, this.memoError });
			this.gridBussinessLog.Size = new System.Drawing.Size(877, 524);
			this.gridBussinessLog.TabIndex = 1;
			this.gridBussinessLog.ViewCollection.AddRange(new BaseView[] { this.viewBussinessLog });
			this.viewBussinessLog.Columns.AddRange(new GridColumn[] { this.colId, this.colFileName, this.colAppName, this.colCreateDate, this.colError });
			this.viewBussinessLog.GridControl = this.gridBussinessLog;
			this.viewBussinessLog.GroupPanelText = "Kéo một cột vào đây để xem dữ liệu theo nhóm";
			this.viewBussinessLog.Name = "viewBussinessLog";
			this.viewBussinessLog.OptionsBehavior.EditingMode = GridEditingMode.EditFormInplace;
			this.viewBussinessLog.OptionsEditForm.ActionOnModifiedRowChange = EditFormModifiedAction.Nothing;
			this.viewBussinessLog.OptionsEditForm.EditFormColumnCount = 2;
			this.viewBussinessLog.OptionsEditForm.ShowOnDoubleClick = DefaultBoolean.True;
			this.viewBussinessLog.OptionsEditForm.ShowUpdateCancelPanel = DefaultBoolean.False;
			this.viewBussinessLog.OptionsFind.FindNullPrompt = "Nhập dữ liệu cần tìm...";
			this.viewBussinessLog.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
			this.viewBussinessLog.OptionsView.ShowFooter = true;
			this.viewBussinessLog.MouseDown += new MouseEventHandler(this.viewBussinessLog_MouseDown);
			this.colId.AppearanceHeader.Options.UseTextOptions = true;
			this.colId.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.colId.Caption = "Id";
			this.colId.FieldName = "Id";
			this.colId.Name = "colId";
			this.colId.OptionsColumn.AllowEdit = false;
			this.colId.OptionsColumn.ReadOnly = true;
			this.colFileName.AppearanceHeader.Options.UseTextOptions = true;
			this.colFileName.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.colFileName.Caption = "Thư mục";
			this.colFileName.FieldName = "FileName";
			this.colFileName.Name = "colFileName";
			this.colFileName.OptionsColumn.AllowEdit = false;
			this.colFileName.OptionsColumn.ReadOnly = true;
			this.colFileName.OptionsEditForm.ColumnSpan = 2;
			this.colFileName.OptionsEditForm.UseEditorColRowSpan = false;
			this.colFileName.Visible = true;
			this.colFileName.VisibleIndex = 0;
			this.colFileName.Width = 200;
			this.colAppName.AppearanceCell.Options.UseTextOptions = true;
			this.colAppName.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
			this.colAppName.AppearanceHeader.Options.UseTextOptions = true;
			this.colAppName.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.colAppName.Caption = "Tên App";
			this.colAppName.FieldName = "AppName";
			this.colAppName.Name = "colAppName";
			this.colAppName.OptionsColumn.AllowEdit = false;
			this.colAppName.OptionsColumn.ReadOnly = true;
			this.colAppName.Visible = true;
			this.colAppName.VisibleIndex = 1;
			this.colAppName.Width = 92;
			this.colCreateDate.AppearanceCell.Options.UseTextOptions = true;
			this.colCreateDate.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
			this.colCreateDate.AppearanceHeader.Options.UseTextOptions = true;
			this.colCreateDate.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.colCreateDate.Caption = "Ngày tạo";
			this.colCreateDate.ColumnEdit = this.dtCreateDate;
			this.colCreateDate.FieldName = "CreateDate";
			this.colCreateDate.Name = "colCreateDate";
			this.colCreateDate.OptionsColumn.AllowEdit = false;
			this.colCreateDate.OptionsColumn.ReadOnly = true;
			this.colCreateDate.Visible = true;
			this.colCreateDate.VisibleIndex = 2;
			this.colCreateDate.Width = 92;
			this.dtCreateDate.AutoHeight = false;
			editorButtonImageOptions1.Image = (Image)resources.GetObject("editorButtonImageOptions1.Image");
			this.dtCreateDate.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new KeyShortcut(Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, ToolTipAnchor.Default) });
			this.dtCreateDate.CalendarTimeProperties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
			this.dtCreateDate.DisplayFormat.FormatString = "dd\\/MM\\/yyyy";
			this.dtCreateDate.DisplayFormat.FormatType = FormatType.DateTime;
			this.dtCreateDate.EditFormat.FormatString = "dd\\/MM\\/yyyy";
			this.dtCreateDate.EditFormat.FormatType = FormatType.DateTime;
			this.dtCreateDate.Mask.EditMask = "dd\\/MM\\/yyyy";
			this.dtCreateDate.Mask.UseMaskAsDisplayFormat = true;
			this.dtCreateDate.Name = "dtCreateDate";
			this.dtCreateDate.NullDate = "";
			this.colError.AppearanceHeader.Options.UseTextOptions = true;
			this.colError.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
			this.colError.Caption = "Nội dung";
			this.colError.ColumnEdit = this.memoError;
			this.colError.FieldName = "Error";
			this.colError.Name = "colError";
			this.colError.OptionsColumn.AllowEdit = false;
			this.colError.OptionsColumn.ReadOnly = true;
			this.colError.OptionsEditForm.RowSpan = 4;
			this.colError.Visible = true;
			this.colError.VisibleIndex = 3;
			this.colError.Width = 475;
			this.memoError.Name = "memoError";
			this.ucPaging.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.ucPaging.BackColor = Color.FromArgb(235, 236, 239);
			this.ucPaging.Location = new Point(2, 524);
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
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.ucPaging);
			base.Controls.Add(this.gridBussinessLog);
			base.Controls.Add(this.panelControl1);
			base.Name = "ucBussinessLog";
			base.Size = new System.Drawing.Size(877, 554);
			base.Load += new EventHandler(this.ucBussinessLog_Load);
			((ISupportInitialize)this.panelControl1).EndInit();
			this.panelControl1.ResumeLayout(false);
			this.panelControl1.PerformLayout();
			((ISupportInitialize)this.gridBussinessLog).EndInit();
			((ISupportInitialize)this.viewBussinessLog).EndInit();
			((ISupportInitialize)this.dtCreateDate.CalendarTimeProperties).EndInit();
			((ISupportInitialize)this.dtCreateDate).EndInit();
			((ISupportInitialize)this.memoError).EndInit();
			base.ResumeLayout(false);
		}

		public void LoadData(int PageIndex)
		{
			this.gridBussinessLog.DataSource = IoC.Resolve<IBussinessLogService>().GetByPaging(this.ucPaging.PageIndex, this.ucPaging.PageSize);
			this.ucPaging.PageIndex = PageIndex;
			this.ucPaging.UpdatePagingState();
		}

		private void ucBussinessLog_Load(object sender, EventArgs e)
		{
			this.LoadData(this.ucPaging.PageIndex);
		}

		private void UCPaging_Click(object sender, PagingEventArgs e)
		{
			SimpleButton button = (SimpleButton)sender;
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
			}
		}

		private void viewBussinessLog_MouseDown(object sender, MouseEventArgs e)
		{
			GridHitInfo hitInfo = this.viewBussinessLog.CalcHitInfo(e.Location);
			if (this.viewBussinessLog.IsEditFormVisible && hitInfo.InRowCell && e.Clicks == 1)
			{
				this.viewBussinessLog.CloseEditForm();
			}
		}
	}
}