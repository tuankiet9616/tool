using FX.Data;
using Parse.Core.Domain;
using System;
using System.Collections.Generic;

namespace Parse.Core.IService
{
	public interface IInvoiceVATService : IBaseService<InvoiceVAT, int>
	{
		bool isStateLess
		{
			get;
			set;
		}

		bool CreateNewInvoice(InvoiceVAT Invoice, out string message);

		bool DeleteInvoice(InvoiceVAT Inv, out string message);

		bool DeleteInvoices(List<InvoiceVAT> Invs, out string message);

		List<InvoiceVAT> FindPublishSuccess(DateTime? tuNgay, DateTime? denNgay, string keyword, ref int pageIndex, int pageSize, out int total);

		List<InvoiceVAT> FindUnPublish(DateTime? tuNgay, DateTime? denNgay, string keyword, ref int pageIndex, int pageSize, out int total);

		List<InvoiceVAT> GetPublishSuccess(ref int pageIndex, int pageSize, out int total);

		List<InvoiceVAT> GetPublishSuccess();

		List<InvoiceVAT> GetUnPublish(ref int pageIndex, int pageSize, out int total);

		List<InvoiceVAT> GetUnPublish();

		bool UpdateInvoice(List<InvoiceVAT> lstInv, out string message);

		bool UpdateInvoice(InvoiceVAT inv, out string message);

		bool UpdateNewInvoice(InvoiceVAT OInvoice, out string message);
	}
}