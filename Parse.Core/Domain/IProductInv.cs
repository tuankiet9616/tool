using System;

namespace Parse.Core.Domain
{
	public interface IProductInv
	{
		decimal Amount
		{
			get;
			set;
		}

		string Code
		{
			get;
			set;
		}

		string Name
		{
			get;
			set;
		}

		decimal Price
		{
			get;
			set;
		}

		decimal Quantity
		{
			get;
			set;
		}

		string Remark
		{
			get;
			set;
		}

		decimal Total
		{
			get;
			set;
		}

		string Unit
		{
			get;
			set;
		}
	}
}