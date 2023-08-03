using DevExpress.XtraBars;
using DevExpress.XtraBars.Controls;
using DevExpress.XtraBars.Helpers.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using FX.Core;
using FX.Data;
using log4net;
using Parse.Core;
using Parse.Core.Domain;
using Parse.Core.IService;
using Parse.Forms.Common;
using Parse.Forms.CustomUC;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Parse.Forms
{
	public partial class frmMain : XtraForm, IMainForm
	{
		private readonly ILog log = LogManager.GetLogger(typeof(frmMain));

		private string comCode = (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["COM_CODE"]) ? ConfigurationManager.AppSettings["COM_CODE"].ToLower() : null);

		private List<CustomWatcher> Watchers = new List<CustomWatcher>();

		private IContainer components;

		private BarManager barManager1;

		private Bar bar2;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private BarSubItem barSubItem1;

		private BarButtonItem btnCompanyInfo;

		private BarButtonItem btnSetup;

		private BarButtonItem barButtonItem1;

		private BarButtonItem barButtonItem2;

		private BarButtonItem barButtonItem5;

		private BarButtonItem btnListInvoice;

		private BarButtonItem barButtonItem7;

		private PopupMenu popupMenu1;

		private BarSubItem barSubItem2;

		private OpenFileDialog openFileDialog;

		private NotifyIcon IconTaskBar;

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;

		private ToolStripMenuItem showToolStripMenuItem;

		private ToolStripMenuItem exitToolStripMenuItem;

		private PanelControl panelParent;

		private BarButtonItem btnInvoiceUnPublish;

		private BarButtonItem btnHome;

		private BarButtonItem btnBussinessLog;

		private BarButtonItem btnProxy;

		public frmMain()
		{
			this.InitializeComponent();
			InternetExplorerBrowserEmulation.SetBrowserEmulationVersion();
			this.SetMenuUpload();
			Version version = Assembly.GetExecutingAssembly().GetName().Version;
			this.Text = string.Format("Hóa đơn điện tử v{0}", version);
		}

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnHome = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.btnListInvoice = new DevExpress.XtraBars.BarButtonItem();
            this.btnBussinessLog = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.btnCompanyInfo = new DevExpress.XtraBars.BarButtonItem();
            this.btnSetup = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.btnInvoiceUnPublish = new DevExpress.XtraBars.BarButtonItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.IconTaskBar = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelParent = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelParent)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barSubItem1,
            this.barButtonItem2,
            this.btnCompanyInfo,
            this.btnSetup,
            this.barButtonItem5,
            this.btnListInvoice,
            this.barButtonItem7,
            this.barSubItem2,
            this.btnInvoiceUnPublish,
            this.btnHome,
            this.btnBussinessLog
            });
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 18;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnHome, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnListInvoice, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnBussinessLog, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnHome
            // 
            this.btnHome.Caption = "Chờ phát hành";
            this.btnHome.Id = 13;
            this.btnHome.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnHome.ImageOptions.Image")));
            this.btnHome.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnHome.ImageOptions.LargeImage")));
            this.btnHome.Name = "btnHome";
            this.btnHome.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnHome_ItemClick);
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "Upload";
            this.barSubItem2.Id = 8;
            this.barSubItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barSubItem2.ImageOptions.Image")));
            this.barSubItem2.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barSubItem2.ImageOptions.LargeImage")));
            this.barSubItem2.Name = "barSubItem2";
            // 
            // btnListInvoice
            // 
            this.btnListInvoice.Caption = "Đã phát hành";
            this.btnListInvoice.Id = 6;
            this.btnListInvoice.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnListInvoice.ImageOptions.Image")));
            this.btnListInvoice.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnListInvoice.ImageOptions.LargeImage")));
            this.btnListInvoice.Name = "btnListInvoice";
            this.btnListInvoice.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnListInvoice_ItemClick);
            // 
            // btnBussinessLog
            // 
            this.btnBussinessLog.Caption = "Log";
            this.btnBussinessLog.Id = 14;
            this.btnBussinessLog.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBussinessLog.ImageOptions.Image")));
            this.btnBussinessLog.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnBussinessLog.ImageOptions.LargeImage")));
            this.btnBussinessLog.Name = "btnBussinessLog";
            this.btnBussinessLog.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBussinessLog_ItemClick);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "Cấu Hình";
            this.barSubItem1.Id = 1;
            this.barSubItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barSubItem1.ImageOptions.Image")));
            this.barSubItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barSubItem1.ImageOptions.LargeImage")));
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnCompanyInfo, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSetup)
            });
            this.barSubItem1.Name = "barSubItem1";
            // 
            // btnCompanyInfo
            // 
            this.btnCompanyInfo.Caption = "Thông tin đơn vị";
            this.btnCompanyInfo.Id = 3;
            this.btnCompanyInfo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCompanyInfo.ImageOptions.Image")));
            this.btnCompanyInfo.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnCompanyInfo.ImageOptions.LargeImage")));
            this.btnCompanyInfo.Name = "btnCompanyInfo";
            this.btnCompanyInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCompanyInfo_ItemClick);
            // 
            // btnSetup
            // 
            this.btnSetup.Caption = "Cấu hình thư mục";
            this.btnSetup.Id = 4;
            this.btnSetup.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSetup.ImageOptions.Image")));
            this.btnSetup.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSetup.ImageOptions.LargeImage")));
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSetup_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlTop.Size = new System.Drawing.Size(983, 30);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 815);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlBottom.Size = new System.Drawing.Size(983, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 30);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 785);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(983, 30);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 785);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 2;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.ActAsDropDown = true;
            this.barButtonItem5.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.barButtonItem5.Caption = "Upload hóa đơn";
            this.barButtonItem5.DropDownControl = this.popupMenu1;
            this.barButtonItem5.Id = 5;
            this.barButtonItem5.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem5.ImageOptions.Image")));
            this.barButtonItem5.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem5.ImageOptions.LargeImage")));
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // popupMenu1
            // 
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "Phát hành hóa đơn";
            this.barButtonItem7.Id = 7;
            this.barButtonItem7.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem7.ImageOptions.Image")));
            this.barButtonItem7.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem7.ImageOptions.LargeImage")));
            this.barButtonItem7.Name = "barButtonItem7";
            // 
            // btnInvoiceUnPublish
            // 
            this.btnInvoiceUnPublish.Caption = "Hóa Đơn Chưa Phát Hành";
            this.btnInvoiceUnPublish.Id = 12;
            this.btnInvoiceUnPublish.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnInvoiceUnPublish.ImageOptions.Image")));
            this.btnInvoiceUnPublish.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnInvoiceUnPublish.ImageOptions.LargeImage")));
            this.btnInvoiceUnPublish.Name = "btnInvoiceUnPublish";
            // 
            // IconTaskBar
            // 
            this.IconTaskBar.ContextMenuStrip = this.contextMenuStrip;
            this.IconTaskBar.Icon = ((System.Drawing.Icon)(resources.GetObject("IconTaskBar.Icon")));
            this.IconTaskBar.Text = "Tools ParseData";
            this.IconTaskBar.Visible = true;
            this.IconTaskBar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.IconTaskBar_MouseDoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(115, 52);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // panelParent
            // 
            this.panelParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelParent.Location = new System.Drawing.Point(0, 30);
            this.panelParent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelParent.Name = "panelParent";
            this.panelParent.Size = new System.Drawing.Size(983, 785);
            this.panelParent.TabIndex = 19;
            this.panelParent.Paint += new System.Windows.Forms.PaintEventHandler(this.panelParent_Paint);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 815);
            this.Controls.Add(this.panelParent);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin hóa đơn";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelParent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
		}
	}
}