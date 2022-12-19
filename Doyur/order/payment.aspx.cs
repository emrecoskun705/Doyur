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

        protected void payBtn_Click(object sender, EventArgs e)
        {
            // if address is zero no address is selected
            int selectedAddress = 0;

            foreach(GridViewRow row in gList.Rows)
            {
                CheckBox cbox = row.FindControl("isChecked") as CheckBox;
                if(cbox != null)
                {
                    if(cbox.Checked)
                    {
                        if(selectedAddress == 0)
                        {
                            selectedAddress = Convert.ToInt32(((HiddenField)row.FindControl("addressId")).Value);
                        } else
                        {
                            // multiple address is selected so give an alert to user
                        }
                    }
                }
            }
        }
    }
}