using FX.Core;
using log4net;
using log4net.Config;
using Microsoft.Win32;
using Parse.Core;
using Parse.Core.Domain;
using Parse.Core.IService;
using Parse.Forms.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Parse.Forms
{
	internal static class Program
	{
		static StringBuilder m_Sb = new StringBuilder();
		static bool m_bDirty = true;
		static System.IO.FileSystemWatcher m_Watcher;
		static bool m_bIsWatching = true;
		private readonly static ILog log;
		private static void OnUploadXML()
		{
			IBussinessLogService logService = IoC.Resolve<IBussinessLogService>();
			string message = "";
			IParserService parserService = ParserResolveHelper.Resolve("IExcelParserService");
			int invSuccess = 0;
			int invTotal = 0;
			string mesError = "";
				//@"D:\ftp\\toolVina\\InvoicesXML\\invoices.xml"
			List <InvoiceVAT> invoices = parserService.GetInvoiceDataXml(Application.StartupPath, ref invSuccess, ref invTotal, ref mesError, null);
			IInvoiceVATService service = IoC.Resolve<IInvoiceVATService>();
			//if (invoices.Count <= 0)
			//{
			//}
			if (!service.UpdateInvoice(invoices, out message))
			{
				mesError = mesError + " - " + message;
				BussinessLog Bussinesslog = new BussinessLog()
				{
					FileName = "Upload file XML hóa đơn",
					AppName = "Upload file XML",
					CreateDate = DateTime.Now,
					Error = string.Concat("Lỗi: ", mesError)
				};
				logService.CreateNew(Bussinesslog);
				logService.CommitChanges();
			}
			
		}
		private static void OnChanged(object sender, FileSystemEventArgs e)
		{
			OnUploadXML();
			m_Sb.Remove(0, m_Sb.Length);
			m_Sb.Append(e.FullPath);
			m_Sb.Append(" ");
			m_Sb.Append(e.ChangeType.ToString());
			m_Sb.Append("    ");
			m_Sb.Append(DateTime.Now.ToString());
		}

		private static void OnRenamed(object sender, RenamedEventArgs e)
		{
			m_Sb.Remove(0, m_Sb.Length);
			m_Sb.Append(e.OldFullPath);
			m_Sb.Append(" ");
			m_Sb.Append(e.ChangeType.ToString());
			m_Sb.Append(" ");
			m_Sb.Append("to ");
			m_Sb.Append(e.Name);
			m_Sb.Append("    ");
			m_Sb.Append(DateTime.Now.ToString());
		}

		[STAThread]
		private static void Main()
		{
			bool ownmutex = false;
			string lower;
			bool flag;
			string key = Application.ProductName;
			RegistryKey registry = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
			if (registry.GetValue(key) == null)
			{
				registry.SetValue(key, string.Concat("\"", Application.ExecutablePath.ToString(), "\""));
			}
			using (Mutex mutex = new Mutex(true, Application.ProductName, out ownmutex))
			{
				if (!ownmutex)
				{
					Application.Exit();
				}
				else
				{
					XmlConfigurator.ConfigureAndWatch(new FileInfo(string.Concat(AppDomain.CurrentDomain.BaseDirectory, "Config/logging.config")));
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Bootstrapper.InitializeContainer();
					CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
					Thread.CurrentThread.CurrentCulture = culture;
					try
					{
						ProxyConfig config = ProxyConfig.GetConfig();
						if (config != null)
						{
							config.SetProxy();
						}
						if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["COM_CODE"]))
						{
							lower = ConfigurationManager.AppSettings["COM_CODE"].ToLower();
						}
						else
						{
							lower = null;
						}
						Company companyByCode = IoC.Resolve<ICompanyService>().GetCompanyByCode(lower);
						Parse.Core.AppContext.InitContext(companyByCode);
						if (companyByCode != null)
						{
							if (Parse.Core.AppContext.Current.company.Config.ContainsKey("PUBLISH_TASK_AUTO_START"))
							{
								flag = (Convert.ToInt32(Parse.Core.AppContext.Current.company.Config["PUBLISH_TASK_AUTO_START"]) == 1 ? true : false);
							}
							else
							{
								flag = false;
							}
							if (flag)
							{
								AutoPublishTask.Start((Parse.Core.AppContext.Current.company.Config.ContainsKey("PUBLISH_TASK_DURATION") ? Convert.ToInt32(Parse.Core.AppContext.Current.company.Config["PUBLISH_TASK_DURATION"]) : 5));
							}
                            m_bIsWatching = true;

                            m_Watcher = new System.IO.FileSystemWatcher();
                            m_Watcher.Filter = "*.*";
                            //m_Watcher.Path = "D:\\ftp\\toolVina\\InvoicesXML\\";
							m_Watcher.Path = Application.StartupPath + @"\\toolVina\\InvoicesXML\\";

							m_Watcher.IncludeSubdirectories = true;

                            m_Watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                                 | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                            //m_Watcher.Changed += new FileSystemEventHandler(OnChanged);
                            m_Watcher.Created += new FileSystemEventHandler(OnChanged);
                            //m_Watcher.Deleted += new FileSystemEventHandler(OnChanged);
                            m_Watcher.Renamed += new RenamedEventHandler(OnRenamed);
                            m_Watcher.EnableRaisingEvents = true;
                        }
					}
					catch (Exception exception)
					{
						LogManager.GetLogger(typeof(Program)).Error(exception);
					}
					Application.Run(new frmMain());
					mutex.ReleaseMutex();
				}
			}
		}
	}
}