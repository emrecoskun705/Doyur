using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.restaurant
{
	public partial class Restaurant : System.Web.UI.Page
	{

		db.doyurEntities db = new db.doyurEntities();

		public List<db.sp_GetProducts_Result> ProductList { get; set; }
		public db.sp_GetRestaurant_Result RestaurantObj { get; set; }
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack) { 
				var id = Request.QueryString["id"];
				if(id != null)
				{
					try
					{
						GetRestaurant(Convert.ToInt32(id));
					} catch (Exception ex)
					{
						// if id is not an integer
					}
				}
			}
		}

		protected void GetRestaurant(int id)
		{
			var getRestaurant = (from p in db.sp_GetRestaurant(1, id) select p).ToList();

			if(getRestaurant != null && getRestaurant.Count() > 0)
			{
				RestaurantObj = getRestaurant.First();
			}
		}

	}
}