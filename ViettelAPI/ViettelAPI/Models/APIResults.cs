using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ViettelAPI.Models
{
	public class APIResults
	{
		public List<APIResult> createInvoiceOutputs
		{
			get;
			set;
		}

        public List<MapError> lstMapError
        {
            get;
            set;
        }

        public int totalSuccess
        {
            get;
            set;
        }

        public int totalFail
        {
            get;
            set;
        }

        public APIResults()
		{
		}
	}
}