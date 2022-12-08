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
        public decimal TotalCost { get; set; }

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

                if(getOrder != null && getOrder.Count() > 0)
                {
                    Order = getOrder;
                    orderRepeater.DataSource = getOrder;
                    orderRepeater.EnableViewState = false;

                    TotalCost = Order.Sum(x => x.ProductQuantity * Convert.ToDecimal(x.Price));
                    OrderID = getOrder[0].OrderId;
                    orderRepeater.DataBind();

                    noOrderRecords.Visible = false;
                    orderSummary.Visible = true;


				} else
                {
					noOrderRecords.Visible = true;
					orderSummary.Visible = false;
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

            // if update quantityi = 1 it is successfull 0 is not success
			var updateQuantity = db.sp_ChangeProductQuantityInOrder(OrderID, productId, quantity).ToList().First();

            if(quantity == 0 && updateQuantity == 1)
            {
                // if quantity is 0 remove product from Order list then bind data again for repeater
			    Order.RemoveAll(s => s.ProductId == productId);
				TotalCost = Order.Sum(x => x.ProductQuantity * Convert.ToDecimal(x.Price));
				UpdateOrderRepeater();
			} else if(quantity > 0 && updateQuantity == 1)
            {
                // if quantity is not zero then update product quantity in Order list
                Order.Find(s => s.ProductId == productId).ProductQuantity = (byte)quantity;
				TotalCost = Order.Sum(x => x.ProductQuantity * Convert.ToDecimal(x.Price));
                UpdateOrderRepeater();
			} else if (updateQuantity == 0)
            {
                // if quantity change is not successful then change it to begining value
                dropDown.SelectedValue = quantity.ToString();
                UpdateOrderRepeater();
			}

		}

        /// <summary>
        /// This method adds select method for every repeater item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void orderRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            DropDownList dropDownList = (DropDownList)e.Item.FindControl("quantityDropdown");
            dropDownList.SelectedIndexChanged += quantityDropdown_SelectedIndexChanged; 

        }

        private void UpdateOrderRepeater()
        {
			if (Order.Count() == 0)
			{
				noOrderRecords.Visible = true;
				orderSummary.Visible = false;
			}
			else
			{
				noOrderRecords.Visible = false;
				orderSummary.Visible = true;
			}
			orderRepeater.DataSource = Order;
			orderRepeater.DataBind();
		}


		protected void orderSubmitBtn_Click(object sender, EventArgs e)
		{
            var updateOrder = db.sp_UpdateOrder(OrderID, false, true, TotalCost, "P", 1).ToList().First();
            // order payment or giving is successful
            if (updateOrder != null && updateOrder == 1) 
            {
                this.Parent.Page.ShowMessage("Success", "Siparişiniz başarılı bir şekilde oluşturuldu");
                Order.Clear();
                UpdateOrderRepeater();
            }
            else
            {
				// order payment or giving is not successful 
				this.Parent.Page.ShowMessage("Warning", "Siparişiniz oluşturulamadı lütfen tekrar deneyiniz");
			}
		}
	}
}