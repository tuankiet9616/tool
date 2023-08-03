using DevExpress.XtraBars;
using DevExpress.XtraBars.Commands;
using DevExpress.XtraBars.Controls;
using DevExpress.XtraBars.Helpers.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Container;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPdfViewer;
using DevExpress.XtraPdfViewer.Bars;
using Parse.Forms.CustomUC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Parse.Forms
{
	public class frmInvoicePdf : XtraForm
	{
		private IMainForm Main;

		private IContainer components;

		private PdfViewer pdfViewerInvoice;

		private PdfBarController pdfBarController1;

		private BarManager barManager1;

		private PdfCommandBar pdfCommandBar1;

		private PdfFileOpenBarItem pdfFileOpenBarItem1;

		private PdfFileSaveAsBarItem pdfFileSaveAsBarItem1;

		private PdfFilePrintBarItem pdfFilePrintBarItem1;

		private PdfFindTextBarItem pdfFindTextBarItem1;

		private PdfPreviousPageBarItem pdfPreviousPageBarItem1;

		private PdfNextPageBarItem pdfNextPageBarItem1;

		private PdfSetPageNumberBarItem pdfSetPageNumberBarItem1;

		private RepositoryItemPageNumberEdit repositoryItemPageNumberEdit1;

		private PdfZoomOutBarItem pdfZoomOutBarItem1;

		private PdfZoomInBarItem pdfZoomInBarItem1;

		private PdfExactZoomListBarSubItem pdfExactZoomListBarSubItem1;

		private PdfZoom10CheckItem pdfZoom10CheckItem1;

		private PdfZoom25CheckItem pdfZoom25CheckItem1;

		private PdfZoom50CheckItem pdfZoom50CheckItem1;

		private PdfZoom75CheckItem pdfZoom75CheckItem1;

		private PdfZoom100CheckItem pdfZoom100CheckItem1;

		private PdfZoom125CheckItem pdfZoom125CheckItem1;

		private PdfZoom150CheckItem pdfZoom150CheckItem1;

		private PdfZoom200CheckItem pdfZoom200CheckItem1;

		private PdfZoom400CheckItem pdfZoom400CheckItem1;

		private PdfZoom500CheckItem pdfZoom500CheckItem1;

		private PdfSetActualSizeZoomModeCheckItem pdfSetActualSizeZoomModeCheckItem1;

		private PdfSetPageLevelZoomModeCheckItem pdfSetPageLevelZoomModeCheckItem1;

		private PdfSetFitWidthZoomModeCheckItem pdfSetFitWidthZoomModeCheckItem1;

		private PdfSetFitVisibleZoomModeCheckItem pdfSetFitVisibleZoomModeCheckItem1;

		private PdfExportFormDataBarItem pdfExportFormDataBarItem1;

		private PdfImportFormDataBarItem pdfImportFormDataBarItem1;

		private PdfCommentBar pdfCommentBar1;

		private PdfTextHighlightBarItem pdfTextHighlightBarItem1;

		private PdfTextStrikethroughBarItem pdfTextStrikethroughBarItem1;

		private PdfTextUnderlineBarItem pdfTextUnderlineBarItem1;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		public frmInvoicePdf()
		{
			this.InitializeComponent();
		}

		public frmInvoicePdf(string pathFile, IMainForm main)
		{
			this.InitializeComponent();
			this.Main = main;
			this.ViewPdf(pathFile);
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
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(frmInvoicePdf));
			this.pdfViewerInvoice = new PdfViewer();
			this.barManager1 = new BarManager(this.components);
			this.pdfCommandBar1 = new PdfCommandBar();
			this.pdfFileOpenBarItem1 = new PdfFileOpenBarItem();
			this.pdfFileSaveAsBarItem1 = new PdfFileSaveAsBarItem();
			this.pdfFilePrintBarItem1 = new PdfFilePrintBarItem();
			this.pdfFindTextBarItem1 = new PdfFindTextBarItem();
			this.pdfPreviousPageBarItem1 = new PdfPreviousPageBarItem();
			this.pdfNextPageBarItem1 = new PdfNextPageBarItem();
			this.pdfSetPageNumberBarItem1 = new PdfSetPageNumberBarItem();
			this.repositoryItemPageNumberEdit1 = new RepositoryItemPageNumberEdit();
			this.pdfZoomOutBarItem1 = new PdfZoomOutBarItem();
			this.pdfZoomInBarItem1 = new PdfZoomInBarItem();
			this.pdfExactZoomListBarSubItem1 = new PdfExactZoomListBarSubItem();
			this.pdfZoom10CheckItem1 = new PdfZoom10CheckItem();
			this.pdfZoom25CheckItem1 = new PdfZoom25CheckItem();
			this.pdfZoom50CheckItem1 = new PdfZoom50CheckItem();
			this.pdfZoom75CheckItem1 = new PdfZoom75CheckItem();
			this.pdfZoom100CheckItem1 = new PdfZoom100CheckItem();
			this.pdfZoom125CheckItem1 = new PdfZoom125CheckItem();
			this.pdfZoom150CheckItem1 = new PdfZoom150CheckItem();
			this.pdfZoom200CheckItem1 = new PdfZoom200CheckItem();
			this.pdfZoom400CheckItem1 = new PdfZoom400CheckItem();
			this.pdfZoom500CheckItem1 = new PdfZoom500CheckItem();
			this.pdfSetActualSizeZoomModeCheckItem1 = new PdfSetActualSizeZoomModeCheckItem();
			this.pdfSetPageLevelZoomModeCheckItem1 = new PdfSetPageLevelZoomModeCheckItem();
			this.pdfSetFitWidthZoomModeCheckItem1 = new PdfSetFitWidthZoomModeCheckItem();
			this.pdfSetFitVisibleZoomModeCheckItem1 = new PdfSetFitVisibleZoomModeCheckItem();
			this.pdfExportFormDataBarItem1 = new PdfExportFormDataBarItem();
			this.pdfImportFormDataBarItem1 = new PdfImportFormDataBarItem();
			this.pdfCommentBar1 = new PdfCommentBar();
			this.pdfTextHighlightBarItem1 = new PdfTextHighlightBarItem();
			this.pdfTextStrikethroughBarItem1 = new PdfTextStrikethroughBarItem();
			this.pdfTextUnderlineBarItem1 = new PdfTextUnderlineBarItem();
			this.barDockControlTop = new BarDockControl();
			this.barDockControlBottom = new BarDockControl();
			this.barDockControlLeft = new BarDockControl();
			this.barDockControlRight = new BarDockControl();
			this.pdfBarController1 = new PdfBarController();
			((ISupportInitialize)this.barManager1).BeginInit();
			((ISupportInitialize)this.repositoryItemPageNumberEdit1).BeginInit();
			((ISupportInitialize)this.pdfBarController1).BeginInit();
			base.SuspendLayout();
			this.pdfViewerInvoice.Dock = DockStyle.Fill;
			this.pdfViewerInvoice.Location = new Point(0, 31);
			this.pdfViewerInvoice.MenuManager = this.barManager1;
			this.pdfViewerInvoice.Name = "pdfViewerInvoice";
			this.pdfViewerInvoice.NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden;
			this.pdfViewerInvoice.Size = new System.Drawing.Size(921, 663);
			this.pdfViewerInvoice.TabIndex = 0;
			this.barManager1.Bars.AddRange(new Bar[] { this.pdfCommandBar1, this.pdfCommentBar1 });
			this.barManager1.DockControls.Add(this.barDockControlTop);
			this.barManager1.DockControls.Add(this.barDockControlBottom);
			this.barManager1.DockControls.Add(this.barDockControlLeft);
			this.barManager1.DockControls.Add(this.barDockControlRight);
			this.barManager1.Form = this;
			this.barManager1.Items.AddRange(new BarItem[] { this.pdfFileOpenBarItem1, this.pdfFileSaveAsBarItem1, this.pdfFilePrintBarItem1, this.pdfFindTextBarItem1, this.pdfPreviousPageBarItem1, this.pdfNextPageBarItem1, this.pdfSetPageNumberBarItem1, this.pdfZoomOutBarItem1, this.pdfZoomInBarItem1, this.pdfExactZoomListBarSubItem1, this.pdfZoom10CheckItem1, this.pdfZoom25CheckItem1, this.pdfZoom50CheckItem1, this.pdfZoom75CheckItem1, this.pdfZoom100CheckItem1, this.pdfZoom125CheckItem1, this.pdfZoom150CheckItem1, this.pdfZoom200CheckItem1, this.pdfZoom400CheckItem1, this.pdfZoom500CheckItem1, this.pdfSetActualSizeZoomModeCheckItem1, this.pdfSetPageLevelZoomModeCheckItem1, this.pdfSetFitWidthZoomModeCheckItem1, this.pdfSetFitVisibleZoomModeCheckItem1, this.pdfTextHighlightBarItem1, this.pdfTextStrikethroughBarItem1, this.pdfTextUnderlineBarItem1, this.pdfExportFormDataBarItem1, this.pdfImportFormDataBarItem1 });
			this.barManager1.MaxItemId = 29;
			this.barManager1.RepositoryItems.AddRange(new RepositoryItem[] { this.repositoryItemPageNumberEdit1 });
			this.pdfCommandBar1.Control = this.pdfViewerInvoice;
			this.pdfCommandBar1.DockCol = 0;
			this.pdfCommandBar1.DockRow = 0;
			this.pdfCommandBar1.DockStyle = BarDockStyle.Top;
			this.pdfCommandBar1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.pdfFileOpenBarItem1), new LinkPersistInfo(this.pdfFileSaveAsBarItem1), new LinkPersistInfo(this.pdfFilePrintBarItem1), new LinkPersistInfo(this.pdfFindTextBarItem1), new LinkPersistInfo(this.pdfPreviousPageBarItem1), new LinkPersistInfo(this.pdfNextPageBarItem1), new LinkPersistInfo(this.pdfSetPageNumberBarItem1), new LinkPersistInfo(this.pdfZoomOutBarItem1), new LinkPersistInfo(this.pdfZoomInBarItem1), new LinkPersistInfo(this.pdfExactZoomListBarSubItem1), new LinkPersistInfo(this.pdfExportFormDataBarItem1), new LinkPersistInfo(this.pdfImportFormDataBarItem1) });
			this.pdfFileOpenBarItem1.Id = 0;
			this.pdfFileOpenBarItem1.ItemShortcut = new BarShortcut(Keys.LButton | Keys.RButton | Keys.Cancel | Keys.MButton | Keys.XButton1 | Keys.XButton2 | Keys.Back | Keys.Tab | Keys.LineFeed | Keys.Clear | Keys.Return | Keys.Enter | Keys.A | Keys.B | Keys.C | Keys.D | Keys.E | Keys.F | Keys.G | Keys.H | Keys.I | Keys.J | Keys.K | Keys.L | Keys.M | Keys.N | Keys.O | Keys.Control);
			this.pdfFileOpenBarItem1.Name = "pdfFileOpenBarItem1";
			this.pdfFileSaveAsBarItem1.Id = 1;
			this.pdfFileSaveAsBarItem1.ItemShortcut = new BarShortcut(Keys.LButton | Keys.RButton | Keys.Cancel | Keys.ShiftKey | Keys.ControlKey | Keys.Menu | Keys.Pause | Keys.A | Keys.B | Keys.C | Keys.P | Keys.Q | Keys.R | Keys.S | Keys.Control);
			this.pdfFileSaveAsBarItem1.Name = "pdfFileSaveAsBarItem1";
			this.pdfFilePrintBarItem1.Id = 2;
			this.pdfFilePrintBarItem1.ItemShortcut = new BarShortcut(Keys.ShiftKey | Keys.P | Keys.Control);
			this.pdfFilePrintBarItem1.Name = "pdfFilePrintBarItem1";
			this.pdfFindTextBarItem1.Id = 3;
			this.pdfFindTextBarItem1.ItemShortcut = new BarShortcut(Keys.RButton | Keys.MButton | Keys.XButton2 | Keys.B | Keys.D | Keys.F | Keys.Control);
			this.pdfFindTextBarItem1.Name = "pdfFindTextBarItem1";
			this.pdfPreviousPageBarItem1.Id = 4;
			this.pdfPreviousPageBarItem1.Name = "pdfPreviousPageBarItem1";
			this.pdfNextPageBarItem1.Id = 5;
			this.pdfNextPageBarItem1.Name = "pdfNextPageBarItem1";
			this.pdfSetPageNumberBarItem1.Edit = this.repositoryItemPageNumberEdit1;
			this.pdfSetPageNumberBarItem1.EditValue = 0;
			this.pdfSetPageNumberBarItem1.Enabled = false;
			this.pdfSetPageNumberBarItem1.Id = 6;
			this.pdfSetPageNumberBarItem1.Name = "pdfSetPageNumberBarItem1";
			this.repositoryItemPageNumberEdit1.AutoHeight = false;
			this.repositoryItemPageNumberEdit1.Mask.EditMask = "########;";
			this.repositoryItemPageNumberEdit1.Name = "repositoryItemPageNumberEdit1";
			this.repositoryItemPageNumberEdit1.Orientation = PagerOrientation.Horizontal;
			this.pdfZoomOutBarItem1.Id = 7;
			this.pdfZoomOutBarItem1.Name = "pdfZoomOutBarItem1";
			this.pdfZoomInBarItem1.Id = 8;
			this.pdfZoomInBarItem1.Name = "pdfZoomInBarItem1";
			this.pdfExactZoomListBarSubItem1.Id = 9;
			this.pdfExactZoomListBarSubItem1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.pdfZoom10CheckItem1, true), new LinkPersistInfo(this.pdfZoom25CheckItem1), new LinkPersistInfo(this.pdfZoom50CheckItem1), new LinkPersistInfo(this.pdfZoom75CheckItem1), new LinkPersistInfo(this.pdfZoom100CheckItem1), new LinkPersistInfo(this.pdfZoom125CheckItem1), new LinkPersistInfo(this.pdfZoom150CheckItem1), new LinkPersistInfo(this.pdfZoom200CheckItem1), new LinkPersistInfo(this.pdfZoom400CheckItem1), new LinkPersistInfo(this.pdfZoom500CheckItem1), new LinkPersistInfo(this.pdfSetActualSizeZoomModeCheckItem1, true), new LinkPersistInfo(this.pdfSetPageLevelZoomModeCheckItem1), new LinkPersistInfo(this.pdfSetFitWidthZoomModeCheckItem1), new LinkPersistInfo(this.pdfSetFitVisibleZoomModeCheckItem1) });
			this.pdfExactZoomListBarSubItem1.Name = "pdfExactZoomListBarSubItem1";
			this.pdfExactZoomListBarSubItem1.PaintStyle = BarItemPaintStyle.CaptionInMenu;
			this.pdfZoom10CheckItem1.Id = 10;
			this.pdfZoom10CheckItem1.Name = "pdfZoom10CheckItem1";
			this.pdfZoom25CheckItem1.Id = 11;
			this.pdfZoom25CheckItem1.Name = "pdfZoom25CheckItem1";
			this.pdfZoom50CheckItem1.Id = 12;
			this.pdfZoom50CheckItem1.Name = "pdfZoom50CheckItem1";
			this.pdfZoom75CheckItem1.Id = 13;
			this.pdfZoom75CheckItem1.Name = "pdfZoom75CheckItem1";
			this.pdfZoom100CheckItem1.Id = 14;
			this.pdfZoom100CheckItem1.Name = "pdfZoom100CheckItem1";
			this.pdfZoom125CheckItem1.Id = 15;
			this.pdfZoom125CheckItem1.Name = "pdfZoom125CheckItem1";
			this.pdfZoom150CheckItem1.Id = 16;
			this.pdfZoom150CheckItem1.Name = "pdfZoom150CheckItem1";
			this.pdfZoom200CheckItem1.Id = 17;
			this.pdfZoom200CheckItem1.Name = "pdfZoom200CheckItem1";
			this.pdfZoom400CheckItem1.Id = 18;
			this.pdfZoom400CheckItem1.Name = "pdfZoom400CheckItem1";
			this.pdfZoom500CheckItem1.Id = 19;
			this.pdfZoom500CheckItem1.Name = "pdfZoom500CheckItem1";
			this.pdfSetActualSizeZoomModeCheckItem1.Id = 20;
			this.pdfSetActualSizeZoomModeCheckItem1.Name = "pdfSetActualSizeZoomModeCheckItem1";
			this.pdfSetPageLevelZoomModeCheckItem1.Id = 21;
			this.pdfSetPageLevelZoomModeCheckItem1.Name = "pdfSetPageLevelZoomModeCheckItem1";
			this.pdfSetFitWidthZoomModeCheckItem1.Id = 22;
			this.pdfSetFitWidthZoomModeCheckItem1.Name = "pdfSetFitWidthZoomModeCheckItem1";
			this.pdfSetFitVisibleZoomModeCheckItem1.Id = 23;
			this.pdfSetFitVisibleZoomModeCheckItem1.Name = "pdfSetFitVisibleZoomModeCheckItem1";
			this.pdfExportFormDataBarItem1.Id = 24;
			this.pdfExportFormDataBarItem1.Name = "pdfExportFormDataBarItem1";
			this.pdfImportFormDataBarItem1.Id = 25;
			this.pdfImportFormDataBarItem1.Name = "pdfImportFormDataBarItem1";
			this.pdfCommentBar1.Control = this.pdfViewerInvoice;
			this.pdfCommentBar1.DockCol = 1;
			this.pdfCommentBar1.DockRow = 0;
			this.pdfCommentBar1.DockStyle = BarDockStyle.Top;
			this.pdfCommentBar1.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.pdfTextHighlightBarItem1), new LinkPersistInfo(this.pdfTextStrikethroughBarItem1), new LinkPersistInfo(this.pdfTextUnderlineBarItem1) });
			this.pdfTextHighlightBarItem1.ButtonStyle = BarButtonStyle.CheckDropDown;
			this.pdfTextHighlightBarItem1.Id = 26;
			this.pdfTextHighlightBarItem1.ImageOptions.Image = (Image)resources.GetObject("pdfTextHighlightBarItem1.ImageOptions.Image");
			this.pdfTextHighlightBarItem1.ImageOptions.LargeImage = (Image)resources.GetObject("pdfTextHighlightBarItem1.ImageOptions.LargeImage");
			this.pdfTextHighlightBarItem1.Name = "pdfTextHighlightBarItem1";
			this.pdfTextStrikethroughBarItem1.ButtonStyle = BarButtonStyle.CheckDropDown;
			this.pdfTextStrikethroughBarItem1.Id = 27;
			this.pdfTextStrikethroughBarItem1.Name = "pdfTextStrikethroughBarItem1";
			this.pdfTextUnderlineBarItem1.ButtonStyle = BarButtonStyle.CheckDropDown;
			this.pdfTextUnderlineBarItem1.Id = 28;
			this.pdfTextUnderlineBarItem1.Name = "pdfTextUnderlineBarItem1";
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = DockStyle.Top;
			this.barDockControlTop.Location = new Point(0, 0);
			this.barDockControlTop.Manager = this.barManager1;
			this.barDockControlTop.Size = new System.Drawing.Size(921, 31);
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = DockStyle.Bottom;
			this.barDockControlBottom.Location = new Point(0, 694);
			this.barDockControlBottom.Manager = this.barManager1;
			this.barDockControlBottom.Size = new System.Drawing.Size(921, 0);
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = DockStyle.Left;
			this.barDockControlLeft.Location = new Point(0, 31);
			this.barDockControlLeft.Manager = this.barManager1;
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 663);
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = DockStyle.Right;
			this.barDockControlRight.Location = new Point(921, 31);
			this.barDockControlRight.Manager = this.barManager1;
			this.barDockControlRight.Size = new System.Drawing.Size(0, 663);
			this.pdfBarController1.BarItems.Add(this.pdfFileOpenBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfFileSaveAsBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfFilePrintBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfFindTextBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfPreviousPageBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfNextPageBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfSetPageNumberBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoomOutBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoomInBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfExactZoomListBarSubItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom10CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom25CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom50CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom75CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom100CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom125CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom150CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom200CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom400CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfZoom500CheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfSetActualSizeZoomModeCheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfSetPageLevelZoomModeCheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfSetFitWidthZoomModeCheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfSetFitVisibleZoomModeCheckItem1);
			this.pdfBarController1.BarItems.Add(this.pdfTextHighlightBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfTextStrikethroughBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfTextUnderlineBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfExportFormDataBarItem1);
			this.pdfBarController1.BarItems.Add(this.pdfImportFormDataBarItem1);
			this.pdfBarController1.Control = this.pdfViewerInvoice;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(921, 694);
			base.Controls.Add(this.pdfViewerInvoice);
			base.Controls.Add(this.barDockControlLeft);
			base.Controls.Add(this.barDockControlRight);
			base.Controls.Add(this.barDockControlBottom);
			base.Controls.Add(this.barDockControlTop);
			base.Name = "frmInvoicePdf";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "In hóa đơn";
			((ISupportInitialize)this.barManager1).EndInit();
			((ISupportInitialize)this.repositoryItemPageNumberEdit1).EndInit();
			((ISupportInitialize)this.pdfBarController1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void ViewPdf(string pathFile)
		{
			this.pdfViewerInvoice.LoadDocument(pathFile);
		}
	}
}