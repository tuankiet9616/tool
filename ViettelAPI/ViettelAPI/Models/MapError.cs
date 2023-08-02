using System;
using System.Runtime.CompilerServices;

namespace ViettelAPI.Models
{
	public class MapError
	{
        public string msg
        {
            get;
            set;
        }

        public string invoiceSeri
        {
            get;
            set;
        }

        public string errorCode
        {
            get;
            set;
        }

		public MapError()
		{
		}
	}
}