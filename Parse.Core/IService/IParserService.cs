using Parse.Core.Domain;
using System;
using System.Collections.Generic;

namespace Parse.Core.IService
{
	public interface IParserService
	{
		string GetFkey(InvoiceVAT invoice);

		List<InvoiceVAT> GetInvoiceData(string filePath, ref int invSuccess, ref int invTotal, ref string mesError);
		List<InvoiceVAT> GetInvoiceDataXml(string filePath, ref int invSuccess, ref int invTotal, ref string mesError, string csvPath);
	}
}