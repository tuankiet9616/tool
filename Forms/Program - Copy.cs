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


		[STAThread]
		private static void Main()
		{
			StringBuilder m_Sb  m_Sb = new StringBuilder();
			bool m_bDirty = false;
			System.IO.FileSystemWatcher m_Watcher;
			bool m_bIsWatching = false;

			m_bIsWatching = true;

			m_Watcher = new System.IO.FileSystemWatcher();
			m_Watcher.IncludeSubdirectories = true;
			m_Watcher.Filter = "*.*";
			m_Watcher.Path = txtFile.Text + "\\";

			m_Watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
								 | NotifyFilters.FileName | NotifyFilters.DirectoryName;
			m_Watcher.Changed += new FileSystemEventHandler(OnChanged);
			m_Watcher.Created += new FileSystemEventHandler(OnChanged);
			m_Watcher.Deleted += new FileSystemEventHandler(OnChanged);
			m_Watcher.Renamed += new RenamedEventHandler(OnRenamed);
			m_Watcher.EnableRaisingEvents = true;

			if (!m_bDirty)
			{
				m_Sb.Remove(0, m_Sb.Length);
				m_Sb.Append(e.FullPath);
				m_Sb.Append(" ");
				m_Sb.Append(e.ChangeType.ToString());
				m_Sb.Append("    ");
				m_Sb.Append(DateTime.Now.ToString());
				m_bDirty = true;
			}

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
								if (m_bIsWatching)
								{
									m_bIsWatching = false;
									m_Watcher.EnableRaisingEvents = false;
									m_Watcher.Dispose();
									btnWatchFile.BackColor = Color.LightSkyBlue;
									btnWatchFile.Text = "Start Watching";

								}
								else
								{
									m_bIsWatching = true;
									btnWatchFile.BackColor = Color.Red;
									btnWatchFile.Text = "Stop Watching";

									m_Watcher = new System.IO.FileSystemWatcher();
									if (rdbDir.Checked)
									{
										m_Watcher.Filter = "*.*";
										m_Watcher.Path = txtFile.Text + "\\";
									}
									else
									{
										m_Watcher.Filter = txtFile.Text.Substring(txtFile.Text.LastIndexOf('\\') + 1);
										m_Watcher.Path = txtFile.Text.Substring(0, txtFile.Text.Length - m_Watcher.Filter.Length);
									}

									if (chkSubFolder.Checked)
									{
										m_Watcher.IncludeSubdirectories = true;
									}

									m_Watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
														 | NotifyFilters.FileName | NotifyFilters.DirectoryName;
									m_Watcher.Changed += new FileSystemEventHandler(OnChanged);
									m_Watcher.Created += new FileSystemEventHandler(OnChanged);
									m_Watcher.Deleted += new FileSystemEventHandler(OnChanged);
									m_Watcher.Renamed += new RenamedEventHandler(OnRenamed);
									m_Watcher.EnableRaisingEvents = true;
								}
							}
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