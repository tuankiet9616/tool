using FX.Data;
using Parse.Core.Domain;
using System;
using System.Collections.Generic;

namespace Parse.Core.IService
{
	public interface IProductInvService : IBaseService<ProductInv, int>
	{
		List<ProductInv> GetByInvoiceID(int invoiceID);
	}
}