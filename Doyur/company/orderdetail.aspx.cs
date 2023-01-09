using Doyur.extensions;
using Doyur.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Types;

namespace Doyur.company
{
    public partial class orderdetail : System.Web.UI.Page
    {

        db.doyurEntities db = new db.doyurEntities();

        public List<OrderDetailCompanyDTO> Order{get; set;     }
        

        public AddressDTO Address
        {
            get
            {
                return ViewState["address"] as AddressDTO;
            }

            set
            {
                ViewState["address"] = value;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {

        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                int addressId = LoadData();
				LoadAddress(addressId);
			}
        }

		private int LoadData()
        {
			int orderId;
			int.TryParse(Request.QueryString["id"], out orderId);

			int companyId = IT.Session.Users.CompanyId();

			if (orderId == 0 || companyId == 0)
			{
				IT.Session.Users.AddMessageSession("warning", "Şirket veya ürün bulunamadı", "Uyarı");
				Response.Redirect("/");
			}

			var order = (from p in db.Orders where p.OrderId == orderId select p).FirstOrDefault();

			if (order == null)
			{
				IT.Session.Users.AddMessageSession("warning", "Sipariş bulunamadı", "Uyarı");
				Response.Redirect("/company/orders");
			}

			GetOrderDetail(orderId, companyId);
            // gList data bind
            gList.DataSource = Order;
			gList.DataKeyNames = new string[] { "Product" };
            gList.DataBind();

            // ddList data bind
            ddl.DataSource = Types.OrderProduct.GetOrderPStatus().Where(x => x.AccessId == 3).ToList();
			ddl.DataTextField = "Title";
			ddl.DataValueField = "StatusId";

			ddl.DataBind();

			ListItem emptyItem = new ListItem("Seçiniz", "0");
			ddl.Items.Insert(0, emptyItem);
			ddl.Items[0].Attributes["disabled"] = "disabled";
            ddl.SelectedValue= "0";

            //var getOrderDetail = db.sp_GetOrderDetail(companyId, orderId).ToList();


            return order.AddressId ?? default(int);
            
        }

		private void GetOrderDetail(int orderId, int companyId)
		{

			Order = (
				(
				from op in db.OrderProductList
				join p in db.Product on op.ProductId equals p.ProductId
				where p.CompanyId == companyId && op.OrderId == orderId
				select new OrderDetailCompanyDTO()
				{

					OPInfo = new OrderProductlistDTO()
					{
						OrderId = op.OrderId,
						ProductId = op.ProductId,
						ProductQuantity = op.ProductQuantity,
						Status = op.Status
					},
					Product = new ProductDTO()
					{
						ProductId = p.ProductId,
						CategoryId = p.CategoryId,
						CompanyId = p.CompanyId,
						Name = p.Name,
						Description = p.Description,
						IsActive = p.IsActive,
						Price = p.Price,
						DiscountPercantage = p.DiscountPercantage,
						ImageUrl = p.ImageUrl,
						Stock = p.Stock
					}
				})
				).ToList();

			if (Order.Count == 0)
			{
				IT.Session.Users.AddMessageSession("warning", "Sipariş bulunamadı", "Uyarı");
				Response.Redirect("/company/orders");
			}
		}

		private void LoadAddress(int addressId)
		{

            Address = (from p in db.Address where p.AddressId == addressId select new AddressDTO()
            {
                AddressId = p.AddressId,
                Firstname = p.Firstname,
                Lastname = p.Lastname,
				UserId = p.UserId,
				Type = p.Type,
                Name = p.Name,
                Town = p.Town,
                District = p.District,
                Description = p.Description,
                Phone = p.Phone
			}).FirstOrDefault();

		}

		protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
		{

			var getSelected = new List<ProductDTO>();

			int ddlValue;
			int.TryParse(ddl.SelectedValue, out ddlValue);

			foreach (GridViewRow row in gList.Rows)
			{
				if (((CheckBox)row.FindControl("chkRow")).Checked)
				{
					// add product Id to getSelected list
					ProductDTO product = (ProductDTO)gList.DataKeys[row.RowIndex].Value;
					((Label)row.FindControl("opstatus")).Text = Types.OrderProduct.GetOrderPStatus()[Convert.ToInt32(ddlValue)].Title;
					getSelected.Add(product);
				}
			}

			bool success = false;
			try
			{
                foreach (ProductDTO product in getSelected)
                {
                    var getOP = (from op in db.OrderProductList where op.ProductId == product.ProductId select op).FirstOrDefault();
                    if (getOP != null)
                    {
                        getOP.Status = (byte)ddlValue;
                    }
					db.SaveChanges();
                }
				success = true;
            } catch (Exception ex)
			{
				success = false;
			}



			if(success)
			{
				// success
				this.ShowMessage("success", "Ürünler başarıyla güncellendi", "Başarılı");

				foreach (GridViewRow row in gList.Rows)
				{
					if (((CheckBox)row.FindControl("chkRow")).Checked)
					{
						// add product Id to getSelected list
						ProductDTO product = (ProductDTO)gList.DataKeys[row.RowIndex].Value;
						((Label)row.FindControl("opstatus")).Text = Types.OrderProduct.GetOrderPStatus()[Convert.ToInt32(ddlValue)].Title;
						getSelected.Add(product);
					}
				}

			} 
			else
			{
				this.ShowMessage("warning", "Ürünler kaydedilirken bir sorun oluştu", "Uyarı");
			}
			ddl.Items[0].Attributes["disabled"] = "disabled";


		}
	}
}