using Doyur.extensions;
using Doyur.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Types;

namespace Doyur.user
{
	public partial class orders : System.Web.UI.Page
	{
		db.doyurEntities db = new db.doyurEntities();

		List<MyOrder> Orders { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadData();
			}
		}

		private void LoadData()
		{
			int userId = IT.Session.Users.UserId();
			Orders = (
				from p in db.Product
				join op in db.OrderProductList on p.ProductId equals op.ProductId
				join o in db.Orders on op.OrderId equals o.OrderId
				where o.UserId == userId
				orderby o.CreateDate descending
				select new MyOrder()
				{
					OPInfo = new OrderProductlistDTO()
					{
						OrderId = op.OrderId,
						ProductId = op.ProductId,
						ProductQuantity = op.ProductQuantity,
						Status = op.Status
					},
					Product = new ProductDTO()
					{
						ProductId = p.ProductId,
						CategoryId = p.CategoryId,
						CompanyId = p.CompanyId,
						Name = p.Name,
						Description = p.Description,
						IsActive = p.IsActive,
						Price = p.Price,
						DiscountPercantage = p.DiscountPercantage,
						ImageUrl = p.ImageUrl,
						Stock = p.Stock,
					},
					Orders = new OrdersDTO()
					{
						OrderId = o.OrderId,
						CompanyId = o.CompanyId,
						Status = o.Status,
						UserId = o.UserId,
						Coupon = o.Coupon,
						TotalCost = o.TotalCost,
						CreateDate = o.CreateDate,
					}


				}
				).ToList();



			var onlyOrders = new List<HeadOrder>();
			foreach (var k in Orders.Select(x => new { x.OPInfo.OrderId }).ToList().Distinct().ToList())
			{
				var getO = Orders.Where(x => x.OPInfo.OrderId == k.OrderId).FirstOrDefault();

				onlyOrders.Add(new HeadOrder()
				{
					OrderId = getO.Orders.OrderId,
					Status = Types.Order.GetOrderStatus()[getO.Orders.Status].Title,
					CreateDate = getO.Orders.CreateDate,
					TotalCost = getO.Orders.TotalCost ?? default(decimal),
					MyOrders = Orders.Where(x => x.Orders.OrderId == getO.Orders.OrderId).ToList(),
				});
			}

			orderList.DataSource = onlyOrders;
			orderList.DataBind();
		}


		private class HeadOrder
		{
			public int OrderId { get; set; }
			public string Status { get; set; }

			public decimal TotalCost { get; set; }

			public DateTime CreateDate { get; set; }

			public List<MyOrder> MyOrders { get; set; }

		}

		private class MyOrder
		{
			public ProductDTO Product { get; set; }
			public OrderProductlistDTO OPInfo { get; set; }

			public OrdersDTO Orders { get; set; }
		}


	}
}