using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Doyur.model
{
	[Serializable]
	public class AddressDTO
	{
		public int AddressId { get; set; }
		public int UserId { get; set; }
		public byte Type { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Name { get; set; }
		public string Town { get; set; }

		public string District { get; set; }
		public string Description { get; set; }
		public string Phone { get; set; }
		public Nullable<decimal> Latitude { get; set; }
		public Nullable<decimal> Longitude { get; set; }
		public bool IsActive { get; set; }
	}
}