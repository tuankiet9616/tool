using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using FX.Core;
using Parse.Core;
using Parse.Core.Domain;
using Parse.Core.IService;
using Parse.Forms.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Parse.Core.Implement;

namespace Parse.Forms
{
    partial class frmMain
    {
		public void AddUC(UserControl uc)
		{
			this.panelParent.Controls.Clear();
			this.panelParent.AutoScroll = true;
			this.panelParent.Controls.Add(uc);
			uc.Dock = DockStyle.Fill;
		}

		private void btnBussinessLog_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.AddUC(new ucBussinessLog());
		}

		private void btnCompanyInfo_ItemClick(object sender, ItemClickEventArgs e)
		{
			frmCompany frm = new frmCompany();
			if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				AppContext.InitContext(frm.COM);
			}
		}

		private void btnHome_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.AddUC(new ucInvoiceVAT(this));
		}

		private void btnListInvoice_ItemClick(object sender, ItemClickEventArgs e)
		{
			this.AddUC(new ucInvoiceList(this));
		}

		private void btnParser_ItemClick(object sender, ItemClickEventArgs e)
		{
			BarButtonItem button = e.Item as BarButtonItem;
			MenuModel menu = MenuModel.GetById(button.Id);
			if (menu == null)
			{
				XtraMessageBox.Show(string.Concat("Không tìm thấy menu: ", button.Caption), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			IBussinessLogService logService = IoC.Resolve<IBussinessLogService>();
			string filter = (menu.FileExtensions == null || menu.FileExtensions.Length == 0 ? "*.*" : string.Join(";",
				from x in menu.FileExtensions
				select string.Concat("*", x)));
			this.openFileDialog.Filter = string.Concat("Files (", filter, ") | ", filter);
			this.openFileDialog.Multiselect = false;
			this.openFileDialog.Title = string.Concat("Upload file ", button.Caption);
			if (this.openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string message = "";
				SplashScreenManager.ShowForm(typeof(ProcessIndicator));
				try
				{
					try
					{
						int invSuccess = 0;
						int invTotal = 0;
						string mesError = "";
						if (menu.FileExtensions.Contains<string>(Path.GetExtension(this.openFileDialog.FileName).ToLower()))
						{
                            if (Path.GetExtension(this.openFileDialog.FileName).ToLower() == ".csv") {
                                IParserService parserService = ParserResolveHelper.Resolve("IExcelParserService");
                                List<InvoiceVAT> invoices = parserService.GetInvoiceDataXml(Application.StartupPath, ref invSuccess, ref invTotal, ref mesError, this.openFileDialog.FileName);
                                IInvoiceVATService service = IoC.Resolve<IInvoiceVATService>();
                                if (invoices.Count <= 0)
                                {
                                    throw new Exception(mesError);
                                }
                                InvoiceVATService.log.Error(string.Format("--------- Start UpdateInvoice From Excel : {0}", Newtonsoft.Json.JsonConvert.SerializeObject(invoices)));
                                if (!service.UpdateInvoice(invoices, out message))
                                {
                                    InvoiceVATService.log.Error(string.Format("--------- UpdateInvoice From Excel Exception : {0}", message));
                                    throw new Exception(message);
                                }
                                InvoiceVATService.log.Error(string.Format("--------- Finish UpdateInvoice From Excel -----------------"));
                            } else {
                                IParserService parserService = null;
                                try
                                {
                                    parserService = ParserResolveHelper.Resolve(menu.ServiceName);
                                }
                                catch (Exception exception)
                                {
                                    Exception ex = exception;
                                    this.log.Error(ex);
                                    XtraMessageBox.Show(string.Concat("Lỗi:", ex.Message), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                    return;
                                }
                                List<InvoiceVAT> invoices = parserService.GetInvoiceData(this.openFileDialog.FileName, ref invSuccess, ref invTotal, ref mesError);
                                IInvoiceVATService service = IoC.Resolve<IInvoiceVATService>();
                                if (invoices.Count <= 0)
                                {
                                    throw new Exception(mesError);
                                }
                                if (!service.UpdateInvoice(invoices, out message))
                                {
                                    throw new Exception(message);
                                }
                            }
						}
						this.AddUC(new ucInvoiceVAT(this));
						BussinessLog Bussinesslog = new BussinessLog()
						{
							FileName = this.openFileDialog.FileName,
							AppName = button.Name,
							CreateDate = DateTime.Now,
							Error = string.Format("{0}{1}/{2}: {3}", new object[] { "Upload hóa đơn thành công ", invSuccess, invTotal, mesError })
						};
						logService.CreateNew(Bussinesslog);
						logService.CommitChanges();
						XtraMessageBox.Show(string.Format("{0}{1}/{2}.", "Upload hóa đơn thành công ", invSuccess, invTotal), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					catch (Exception exception1)
					{
						Exception ex = exception1;
						BussinessLog Bussinesslog = new BussinessLog()
						{
							FileName = this.openFileDialog.FileName,
							AppName = button.Name,
							CreateDate = DateTime.Now,
							Error = ex.Message
						};
						logService.CreateNew(Bussinesslog);
						logService.CommitChanges();
						this.log.Error(ex);
						XtraMessageBox.Show("Có lỗi xảy ra, vui lòng thực hiện lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				finally
				{
					SplashScreenManager.CloseForm();
				}
			}
		}

		private void btnProxy_ItemClick(object sender, ItemClickEventArgs e)
		{
			(new frmProxy()).ShowDialog();
		}

		private void btnSetup_ItemClick(object sender, ItemClickEventArgs e)
		{
			(new frmSetting(this)).ShowDialog();
		}

		protected override void Dispose(bool disposing)
		{
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
            this.IconTaskBar.Dispose();
            this.Close();
            Application.ExitThread();
            Environment.Exit(Environment.ExitCode);
        }

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			base.ShowInTaskbar = false;
			base.Hide();
			this.IconTaskBar.Visible = true;
			this.IconTaskBar.ShowBalloonTip(1000, "Invoice", "Pin to taskbar. Click to open.", ToolTipIcon.Info);
		}

		public void frmMain_Load(object sender, EventArgs e)
		{
			this.IconTaskBar.Visible = false;
			if (AppContext.Current.company == null && XtraMessageBox.Show("Thông tin đơn vị chưa được khởi tạo. Thêm ngay bây giờ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.Yes)
			{
				(new frmCompany()).ShowDialog();
			}
			this.AddUC(new ucInvoiceVAT(this));
			this.InitWatcher();

            Thread thr = new Thread(new ThreadStart(frmMain.AutoUpdateCQTJob));
            thr.Start();
        }

        private static void AutoUpdateCQTJob()
        {
            string pathFile = System.IO.Directory.GetCurrentDirectory();
            int milliseconds = 30000; // 30s
            while (true)
            {
                string domain = Parse.Core.AppContext.Current.company.Domain;
                string username = Parse.Core.AppContext.Current.company.UserName;
                string password = Parse.Core.AppContext.Current.company.PassWord;

                Process p = new Process();
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.CreateNoWindow = true;
                string folderLog = pathFile + "\\log\\Auto_Check_CQT";
                string args = string.Format("{0} {1} {2} {3} {4}", "\"" + pathFile + "\"", username, password, domain, "\"" + folderLog + "\"");
                p.StartInfo.Arguments = args;
                p.StartInfo.FileName = pathFile + "\\Auto_Check_CQT.exe";
                p.Start();
                p.WaitForExit();
                Thread.Sleep(milliseconds);
            }
        }

        private void IconTaskBar_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			base.ShowInTaskbar = true;
			this.IconTaskBar.Visible = false;
			base.Show();
		}

		public void InitWatcher()
		{
			List<Setup> list = IoC.Resolve<ISetupService>().GetAll();
			if (list.Count == 0)
			{
				return;
			}
			foreach (MenuModel menuItem in MenuModel.MenuItems)
			{
				Setup config = list.FirstOrDefault<Setup>((Setup p) => p.Code == menuItem.Code);
				if (config == null || !Directory.Exists(config.FilePath))
				{
					continue;
				}
				CustomWatcher watcher = new CustomWatcher()
				{
					Menu = menuItem,
					Path = config.FilePath,
					NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite | NotifyFilters.LastAccess
				};
				watcher.Created += new FileSystemEventHandler(this.WatcherOnChanged);
				watcher.EnableRaisingEvents = true;
				this.Watchers.Add(watcher);
			}
		}

		private void SetMenuUpload()
		{
			foreach (MenuModel item in MenuModel.MenuItems)
			{
				BarButtonItem barButton = new BarButtonItem()
				{
					Id = item.Id,
					Caption = item.Caption,
					ImageUri = item.ImageUri,
					Name = item.Code
				};
				barButton.ItemClick += new ItemClickEventHandler(this.btnParser_ItemClick);
				this.barSubItem2.AddItem(barButton);
			}
		}

		private void showToolStripMenuItem_Click(object sender, EventArgs e)
		{
			base.ShowInTaskbar = true;
			this.IconTaskBar.Visible = false;
			base.Show();
		}

		private void WatcherOnChanged(object source, FileSystemEventArgs e)
		{
			IBussinessLogService logService = IoC.Resolve<IBussinessLogService>();
			IoC.Resolve<ISetupService>();
			CustomWatcher currentWatcher = (CustomWatcher)source;
			string[] fileExtensions = currentWatcher.Menu.FileExtensions;
			string fileName = e.FullPath;
			FileInfo file = new FileInfo(fileName);
			if (!fileExtensions.Contains<string>(file.Extension))
			{
				return;
			}
			string message = "";
			string folderPath = ((FileSystemWatcher)source).Path;
			try
			{
				try
				{
					IParserService parserService = null;
					try
					{
						parserService = ParserResolveHelper.Resolve(currentWatcher.Menu.ServiceName);
					}
					catch (Exception exception)
					{
						Exception ex = exception;
						this.log.Error(ex);
						XtraMessageBox.Show(string.Concat("Lỗi:", ex.Message), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
					int invSuccess = 0;
					int invTotal = 0;
					string mesError = "";
					List<InvoiceVAT> invoices = parserService.GetInvoiceData(file.FullName, ref invSuccess, ref invTotal, ref mesError);
					IInvoiceVATService service = IoC.Resolve<IInvoiceVATService>();
					if (invoices.Count > 0 && !service.UpdateInvoice(invoices, out message))
					{
						throw new Exception(message);
					}
					BussinessLog Bussinesslog = new BussinessLog()
					{
						FileName = file.FullName,
						AppName = currentWatcher.Menu.Code,
						CreateDate = DateTime.Now,
						Error = string.Format("{0}{1}/{2}: {3}.", new object[] { "Upload hóa đơn thành công ", invSuccess, invTotal, mesError })
					};
					logService.CreateNew(Bussinesslog);
					logService.CommitChanges();
					File.Delete(file.FullName);
					this.IconTaskBar.Visible = true;
					this.IconTaskBar.ShowBalloonTip(1000, "Thông báo", "Có hóa đơn mới chờ phát hành!", ToolTipIcon.Info);
					this.IconTaskBar.Visible = false;
				}
				catch (Exception exception1)
				{
					Exception ex = exception1;
					BussinessLog Bussinesslog = new BussinessLog()
					{
						FileName = file.FullName,
						AppName = currentWatcher.Menu.Code,
						CreateDate = DateTime.Now,
						Error = ex.Message
					};
					logService.CreateNew(Bussinesslog);
					logService.CommitChanges();
					DateTime now = DateTime.Now;
					string errorDir = Path.Combine(folderPath, "ERROR", now.ToString("yyyyMMdd"));
					if (!Directory.Exists(errorDir))
					{
						Directory.CreateDirectory(errorDir);
					}
					File.Move(file.FullName, Path.Combine(errorDir, file.Name));
					this.IconTaskBar.Visible = true;
					this.IconTaskBar.ShowBalloonTip(1000, "Thông báo", "Hóa đơn lỗi!", ToolTipIcon.Warning);
					this.IconTaskBar.Visible = false;
				}
			}
			catch (Exception exception2)
			{
				Exception ex = exception2;
				BussinessLog Bussinesslog = new BussinessLog()
				{
					FileName = fileName,
					AppName = currentWatcher.Menu.Code,
					CreateDate = DateTime.Now,
					Error = ex.Message
				};
				logService.CreateNew(Bussinesslog);
				logService.CommitChanges();
				this.log.Error(ex);
				this.IconTaskBar.Visible = true;
				this.IconTaskBar.ShowBalloonTip(1000, "Thông báo", "Hóa đơn lỗi!", ToolTipIcon.Warning);
				this.IconTaskBar.Visible = false;
			}
		}
		private void panelParent_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }
    }
}
