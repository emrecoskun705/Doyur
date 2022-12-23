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
	public partial class categoryfeature : System.Web.UI.Page
	{
		
		db.doyurEntities db = new db.doyurEntities();

		private List<int> SelectedFeatures { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
            if (IT.Session.Users.MsgType() != "" && IT.Session.Users.Msg() != "")
            {
                this.ShowMessage(IT.Session.Users.MsgType(), IT.Session.Users.Msg(), IT.Session.Users.MsgTitle());
                IT.Session.Users.RemoveSessionMsg();
            }

            if (!IsPostBack)
			{
                LoadData();
			}
		}

		/// <summary>
		/// Loads root features for given category
		/// </summary>
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

        protected void saveBtn_Click(object sender, EventArgs e)
		{
            var categoryId = Convert.ToInt32(Request.QueryString["id"]);

			var newSelectedF = new List<int>();

			foreach(GridViewRow row in gList.Rows)
			{
				if(((CheckBox)row.FindControl("isChecked")).Checked)
				{
					newSelectedF.Add(Convert.ToInt32(row.Cells[0].Text));
				}
			}

			// delete old features
			var deleted = db.sp_DeleteFeatureC(categoryId).FirstOrDefault();
			if(deleted != null)
			{
				// if delete is succesfull add new features to category
				foreach(int ftr in newSelectedF)
				{
					var addFtr = db.sp_AddFeatureCtg(categoryId, ftr);
				}

				IT.Session.Users.AddMessageSession("success", "Özellikler başarıyla kaydedildi.", "Başarılı");
				Response.Redirect(Request.RawUrl);
			}

			return;
        }
    }
}