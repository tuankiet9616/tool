using FX.Data;
using Parse.Core.Domain;
using Parse.Core.IService;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Parse.Core.Implement
{
	public class SetupService : BaseService<Setup, int>, ISetupService, IBaseService<Setup, int>
	{
		public SetupService(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath, "")
		{
		}

		public Setup GetbyCode(string code)
		{
			return (
				from x in base.Query
				where x.Code == code
				select x).SingleOrDefault<Setup>();
		}
	}
}