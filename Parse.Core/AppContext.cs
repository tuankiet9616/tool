using Parse.Core.Domain;
using System;

namespace Parse.Core
{
	public class AppContext
	{
		private static AppContext _Current;

		private Company _company;

		public Company company
		{
			get
			{
				return this._company;
			}
			set
			{
				this._company = value;
			}
		}

		public static AppContext Current
		{
			get
			{
				return AppContext._Current;
			}
		}

		public AppContext()
		{
		}

		public static void InitContext(Company com)
		{
			_Current = new AppContext()
			{
				_company = com
			};
		}
	}
}