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

        public List<OrderDetailCompanyDTO> Order
        {
            get
            {
                return ViewState["order"] as List<OrderDetailCompanyDTO>;
            }

            set
            {
                ViewState["order"] = value;
            }
        }
        

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
                LoadData();
            }
        }

        private void LoadData()
        {
            int orderId;
            int.TryParse(Request.QueryString["id"], out orderId);

            int companyId = IT.Session.Users.CompanyId();

            if (orderId == 0 || companyId == 0)
            {
                IT.Session.Users.AddMessageSession("warning", "Şirket veya ürün bulunamadı", "Uyarı");
                Response.Redirect("/");
                return;
            }
            
            Order = (
                (
                from o in db.Orders 
                join op in db.OrderProductList on o.OrderId equals op.OrderId
                join p in db.Product on op.ProductId equals p.ProductId
                join a in db.Address on o.AddressId equals a.AddressId
                where o.OrderId == orderId && p.CompanyId == companyId
                select new OrderDetailCompanyDTO()
                {
                    Order = new OrdersDTO()
                    {
                        OrderId = o.OrderId,
                        UserId = o.UserId,
                        Status = o.Status,
                        Coupon = o.Coupon,
                        IsActive = o.IsActive,
                        IsPaid = o.IsPaid,
                        TotalCost = o.TotalCost,
                        AddressId = o.AddressId

                    },
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

            gList.DataSource = Order;
            gList.DataBind();

            //var getOrderDetail = db.sp_GetOrderDetail(companyId, orderId).ToList();
            if(Order.Count > 0)
            {
                LoadAddress(Order[0].Order.AddressId ?? default(int));
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

		protected void Button1_Click(object sender, EventArgs e)
		{

		}

		protected void gList_RowDataBound(object sender, GridViewRowEventArgs e)
		{

		}

		protected void gList_RowCommand(object sender, GridViewCommandEventArgs e)
		{

		}

		protected void chkAll_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox chckheader = (CheckBox)gList.HeaderRow.FindControl("chkAll");
			foreach (GridViewRow row in gList.Rows)
			{
				CheckBox chckrw = (CheckBox)row.FindControl("chkRow");
				if (chckheader.Checked == true)
				{
					chckrw.Checked = true;

				}
				else
				{
					chckrw.Checked = false;
				}

			}
		}
	}
}