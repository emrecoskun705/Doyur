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

		public List<List<db.Category>> categories= new List<List<db.Category>>();
		protected void Page_Load(object sender, EventArgs e)
		{
			var rootCategory = (from p in db.Category where p.ParentId == null select p).ToList();
			var subCategory = (from p in db.Category where p.ParentId == 3 select p).ToList();
			categories.Add(rootCategory);
			categories.Add(subCategory);
			parentR.DataSource = categories;
			parentR.DataBind();
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
	}
}