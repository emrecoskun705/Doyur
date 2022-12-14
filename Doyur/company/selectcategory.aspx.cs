using Doyur.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.company
{
	public partial class selectcategory : System.Web.UI.Page
	{

		db.doyurEntities db = new db.doyurEntities();

        public List<List<db.Category>> categories = new List<List<db.Category>>();

		private string categoryIdVS;

		protected void Page_PreRender(Object sender, EventArgs e)
		{
			ViewState["categoryId"] = categoryIdVS;
		}

		protected void Page_Load(object sender, EventArgs e)
		{

			if(IsPostBack)
			{
				categoryIdVS = (string)ViewState["categoryId"];
			}

			if (!IsPostBack)
			{

				GetCategories();
                parentR.DataSource = categories;
                parentR.DataBind();

            }
		}


		private void GetCategories()
		{
            var rootCategory = (from p in db.Category where p.ParentId == null select p).ToList();
            categories.Add(rootCategory);

			//foreach(var ctg in rootCategory)
			//{
			//	var childCategory = (from p in db.Category where p.ParentId == ctg.CategoryId select p).ToList();
			//	if(childCategory.Count() > 0)
			//	{
			//		categories.Add(childCategory);
			//	}
   //         }   
        }

		protected void parentR_ItemDataBound(object sender, RepeaterItemEventArgs args)
		{
			if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
			{
				Repeater childRepeater = (Repeater)args.Item.FindControl("childR");
				List<db.Category> categoryList = (List<db.Category>)args.Item.DataItem;
				childRepeater.DataSource = categoryList;
				childRepeater.DataBind();
			}
		}

        protected void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
			// child repeater
			RepeaterItem child = (RepeaterItem)btn.NamingContainer;
			//parent repeater
			RepeaterItem parent = (RepeaterItem)btn.NamingContainer.Parent.NamingContainer;
			int childIndex = child.ItemIndex;
			int parentIndex = parent.ItemIndex;

			Label lbl = child.FindControl("cValue") as Label;
			var categoryId = Convert.ToInt32(lbl.Text);

			var subCategory = (from p in db.Category where p.ParentId == categoryId select p).ToList();

			if(subCategory.Count() == 0)
			{
				createBtn.Visible = true;
				categoryIdVS = Convert.ToString(categoryId);
				this.ShowMessage("Success", "Katogeri seçimi tamanlandı lütfen devam butonuna basınız");
				this.ShowMessage("Info", "Seçilen kategori: " + ((Label)child.FindControl("cName")).Text);
			} 
			else
			{
				categories.Add(subCategory);
				parentR.DataSource = categories;
				parentR.DataBind();
			}
		}

		protected void createBtn_Click(object sender, EventArgs e)
		{
			Response.Redirect("/company/create.aspx?category=" + categoryIdVS);
		}
	}
}