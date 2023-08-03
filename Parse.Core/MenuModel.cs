using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Parse.Core
{
	public class MenuModel
	{
		private static List<MenuModel> _MenuItems;

		public string Caption
		{
			get;
			set;
		}

		public string Code
		{
			get;
			set;
		}

		public string[] FileExtensions
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public string ImageUri
		{
			get;
			set;
		}

		public static List<MenuModel> MenuItems
		{
			get
			{
				if (MenuModel._MenuItems == null || MenuModel._MenuItems.Count == 0)
				{
					string MENU_CONFIG = string.Concat(ConfigurationManager.AppSettings["COM_CODE"], ".json");
					string configPath = string.Concat(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]), "\\Config\\", MENU_CONFIG.ToLower());
					if (!string.IsNullOrEmpty(configPath) && File.Exists(configPath))
					{
						using (StreamReader r = new StreamReader(configPath))
						{
							MenuModel._MenuItems = JsonConvert.DeserializeObject<List<MenuModel>>(r.ReadToEnd());
						}
					}
				}
				return MenuModel._MenuItems;
			}
		}

		public string ServiceName
		{
			get;
			set;
		}

		static MenuModel()
		{
			MenuModel._MenuItems = new List<MenuModel>();
		}

		public MenuModel()
		{
		}

		public static MenuModel GetByCode(string code)
		{
			return MenuModel.MenuItems.FirstOrDefault<MenuModel>((MenuModel x) => x.Code.ToLower() == code.ToLower());
		}

		public static MenuModel GetById(int id)
		{
			return MenuModel.MenuItems.FirstOrDefault<MenuModel>((MenuModel x) => x.Id == id);
		}
	}
}