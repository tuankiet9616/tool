using System;
using System.Runtime.CompilerServices;

namespace Parse.Core.Utils
{
	public class PagingComponent
	{
		public int PageIndex { get; set; } = 1;

		public int PageSize { get; set; } = 20;

		public int? Total
		{
			get;
			set;
		}

		public int? TotalPage
		{
			get
			{
				int? total = this.Total;
				if (!total.HasValue)
				{
					total = null;
					return total;
				}
				total = this.Total;
				if (total.Value % this.PageSize == 0)
				{
					total = this.Total;
					return new int?(total.Value / this.PageSize);
				}
				total = this.Total;
				return new int?(total.Value / this.PageSize + 1);
			}
		}

		public PagingComponent()
		{
		}

		public PagingComponent(int page, int size) : this()
		{
			this.PageIndex = page;
			this.PageSize = size;
		}

		public PagingComponent(int page, int size, int total) : this(page, size)
		{
			this.Total = new int?(total);
		}

		public bool HasNext()
		{
			int? totalPage = this.TotalPage;
			if (!totalPage.HasValue)
			{
				return false;
			}
			int pageIndex = this.PageIndex;
			totalPage = this.TotalPage;
			if (pageIndex >= totalPage.GetValueOrDefault())
			{
				return false;
			}
			return totalPage.HasValue;
		}

		public bool HasPrev()
		{
			return this.PageIndex > 1;
		}
	}
}