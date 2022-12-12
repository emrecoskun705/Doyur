using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Doyur.extensions;

namespace Doyur.company
{
	public partial class create : System.Web.UI.Page
	{

        db.doyurEntities db = new db.doyurEntities();

		public List<db.sp_GetFeature_Result> FeatureList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				LoadPage();
			}
		}

		private void LoadPage()
		{
			var categoryId = Convert.ToInt32(Request.QueryString["category"]);

            FeatureList = db.sp_GetFeature(categoryId).ToList();

			List<db.sp_GetFeature_Result>  onlyHeads = FeatureList.FindAll(x => x.SubFeatureId == null);

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

            List<int> checkedFeatures = new List<int>();
            foreach(RepeaterItem item in parentR.Items)
            {
                if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
                {
                    Repeater childR = item.FindControl("childR") as Repeater;
                    foreach(RepeaterItem childitem in childR.Items)
                    {
                        CheckBox check = childitem.FindControl("cBoxId") as CheckBox;
                        if (check != null && check.Checked)
                        {
                            int checkedId = Convert.ToInt32((childitem.FindControl("hdnId") as HiddenField).Value);
                            checkedFeatures.Add(checkedId);
                        }
                    }
                }
            }

            string savedName = SaveImg();
            if(savedName != "")
            {
                var categoryId = Convert.ToInt32(Request.QueryString["category"]);

                var createProduct = new db.Product()
                {
                    CategoryId = categoryId,
                    CompanyId = 1,
                    Name = pName.Text.Trim(),
                    IsActive = true,
                    Price = Convert.ToDecimal(pPrice.Text.Trim()),
                    ImageUrl = savedName,
                    Stock = Convert.ToInt32(pStock.Text.Trim())
                };

                db.Product.Add(createProduct);
                if(db.SaveChanges() > 0)
                {
                    foreach (int i in checkedFeatures)
                    {
                        var count = db.sp_AddFeatureProduct(createProduct.ProductId, i).FirstOrDefault();
                        if(count != null && count > 0)
                        {
                            //success
                        } 
                        else
                        {
                            // fail
                            
                        }
                    }

                    this.ShowMessage("Success", "Ürün başarıyla kaydedildi.");
                } else
                {
                    this.ShowMessage("Warning", "Üürn kaydedilirken bir hata meydana geldi.");
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


        /*        protected void btnbtnConfirm_OnClick(object sender, EventArgs e)
                {


                    foreach (RepeaterItem item in rptStdent.Items)
                    {
                        if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
                        {
                            CheckBox cbStdent = item.FindControl("cbStdent") as CheckBox;
                            if (cbStdent.Checked)
                            {
                                // Do your logic
                            }
                        }
                    }

                }*/

    }
}