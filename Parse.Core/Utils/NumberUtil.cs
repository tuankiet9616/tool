using System;
using System.Security.Cryptography;
using System.Text;

namespace Parse.Core.Utils
{
	public class NumberUtil
	{
		public NumberUtil()
		{
		}

		public static long ConvertToUnixTime(DateTime datetime)
		{
			DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return (long)(datetime.ToUniversalTime() - sTime).TotalMilliseconds;
		}

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public static string DocCacSoRaChu(string number)
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
                s = decS.ToString();
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
			return string.Concat(strReturn.Trim().Substring(0, 1).ToUpper(), strReturn.Trim().Substring(1));
		}

		public static string DocSoThanhChu(string number)
		{
			string[] strArrays = new string[2];
			string[] lstSoTien = number.Split(new char[] { '.' });
			if ((int)lstSoTien.Length == 1 || lstSoTien[1] == "0")
			{
				return string.Concat(NumberUtil.DocCacSoRaChu(lstSoTien[0]), " đồng");
			}
			return string.Concat(NumberUtil.DocCacSoRaChu(lstSoTien[0]), " phẩy ", NumberUtil.DocCacSoRaChu(lstSoTien[1]).ToLower(), " đồng");
		}

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}