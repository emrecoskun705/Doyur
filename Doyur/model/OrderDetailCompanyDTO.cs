using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Doyur.model
{
	[Serializable]
	public class OrderDetailCompanyDTO
	{
		public OrdersDTO Order { get ; set; }

		public OrderProductlistDTO OPInfo { get; set; }

		public ProductDTO Product { get; set; }

	}
}