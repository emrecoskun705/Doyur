using Doyur.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.company
{
	public partial class _default : System.Web.UI.Page
	{

        db.doyurEntities db = new db.doyurEntities();

        protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				LoadProducts();
			}
		}

		private void LoadProducts()
		{
			int compnayId = IT.Session.Users.CompanyId();
			var getProducts = db.sp_GetProductsC(100, compnayId).ToList();
			if(getProducts.Count() > 0)
			{
				gList.DataSource = getProducts;
				gList.DataBind();
			} else
            {
                gList.DataSource = null;
                gList.DataBind();
            }

		}

		protected void gList_RowCommand(object sender, GridViewCommandEventArgs e)
		{
            if (e.CommandName == "Edit")
            {
                int productId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("/company/edit.aspx?id=" + productId);

            }
            else if (e.CommandName == "DeleteProduct")
            {
                int userId = IT.Session.Users.UserId();
                int companydId = IT.Session.Users.CompanyId();
                int addressId = Convert.ToInt32(e.CommandArgument);
                var getProduct = (from p in db.Product where p.ProductId == addressId && p.CompanyId == companydId select p).FirstOrDefault();

                if (getProduct != null)
                {
                    db.Product.Remove(getProduct);
                    if (db.SaveChanges() > 0)
                    {
                        this.ShowMessage("Success", "Ürün başarıyla silindi");
                        LoadProducts();
                    }
                    else
                    {
                        this.ShowMessage("Warning", "Ürün silinirken bir hata oluştu");
                    }
                }
                else
                {
                    this.ShowMessage("Warning", "Ürün bulunamadı");
                }
            }
        }
	}
}