using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Models
{
	public class ZipFileResponse
	{
		public string description
		{
			get;
			set;
		}

		public string errorCode
		{
			get;
			set;
		}

		public string fileName
		{
			get;
			set;
		}

		public byte[] fileToBytes
		{
			get;
			set;
		}

		public bool paymentStatus
		{
			get;
			set;
		}

		public ZipFileResponse()
		{
		}
	}
}