using System;
using System.Collections.Generic;

namespace Parse.Core.Domain
{
	public interface Invoice
	{
		decimal Amount
		{
			get;
			set;
		}

		string AmountInWords
		{
			get;
			set;
		}

		DateTime ArisingDate
		{
			get;
			set;
		}

		string ComAddress
		{
			get;
			set;
		}

		string ComBankName
		{
			get;
			set;
		}

		string ComBankNo
		{
			get;
			set;
		}

		string ComFax
		{
			get;
			set;
		}

		string ComName
		{
			get;
			set;
		}

		string ComPhone
		{
			get;
			set;
		}

		string ComTaxCode
		{
			get;
			set;
		}

		string CreateBy
		{
			get;
			set;
		}

		string CusAddress
		{
			get;
			set;
		}

		string CusBankName
		{
			get;
			set;
		}

		string CusBankNo
		{
			get;
			set;
		}

		string CusCode
		{
			get;
			set;
		}

		string CusName
		{
			get;
			set;
		}

		string CusPhone
		{
			get;
			set;
		}

		string CusTaxCode
		{
			get;
			set;
		}

		string Fkey
		{
			get;
			set;
		}

		string Name
		{
			get;
			set;
		}

		string No
		{
			get;
			set;
		}

		string Note
		{
			get;
			set;
		}

		string Pattern
		{
			get;
			set;
		}

		string PaymentMethod
		{
			get;
			set;
		}

		int? Paymentstatus
		{
			get;
			set;
		}

		int PaymentStatus
		{
			get;
			set;
		}

		string ProcessInvNote
		{
			get;
			set;
		}

		IList<ProductInv> Products
		{
			get;
			set;
		}

		string Serial
		{
			get;
			set;
		}

		string SignDate
		{
			get;
			set;
		}

		int? Status
		{
			get;
			set;
		}

		decimal Total
		{
			get;
			set;
		}

		int? Type
		{
			get;
			set;
		}

		decimal VATAmount
		{
			get;
			set;
		}

		float VATRate
		{
			get;
			set;
		}

		string SerializeToXML();
	}
}