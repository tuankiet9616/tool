using FX.Data;
using Parse.Core.Domain;
using Parse.Core.IService;
using System;

namespace Parse.Core.Implement
{
	public class ConfigService : BaseService<Config, int>, IConfigService, IBaseService<Config, int>
	{
		public ConfigService(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath, "")
		{
		}
	}
}