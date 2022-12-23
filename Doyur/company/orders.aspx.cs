using Doyur.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.company
{
    public partial class orders : System.Web.UI.Page
    {
        db.doyurEntities db = new db.doyurEntities();

        List<db.sp_GetCompanyOP_Result> Orders{ get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            int compnayId = IT.Session.Users.CompanyId();
            Orders = db.sp_GetCompanyOP(compnayId).ToList();

            var onlyOrders = new List<MyOrder>();
            foreach (var c in Orders.Select(x => new { x.OrderId, x.Status }).ToList().Distinct().ToList())
            {
                onlyOrders.Add(new MyOrder()
                {
                    OrderId = c.OrderId,
                    Status = Types.Order.GetOrderStatus()[(int)c.Status].Title         
                });
            }

            parentR.DataSource = onlyOrders;
            parentR.DataBind();

        }

        protected void parentR_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var childR = (Repeater)e.Item.FindControl("childR");
                var parentId = Convert.ToInt32(((HiddenField)e.Item.FindControl("orderIdLbl")).Value);
                if (childR != null)
                {
                    childR.DataSource = Orders.Where(x => x.OrderId == parentId).ToList();
                    childR.DataBind();
                }
            }
        }


        private class MyOrder
        {
            public int OrderId { get; set;}
            public string Status { get; set;}
        }
    }
}