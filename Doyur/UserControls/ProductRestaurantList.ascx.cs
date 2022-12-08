using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Doyur.extensions;

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
			int userId = IT.Session.Users.UserId();
			// TODO: Turn it into == when debug ends
			if(userId == 0)
			{
				this.Parent.Page.ShowMessage("Warning", "Lütfen sepete ürün eklemek için giriş yapınız");
                return;
			}

			if(e.CommandName == "AddToOrderCmnd")
			{
				int productId = Convert.ToInt32(e.CommandArgument);
                int restaurantId = Convert.ToInt32(Request.QueryString["id"]);
				//Create or sp creates an order if there is no active orders and returns that order
				// even if it is exist already
                var getOrderList = (from p in db.sp_CreateOrder(userId, restaurantId) select p).ToList();
				
				// order exists
				if(getOrderList != null && getOrderList.Count > 0)
				{
					var getOrder = getOrderList.First();
					// if restaurant ids are differen then user cannot add that product in his/her Order
					if(restaurantId != getOrderList[0].RestaurantId)
					{
						this.Parent.Page.ShowMessage("Warning", "Farklı restoranlardan sepetinize ürün ekleyemezsiniz");
						return;
					}
					var count = db.sp_AddProductToOrder(productId, getOrder.OrderId, 1).ToList();
					db.SaveChanges();
					if(count!=null && Convert.ToInt32(count.First())> 0)
					{
						productBasket productBasketC = Parent.FindControl("productBasket") as productBasket;
						//success then disable button
						Button btn = (Button)e.Item.FindControl("addOrderBtn") as Button;
                        //btn.Enabled = false;
                        //btn.BackColor = System.Drawing.ColorTranslator.FromHtml("#808080");
						// gets user control from parent page to update order list
						productBasketC.GetUserOrder();
                    }
				}
				// order does not exist
				else
				{

				}


				

            }
        }

        protected void productRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
			//handle auto disable button when page is loaded using productbasket user controller
			productBasket productBasketC = Parent.FindControl("productBasket") as productBasket;

		}
	}
}