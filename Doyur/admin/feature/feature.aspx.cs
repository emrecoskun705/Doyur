using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.admin
{
	public partial class feature : System.Web.UI.Page
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
			var getHeadFeatures = (from p in db.Feature where p.SubFeatureId == null select p).ToList();

			gList.DataSource = getHeadFeatures;
			gList.DataBind();
		}


        protected void gList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}