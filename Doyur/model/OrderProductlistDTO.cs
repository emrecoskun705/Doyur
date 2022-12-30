using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Doyur.model
{

	[Serializable]
	public class OrderProductlistDTO
	{
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public int ProductQuantity { get; set; }
		public byte Status { get; set; }
	}
}