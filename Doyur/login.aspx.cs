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


		/// <summary>
		/// If activation is successfull redirect to first page, if it is not show an alert to customer that code is not correct
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static bool Activation(string code)
        {
            db.doyurEntities db = new db.doyurEntities();

			int userId = IT.Session.Users.Activation();

			var getUser = (from p in db.Users where p.UserId == userId select p).FirstOrDefault();
			if(getUser != null && getUser.Activation == code)
			{
				getUser.AccessId = 2;
				getUser.IsActivation = true;
				if (db.SaveChanges() > 0)
				{
					IT.Session.Users.RevomeSessionList();
					IT.Session.Users.AddLoginSessionList(
							getUser.UserId,
							getUser.AccessId,
							getUser.Firstname,
							getUser.Lastname,
							getUser.Name,
							getUser.Phone,
							getUser.Mail);
					IT.Session.Users.AddMessageSession("success", "Hesabınız aktive edildi", "Başarılı");
					return true;
				}
				return true;
			}

			return false;
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

					} else if(getUser.AccessId == 0 && getUser.IsActivation == false)
					{
						IT.Session.Users.AddActivationSession(getUser.UserId);
						ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "OpenActivation()", true);
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