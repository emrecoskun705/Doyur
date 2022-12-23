using Doyur.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.admin
{
	public partial class featurelist : System.Web.UI.Page
	{
		
		db.doyurEntities db = new db.doyurEntities();

		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
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
			if (e.CommandName == "EditFeature")
			{
				int productId = Convert.ToInt32(e.CommandArgument);
				Response.Redirect("/admin/featureedit.aspx?id=" + productId);

			}
			else if (e.CommandName == "DeleteFeature")
			{
				int featureId = Convert.ToInt32(e.CommandArgument);

				// delete head feature and it'sub features
				var deleteFeature = db.sp_DeleteFeature(featureId, 1).FirstOrDefault();

				if (deleteFeature != null && deleteFeature > 0)
				{
					// success
					var getParentId = Convert.ToInt32(Request.QueryString["id"]);
					IT.Session.Users.AddMessageSession("success", "İç özellik başarıyla silindi", "Başarılı");
					Response.Redirect(Request.RawUrl);
				}
				else
				{
					// fail
					this.ShowMessage("warning", "İç özellik silinemedi", "Hata");
				}
			}
		}

        protected void saveBtn_Click(object sender, EventArgs e)
        {
			db.Feature addParentFeature = new db.Feature()
			{
				Name = newFeatureLbl.Text.Trim(),
			};

			db.Feature.Add(addParentFeature);

			if(db.SaveChanges() > 0)
			{
				// success
				IT.Session.Users.AddMessageSession("success", "Özellik başarıyla eklendi", "Başarılı");
				Response.Redirect(Request.RawUrl);
			}else
			{
				// fail
				IT.Session.Users.AddMessageSession("warning", "Özellik eklenemedi", "Hata");
				Response.Redirect(Request.RawUrl);
			}

        }
    }
}