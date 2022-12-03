using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur
{
    public partial class _default : System.Web.UI.Page
    {

        db.doyurEntities db = new db.doyurEntities();

        public List<db.sp_GetRestaurants_Result> RestaurantList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var getRestaurants = (from p in db.sp_GetRestaurants(10) select p).ToList();

                if(getRestaurants != null && getRestaurants.Count > 0 ) {
                    RestaurantList = getRestaurants;
                }
            }
        }
    }
}