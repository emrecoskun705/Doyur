using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.order
{
	public partial class _default : System.Web.UI.Page
	{

		db.doyurEntities db = new db.doyurEntities();
		List<db.sp_getOProducts_Result> OrderDetails { get; set; }

		protected void Page_Init(object sender, EventArgs e)
		{
				IT.Session.Users.AddLoginSessionDebug();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				GetOrder();
			}
		}

		private void GetOrder()
		{
			int userId = IT.Session.Users.UserId();

			var order = db.sp_GetOrCreateOrder(userId).FirstOrDefault();
			if (order != null)
			{
                OrderDetails = db.sp_getOProducts(order.OrderId).ToList();
				var onlyCompanies = new List<Company>();
                foreach(var c in OrderDetails.Select(x => x.CName).ToList().Distinct().ToList())
				{
					onlyCompanies.Add(new Company()
					{
						CName = c
					});
				}

				parentR.DataSource = onlyCompanies;
				parentR.DataBind();
			}
		}

        protected void parentR_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var childR = (Repeater)e.Item.FindControl("childR");
				var parentName = ((Label)e.Item.FindControl("CName")).Text;
				if (childR != null)
				{
					childR.DataSource = OrderDetails.Where(x => x.CName == parentName).ToList();
					childR.DataBind();
				}
            }
        }

		private class Company
		{
			public string CName { get; set;}
		}
    }
}