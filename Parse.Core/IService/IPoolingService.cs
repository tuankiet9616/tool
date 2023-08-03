using System;
using System.IO;

namespace Parse.Core.IService
{
	public interface IPoolingService
	{
		void ParseMicros(FileInfo[] files, string pattern);
	}
}