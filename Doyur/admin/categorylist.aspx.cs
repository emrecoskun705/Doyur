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
	public partial class categorylist : System.Web.UI.Page
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
			if (Request.QueryString["id"] != null) 
			{
				var getParentId = Convert.ToInt32(Request.QueryString["id"]);
				var getCategories = (from p in db.Category where p.ParentId == getParentId select p).ToList();
				LoadParentPath();
				gList.DataSource = getCategories;
				gList.DataBind();
			}
			else
			{
				var getCategories = (from p in db.Category where p.ParentId == null select p).ToList();
				gList.DataSource = getCategories;
				gList.DataBind();
			}
			
		}

		private void LoadParentPath()
		{
            if (Request.QueryString["id"] != null)
            {
                List<Category> categories = new List<Category>();
				
				var getParentId = Convert.ToInt32(Request.QueryString["id"]);
				var ctg = (from p in db.Category where p.CategoryId == getParentId select p).FirstOrDefault();
				while(ctg != null )
				{
					categories.Insert(0, ctg);
					ctg = (from p in db.Category where p.CategoryId == ctg.ParentId select p).FirstOrDefault();
                }

				pathRepeater.DataSource = categories;
				pathRepeater.DataBind();
				
			}
        }


		protected void gList_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "EditCategory")
			{
				int categoryId = Convert.ToInt32(e.CommandArgument);
				Response.Redirect("/admin/categorylist.aspx?id=" + categoryId);
			}
			else if(e.CommandName == "EditFeature")
			{
				int categoryId = Convert.ToInt32(e.CommandArgument);
				Response.Redirect("/admin/categoryfeature.aspx?id=" + categoryId);
			}
		}

		protected void saveBtn_Click(object sender, EventArgs e)
		{
			if (Request.QueryString["id"] != null)
			{
				var getParentId = Convert.ToInt32(Request.QueryString["id"]);

				db.Category addCtg = new db.Category() { 
					Name = newCategoryLbl.Text.Trim(),
					ParentId = getParentId
				};

				db.Category.Add(addCtg);

				if(db.SaveChanges() > 0)
				{
					IT.Session.Users.AddMessageSession("success", "Kategori başarıyla eklendi", "Başarılı");
					Response.Redirect(Request.RawUrl);
				} else
				{
					IT.Session.Users.AddMessageSession("warning", "Kategori eklenemedi", "Hata");
					Response.Redirect(Request.RawUrl);
				}

			}
			else
			{
				// if id = null then it is root categories
				db.Category addCtg = new db.Category()
				{
					Name = newCategoryLbl.Text.Trim(),
					ParentId = null
				};

				db.Category.Add(addCtg);

				if (db.SaveChanges() > 0)
				{
					IT.Session.Users.AddMessageSession("success", "Kategori başarıyla eklendi", "Başarılı");
					Response.Redirect(Request.RawUrl);
				}
				else
				{
					IT.Session.Users.AddMessageSession("warning", "Kategori eklenemedi", "Hata");
					Response.Redirect(Request.RawUrl);
				}
			}
		}
	}
}