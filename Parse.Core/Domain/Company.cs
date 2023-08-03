using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Parse.Core.Domain
{
	public class Company
	{
		private IDictionary<string, string> _Config = new Dictionary<string, string>();

		public virtual string Address
		{
			get;
			set;
		}

		public virtual string BankName
		{
			get;
			set;
		}

		public virtual string BankNumber
		{
			get;
			set;
		}

		public virtual string Code
		{
			get;
			set;
		}

		public virtual IDictionary<string, string> Config
		{
			get
			{
				return this._Config;
			}
			set
			{
				this._Config = value;
			}
		}

		public virtual string Domain
		{
			get;
			set;
		}

		public virtual string Email
		{
			get;
			set;
		}

		public virtual string Fax
		{
			get;
			set;
		}

		public virtual int Id
		{
			get;
			set;
		}

		public virtual string InvPattern
		{
			get;
			set;
		}

		public virtual string InvSerial
		{
			get;
			set;
		}

		public virtual string Name
		{
			get;
			set;
		}

		public virtual string PassWord
		{
			get;
			set;
		}

		public virtual string Phone
		{
			get;
			set;
		}

		public virtual string TaxCode
		{
			get;
			set;
		}

		public virtual string UserName
		{
			get;
			set;
		}

        public virtual string Token
        {
            get;
            set;
        }

        public virtual string Token_Update_At
        {
            get;
            set;
        }

        public Company()
		{
		}
	}
}