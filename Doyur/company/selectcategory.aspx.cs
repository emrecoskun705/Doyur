using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.company
{
	public partial class selectcategory : System.Web.UI.Page
	{

		db.doyurEntities db = new db.doyurEntities();

        public List<List<db.Category>> categories = new List<List<db.Category>>();
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
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

			foreach(var ctg in rootCategory)
			{
				var childCategory = (from p in db.Category where p.ParentId == ctg.CategoryId select p).ToList();
				if(childCategory.Count() > 0)
				{
					categories.Add(childCategory);
				}
            }   
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
			var categoryId = lbl.Text;

			Response.Redirect("/company/create.aspx?category=" + categoryId);

        }
    }
}