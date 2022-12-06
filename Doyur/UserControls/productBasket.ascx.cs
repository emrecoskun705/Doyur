using Doyur.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.UserControls
{
    public partial class productBasket : System.Web.UI.UserControl
    {

        db.doyurEntities db = new db.doyurEntities();

        public List<db.sp_GetActiveOrderProductList_Result> Order { get; set; }

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
                db.SaveChanges();
                if(getOrder != null && getOrder.Count() > 0)
                {
                    Order = getOrder;
                    orderRepeater.DataSource = getOrder;
                    orderRepeater.DataBind();
                }

            }

        }

        protected void quantityDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //data send
            
        }

        protected void orderRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            DropDownList dropDownList = (DropDownList)e.Item.FindControl("quantityDropdown");
            //dropDownList.SelectedIndexChanged += quantityDropdown_SelectedIndexChanged; 

        }
    }
}