using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Models
{
	public class GetFileRequest
	{
		public string additionalReferenceDate
		{
			get;
			set;
		}

		public string additionalReferenceDesc
		{
			get;
			set;
		}

		public string fileType
		{
			get;
			set;
		}

		public string invoiceNo
		{
			get;
			set;
		}

		public string pattern
		{
			get;
			set;
		}

		public string strIssueDate
		{
			get;
			set;
		}

		public string transactionUuid
		{
			get;
			set;
		}

		public GetFileRequest()
		{
		}
	}
}