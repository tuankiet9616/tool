using FX.Core;
using FX.Data;
using log4net;
using Parse.Core;
using Parse.Core.Domain;
using Parse.Core.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ViettelAPI.Models;

namespace ViettelAPI
{
	public class InvoiceHelper
	{
		private static ILog log;

		private static IInvoiceVATService InvSrc;

		private static IBussinessLogService logService;

		static InvoiceHelper()
		{
			InvoiceHelper.log = LogManager.GetLogger(typeof(InvoiceHelper));
			InvoiceHelper.InvSrc = IoC.Resolve<IInvoiceVATService>();
			InvoiceHelper.logService = IoC.Resolve<IBussinessLogService>();
		}

		public InvoiceHelper()
		{
		}

		public static void UpdatePublishResult(List<InvoiceVAT> ListInv, APIResults results)
		{
            InvoiceHelper.InvSrc.BeginTran();
            try
            {
                foreach (APIResult createInvoiceOutput in results.createInvoiceOutputs)
                {
                    InvoiceVAT inv = new InvoiceVAT();
                    inv = (
                        from x in ListInv
                        where x.Fkey == createInvoiceOutput.transactionUuid
                        select x).SingleOrDefault<InvoiceVAT>();
                    if (string.IsNullOrEmpty(createInvoiceOutput.errorCode) || createInvoiceOutput.errorCode == "200")
                    {
                        inv.Publish = PublishStatus.Success;
                        inv.MessageError = "";
                        if (!string.IsNullOrEmpty(createInvoiceOutput.result.invoiceNo))
                        {
                            inv.No = createInvoiceOutput.result.invoiceNo;
                            inv.Serial = createInvoiceOutput.result.invoiceNo.Substring(0, 6);
                        }
                        else
                        {
                            inv.No = null;
                            inv.Serial = null;
                        }
                        inv.Pattern = Parse.Core.AppContext.Current.company.InvPattern;
                    }
                    else
                    {
                        inv.Publish = PublishStatus.Error;
                        inv.MessageError = string.Format("{0}: {1}", createInvoiceOutput.errorCode, createInvoiceOutput.description);
                    }

                    if (inv.InvoiceNoSAP != null)
                    {
                        InvoiceHelper.InvSrc.Update(inv);
                    }
                }
                InvoiceHelper.InvSrc.CommitTran();
            }
            catch (Exception ex)
            {
                InvoiceHelper.InvSrc.RolbackTran();
                throw ex;
            }
        }

        public static void UpdatePublishResultV2(InvoiceVAT invoiceVAT, APIResult result)
        {
            InvoiceHelper.InvSrc.BeginTran();
            try
            {
                if (string.IsNullOrEmpty(result.errorCode))
                {
                    invoiceVAT.Publish = PublishStatus.Success;
                    invoiceVAT.MessageError = "";
                    if (!string.IsNullOrEmpty(result.result.invoiceNo))
                    {
                        invoiceVAT.No = result.result.invoiceNo;
                    }
                    else
                    {
                        invoiceVAT.No = null;
                    }
                    invoiceVAT.Pattern = Parse.Core.AppContext.Current.company.InvPattern;
                    invoiceVAT.Serial = result.result.invoiceNo.Substring(0, 6);
                }
                else
                {
                    invoiceVAT.Publish = PublishStatus.Error;
                    invoiceVAT.MessageError = string.Format("{0}: {1}", result.errorCode, result.description);
                }
                InvoiceHelper.InvSrc.Update(invoiceVAT);
                InvoiceHelper.InvSrc.CommitTran();
            }
            catch (Exception ex) {
                InvoiceHelper.InvSrc.RolbackTran();
                throw ex;
            }
        }

    }
}