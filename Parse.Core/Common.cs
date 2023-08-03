using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;

namespace Parse.Core
{
	public class Common
	{
		public static Dictionary<int, string> lstProductType;

		public static Dictionary<string, string> lstPayMethodType;

		public static Dictionary<float, string> lstTax;

		static Common()
		{
			Common.lstProductType = new Dictionary<int, string>()
			{
				{ 0, "Hàng bán" },
				{ 1, "Hàng khuyến mại" }
			};
			Common.lstPayMethodType = new Dictionary<string, string>()
			{
				{ "TM", "TM" },
				{ "CK", "CK" },
				{ "TM/CK", "TM/CK" },
				{ "Khác", "Khác" }
			};
			Common.lstTax = new Dictionary<float, string>()
			{
				{ -1f, "Không thuế" },
				{ 0f, "0%" },
				{ 5f, "5%" },
				{ 10f, "10%" },
				{ 15f, "15%" }
			};
		}

		public Common()
		{
		}
		public enum ProductType
		{
			Product,
			Free
		}
	}
}