using Doyur.extensions;
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

        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
				var getUser = (from p in db.Users where p.Mail == mail.Text.Trim() && p.Password == password.Text.Trim() && p.IsActive == true select p).FirstOrDefault();


				if (getUser != null)
				{
					// user and admin login
					if ((getUser.AccessId == 2 || getUser.AccessId == 255) && getUser.Activation != null)
					{
						IT.Session.Users.AddLoginSessionList(
							getUser.UserId,
							getUser.AccessId,
							getUser.Firstname,
							getUser.Lastname,
							getUser.Name,
							getUser.Phone,
							getUser.Mail);



						IT.Session.Users.AddMessageSession("success", "Başarıyla giriş yapıldı", "Başarılı");

						Response.Redirect("/");


					}
					// company login
					else if (getUser.AccessId == 3)
					{
						var getCompany = (from p in db.Company where p.UserId == getUser.UserId select p).FirstOrDefault();
						if (getCompany != null)
						{
							IT.Session.Users.AddLoginSessionListCompany(
								getUser.UserId,
								getUser.AccessId,
								getUser.Firstname,
								getUser.Lastname,
								getUser.Name,
								getUser.Phone,
								getUser.Mail,
								getCompany.CompanyId);

							IT.Session.Users.AddMessageSession("success", "Başarıyla şirket hesabına giriş yapıldı", "Başarılı");
							Response.Redirect("/company/");

						}

					}
					else
					{
						IT.Session.Users.AddMessageSession("warning", "Hesap yetkileri kısıtlandırıldı.", "Hata");
						Response.Redirect("/login.aspx");
					}

				}
				else
				{
					// Reirect same page
					IT.Session.Users.AddMessageSession("warning", "Hesap bilgilerinizi kontrol ediniz.", "Hata");
					Response.Redirect("/login.aspx");
				}
			}

        }
    }
}