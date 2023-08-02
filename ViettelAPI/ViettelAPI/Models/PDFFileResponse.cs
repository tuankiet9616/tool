using System;
using System.Runtime.CompilerServices;

namespace ViettelAPI.Models
{
	public class PDFFileResponse
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

		public PDFFileResponse()
		{
		}
	}
}