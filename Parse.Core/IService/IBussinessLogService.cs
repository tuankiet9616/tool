using FX.Data;
using Parse.Core.Domain;
using System;
using System.Collections.Generic;

namespace Parse.Core.IService
{
	public interface IBussinessLogService : IBaseService<BussinessLog, int>
	{
		List<BussinessLog> GetByPaging(int pageIndex, int pageSize);
	}
}