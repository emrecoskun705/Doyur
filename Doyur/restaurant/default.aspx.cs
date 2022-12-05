using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.restaurant
{
    public partial class _default : System.Web.UI.Page
    {

        db.doyurEntities db = new db.doyurEntities();

        public static List<db.sp_GetProducts_Result> ProductList { get; set; }
        public static db.sp_GetRestaurant_Result RestaurantObj { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = Request.QueryString["id"];
                if (id != null)
                {
                    try
                    {
                        GetRestaurant(Convert.ToInt32(id));
                    }
                    catch (Exception)
                    {
                        // if id is not an integer
                        throw;
                    }
                }
            }
        }

        protected void GetRestaurant(int id)
        {
            var getRestaurant = (from p in db.sp_GetRestaurant(1, id) select p).ToList();

            if (getRestaurant != null && getRestaurant.Count() > 0)
            {
                RestaurantObj = getRestaurant.First();
            }
        }

        protected void GetUserOrder()
        {
            int userId = IT.Session.Users.UserId();

            if (userId != 0)
            {
                var getOrder = (from p in db.sp_GetActiveOrder(userId) select p).ToList();
                if (getOrder != null && getOrder.Count() > 0)
                {
                    // show order in order table

                }
                else
                {
                    // show empty order
                }
            }
        }



    }
}