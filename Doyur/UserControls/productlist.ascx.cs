using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.UserControls
{
	public partial class RestaurantList : System.Web.UI.UserControl
	{
		db.doyurEntities db = new db.doyurEntities();
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				GetProducts();
			}
		}

		private void GetProducts()
		{
			var getProducts = (from p in db.sp_GetProducts(10) select p).ToList();

			if (getProducts != null && getProducts.Count > 0)
			{
				try
				{
					Repeater1.DataSource = getProducts;
					Repeater1.DataBind();
				} catch (Exception)
				{
					throw;
				}
			}
		}
	}
}