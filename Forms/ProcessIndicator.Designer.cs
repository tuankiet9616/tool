using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashForm;
using DevExpress.XtraWaitForm;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Parse.Forms
{
	public class ProcessIndicator : WaitForm
	{
		private IContainer components;

		private ProgressPanel progressPanel1;

		private TableLayoutPanel tableLayoutPanel1;

		public ProcessIndicator()
		{
			this.InitializeComponent();
			this.progressPanel1.AutoHeight = true;
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
			this.progressPanel1 = new ProgressPanel();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.tableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.progressPanel1.Appearance.BackColor = Color.Transparent;
			this.progressPanel1.Appearance.Options.UseBackColor = true;
			this.progressPanel1.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f);
			this.progressPanel1.AppearanceCaption.Options.UseFont = true;
			this.progressPanel1.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			this.progressPanel1.AppearanceDescription.Options.UseFont = true;
			this.progressPanel1.Caption = "Vui lòng đợi trong giây lát";
			this.progressPanel1.Description = "Đang tải dữ liệu ...";
			this.progressPanel1.Dock = DockStyle.Fill;
			this.progressPanel1.ImageHorzOffset = 20;
			this.progressPanel1.Location = new Point(0, 17);
			this.progressPanel1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.progressPanel1.Name = "progressPanel1";
			this.progressPanel1.Size = new System.Drawing.Size(246, 39);
			this.progressPanel1.TabIndex = 0;
			this.progressPanel1.Text = "progressPanel1";
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.BackColor = Color.Transparent;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add(this.progressPanel1, 0, 0);
			this.tableLayoutPanel1.Dock = DockStyle.Fill;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 14, 0, 14);
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(246, 73);
			this.tableLayoutPanel1.TabIndex = 1;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.ClientSize = new System.Drawing.Size(246, 73);
			base.Controls.Add(this.tableLayoutPanel1);
			this.DoubleBuffered = true;
			base.Name = "ProcessIndicator";
			base.StartPosition = FormStartPosition.Manual;
			this.Text = "Form1";
			this.tableLayoutPanel1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public override void ProcessCommand(Enum cmd, object arg)
		{
			base.ProcessCommand(cmd, arg);
		}

		public override void SetCaption(string caption)
		{
			base.SetCaption(caption);
			this.progressPanel1.Caption = caption;
		}

		public override void SetDescription(string description)
		{
			base.SetDescription(description);
			this.progressPanel1.Description = description;
		}

		public enum WaitFormCommand
		{

		}
	}
}