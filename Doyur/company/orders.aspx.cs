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
            var getOrderP = db.sp_GetCompanyOP(compnayId);
            gList.DataSource = getOrderP;
            gList.DataBind();
        }
    }
}