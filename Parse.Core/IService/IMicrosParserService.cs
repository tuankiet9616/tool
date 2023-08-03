using System;

namespace Parse.Core.IService
{
	public interface IMicrosParserService : IParserService
	{
		string ParseToText(string path);

		void SplitMicrosFile(string filePath, string storeDir);
	}
}