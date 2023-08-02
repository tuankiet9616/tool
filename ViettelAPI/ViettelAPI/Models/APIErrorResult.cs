using System;
using System.Runtime.CompilerServices;

namespace ViettelAPI.Models
{
	public class APIErrorResult
	{
		public string code
		{
			get;
			set;
		}

		public string message
		{
			get;
			set;
		}

		public string data
		{
			get;
			set;
		}

		public APIErrorResult()
		{
		}
	}
}