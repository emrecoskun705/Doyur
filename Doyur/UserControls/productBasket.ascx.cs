﻿using Doyur.extensions;
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

        public static db.sp_GetActiveOrder_Result Order { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetUserOrder();
            }
        }

        private void GetUserOrder()
        {
            int userId = IT.Session.Users.UserId();
            if(userId != 0)
            {
                var getOrder = (from p in db.sp_GetActiveOrder(userId) select p);
                if(getOrder != null && getOrder.Count() > 0)
                {
                    orderRepeater.DataSource = getOrder.First();
                    orderRepeater.DataBind();
                    Order = getOrder.First();
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
            dropDownList.SelectedIndexChanged += quantityDropdown_SelectedIndexChanged; 

        }
    }
}