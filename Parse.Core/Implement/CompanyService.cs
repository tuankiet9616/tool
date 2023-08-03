using FX.Data;
using Parse.Core.Domain;
using Parse.Core.IService;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Parse.Core.Implement
{
	public class CompanyService : BaseService<Company, int>, ICompanyService, IBaseService<Company, int>
	{
		public CompanyService(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath, "")
		{
		}

		public Company GetCompanyByCode(string code)
		{
			return base.Query.FirstOrDefault<Company>((Company p) => p.Code.ToLower() == code);
		}
	}
}