using Microsoft.Win32;
using System;
using System.IO;
using System.Security;

namespace Parse.Forms
{
	public static class InternetExplorerBrowserEmulation
	{
		private const string InternetExplorerRootKey = "Software\\Microsoft\\Internet Explorer";

		private const string BrowserEmulationKey = "Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION";

		public static BrowserEmulationVersion GetBrowserEmulationVersion()
		{
			BrowserEmulationVersion result = BrowserEmulationVersion.Default;
			try
			{
				RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
				if (key != null)
				{
					string programName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
					object value = key.GetValue(programName, null);
					if (value != null)
					{
						result = (BrowserEmulationVersion)Convert.ToInt32(value);
					}
				}
			}
			catch (SecurityException securityException)
			{
			}
			catch (UnauthorizedAccessException unauthorizedAccessException)
			{
			}
			return result;
		}

		public static int GetInternetExplorerMajorVersion()
		{
			int result = 0;
			try
			{
				RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Internet Explorer");
				if (key != null)
				{
					object value = key.GetValue("svcVersion", null) ?? key.GetValue("Version", null);
					if (value != null)
					{
						string version = value.ToString();
						int separator = version.IndexOf('.');
						if (separator != -1)
						{
							int.TryParse(version.Substring(0, separator), out result);
						}
					}
				}
			}
			catch (SecurityException securityException)
			{
			}
			catch (UnauthorizedAccessException unauthorizedAccessException)
			{
			}
			return result;
		}

		public static bool IsBrowserEmulationSet()
		{
			return InternetExplorerBrowserEmulation.GetBrowserEmulationVersion() != BrowserEmulationVersion.Default;
		}

		public static bool SetBrowserEmulationVersion(BrowserEmulationVersion browserEmulationVersion)
		{
			bool result = false;
			try
			{
				RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true) ?? Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION");
				string programName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);
				if (browserEmulationVersion == BrowserEmulationVersion.Default)
				{
					key.DeleteValue(programName, false);
				}
				else
				{
					key.SetValue(programName, (int)browserEmulationVersion, RegistryValueKind.DWord);
				}
				result = true;
			}
			catch (SecurityException securityException)
			{
			}
			catch (UnauthorizedAccessException unauthorizedAccessException)
			{
			}
			return result;
		}

		public static bool SetBrowserEmulationVersion()
		{
			BrowserEmulationVersion emulationCode;
			int ieVersion = InternetExplorerBrowserEmulation.GetInternetExplorerMajorVersion();
			if (ieVersion < 11)
			{
				switch (ieVersion)
				{
					case 8:
					{
						emulationCode = BrowserEmulationVersion.Version8;
						break;
					}
					case 9:
					{
						emulationCode = BrowserEmulationVersion.Version9;
						break;
					}
					case 10:
					{
						emulationCode = BrowserEmulationVersion.Version10;
						break;
					}
					default:
					{
						emulationCode = BrowserEmulationVersion.Version7;
						break;
					}
				}
			}
			else
			{
				emulationCode = BrowserEmulationVersion.Version11;
			}
			return InternetExplorerBrowserEmulation.SetBrowserEmulationVersion(emulationCode);
		}
	}
}