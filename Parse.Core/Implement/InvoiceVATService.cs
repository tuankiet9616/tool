using FX.Core;
using FX.Data;
using log4net;
using Parse.Core;
using Parse.Core.Domain;
using Parse.Core.IService;
using Parse.Core.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Parse.Core.Implement
{
	public class InvoiceVATService : BaseService<InvoiceVAT, int>, IInvoiceVATService, IBaseService<InvoiceVAT, int>
	{
		public static ILog log;

		static InvoiceVATService()
		{
			InvoiceVATService.log = LogManager.GetLogger(typeof(InvoiceVATService));
		}

		public InvoiceVATService(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath, "")
		{
		}

		public bool CreateNewInvoice(InvoiceVAT NewInvoice, out string message)
		{
			bool flag;
			message = "";
			try
			{
				if (NewInvoice.VATRate != -1f)
				{
					string folioOrigin = NewInvoice.FolioOrigin;
					float vATRate = NewInvoice.VATRate;
					NewInvoice.FolioNo = string.Concat(folioOrigin, "_", vATRate.ToString());
				}
				else
				{
					NewInvoice.FolioNo = string.Concat(NewInvoice.FolioOrigin, "_KoThue");
				}
				if (NewInvoice.Products == null || NewInvoice.Products.Count == 0)
				{
					message = "Hóa đơn không có sản phẩm xin vui lòng nhập lại!";
					flag = false;
				}
				else
				{
					NewInvoice.Fkey = this.GetFkey(NewInvoice);
					base.BeginTran();
					IProductInvService prodSrv = IoC.Resolve<IProductInvService>();
					InvoiceVAT invNew = this.CreateNew(NewInvoice);
					foreach (ProductInv p in NewInvoice.Products)
					{
						p.InvoiceID = invNew.Id;
						prodSrv.CreateNew(p);
					}
					base.CommitTran();
					flag = true;
				}
			}
			catch (Exception exception)
			{
				message = "Chưa tạo được hóa đơn!";
				base.RolbackTran();
				flag = false;
			}
			return flag;
		}

		public bool DeleteInvoice(InvoiceVAT Inv, out string message)
		{
			bool flag;
			message = "";
			IProductInvService prodSrc = IoC.Resolve<IProductInvService>();
			try
			{
				base.BeginTran();
				this.Delete(Inv.Id);
				if (Inv.Products.Count > 0)
				{
					foreach (ProductInv prod in Inv.Products)
					{
						prodSrc.Delete(prod.Id);
					}
				}
				base.CommitTran();
				flag = true;
			}
			catch (Exception exception)
			{
				message = exception.Message;
				base.RolbackTran();
				flag = false;
			}
			return flag;
		}

		public bool DeleteInvoices(List<InvoiceVAT> Invs, out string message)
		{
			bool flag;
			message = "";
			IProductInvService prodSrc = IoC.Resolve<IProductInvService>();
			try
			{
				base.BeginTran();
				foreach (InvoiceVAT inv in Invs)
				{
					this.Delete(inv.Id);
					if (inv.Products.Count <= 0)
					{
						continue;
					}
					foreach (ProductInv prod in inv.Products)
					{
						prodSrc.Delete(prod.Id);
					}
				}
				base.CommitTran();
				flag = true;
			}
			catch (Exception exception)
			{
				message = exception.Message;
				base.RolbackTran();
				flag = false;
			}
			return flag;
		}

		public List<InvoiceVAT> FindPublishSuccess(DateTime? tuNgay, DateTime? denNgay, string strFind, ref int pageIndex, int pageSize, out int total)
		{
			IQueryable<InvoiceVAT> query = 
				from c in base.Query
				where (int)c.Publish == 1
				select c;
			if (tuNgay.HasValue)
			{
				query = 
					from c in query
					where (DateTime?)c.ArisingDate >= tuNgay
					select c;
			}
			if (denNgay.HasValue)
			{
				query = 
					from c in query
					where (DateTime?)c.ArisingDate <= denNgay
					select c;
			}
			if (!string.IsNullOrEmpty(strFind))
			{
				query = 
					from c in query
					where c.ArisingDate.ToString().Contains(strFind) || c.No.Contains(strFind) || c.CusName.Contains(strFind) || c.Buyer.Contains(strFind) || c.CusAddress.Contains(strFind) || c.CusTaxCode.Contains(strFind) || c.FolioNo.Contains(strFind) || c.Total.ToString().Contains(strFind) || c.VATRate.ToString().Contains(strFind) || c.VATAmount.ToString().Contains(strFind) || c.Amount.ToString().Contains(strFind) || c.AmountInWords.Contains(strFind)
					select c;
			}
			total = query.Count<InvoiceVAT>();
			query = 
				from x in query
				orderby x.Id descending
				select x;
			pageIndex = (Math.Ceiling(decimal.Parse(total.ToString()) / decimal.Parse(pageSize.ToString())) < pageIndex ? pageIndex - 1 : pageIndex);
			List<InvoiceVAT> list = query.Skip<InvoiceVAT>((pageIndex - 1) * pageSize).Take<InvoiceVAT>(pageSize).ToList<InvoiceVAT>();
			base.UnbindSession(list);
			return list;
		}

		public List<InvoiceVAT> FindUnPublish(DateTime? tuNgay, DateTime? denNgay, string strFind, ref int pageIndex, int pageSize, out int total)
		{
			IQueryable<InvoiceVAT> query = 
				from c in base.Query
				where (int)c.Publish != 1
				select c;
			if (tuNgay.HasValue)
			{
				query = 
					from c in query
					where (DateTime?)c.ArisingDate >= tuNgay
					select c;
			}
			if (denNgay.HasValue)
			{
				query = 
					from c in query
					where (DateTime?)c.ArisingDate <= denNgay
					select c;
			}
			if (!string.IsNullOrEmpty(strFind))
			{
				query = 
					from c in query
					where c.CusCode.Contains(strFind) || c.CusName.Contains(strFind) || c.Buyer.Contains(strFind) || c.CusAddress.Contains(strFind) || c.CusPhone.Contains(strFind) || c.CusTaxCode.Contains(strFind) || c.FolioNo.Contains(strFind) || c.Total.ToString().Contains(strFind) || c.VATRate.ToString().Contains(strFind) || c.VATAmount.ToString().Contains(strFind) || c.Amount.ToString().Contains(strFind) || c.AmountInWords.Contains(strFind)
					select c;
			}
			total = query.Count<InvoiceVAT>();
			query = 
				from x in query
				orderby x.Id descending
				select x;
			pageIndex = (Math.Ceiling(decimal.Parse(total.ToString()) / decimal.Parse(pageSize.ToString())) < pageIndex ? pageIndex - 1 : pageIndex);
			List<InvoiceVAT> list = query.Skip<InvoiceVAT>((pageIndex - 1) * pageSize).Take<InvoiceVAT>(pageSize).ToList<InvoiceVAT>();
			base.UnbindSession(list);
			return list;
		}

		public string GetFkey(InvoiceVAT invoice)
		{
            string fkeyCode = invoice.InvoiceNoSAP;
            string fkey = fkeyCode.Length < 36 ? fkeyCode : fkeyCode.Substring(0, 35);

            return fkey;
        }

		public List<InvoiceVAT> GetPublishSuccess(ref int pageIndex, int pageSize, out int total)
		{
			IQueryable<InvoiceVAT> query = 
				from c in base.Query
				where (int)c.Publish == 1
				select c;
			total = query.Count<InvoiceVAT>();
			query = 
				from x in query
				orderby x.Id descending
				select x;
			pageIndex = (Math.Ceiling(decimal.Parse(total.ToString()) / decimal.Parse(pageSize.ToString())) < pageIndex ? pageIndex - 1 : pageIndex);
			List<InvoiceVAT> list = query.Skip<InvoiceVAT>((pageIndex - 1) * pageSize).Take<InvoiceVAT>(pageSize).ToList<InvoiceVAT>();
			base.UnbindSession(list);
			return list;
		}

		public List<InvoiceVAT> GetPublishSuccess()
		{
			List<InvoiceVAT> list = (
				from c in base.Query
				where (int)c.Publish == 1
				select c).ToList<InvoiceVAT>();
			base.UnbindSession(list);
			return list;
		}

		public List<InvoiceVAT> GetUnPublish(ref int pageIndex, int pageSize, out int total)
		{
			IQueryable<InvoiceVAT> query = 
				from c in base.Query
				where (int)c.Publish != 1
				select c;
			total = query.Count<InvoiceVAT>();
			query = 
				from x in query
				orderby x.InvoiceNoSAP ascending
				select x;
			pageIndex = (Math.Ceiling(decimal.Parse(total.ToString()) / decimal.Parse(pageSize.ToString())) < pageIndex ? pageIndex - 1 : pageIndex);
			List<InvoiceVAT> list = query.Skip<InvoiceVAT>((pageIndex - 1) * pageSize).Take<InvoiceVAT>(pageSize).ToList<InvoiceVAT>();
			base.UnbindSession(list);
			return list;
		}

		public List<InvoiceVAT> GetUnPublish()
		{
			List<InvoiceVAT> list = (
				from c in base.Query
				where (int)c.Publish != 1
                orderby c.InvoiceNoSAP ascending
                select c).ToList<InvoiceVAT>();
			base.UnbindSession(list);
			return list;
		}

		//bool Parse.Core.IService.IInvoiceVATService.get_isStateLess()
		//{
		//	return base.isStateLess;
		//}

		//void Parse.Core.IService.IInvoiceVATService.set_isStateLess(bool value)
		//{
		//	base.isStateLess = value;
		//}

		public bool UpdateInvoice(List<InvoiceVAT> lstInv, out string message)
		{
			bool flag;
			message = "";
			IProductInvService prodSrc = IoC.Resolve<IProductInvService>();
			if (lstInv == null || lstInv.Count<InvoiceVAT>() == 0)
			{
				return false;
			}
			try
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				base.BeginTran();
				foreach (InvoiceVAT inv in lstInv)
				{
					this.CreateNew(inv);
					foreach (ProductInv prod in inv.Products)
					{
						prod.InvoiceID = inv.Id;
						prod.Quantity = (prod.Quantity > decimal.Zero ? prod.Quantity : decimal.One);
						prod.Price = prod.Price;
						prodSrc.CreateNew(prod);
					}
				}
				base.CommitTran();
				InvoiceVATService.log.Error(string.Format("Thoi gian xu ly:{0} Milliseconds", stopwatch.ElapsedMilliseconds));
				flag = true;
			}
			catch (Exception exception)
			{
				message = exception.Message;
				base.RolbackTran();
				flag = false;
			}
			return flag;
		}

		public bool UpdateInvoice(InvoiceVAT inv, out string message)
		{
			bool flag;
			message = "";
			IProductInvService prodSrc = IoC.Resolve<IProductInvService>();
			try
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				base.BeginTran();
				this.CreateNew(inv);
				foreach (ProductInv prod in inv.Products)
				{
					prod.InvoiceID = inv.Id;
					prod.Quantity = (prod.Quantity > decimal.Zero ? prod.Quantity : decimal.One);
					prod.Price = (prod.Price > decimal.Zero ? prod.Price : prod.Amount);
					prodSrc.CreateNew(prod);
				}
				base.CommitTran();
				InvoiceVATService.log.Error(string.Format("Thoi gian xu ly:{0} Milliseconds", stopwatch.ElapsedMilliseconds));
				flag = true;
			}
			catch (Exception exception)
			{
				message = exception.Message;
				base.RolbackTran();
				flag = false;
			}
			return flag;
		}

		public bool UpdateNewInvoice(InvoiceVAT OInvoice, out string message)
		{
			bool flag;
			message = "";
			base.BeginTran();
			try
			{
				if (OInvoice.Products == null || OInvoice.Products.Count == 0)
				{
					message = "Hóa đơn không có sản phẩm xin vui lòng nhập lại!";
					flag = false;
				}
				else
				{
					OInvoice.Fkey = this.GetFkey(OInvoice);
					IProductInvService prodSrv = IoC.Resolve<IProductInvService>();
					foreach (ProductInv p in (IEnumerable<ProductInv>)(
						from p in prodSrv.Query
						where p.InvoiceID == OInvoice.Id
						select p).ToList<ProductInv>())
					{
						prodSrv.Delete(p);
					}
					foreach (ProductInv p in OInvoice.Products)
					{
						p.InvoiceID = OInvoice.Id;
						prodSrv.CreateNew(p);
					}
					this.Update(OInvoice);
					base.CommitTran();
					flag = true;
				}
			}
			catch (Exception exception)
			{
				message = "Chưa tạo được hóa đơn!";
				base.RolbackTran();
				flag = false;
			}
			return flag;
		}
	}
}