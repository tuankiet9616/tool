using FX.Data;
using Parse.Core.Domain;
using System;

namespace Parse.Core.IService
{
	public interface ISetupService : IBaseService<Setup, int>
	{
		Setup GetbyCode(string code);
	}
}