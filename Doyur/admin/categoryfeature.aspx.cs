using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.admin
{
	public partial class categoryfeature : System.Web.UI.Page
	{
		
		db.doyurEntities db = new db.doyurEntities();

		private List<int> SelectedFeatures { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				LoadData();
			}
		}

		private void LoadData()
		{
			var categoryId = Convert.ToInt32(Request.QueryString["id"]);
			var cName = (from p in db.Category where p.CategoryId== categoryId select p).FirstOrDefault();
			if(cName!=null)
			{
				parentLbl.Text = cName.Name;
			}

			SelectedFeatures = (from p in db.sp_GetFeatureCategory(categoryId) select p.FeatureId).ToList();



			var rootFeatures = (from p in db.Feature where p.SubFeatureId == null select p).ToList();

			gList.DataSource = rootFeatures;
			gList.DataBind();
		}

        protected void gList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

		protected void gList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType != DataControlRowType.DataRow) return;

			db.Feature c = (db.Feature) e.Row.DataItem;

			CheckBox isChecked = e.Row.FindControl("isChecked") as CheckBox;

			isChecked.Checked = SelectedFeatures.Contains(c.FeatureId) ? true : false;
		}
	}
}