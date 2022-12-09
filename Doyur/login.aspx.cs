using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur
{
    public partial class login : System.Web.UI.Page
    {

        db.doyurEntities db = new db.doyurEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

            //select * from abc where users ...... blaa
            // hasan, id side 1 , accessid 2 aktivasyonlu bir user var sayalim.

            


        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            var getUserList = (from p in db.Users where p.Mail == mail.Text.Trim() && p.Password == password.Text.Trim() && p.IsActive == true select p);
            

            if (getUserList != null && getUserList.Count() == 1)
            {
                // user is logged in

                db.Users getUser = getUserList.First();

                if(getUser.AccessId == 2 && !getUser.Activation.Trim().Equals(""))
                {
                    IT.Session.Users.AddLoginSessionList(
                        getUser.UserId, 
                        1, 
                        getUser.Firstname, 
                        getUser.Lastname, 
                        getUser.Name, 
                        getUser.Phone, 
                        getUser.Mail);

                    var getAddress = (from p in db.sp_GetAddress(getUser.UserId, 1) select p).ToList();

                    // user has an active address so no need to add address
                    if(getAddress != null && getAddress.Count() > 0) 
                    {
                        IT.Session.Users.AddAddressSession(getAddress.First().AddressId);
						Response.Redirect("user/default.aspx");
					}
                    // no active address exist for user so redirect to address page
                    else
                    {
                        Response.Redirect("user/address.aspx");
					}

					
                } else
                {
                    Response.Redirect("/login.aspx");
                }

            } else
            {
                // Reirect same page
                Response.Redirect("/login.aspx");
            }

        }
    }
}