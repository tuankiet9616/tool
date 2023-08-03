using Parse.Core.Domain;
using Parse.Core.Models;
using System.Collections.Generic;

namespace Parse.Core.IService
{
	public interface IApiParserService
	{
		List<InvoiceModels> ConvertToAPIModel(List<InvoiceVAT> invoices);

        InvoiceModels MappingToInvoiceModel(InvoiceVAT invoice);

    }
}