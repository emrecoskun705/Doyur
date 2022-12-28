using Doyur.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.order
{
	public partial class _default : System.Web.UI.Page
	{

		db.doyurEntities db = new db.doyurEntities();
		List<db.sp_getOProducts_Result> OrderDetails { get; set; }

		public decimal TotalPrice { get; set; }

		protected void Page_Init(object sender, EventArgs e)
		{
			
			IT.Session.Users.UserIsNotLoginRedirect("/");

		}

		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
			{
				GetOrder();
			}
		}

		// Gets active order for a given user
		private void GetOrder()
		{
			int userId = IT.Session.Users.UserId();

			var order = db.sp_GetOrCreateOrder(userId).FirstOrDefault();
			// if order is null there should be an error we can log this
			if (order == null)
			{
				IT.Session.Users.AddMessageSession("error", "Sepete ulaşılırken bir hata meydana geldi", "Hata");
				Response.Redirect("/");
				return;
			}
			
			// get products that belongs to that order
            OrderDetails = db.sp_getOProducts(order.OrderId).ToList();

			// if order product is removed from 
			var opListRemove = new List<db.OrderProductList>();
            foreach (var oProduct in OrderDetails)
			{

				if(!oProduct.IsActive || oProduct.Stock < 1)
				{
					// product is disabled or stock is finished
					db.OrderProductList op = (from p in db.OrderProductList where p.OrderId == order.OrderId && p.ProductId == oProduct.ProductId select p).FirstOrDefault();
					opListRemove.Add(op);
					// remove order product from order
					db.OrderProductList.Remove(op);
				}
			}

			if(opListRemove.Count > 0)
			{
				if(db.SaveChanges() > 0)
				{
					OrderDetails.RemoveAll(x => opListRemove.Select(k => k.ProductId).ToList().Contains(x.ProductId));
					this.ShowMessage("info", "Sepetinizdeki bazı ürünler stok veya satıcı şirket tarafından kaldırıldı", "Bilgilendirme");
				}

            }

            if (OrderDetails.Count == 0)
			{
				IT.Session.Users.AddMessageSession("warning", "Sepetinizi görmek için ürün ekleyin." , "Sepet Boş");
				Response.Redirect("/");
			}
			TotalPrice = OrderDetails.Sum(x => x.Price * x.ProductQuantity);
			var onlyCompanies = new List<Company>();
            foreach(var c in OrderDetails.Select(x => x.CName).ToList().Distinct().ToList())
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
                    childR.DataSource = OrderDetails.Where(x => x.CName == parentName).ToList();
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
                var order = db.sp_GetOrCreateOrder(userId).FirstOrDefault();
				var removeP = db.sp_DeleteOProduct(order.OrderId, productId).FirstOrDefault();
				if(removeP!= null && removeP > 0)
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
            var order = db.sp_GetOrCreateOrder(userId).FirstOrDefault();
            int productId;
            int.TryParse(((HiddenField)child.FindControl("ProductId")).Value, out productId);

			int pQuantity;
            int.TryParse(((Label)child.FindControl("quantityId")).Text, out pQuantity);

            if (productId != 0 && userId != 0 && pQuantity != 0)
			{
				var changePQO = db.sp_ChangePQO(order.OrderId, productId, pQuantity - 1);
			}

            Response.Redirect(Request.RawUrl);
        }

        protected void incrementbtn_Click(object sender, EventArgs e)
        {
            int userId = IT.Session.Users.UserId();
            Button btn = (Button)sender;
            // child repeater
            RepeaterItem child = (RepeaterItem)btn.NamingContainer;
            var order = db.sp_GetOrCreateOrder(userId).FirstOrDefault();
            int productId;
            int.TryParse(((HiddenField)child.FindControl("ProductId")).Value, out productId);

            int pQuantity;
            int.TryParse(((Label)child.FindControl("quantityId")).Text, out pQuantity);

            if (productId != 0 && userId != 0 && pQuantity != 0)
            {
                var changePQO = db.sp_ChangePQO(order.OrderId, productId, pQuantity + 1);
            }

            Response.Redirect(Request.RawUrl);
        }
    }
}