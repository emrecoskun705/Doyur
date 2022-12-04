using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.UserControls
{
	public partial class ProductRestaurantList : System.Web.UI.UserControl
	{
		db.doyurEntities db = new db.doyurEntities();

		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				GetProducts();
			}
		}

		protected void GetProducts()
		{

			try
			{
				int id = Convert.ToInt32(Request.QueryString["id"]);

				var getproducts = (from p in db.sp_GetProducts(100, id) select p).ToList();

				if (getproducts != null && getproducts.Count > 0)
				{
					productRepeater.DataSource = getproducts;
					productRepeater.DataBind();
				}
			}
			catch (Exception)
			{
				// if id is not an integer
				throw;
			}
			


		}

        protected void productRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
			if(e.CommandName == "AddToOrderCmnd")
			{
				int id = Convert.ToInt32(e.CommandArgument);
				Debug.WriteLine("" + id);
			}
        }
    }
}