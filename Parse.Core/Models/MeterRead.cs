using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Models
{
	public class MeterRead
	{
		public decimal amount
		{
			get;
			set;
		}

		public decimal currentIndex
		{
			get;
			set;
		}

		public decimal factor
		{
			get;
			set;
		}

		public decimal previousIndex
		{
			get;
			set;
		}

		public MeterRead()
		{
		}
	}
}