using Doyur.db;
using Doyur.extensions;
using Doyur.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.company
{
    public partial class orders : System.Web.UI.Page
    {
        db.doyurEntities db = new db.doyurEntities();

        List<MyOrder> Orders{ get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            int compnayId = IT.Session.Users.CompanyId();
            Orders = (
				from p in db.Product
				join op in db.OrderProductList on p.ProductId equals op.ProductId
				join o in db.Orders on op.OrderId equals o.OrderId
				where p.CompanyId == compnayId && o.IsPaid == true
				orderby o.OrderId descending
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
						Status= o.Status,
						UserId = o.UserId,
						Coupon = o.Coupon,
						TotalCost = o.TotalCost,

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
                });
            }

			gList.DataSource = onlyOrders;
			gList.DataBind();

		}

		protected void gList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				int parentId = Convert.ToInt32(gList.DataKeys[e.Row.RowIndex].Value);
				GridView gvOrders = e.Row.FindControl("gSubList") as GridView;

				gvOrders.DataSource = Orders.Where(x => x.Orders.OrderId == parentId).ToList();
				gvOrders.DataBind();
			}
		}

		protected void gList_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Detail")
			{
				int productId = Convert.ToInt32(e.CommandArgument);
				Response.Redirect("/company/edit?id=" + productId);

			}
			else if (e.CommandName == "DeleteProduct")
			{
				int userId = IT.Session.Users.UserId();
				int companydId = IT.Session.Users.CompanyId();
				int addressId = Convert.ToInt32(e.CommandArgument);
				var getProduct = (from p in db.Product where p.ProductId == addressId && p.CompanyId == companydId select p).FirstOrDefault();

				if (getProduct != null)
				{
					getProduct.IsActive = false;
					if (db.SaveChanges() > 0)
					{
						this.ShowMessage("success", "Ürün sadece sizin tarafınızdan görünülebilir hale geldi", "Başarılı");
						//LoadProducts();
					}
					else
					{
						this.ShowMessage("warning", "Ürün silinirken bir hata oluştu", "Hata");
					}
				}
				else
				{
					this.ShowMessage("warning", "Ürün bulunamadı", "Hata");
				}
			}
		}

		private class HeadOrder
		{
			public int OrderId { get; set;}
			public string Status { get; set;}


		}

		private class MyOrder
        {
            public ProductDTO Product { get; set;}
            public OrderProductlistDTO OPInfo { get; set;}

			public OrdersDTO Orders { get; set;}
        }

	}
}