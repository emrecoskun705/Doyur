using Doyur.db;
using Doyur.extensions;
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

        List<db.sp_GetCompanyOP_Result> Orders{ get; set; }

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
            Orders = db.sp_GetCompanyOP(compnayId).ToList();

            var onlyOrders = new List<MyOrder>();
            foreach (var c in Orders.Select(x => new { x.OrderId, x.Status }).ToList().Distinct().ToList())
            {
                onlyOrders.Add(new MyOrder()
                {
                    OrderId = c.OrderId,
                    Status = Types.Order.GetOrderStatus()[(int)c.Status].Title         
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

				gvOrders.DataSource = Orders.Where(x => x.OrderId == parentId).ToList();
				gvOrders.DataBind();
			}
		}

		protected void gList_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "Edit")
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


		private class MyOrder
        {
            public int OrderId { get; set;}
            public string Status { get; set;}
        }

	}
}