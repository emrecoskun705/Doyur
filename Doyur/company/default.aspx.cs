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
			}

		}

		protected void gList_RowCommand(object sender, GridViewCommandEventArgs e)
		{

		}
	}
}