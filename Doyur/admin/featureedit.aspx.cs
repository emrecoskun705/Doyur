using Doyur.db;
using Doyur.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.admin
{
	public partial class featureedit : System.Web.UI.Page
	{

		db.doyurEntities db = new db.doyurEntities();

		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				if (IT.Session.Users.MsgType() != "" && IT.Session.Users.Msg() != "")
				{
					this.ShowMessage(IT.Session.Users.MsgType(), IT.Session.Users.Msg());
					IT.Session.Users.RemoveSessionMsg();
				}
				LoadData();
			}
		}

		private void LoadData()
		{
			var getParentId = Convert.ToInt32(Request.QueryString["id"]);

			var parantName = (from p in db.Feature where p.FeatureId== getParentId select p).FirstOrDefault();

			if (parantName != null)
			{
				headL.Text = parantName.Name;
			}

			var getFeatures = (from p in db.Feature where p.SubFeatureId == getParentId select p).ToList();

			gList.DataSource = getFeatures;
			gList.DataBind();
		}

		private void LoadSubData()
		{
			var getParentId = Convert.ToInt32(Request.QueryString["id"]);

			var getFeatures = (from p in db.Feature where p.SubFeatureId == getParentId select p).ToList();

			gList.DataSource = getFeatures;
			gList.DataBind();
		}


		protected void gList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
			if(e.CommandName == "DeleteFeature")
			{
				int featureId = Convert.ToInt32(e.CommandArgument);

				var deleteFeature = db.sp_DeleteFeature(featureId).FirstOrDefault();

				if(deleteFeature != null &&  deleteFeature > 0)
				{
					// success
					var getParentId = Convert.ToInt32(Request.QueryString["id"]);
					IT.Session.Users.AddMessageSession("Success", "İç özellik başarıyla silindi");
					Response.Redirect("/admin/featureedit.aspx?id=" + getParentId);
				} else
				{
					// fail
					this.ShowMessage("Warning", "İç özellik silinemedi");
				}



			}
		}

        protected void saveBtn_Click(object sender, EventArgs e)
        {
			var getParentId = Convert.ToInt32(Request.QueryString["id"]);

			var newSubFeature = new db.Feature()
			{
				SubFeatureId = getParentId,
				Name = newFeatureLbl.Text.Trim()
			};

			db.Feature.Add(newSubFeature);
			if(db.SaveChanges() > 0)
			{
				// success
				IT.Session.Users.AddMessageSession("Success", "İç özellik başarıyla eklendi");
				Response.Redirect("/admin/featureedit.aspx?id=" + getParentId);
			}
			else
			{
				// fail
				this.ShowMessage("Warning", "İç özellik eklenemedi");
			}
		}
    }
}