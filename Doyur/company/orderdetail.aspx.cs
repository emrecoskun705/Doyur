using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.company
{
    public partial class orderdetail : System.Web.UI.Page
    {

        db.doyurEntities db = new db.doyurEntities();
        
        protected void Page_Init(object sender, EventArgs e)
        {

        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {
            int orderId;
            int.TryParse(Request.QueryString["id"], out orderId);

            int companyId = IT.Session.Users.CompanyId();

            if (orderId == 0 || companyId == 0) return;

            var getOrderDetail = db.sp_GetOrderDetail(companyId, orderId).ToList();



        }
    }
}