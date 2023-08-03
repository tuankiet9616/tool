using FX.Core;
using FX.Data;
using log4net;
using Parse.Core.Domain;
using Parse.Core.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Parse.Core
{
	public static class PublishUtil
	{
		private static ILog log;

		private static IInvoiceVATService InvSrc;

		static PublishUtil()
		{
			PublishUtil.log = LogManager.GetLogger(typeof(PublishUtil));
			PublishUtil.InvSrc = IoC.Resolve<IInvoiceVATService>();
		}

		public static void UpdatePublishResult(List<InvoiceVAT> ListInv, PublishResultConverted result)
		{
			PublishUtil.InvSrc.BeginTran();
			try
			{
				foreach (Data datum in result.data)
				{
					InvoiceVAT inv = (
						from x in ListInv
						where x.Fkey == datum.Key
						select x).SingleOrDefault<InvoiceVAT>();
					inv.Serial = datum.InvSerial;
					inv.No = datum.InvNo;
					inv.Publish = PublishStatus.Success;
					inv.MessageError = "";
					PublishUtil.InvSrc.Update(inv);
				}
				PublishUtil.InvSrc.CommitTran();
			}
			catch (Exception exception)
			{
				PublishUtil.InvSrc.RolbackTran();
			}
		}
	}
}