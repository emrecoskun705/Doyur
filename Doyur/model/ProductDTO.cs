using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Doyur.model
{
	[Serializable]
	public class ProductDTO
	{
		public int ProductId { get; set; }
		public int CategoryId { get; set; }
		public int CompanyId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsActive { get; set; }
		public decimal Price { get; set; }
		public Nullable<byte> DiscountPercantage { get; set; }
		public string ImageUrl { get; set; }
		public int Stock { get; set; }
	}
}