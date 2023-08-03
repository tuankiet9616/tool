using FX.Data;
using Parse.Core.Domain;
using Parse.Core.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Parse.Core.Implement
{
	public class ProductInvService : BaseService<ProductInv, int>, IProductInvService, IBaseService<ProductInv, int>
	{
		public ProductInvService(string sessionFactoryConfigPath) : base(sessionFactoryConfigPath, "")
		{
		}

		public List<ProductInv> GetByInvoiceID(int invoiceID)
		{
			return (
				from x in base.Query
				where x.InvoiceID == invoiceID
				select x).ToList<ProductInv>();
		}
	}
}