using Doyur.extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Doyur.UserControls
{
    public partial class productBasket : System.Web.UI.UserControl
    {

        db.doyurEntities db = new db.doyurEntities();

        public List<db.sp_GetActiveOrderProductList_Result> Order { get; set; }
        public int OrderID { get; set; }

        public int MaxQuantity = 10;

        protected void Page_Load(object sender, EventArgs e)
        {

            GetUserOrder();          

        }

        public void GetUserOrder()
        {
            int userId = IT.Session.Users.UserId();
            if(userId != 0)
            {
                var getOrder = (from p in db.sp_GetActiveOrderProductList(userId) select p).ToList();
                HttpCookie cookie = new HttpCookie("Order");

                if(getOrder != null && getOrder.Count() > 0)
                {
                    Order = getOrder;
                    orderRepeater.DataSource = getOrder;
                    orderRepeater.EnableViewState = false;

                    OrderID = getOrder[0].OrderId;
                    orderRepeater.DataBind();

                    noOrderRecords.Visible = false;

                } else
                {
					noOrderRecords.Visible = true;
				}

            }

        }

        protected void quantityDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

			DropDownList dropDown = sender as DropDownList;
            RepeaterItem repeaterI = (RepeaterItem)dropDown.NamingContainer;
 
            String a = ((Label)repeaterI.FindControl("productlbl")).Text;

			int quantity = Convert.ToInt32(dropDown.SelectedValue);
			int productId = Convert.ToInt32(a);

			var updateQuantity = db.sp_ChangeProductQuantityInOrder(OrderID, productId, quantity);

            if(quantity == 0)
            {
                // if quantity is 0 remove product from Order list then bind data again for repeater
			    Order.RemoveAll(s => s.ProductId == productId);
				orderRepeater.DataSource = Order;
				orderRepeater.DataBind();
                if(Order.Count() == 0)
                {
                    noOrderRecords.Visible = true;
                } else
                {
					noOrderRecords.Visible = false;
				}
			} else
            {
                // if quantity is not zero then update product quantity in Order list
                Order.Find(s => s.ProductId == productId).ProductQuantity = (byte)quantity;
				orderRepeater.DataSource = Order;
				orderRepeater.DataBind();
			}

		}

        protected void orderRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            DropDownList dropDownList = (DropDownList)e.Item.FindControl("quantityDropdown");
            dropDownList.SelectedIndexChanged += quantityDropdown_SelectedIndexChanged; 

        }


        protected DataTable LoadQuantity()
        {

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Text");
            dt.Columns.Add("Value");


            for(int i=0; i< MaxQuantity; i++)
            {
                DataRow dr = dt.NewRow();
                dr["Text"] = "" + i;
                dr["Value"] = "" + i;
            }

            return dt;
        }

	}
}