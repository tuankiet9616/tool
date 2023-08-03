using FX.Data;
using Parse.Core.Domain;
using System;

namespace Parse.Core.IService
{
	public interface ICompanyService : IBaseService<Company, int>
	{
		Company GetCompanyByCode(string code);
	}
}