﻿using Doyur.extensions;
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
			if (Request.QueryString["id"] != null) 
			{
				var getParentId = Convert.ToInt32(Request.QueryString["id"]);
				var getCategories = (from p in db.Category where p.ParentId == getParentId select p).ToList();
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
					IT.Session.Users.AddMessageSession("Success", "Kategori başarıyla eklendi");
					Response.Redirect(Request.RawUrl);
				} else
				{
					IT.Session.Users.AddMessageSession("Warning", "Kategori eklenemedi");
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
					IT.Session.Users.AddMessageSession("Success", "Kategori başarıyla eklendi");
					Response.Redirect(Request.RawUrl);
				}
				else
				{
					IT.Session.Users.AddMessageSession("Warning", "Kategori eklenemedi");
					Response.Redirect(Request.RawUrl);
				}
			}
		}
	}
}