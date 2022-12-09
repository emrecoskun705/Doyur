using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.user
{
	public partial class addresslist : System.Web.UI.Page
	{

		db.doyurEntities db = new db.doyurEntities();
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				LoadAddress();
			}
		}

		private void LoadAddress()
		{
			int userId = IT.Session.Users.UserId();

			var getAddressList = db.sp_GetAddress(userId, 2).ToList();

			if(getAddressList != null && getAddressList.Count() > 0)
			{
				gList.DataSource= getAddressList;
			} else
			{
				// empty list
			}

			gList.DataBind();
		}
	}
}