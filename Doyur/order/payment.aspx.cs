using Doyur.db;
using Doyur.extensions;
using Doyur.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.order
{
    public partial class payment : System.Web.UI.Page
    {

        db.doyurEntities db = new db.doyurEntities();

        public decimal TotalPrice { get { return Convert.ToDecimal(ViewState["TotalPrice"]); } set { ViewState["TotalPrice"] = value; } }
        public int AddressCount { get { return Convert.ToInt32(ViewState["AddressCount"]); } set { ViewState["AddressCount"] = value; } }

        public int CartQuantity {get { return Convert.ToInt32(ViewState["CartQuantity"]); } set { ViewState["CartQuantity"] = value; } }

        protected void Page_Init(object sender, EventArgs e)
        {
            IT.Session.Users.UserIsNotLoginRedirect();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
				GetCart();
                LoadAddress();
            }
        }

        // Gets active cart for a given user
        private List<sp_getCProducts_Result> GetCart()
        {
            int userId = IT.Session.Users.UserId();

            var cart = (from p in db.Cart where p.UserId == userId && p.IsActive == true select p).FirstOrDefault();
            if (cart == null)
            {
				IT.Session.Users.AddMessageSession("warning", "Ürün ödemesine geçilemiyor lütfen sepetinizi kontrol ediniz.", "Uyarı");
				Response.Redirect("/");
            }
          
            var cartDetails = db.sp_getCProducts(cart.CartId).ToList();
            if(cartDetails.Count == 0)
            {
				// there is no cart item so redirect to initial page
				IT.Session.Users.AddMessageSession("warning", "Devam etmek için sepetinize ürün ekleyin", "Sepet Boş");
				Response.Redirect("/");
			}

            var productList = (from p in cartDetails
                               select new MyProduct
                               {
                                   OPInfo = new OrderProductlistDTO()
                                   {
                                       ProductQuantity = p.Quantity,
                                   },

                                   Product = new ProductDTO()
                                   {
                                       ProductId = p.ProductId,
                                       Name = p.Name,
                                       Price = p.Price,
                                   }
                               }).ToList();

            CartQuantity = productList.Count;

            pList.DataSource = productList;
            pList.DataBind();




            TotalPrice = cartDetails.Sum(x => x.Price * x.Quantity);

            return cartDetails;
        }

        private void LoadAddress()
        {
            int userId = IT.Session.Users.UserId();

            var getAddressList = db.sp_GetAddress(userId, -1, 0, 2).ToList();
            AddressCount= getAddressList.Count;
            if (getAddressList != null && getAddressList.Count() > 0)
            {
                gList.DataSource = getAddressList;
            }
            else
            {
                // empty list
            }

            gList.DataBind();
        }

        protected void gList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int addressId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("/user/address?AddressId=" + addressId + "&redirect=payment");

            }
            else if (e.CommandName == "DeleteAddress")
            {
                int userId = IT.Session.Users.UserId();
                int addressId = Convert.ToInt32(e.CommandArgument);
                var getAddress = (from p in db.Address where p.AddressId == addressId && p.UserId == userId select p).FirstOrDefault();

                if (getAddress != null)
                {
                    db.Address.Remove(getAddress);
                    if (db.SaveChanges() > 0)
                    {
                        this.ShowMessage("success", "Adres başarıyla silindi", "Başarılı");
                        LoadAddress();
                    }
                    else
                    {
                        this.ShowMessage("warning", "Adres silinirken bir hata oluştu", "Hata");
                    }
                }
                else
                {
                    this.ShowMessage("warning", "Adres bulunamadı", "Hata");
                }
            }

        }


        private int getSelectedAddress()
        {
            int selectedAddress = 0;

            foreach (GridViewRow row in gList.Rows)
            {
                CheckBox cbox = row.FindControl("isChecked") as CheckBox;
                if (cbox != null && cbox.Checked)
                {

                    if (selectedAddress == 0)
                    {
                        selectedAddress = Convert.ToInt32(((HiddenField)row.FindControl("addressId")).Value);
                    } else if(selectedAddress != 0)
                    {
                        return 0;
                    }

                }
            }

            return selectedAddress;
        }

        /// <summary>
        /// Create order if quantity is less than or equal to stock, if sp returns (2, 3 or null) remove address that is created for shipping and billing
        /// </summary>
        /// <param name="order"></param>
        /// <param name="selectedAddress"></param>
        /// <returns>bool</returns>
        private bool CreateOrder(int selectedAddress)
        {
            db.Address getAddr = (from p in db.Address where p.AddressId == selectedAddress select p).FirstOrDefault();
            var getCart = GetCart();
			int userId = IT.Session.Users.UserId();

			var created = false;

			if (getAddr != null)
            {
                
                using(var tran = db.Database.BeginTransaction())
                {
                    try
                    {
						db.Address newAddr = new db.Address()
						{
							Name = getAddr.Name,
							Firstname = getAddr.Firstname,
							Lastname = getAddr.Lastname,
							Description = getAddr.Description,
							UserId = getAddr.UserId,
							Town = getAddr.Town,
							District = getAddr.District,
							Type = 1,
							Phone = getAddr.Phone,
							IsActive = true,
						};

						db.Address.Add(newAddr);

                        foreach(var company in getCart.Select(x => x.CompanyId).Distinct().ToList())
                        {

                            var order = new db.Orders()
                            {
                                CompanyId = company,
                                AddressId = newAddr.AddressId,
                                UserId = userId,
                                Status = 1,
                                IsActive = true,
                                IsPaid = true,
                                CreateDate = DateTime.Now,
                            };
                            
                            db.Orders.Add(order);
							db.SaveChanges();
							decimal totalCost = 0;
							foreach (var item in getCart.Where(x => x.CId == company))
                            {
                                var orderItem = new db.OrderProductList()
                                {
                                    OrderId = order.OrderId,
                                    ProductId = item.ProductId,
                                    ProductQuantity = item.Quantity,
                                    Status = 1
                                };
								var product = (from p in db.Product where p.ProductId == orderItem.ProductId select p).FirstOrDefault();
                                totalCost += product.Price * orderItem.ProductQuantity;
                                if(product.Stock - item.Quantity < 0)
                                {
                                    throw new DatabaseStockException("Invalid Amount Exception");
                                }

                                product.Stock -= item.Quantity; 
								db.OrderProductList.Add(orderItem);
                                db.SaveChanges();
                            }

                            order.TotalCost = totalCost;
                            db.SaveChanges();
                        }

                        var cart = (from c in db.Cart where c.UserId == userId select c).FirstOrDefault();
                        db.Cart.Remove(cart);

                        db.SaveChanges();

                        created= true;
                        tran.Commit();
					}
                    catch(DatabaseStockException ex)
                    {
						created = false;
						tran.Rollback();
						IT.Session.Users.AddMessageSession("warning", "Satın almaya çalıştığınız ürün stoklarda azaldığından lütfen ürün miktarını değiştiriniz", "Stoklar azalıyor");
						Response.Redirect(Request.RawUrl);
					}
                    catch(Exception ex)
                    {
                        created = false;
                        tran.Rollback();
                    }


				}

            
            }

            return created;
        }

        protected void payBtn_Click(object sender, EventArgs e)
        {
            // if address is zero no address is selected
            int selectedAddress = getSelectedAddress();

            if(selectedAddress == 0)
            {
                GetCart();
                this.ShowMessage("warning", "Lütfen 1 tane adres seçiniz", "Hata");
                return;
            }


            bool success = CreateOrder(selectedAddress);
            if (!success) 
            {
                IT.Session.Users.AddMessageSession("warning", "Sipariş oluşturulurken bir hata meydana geldi.", "Hata");
                Response.Redirect(Request.RawUrl);
            }

            IT.Session.Users.AddMessageSession("success", "Sipariş başarılı bir şekilde verildi", "Başarılı");
            Response.Redirect("/");
        }

		public class DatabaseStockException : Exception
		{
			public DatabaseStockException()
			{
			}

			public DatabaseStockException(string message)
				: base(message)
			{
			}

			public DatabaseStockException(string message, Exception inner)
				: base(message, inner)
			{
			}
		}


        private class MyProduct
        {
            public ProductDTO Product { get; set; }
            public OrderProductlistDTO OPInfo { get; set; }
        }
    }
}