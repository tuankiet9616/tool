using FX.Data;
using Parse.Core.Domain;
using Parse.Core.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Parse.Core.Implement
{
	public class BussinessLogService : BaseService<BussinessLog, int>, IBussinessLogService, IBaseService<BussinessLog, int>
	{
		public BussinessLogService(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath, "")
		{
		}

		public List<BussinessLog> GetByPaging(int pageIndex, int pageSize)
		{
            List<BussinessLog> bussinessLogs = new List<BussinessLog>();

            if (pageIndex == 0)
			{
                bussinessLogs = (
                    from x in base.Query
                    orderby x.CreateDate descending
                    select x).ToList<BussinessLog>();
                base.UnbindSession(bussinessLogs);
                return bussinessLogs;
			}

            bussinessLogs = (
                from x in base.Query
                orderby x.CreateDate descending
                select x).Skip<BussinessLog>((pageIndex - 1) * pageSize).Take<BussinessLog>(pageSize).ToList<BussinessLog>();
            base.UnbindSession(bussinessLogs);
            return bussinessLogs;
		}
	}
}