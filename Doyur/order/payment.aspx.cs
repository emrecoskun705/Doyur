using Doyur.db;
using Doyur.extensions;
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

        public decimal TotalPrice { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            IT.Session.Users.UserIsNotLoginRedirect();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                GetOrder();
                LoadAddress();
            }
        }

        // Gets active order for a given user
        private void GetOrder()
        {
            int userId = IT.Session.Users.UserId();

            var order = db.sp_GetOrCreateOrder(userId).FirstOrDefault();
            if (order != null)
            {
                var orderDetails = db.sp_getOProducts(order.OrderId).ToList();
                TotalPrice = orderDetails.Sum(x => x.Price * x.ProductQuantity);
            } else
            {
                // giver error because order is not found
            }
        }

        private void LoadAddress()
        {
            int userId = IT.Session.Users.UserId();

            var getAddressList = db.sp_GetAddress(userId, -1, 0, 2).ToList();

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
                Response.Redirect("/user/address.aspx?AddressId=" + addressId);

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
                        this.ShowMessage("Success", "Adres başarıyla silindi");
                        LoadAddress();
                    }
                    else
                    {
                        this.ShowMessage("Warning", "Adres silinirken bir hata oluştu");
                    }
                }
                else
                {
                    this.ShowMessage("Warning", "Adres bulunamadı");
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

        private bool updateOrder(db.sp_GetOrCreateOrder_Result order, int selectedAddress)
        {
            db.Address getAddr = (from p in db.Address where p.AddressId == selectedAddress select p).FirstOrDefault();

            if (getAddr != null)
            {
                db.Address newAddr = new db.Address()
                {
                    Name = getAddr.Name,
                    AddressId = getAddr.AddressId,
                    Description = getAddr.Description,
                    UserId = getAddr.UserId,
                    Town = getAddr.Town,
                    District = getAddr.District,
                    Type = 1,
                    Phone = getAddr.Phone,
                    IsActive = true,
                };

                db.Address.Add(newAddr);

                if (db.SaveChanges() > 0)
                {
                    // success

                    var success = db.sp_UpdateOrder
                    (
                    orderId: order.OrderId,
                    isActive: false,
                    isPaid: true,
                    addressId: newAddr.AddressId,
                    totalCost: 100,
                    orderStatus: Types.Order.GetOrderStatus()[1].StatusId,
                    productStatus: Types.OrderProduct.GetOrderPStatus()[1].StatusId,
                    funcId: 0
                    ).FirstOrDefault();

                    if (success == null) return false;
                    return true;
                }
                else
                {
                    // fail
                    return false;
                }
            }

            return false;
        }

        protected void payBtn_Click(object sender, EventArgs e)
        {
            int userId = IT.Session.Users.UserId();
            var order = db.sp_GetOrCreateOrder(userId).FirstOrDefault();
            if (order == null) return;

            // if address is zero no address is selected
            int selectedAddress = getSelectedAddress();

            if(selectedAddress == 0)
            {
                GetOrder();
                this.ShowMessage("Warning", "Lütfen 1 tane adres seçiniz");
                return;
            }


            bool success = updateOrder(order, selectedAddress);
            if (!success) 
            {
                IT.Session.Users.AddMessageSession("Warning", "Sipariş oluşturulurken bir hata meydana geldi.");
                Response.Redirect(Request.RawUrl);
            }

            IT.Session.Users.AddMessageSession("Success", "Sipariş başarılı bir şekilde verildi");
            Response.Redirect("/");
        }
    }
}