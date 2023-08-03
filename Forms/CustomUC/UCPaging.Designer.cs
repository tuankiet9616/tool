using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Parse.Core.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace Parse.Forms.CustomUC
{
	public class UCPaging : UserControl
	{
		private PagingComponent paging = new PagingComponent(1, 30);

		private IContainer components;

		private PanelControl panelControl1;

		private SimpleButton btnFirst;

		private SimpleButton btnLast;

		private SimpleButton btnNext;

		private SimpleButton btnPrev;

		private LabelControl lblPageInfo;

		public int PageIndex
		{
			get
			{
				return this.paging.PageIndex;
			}
			set
			{
				this.paging.PageIndex = value;
			}
		}

		public int PageSize
		{
			get
			{
				return this.paging.PageSize;
			}
			set
			{
				this.paging.PageSize = value;
			}
		}

		public int? Total
		{
			get
			{
				return this.paging.Total;
			}
			set
			{
				this.paging.Total = value;
			}
		}

		public int? TotalPage
		{
			get
			{
				return this.paging.TotalPage;
			}
		}

		public UCPaging()
		{
			this.InitializeComponent();
			this.btnNext.Click += new EventHandler(this.OnNext_Click);
			this.btnPrev.Click += new EventHandler(this.OnPrev_Click);
			this.btnFirst.Click += new EventHandler(this.OnFirst_Click);
			this.btnLast.Click += new EventHandler(this.OnLast_Click);
			this.UpdatePagingState();
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
			ComponentResourceManager resources = new ComponentResourceManager(typeof(UCPaging));
			this.panelControl1 = new PanelControl();
			this.btnFirst = new SimpleButton();
			this.btnLast = new SimpleButton();
			this.btnNext = new SimpleButton();
			this.btnPrev = new SimpleButton();
			this.lblPageInfo = new LabelControl();
			((ISupportInitialize)this.panelControl1).BeginInit();
			this.panelControl1.SuspendLayout();
			base.SuspendLayout();
			this.panelControl1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.panelControl1.Appearance.BackColor = Color.Transparent;
			this.panelControl1.Appearance.BorderColor = Color.Transparent;
			this.panelControl1.Appearance.Options.UseBackColor = true;
			this.panelControl1.Appearance.Options.UseBorderColor = true;
			this.panelControl1.BorderStyle = BorderStyles.NoBorder;
			this.panelControl1.Controls.Add(this.btnFirst);
			this.panelControl1.Controls.Add(this.btnLast);
			this.panelControl1.Controls.Add(this.btnNext);
			this.panelControl1.Controls.Add(this.btnPrev);
			this.panelControl1.Controls.Add(this.lblPageInfo);
			this.panelControl1.Location = new Point(0, 0);
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Size = new System.Drawing.Size(200, 26);
			this.panelControl1.TabIndex = 47;
			this.btnFirst.ButtonStyle = BorderStyles.UltraFlat;
			this.btnFirst.Image = (Image)resources.GetObject("btnFirst.Image");
			this.btnFirst.Location = new Point(5, 4);
			this.btnFirst.Name = "btnFirst";
			this.btnFirst.Size = new System.Drawing.Size(24, 19);
			this.btnFirst.TabIndex = 44;
			this.btnLast.ButtonStyle = BorderStyles.UltraFlat;
			this.btnLast.Image = (Image)resources.GetObject("btnLast.Image");
			this.btnLast.Location = new Point(173, 4);
			this.btnLast.Name = "btnLast";
			this.btnLast.Size = new System.Drawing.Size(24, 19);
			this.btnLast.TabIndex = 43;
			this.btnNext.ButtonStyle = BorderStyles.UltraFlat;
			this.btnNext.Image = (Image)resources.GetObject("btnNext.Image");
			this.btnNext.Location = new Point(145, 4);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(24, 19);
			this.btnNext.TabIndex = 38;
			this.btnPrev.ButtonStyle = BorderStyles.UltraFlat;
			this.btnPrev.Image = (Image)resources.GetObject("btnPrev.Image");
			this.btnPrev.Location = new Point(35, 4);
			this.btnPrev.Name = "btnPrev";
			this.btnPrev.Size = new System.Drawing.Size(24, 19);
			this.btnPrev.TabIndex = 36;
			this.lblPageInfo.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
			this.lblPageInfo.AutoSizeMode = LabelAutoSizeMode.Vertical;
			this.lblPageInfo.Location = new Point(66, 6);
			this.lblPageInfo.Name = "lblPageInfo";
			this.lblPageInfo.Size = new System.Drawing.Size(74, 13);
			this.lblPageInfo.TabIndex = 37;
			this.lblPageInfo.Text = "1/1200";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.panelControl1);
			base.Name = "UCPaging";
			base.Size = new System.Drawing.Size(200, 26);
			((ISupportInitialize)this.panelControl1).EndInit();
			this.panelControl1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		protected void OnFirst_Click(object sender, EventArgs e)
		{
			if (this.FirstClick != null)
			{
				PagingEventArgs arg = new PagingEventArgs()
				{
					NextPageIndex = 1
				};
				this.FirstClick(sender, arg);
				this.UpdatePagingState();
			}
		}

		protected void OnLast_Click(object sender, EventArgs e)
		{
			if (this.LastClick != null)
			{
				PagingEventArgs arg = new PagingEventArgs()
				{
					NextPageIndex = (this.paging.TotalPage.HasValue ? this.paging.TotalPage.Value : 1)
				};
				this.LastClick(sender, arg);
				this.UpdatePagingState();
			}
		}

		protected void OnNext_Click(object sender, EventArgs e)
		{
			int pageIndex;
			if (this.NextClick != null)
			{
				PagingEventArgs arg = new PagingEventArgs();
				PagingEventArgs pagingEventArg = arg;
				if (this.paging.TotalPage.HasValue)
				{
					pageIndex = (this.PageIndex < this.paging.TotalPage.Value ? this.PageIndex + 1 : this.paging.TotalPage.Value);
				}
				else
				{
					pageIndex = this.PageIndex + 1;
				}
				pagingEventArg.NextPageIndex = pageIndex;
				this.NextClick(sender, arg);
				this.UpdatePagingState();
			}
		}

		protected void OnPrev_Click(object sender, EventArgs e)
		{
			if (this.PrevClick != null)
			{
				PagingEventArgs arg = new PagingEventArgs()
				{
					NextPageIndex = (this.PageIndex > 1 ? this.PageIndex - 1 : 1)
				};
				this.PrevClick(sender, arg);
				this.UpdatePagingState();
			}
		}

		public void UpdatePagingState()
		{
			this.btnNext.Enabled = this.paging.HasNext();
			this.btnPrev.Enabled = this.paging.HasPrev();
			this.btnLast.Enabled = this.paging.HasNext();
			this.btnFirst.Enabled = this.paging.HasPrev();
			if (!this.TotalPage.HasValue)
			{
				this.lblPageInfo.Text = string.Format("Trang {0}/1", this.PageIndex);
				return;
			}
			this.lblPageInfo.Text = string.Format("Trang {0}/{1}", this.PageIndex, (this.TotalPage.Value > 0 ? this.TotalPage.Value : 1));
		}

		public event PagingEventHandler FirstClick;

		public event PagingEventHandler LastClick;

		public event PagingEventHandler NextClick;

		public event PagingEventHandler PrevClick;
	}
}