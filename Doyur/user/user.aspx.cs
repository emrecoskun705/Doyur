using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.user
{
    public partial class user : System.Web.UI.Page
    {

        db.doyurEntities db = new db.doyurEntities();

		protected void Page_Load(object sender, EventArgs e)
        {


            if(!IsPostBack)
            {
				int id = IT.Session.Users.UserId();
				var getUser = (from p in db.Users where p.UserId == id select p).First();

				username.Text = getUser.Name;
				mail.Text = getUser.Mail;
				firstname.Text = getUser.Firstname;
				lastname.Text = getUser.Lastname;
				phone.Text = getUser.Phone;
			}
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            int id = IT.Session.Users.UserId();
			var getUser = (from p in db.Users where p.UserId == id select p).First();

			if ( getUser != null )
            {
                getUser.Name = username.Text.Trim();
                getUser.Mail = mail.Text.Trim();
                getUser.Firstname = firstname.Text.Trim();
                getUser.Lastname = lastname.Text.Trim();
                getUser.Phone = phone.Text.Trim();

                if(!password.Text.Trim().Equals(""))
                {
                    getUser.Password = password.Text.Trim();
                }

                try
                {
					int count = db.SaveChanges();
                    if (count > 0)
                    {
						// update session
						IT.Session.Users.AddLoginSessionList(
						    getUser.UserId,
						    2,
						    getUser.Firstname,
						    getUser.Lastname,
						    getUser.Name,
						    getUser.Phone,
						    getUser.Mail);
                        ShowMessage("Kullanıcı başarıyla güncellendi", "Success");

					} else
                    {
						ShowMessage("Kullanıcı güncellenemedi", "Danger");
					}
				} 
                catch(Exception ex)
                {
                    //handle exception
                }
                    

            }
            

        }

		protected void ShowMessage(string Message, string type)
		{
			ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "SendAlert('" + type + "','" + Message + "');", true);
		}
	}
}