using Doyur.db;
using Doyur.extensions;
using System;
using System.Collections.Generic;
using System.IO;
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
                IsActive.Checked = getProduct.IsActive;

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
            List<int> newCheckedFeatures = GetNewFeatures();
			var productId = Convert.ToInt32(Request.QueryString["id"]);

			string strFileName = oFile.PostedFile.FileName;

            // dont update photo if no file is selected
            if(strFileName == "")
            {
                byte FuncId = (byte)1;

				

                var count = db.sp_UpdateProduct(productId, pName.Text.Trim(), IsActive.Checked, Convert.ToDecimal(pPrice.Text), "", Convert.ToInt32(pStock.Text), FuncId).FirstOrDefault();
                if(count != null && count > 0)
                {
					// update successfull
					var deleteFeatures = db.sp_DeleteFeatureProduct(productId).FirstOrDefault();
					if (deleteFeatures != null && deleteFeatures != 0)
					{
						foreach (var ftr in newCheckedFeatures)
						{
							db.sp_AddFeatureProduct(productId, ftr);
						}

					}
					this.ShowMessage("Success", "Ürün başarıyla kaydedildi");
                } else
				{
					this.ShowMessage("Warning", "Ürün kaydedilemedi");
				}
            }
            // update photo if file selected
            else
            {
                string savedName = SaveImg();
				byte FuncId = (byte)0;
				var count = db.sp_UpdateProduct(productId, pName.Text.Trim(), IsActive.Checked, Convert.ToDecimal(pPrice.Text), savedName, Convert.ToInt32(pStock.Text), FuncId).FirstOrDefault();
				if (count != null && count > 0)
				{
					// update successfull
					var deleteFeatures = db.sp_DeleteFeatureProduct(productId).FirstOrDefault();
					if (deleteFeatures != null && deleteFeatures != 0)
					{
						foreach (var ftr in newCheckedFeatures)
						{
							db.sp_AddFeatureProduct(productId, ftr);
						}

					}
					this.ShowMessage("Success", "Ürün başarıyla kaydedildi");
				} else
				{
					this.ShowMessage("Warning", "Ürün kaydedilemedi");
				}
			}


			


		}

		private string SaveImg()
		{
			string strFileName;
			string strFilePath;
			string strFolder;
			strFolder = Server.MapPath("~/image/");

			// Get the name of the file that is posted.
			strFileName = oFile.PostedFile.FileName;
			strFileName = Path.GetFileName(strFileName);

			if (oFile.Value != "")
			{
				// Create the directory if it does not exist.
				if (!Directory.Exists(strFolder))
				{
					Directory.CreateDirectory(strFolder);
				}
				// Save the uploaded file to the server.
				strFilePath = strFolder + strFileName;
				while (File.Exists(strFilePath))
				{
					// lblUploadResult.Text = strFileName + " already exists on the server!";
					strFileName = "" + (new Random()).Next(1, 10) + strFileName;
					strFilePath = strFolder + strFileName;
				}
				oFile.PostedFile.SaveAs(strFilePath);
				//lblUploadResult.Text = strFileName + " has been successfully uploaded.";
				return strFileName;

			}
			else
			{
				// lblUploadResult.Text = "Click 'Browse' to select the file to upload.";
				return "";
			}
			// Display the result of the upload.
			//frmConfirmation.Visible = true;
		}

		/// <summary>
		/// Gets checked Feature id list from page
		/// </summary>
		/// <returns></returns>
		private List<int> GetNewFeatures()
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

            return newCheckedFeatures;

		}

	}
}