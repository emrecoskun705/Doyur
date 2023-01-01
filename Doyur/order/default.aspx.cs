using Doyur.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.order
{
	public partial class _default : System.Web.UI.Page
	{

		db.doyurEntities db = new db.doyurEntities();
		List<db.sp_getCProducts_Result> CartDetails { get; set; }

		public decimal TotalPrice { get { return Convert.ToDecimal(ViewState["totalPrice"]); } set { ViewState["totalPrice"] = value; } }

		protected void Page_Init(object sender, EventArgs e)
		{
			
			IT.Session.Users.UserIsNotLoginRedirect("/");

		}

		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
			{
				GetCart();
			}
		}

		// Gets active cart for a given user
		private void GetCart()
		{
			int userId = IT.Session.Users.UserId();

			var cart = (from c in db.Cart where c.UserId == userId select c).FirstOrDefault();

			if (cart == null)
			{
				cart = new db.Cart()
				{
					UserId = userId,
					CreateDate = DateTime.Now,
					IsActive = true,
					ExpireDate = DateTime.Now.AddDays(30),
				};

				db.Cart.Add(cart);

				// new cart is not created
				if (db.SaveChanges() < 1)
				{
					IT.Session.Users.AddMessageSession("error", "Sepete ulaşılırken bir hata meydana geldi", "Hata");
					Response.Redirect("/");
					return;
				}
			}

			// get products that belongs to that cart
			CartDetails = db.sp_getCProducts(cart.CartId).ToList();


			// if cartItem is removed from 
			var ciListRemove = new List<db.CartItem>();
			var updatedItems = new List<db.CartItem>();

			StringBuilder sb = new StringBuilder();
			sb.Append("<ul>");
			foreach (var oProduct in CartDetails)
			{
				db.CartItem op = (from p in db.CartItem where p.CartId == cart.CartId && p.ProductId == oProduct.ProductId select p).FirstOrDefault();

				if (!oProduct.IsActive || oProduct.Stock < 1)
				{
					// product is disabled or stock is finished
					ciListRemove.Add(op);
					sb.Append("<li><a href=/product?id=" + op.ProductId + ">");
					sb.Append(CartDetails.Where(x => x.ProductId == op.ProductId).First().Name);
					sb.Append("</a>");
					sb.Append(" isimli ürün kaldırıldı</li>");
					// remove order product from order
					db.CartItem.Remove(op);
				}
				else if(oProduct.Quantity > oProduct.Stock)
				{
					updatedItems.Add(op);
					CartDetails.Where(x => x.ProductId == oProduct.ProductId).First().Quantity = oProduct.Stock;
					sb.Append("<li>" + CartDetails.Where(x => x.ProductId == op.ProductId).First().Name);
					sb.Append(" isimli ürünün miktarı güncellendi</li>");
					op.Quantity = oProduct.Stock;
				}
			}
			sb.Append("</ul>");
			infoLbl.Text= sb.ToString();

			if (ciListRemove.Count > 0 || updatedItems.Count > 0)
			{
				if(db.SaveChanges() > 0)
				{

					CartDetails.RemoveAll(x => ciListRemove.Select(k => k.ProductId).ToList().Contains(x.ProductId));
					string txtlbl = sb.ToString();
					infoLbl.Text = txtlbl;
				}

            }

			if (CartDetails.Count == 0)
			{
				IT.Session.Users.AddMessageSession("warning", "Sepetinizi görmek için ürün ekleyin.", "Sepet Boş");
				Response.Redirect("/");
			}


			TotalPrice = CartDetails.Sum(x => x.Price * x.Quantity);
			var onlyCompanies = new List<Company>();
            foreach(var c in CartDetails.Select(x => x.CName).ToList().Distinct().ToList())
			{
				onlyCompanies.Add(new Company()
				{
					CName = c
				});
			}

            parentR.DataSource = onlyCompanies;
			parentR.DataBind();
			
		}

        protected void parentR_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var childR = (Repeater)e.Item.FindControl("childR");
				var parentName = ((Label)e.Item.FindControl("CName")).Text;
				if (childR != null)
				{
                    childR.DataSource = CartDetails.Where(x => x.CName == parentName).ToList();
					childR.DataBind();
				}
            }
        }

		private class Company
		{
			public string CName { get; set;}
		}

        protected void trashBtn_Click(object sender, ImageClickEventArgs e)
        {
			int userId = IT.Session.Users.UserId();
            ImageButton btn = (ImageButton)sender;
            // child repeater
            RepeaterItem child = (RepeaterItem)btn.NamingContainer;

			int productId; 
			int.TryParse(((HiddenField)child.FindControl("ProductId")).Value, out productId);
			if (productId != 0 && userId != 0)
			{
                var cart = (from p in db.Cart where p.UserId == userId && p.IsActive == true select p).FirstOrDefault();
				var cartItem = (from p in db.CartItem where p.CartId == cart.CartId && p.ProductId == productId select p).FirstOrDefault();

				db.CartItem.Remove(cartItem);

				if(db.SaveChanges() > 0)
				{
					IT.Session.Users.AddMessageSession("success", "Ürün başarılı bir şekilde sepetten çıkarıldı", "Başarılı");
					Response.Redirect(Request.RawUrl);
				} 
				else
				{
                    IT.Session.Users.AddMessageSession("warning", "Ürün sepetten çıkarılırken hata oluştu", "Hata");
                    Response.Redirect(Request.RawUrl);
                }
			} else
			{
                IT.Session.Users.AddMessageSession("error", "Ürün bulunamadı", "Hata");
                Response.Redirect(Request.RawUrl);
            }
		}

        protected void decrementbtn_Click(object sender, EventArgs e)
        {
			int userId = IT.Session.Users.UserId();
			Button btn = (Button)sender;
			// child repeater
			RepeaterItem child = (RepeaterItem)btn.NamingContainer;
			var cart = (from p in db.Cart where p.UserId == userId select p).FirstOrDefault();
			int productId;
			int.TryParse(((HiddenField)child.FindControl("ProductId")).Value, out productId);

			int pQuantity;
			int.TryParse(((Label)child.FindControl("quantityId")).Text, out pQuantity);

			if (productId != 0 && userId != 0 && pQuantity != 0)
			{
				var product = (from p in db.Product where p.ProductId == productId select p).FirstOrDefault();
				var cartItem = (from p in db.CartItem where p.ProductId == productId && p.CartId == cart.CartId select p).FirstOrDefault();

				if (product == null || cartItem == null)
				{
					return;
				}

				if (cartItem.Quantity - 1 < 0)
				{
					return;
				}

				cartItem.Quantity -= 1;
				db.SaveChanges();
			}

			Response.Redirect(Request.RawUrl);
        }

        protected void incrementbtn_Click(object sender, EventArgs e)
        {
            int userId = IT.Session.Users.UserId();
            Button btn = (Button)sender;
            // child repeater
            RepeaterItem child = (RepeaterItem)btn.NamingContainer;
            var cart = (from p in db.Cart where p.UserId == userId select p).FirstOrDefault();
            int productId;
            int.TryParse(((HiddenField)child.FindControl("ProductId")).Value, out productId);

            int pQuantity;
            int.TryParse(((Label)child.FindControl("quantityId")).Text, out pQuantity);

            if (productId != 0 && userId != 0 && pQuantity != 0)
            {
				var product = (from p in db.Product where p.ProductId == productId select p).FirstOrDefault();
				var cartItem = (from p in db.CartItem where p.ProductId == productId && p.CartId == cart.CartId select p).FirstOrDefault();

				if(product == null ||cartItem == null)
				{
					return;
				}

				if(product.Stock < cartItem.Quantity + 1)
				{
					return;
				}

                cartItem.Quantity += 1;
				db.SaveChanges();
            }

            Response.Redirect(Request.RawUrl);
        }
    }
}