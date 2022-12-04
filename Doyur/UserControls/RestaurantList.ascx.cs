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
				GetRestaurants();
			}
		}

		private void GetRestaurants()
		{
			var getRestaurants = (from p in db.sp_GetRestaurants(10) select p).ToList();

			if (getRestaurants != null && getRestaurants.Count > 0)
			{
				try
				{
					Repeater1.DataSource = getRestaurants;
					Repeater1.DataBind();
				} catch (Exception)
				{
					throw;
				}
			}
		}
	}
}