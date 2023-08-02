using System;

namespace ViettelAPI.Utils
{
	public class NumberUtil
	{
		public NumberUtil()
		{
		}

		public static string DocSoThanhChu(string number)
		{
			int chuc;
			int tram;
			string strReturn = "";
			string s = number;
			while (s.Length > 0 && s.Substring(0, 1) == "0")
			{
				s = s.Substring(1);
			}
			string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
			string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
			bool booAm = false;
			decimal decS = new decimal();
			try
			{
				decS = Convert.ToDecimal(s.ToString());
			}
			catch
			{
			}
			if (decS < decimal.Zero)
			{
				decS = -decS;
				booAm = true;
			}
			int i = s.Length;
			if (i != 0)
			{
				int j = 0;
				while (i > 0)
				{
					int donvi = Convert.ToInt32(s.Substring(i - 1, 1));
					i--;
					chuc = (i <= 0 ? -1 : Convert.ToInt32(s.Substring(i - 1, 1)));
					i--;
					tram = (i <= 0 ? -1 : Convert.ToInt32(s.Substring(i - 1, 1)));
					i--;
					if (donvi > 0 || chuc > 0 || tram > 0 || j == 3)
					{
						strReturn = string.Concat(hang[j], strReturn);
					}
					j++;
					if (j > 3)
					{
						j = 1;
					}
					if (donvi == 1 && chuc > 1)
					{
						strReturn = string.Concat("mốt ", strReturn);
					}
					else if (donvi == 5 && chuc > 0)
					{
						strReturn = string.Concat("lăm ", strReturn);
					}
					else if (donvi > 0)
					{
						strReturn = string.Concat(so[donvi], " ", strReturn);
					}
					if (chuc < 0)
					{
						break;
					}
					if (chuc == 0 && donvi > 0)
					{
						strReturn = string.Concat("linh ", strReturn);
					}
					if (chuc == 1)
					{
						strReturn = string.Concat("mười ", strReturn);
					}
					if (chuc > 1)
					{
						strReturn = string.Concat(so[chuc], " mươi ", strReturn);
					}
					if (tram < 0)
					{
						break;
					}
					if (tram > 0 || chuc > 0 || donvi > 0)
					{
						strReturn = string.Concat(so[tram], " trăm ", strReturn);
					}
					strReturn = string.Concat(" ", strReturn);
				}
			}
			else
			{
				strReturn = string.Concat(so[0], strReturn);
			}
			if (booAm)
			{
				strReturn = string.Concat("Âm ", strReturn);
			}
			return string.Concat(strReturn.Trim().Substring(0, 1).ToUpper(), strReturn.Trim().Substring(1), " đồng chẵn");
		}
	}
}