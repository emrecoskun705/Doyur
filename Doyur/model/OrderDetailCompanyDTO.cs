using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Doyur.model
{
	[Serializable]
	public class OrderDetailCompanyDTO
	{

		public OrderProductlistDTO OPInfo { get; set; }

		public ProductDTO Product { get; set; }

	}
}