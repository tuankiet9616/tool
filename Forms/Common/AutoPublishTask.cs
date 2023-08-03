using FX.Core;
using FX.Data;
using log4net;
using Parse.Core.Domain;
using Parse.Core.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ViettelAPI;
using ViettelAPI.Models;

namespace Parse.Forms.Common
{
	public class AutoPublishTask
	{
		private readonly static ILog log;

		private static Task PublishTask;

		private static int Duration;

		private static bool AllowRun;

		static AutoPublishTask()
		{
			AutoPublishTask.log = LogManager.GetLogger(typeof(AutoPublishTask));
			AutoPublishTask.Duration = 5;
			AutoPublishTask.AllowRun = true;
		}

		public AutoPublishTask()
		{
		}

		private static void PublishInv()
		{
			while (AutoPublishTask.AllowRun)
			{
				IBussinessLogService logService = IoC.Resolve<IBussinessLogService>();
				try
				{
					List<InvoiceVAT> lstInvoice = IoC.Resolve<IInvoiceVATService>().GetUnPublish();
					if(lstInvoice.Count == 0){
						continue;
					}
					foreach (InvoiceVAT invoice in lstInvoice)
					{
						List<ProductInv> lstprod = IoC.Resolve<IProductInvService>().GetByInvoiceID(invoice.Id);
						invoice.Products = lstprod;
					}
					string strlstFolio = string.Concat("Danh sách hóa đơn: ", lstInvoice[0].FolioNo);
					for (int i = 1; i < lstInvoice.Count; i++)
					{
						strlstFolio = string.Concat(strlstFolio, " - ", lstInvoice[i].FolioNo);
					}
					try
					{
						APIResults results = APIHelper.SendInvoices(IoC.Resolve<IApiParserService>().ConvertToAPIModel(lstInvoice));
						InvoiceHelper.UpdatePublishResult(lstInvoice, results);
						List<APIResult> successResults = (
							from p in results.createInvoiceOutputs
							where string.IsNullOrEmpty(p.errorCode)
							select p).ToList<APIResult>();
						List<APIResult> failResults = (
							from p in results.createInvoiceOutputs
							where !string.IsNullOrEmpty(p.errorCode)
							select p).ToList<APIResult>();
						string lstFail = "";
						if (failResults.Count > 0)
						{
							foreach (APIResult item in failResults)
							{
								lstFail = string.Concat(lstFail, " - ", item.transactionUuid.Split(new char[] { '-' }).ToList<string>()[item.transactionUuid.Split(new char[] { '-' }).ToList<string>().Count<string>() - 2]);
							}
						}
						BussinessLog Bussinesslog = new BussinessLog()
						{
							FileName = "Phát hành tự động hóa đơn",
							AppName = "Phát hành",
							CreateDate = DateTime.Now
						};
						if (failResults.Count<APIResult>() != 0)
						{
							Bussinesslog.Error = string.Format("Phát hành tự động hóa đơn thành công {0}/{1} - {2} - Hóa đơn lỗi: {3}", new object[] { successResults.Count<APIResult>(), lstInvoice.Count, strlstFolio, lstFail });
						}
						else
						{
							Bussinesslog.Error = string.Format("Phát hành tự động hóa đơn thành công - {0}", strlstFolio);
						}
						logService.CreateNew(Bussinesslog);
						logService.CommitChanges();
					}
					catch (Exception exception)
					{
						Exception ex = exception;
						BussinessLog Bussinesslog = new BussinessLog()
						{
							FileName = "Phát hành hóa đơn",
							AppName = "Phát hành",
							CreateDate = DateTime.Now,
							Error = string.Concat(strlstFolio, " + Lỗi: ", ex.Message)
						};
						logService.CreateNew(Bussinesslog);
						logService.CommitChanges();
						AutoPublishTask.log.Error(ex);
					}
				}
				catch (Exception exception1)
				{
					Exception ex = exception1;
					BussinessLog Bussinesslog = new BussinessLog()
					{
						FileName = "Phát hành hóa đơn",
						AppName = "Phát hành",
						CreateDate = DateTime.Now,
						Error = string.Concat("Lỗi: ", ex.Message)
					};
					logService.CreateNew(Bussinesslog);
					logService.CommitChanges();
					AutoPublishTask.log.Error(ex);
				}
				Thread.Sleep(1000 * AutoPublishTask.Duration);
			}
		}

		public static void Start(int duration)
		{
			if (duration >= 5)
			{
				AutoPublishTask.Duration = duration;
			}
			AutoPublishTask.PublishTask = Task.Factory.StartNew(() => AutoPublishTask.PublishInv());
		}

		public static void Stop()
		{
			AutoPublishTask.AllowRun = false;
			AutoPublishTask.PublishTask = null;
		}
	}
}