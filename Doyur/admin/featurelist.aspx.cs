using Doyur.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.admin
{
	public partial class featurelist : System.Web.UI.Page
	{
		
		db.doyurEntities db = new db.doyurEntities();

		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				LoadData();
			}
		}

		private void LoadData()
		{
			var getHeadFeatures = (from p in db.Feature where p.SubFeatureId == null select p).ToList();

			gList.DataSource = getHeadFeatures;
			gList.DataBind();
		}


        protected void gList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
			if (e.CommandName == "Edit")
			{
				int productId = Convert.ToInt32(e.CommandArgument);
				Response.Redirect("/admin/featureedit.aspx?id=" + productId);

			}
			else if (e.CommandName == "DeleteProduct")
			{
				//int userId = IT.Session.Users.UserId();
				//int companydId = IT.Session.Users.CompanyId();
				//int addressId = Convert.ToInt32(e.CommandArgument);
				//var getProduct = (from p in db.Product where p.ProductId == addressId && p.CompanyId == companydId select p).FirstOrDefault();

				//if (getProduct != null)
				//{
				//	db.Product.Remove(getProduct);
				//	if (db.SaveChanges() > 0)
				//	{
				//		this.ShowMessage("Success", "Adres başarıyla silindi");
				//		LoadProducts();
				//	}
				//	else
				//	{
				//		this.ShowMessage("Warning", "Adres silinirken bir hata oluştu");
				//	}
				//}
				//else
				//{
				//	this.ShowMessage("Warning", "Adres bulunamadı");
				//}
			}
		}
    }
}