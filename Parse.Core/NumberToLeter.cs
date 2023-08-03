using Parse.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Parse.Core
{
	public static class NumberToLeter
	{
		private static string[] ChuSo;

		private static string[] Tien;

		static NumberToLeter()
		{
			NumberToLeter.ChuSo = new string[] { " không", " một", " hai", " ba", " bốn", " năm", " sáu", " bảy", " tám", " chín" };
			NumberToLeter.Tien = new string[] { "", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ" };
		}

		private static string AddZero(string str)
		{
			if (str.Length == 2)
			{
				str = string.Concat("0", str);
			}
			else if (str.Length == 1)
			{
				str = string.Concat("00", str);
			}
			return str;
		}

		public static string DocCacSoRaChu(decimal SoTien)
		{
			int lan;
			decimal so;
			string KetQua = "";
			string tmp = "";
			int[] ViTri = new int[6];
			if (SoTien < decimal.Zero)
			{
				return "Số tiền âm.";
			}
			if (SoTien == decimal.Zero)
			{
				return "Không";
			}
			so = (SoTien <= decimal.Zero ? -SoTien : SoTien);
			if (SoTien > new decimal(8999999999999999L))
			{
				SoTien = new decimal();
				return "";
			}
			ViTri[5] = (int)(so / new decimal(1000000000000000L));
			so -= long.Parse(ViTri[5].ToString()) * 1000000000000000L;
			ViTri[4] = (int)(so / new decimal(1000000000000L));
			so -= long.Parse(ViTri[4].ToString()) * 1000000000000L;
			ViTri[3] = (int)(so / new decimal(1000000000));
			so -= long.Parse(ViTri[3].ToString()) * (long)1000000000;
			ViTri[2] = (int)(so / new decimal(1000000));
			ViTri[1] = (int)((so % new decimal(1000000)) / new decimal(1000));
			ViTri[0] = (int)(so % new decimal(1000));
			if (ViTri[5] > 0)
			{
				lan = 5;
			}
			else if (ViTri[4] > 0)
			{
				lan = 4;
			}
			else if (ViTri[3] > 0)
			{
				lan = 3;
			}
			else if (ViTri[2] <= 0)
			{
				lan = (ViTri[1] <= 0 ? 0 : 1);
			}
			else
			{
				lan = 2;
			}
			for (int i = lan; i >= 0; i--)
			{
				bool isDoc = false;
				if (ViTri[i].ToString().Length < 3 && i < lan)
				{
					ViTri[i].ToString();
					isDoc = true;
				}
				tmp = NumberToLeter.DocSo3ChuSo(ViTri[i], isDoc);
				isDoc = false;
				KetQua = string.Concat(KetQua, tmp);
				if (ViTri[i] != 0)
				{
					KetQua = string.Concat(KetQua, NumberToLeter.Tien[i]);
				}
				if (i > 0 && !string.IsNullOrEmpty(tmp))
				{
					KetQua = KetQua ?? "";
				}
			}
			if (KetQua.Substring(KetQua.Length - 1, 1) == ",")
			{
				KetQua = KetQua.Substring(0, KetQua.Length - 1);
			}
			KetQua = KetQua.Trim();
			return string.Concat(KetQua.Substring(0, 1).ToUpper(), KetQua.Substring(1));
		}

		private static string DocSo3ChuSo(int baso, bool isDoc0)
		{
			string KetQua = "";
			int tram = baso / 100;
			int chuc = baso % 100 / 10;
			int donvi = baso % 10;
			if (tram == 0 && chuc == 0 && donvi == 0)
			{
				return "";
			}
			if (tram != 0 | isDoc0)
			{
				KetQua = string.Concat(KetQua, NumberToLeter.ChuSo[tram], " trăm");
				if (chuc == 0 && donvi != 0)
				{
					KetQua = string.Concat(KetQua, " linh");
				}
			}
			if (chuc != 0 && chuc != 1)
			{
				KetQua = string.Concat(KetQua, NumberToLeter.ChuSo[chuc], " mươi");
				if (chuc == 0 && donvi != 0)
				{
					KetQua = string.Concat(KetQua, " linh");
				}
			}
			if (chuc == 1)
			{
				KetQua = string.Concat(KetQua, " mười");
			}
			if (donvi == 1)
			{
				KetQua = (chuc == 0 || chuc == 1 ? string.Concat(KetQua, NumberToLeter.ChuSo[donvi]) : string.Concat(KetQua, " mốt"));
			}
			else if (donvi == 5)
			{
				KetQua = (chuc != 0 ? string.Concat(KetQua, " lăm") : string.Concat(KetQua, NumberToLeter.ChuSo[donvi]));
			}
			else if (donvi != 0)
			{
				KetQua = string.Concat(KetQua, NumberToLeter.ChuSo[donvi]);
			}
			return KetQua;
		}

		public static string DocTienBangChu(decimal SoTien)
		{
			string[] strArrays = new string[2];
			List<decimal> lstSoTien = SoTien.ToString().Split(new char[] { '.' }).Select<string, decimal>(new Func<string, decimal>(decimal.Parse)).ToList<decimal>();
			if (lstSoTien.Count == 1 || lstSoTien[1] == decimal.Zero)
			{
				return string.Concat(NumberUtil.DocCacSoRaChu(lstSoTien[0].ToString()), " đồng");
			}
			return string.Concat(NumberUtil.DocCacSoRaChu(lstSoTien[0].ToString()), " phẩy ", NumberToLeter.DocCacSoRaChu(lstSoTien[1]).ToLower(), " đồng");
		}
	}
}