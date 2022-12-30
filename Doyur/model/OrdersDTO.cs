using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Doyur.model
{
	[Serializable]
	public class OrdersDTO
	{
		public int OrderId { get; set; }
		public int UserId { get; set; }
		public byte Status { get; set; }
		public string Coupon { get; set; }
		public bool IsActive { get; set; }
		public bool IsPaid { get; set; }
		public Nullable<decimal> TotalCost { get; set; }

		public Nullable<int> AddressId { get; set; }
	}
}