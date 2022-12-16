using Doyur.admin;
using Doyur.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.product
{
    public partial class _default : System.Web.UI.Page
    {

        db.doyurEntities db = new db.doyurEntities();
        public List<db.sp_GetFeature_Result> FeatureList { get; set; }

        public List<int> SelectedFeatureIds { get; set; }

        public db.sp_GetProduct_Result Product { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var productId = Convert.ToInt32(Request.QueryString["id"]);

            var getProduct = db.sp_GetProduct(productId).FirstOrDefault();

            if (getProduct != null)
            {
                Product = getProduct;
                LoadSelectedFeatures(productId);
                LoadFeatures(getProduct.CategoryId);
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

    }
}