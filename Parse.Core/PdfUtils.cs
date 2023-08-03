using Bytescout.PDFExtractor;
using System;
using System.IO;

namespace Parse.Core
{
	public class PdfUtils
	{
		public PdfUtils()
		{
		}

		public static string GetKey(string filename)
		{
			return Guid.NewGuid().ToString();
		}

		public static string PdfToXml(string filepath)
		{
			string str;
			string folder = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "XML_DATA");
			if (!Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}
			Guid guid = Guid.NewGuid();
			string path = string.Format("{0}\\{1}.xml", folder, guid.ToString("N"));
			using (XMLExtractor extractor = new XMLExtractor())
			{
				extractor.RegistrationName = "demo";
				extractor.RegistrationKey = "demo";
				extractor.LoadDocumentFromFile(filepath);
				extractor.TrimSpaces = true;
				extractor.ExtractColumnByColumn = true;
				extractor.SaveXMLToFile(path);
				str = path;
			}
			return str;
		}
	}
}