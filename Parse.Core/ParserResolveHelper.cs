using FX.Core;
using Parse.Core.IService;
using System;

namespace Parse.Core
{
	public class ParserResolveHelper
	{
		public ParserResolveHelper()
		{
		}

		public static IParserService Resolve(string serviceName)
		{
			return IoC.Resolve<IParserService>(serviceName);
		}
	}
}