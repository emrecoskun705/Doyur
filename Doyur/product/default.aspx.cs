using Doyur.admin;
using Doyur.db;
using Doyur.extensions;
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

        public db.Company Company { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            int productId;
			int.TryParse(Request.QueryString["id"], out productId);

            var getProduct = db.sp_GetProduct(productId).FirstOrDefault();

            if (getProduct != null && productId != 0)
            {
                Product = getProduct;
                
                LoadSelectedFeatures(productId);
                LoadFeatures(getProduct.CategoryId);
                LoadCompany(getProduct.CompanyId);
            } else
            {
                Response.Clear();
                Response.StatusCode = 404;
                Response.End();
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

        private void LoadCompany(int companyId)
        {
            Company = (from p in db.Company where p.CompanyId == companyId select p).FirstOrDefault();
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

        protected void orderBtn_Click(object sender, EventArgs e)
        {
            int userId = IT.Session.Users.UserId();
            // if user is not logged in dont add product show alert for logging in
            if(userId == 0)
            {
                this.ShowMessage("warning", "Sepete ürün eklemek için lütfen giriş yapınız", "Hata");
                return;
            }

            int productId;
			int.TryParse(Request.QueryString["id"], out productId);

			var getOrCreateOrdr = db.sp_GetOrCreateOrder(userId).FirstOrDefault();

            if (getOrCreateOrdr != null)
            {
                var addProduct = db.sp_AddProductToOrder(productId, getOrCreateOrdr.OrderId, 1).FirstOrDefault();
                if (addProduct != null && addProduct > 0) 
                {
                    IT.Session.Users.AddMessageSession("success", "Ürün sepete başarıyla eklendi", "Başarılı");
                } 
                else
                {
					IT.Session.Users.AddMessageSession("warning", "Ürün zaten sepete eklendi", "Hata");
				}
            } 
            else
            {
				IT.Session.Users.AddMessageSession("error", "Ürüne veya siparişe ulaşılamıyor", "Hata");
			}

            Response.Redirect(Request.RawUrl);
        }
    }
}