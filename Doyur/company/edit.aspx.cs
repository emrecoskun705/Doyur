using Doyur.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.company
{
    public partial class edit : System.Web.UI.Page
    {

        db.doyurEntities db = new db.doyurEntities();

        public List<db.sp_GetFeature_Result> FeatureList { get; set; }
        public List<int> SelectedFeatureIds { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadProdut();
            }
        }

        private void LoadProdut()
        {
            var productId = Convert.ToInt32(Request.QueryString["id"]);

            var getProduct = db.sp_GetProduct(productId).FirstOrDefault();
            if (getProduct != null)
            {
                pName.Text = getProduct.Name.Trim();
                pPrice.Text = getProduct.Price.ToString();
                pStock.Text = getProduct.Stock.ToString();

                int categoryId = getProduct.CategoryId;
                LoadSelectedFeatures(productId);
                LoadFeatures(categoryId);
            }
        }

        /// <summary>
        /// loads selected features for a product
        /// </summary>
        /// <param name="productId"></param>
        private void LoadSelectedFeatures(int productId)
        {
            SelectedFeatureIds = db.sp_GetSelectedFeatures(productId).ToList().Select(x => x.FeatureId).ToList();
        }


        /// <summary>
        /// loads all feature that is related to product
        /// </summary>
        /// <param name="categoryId"></param>
        private void LoadFeatures(int categoryId)
        {
            FeatureList = db.sp_GetFeature(categoryId).ToList();

            List<db.sp_GetFeature_Result> onlyHeads = FeatureList.FindAll(x => x.SubFeatureId == null);

            parentR.DataSource = onlyHeads;
            parentR.DataBind();
        }


        protected void parentR_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater childRepeater = (Repeater)args.Item.FindControl("childR");

                var parentFeature = args.Item.DataItem as db.sp_GetFeature_Result;

                var subList = FeatureList.FindAll(x => x.SubFeatureId == (parentFeature.FeatureId));
                childRepeater.DataSource = subList;
                childRepeater.DataBind();
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            List<int> newCheckedFeatures = new List<int>();
            foreach (RepeaterItem item in parentR.Items)
            {
                if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
                {
                    Repeater childR = item.FindControl("childR") as Repeater;
                    foreach (RepeaterItem childitem in childR.Items)
                    {
                        CheckBox check = childitem.FindControl("cBoxId") as CheckBox;
                        if (check != null && check.Checked)
                        {
                            int checkedId = Convert.ToInt32((childitem.FindControl("hdnId") as HiddenField).Value);
                            newCheckedFeatures.Add(checkedId);
                        }
                    }
                }
            }

            


        }
    }
}