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

        public string Username { get; set; }
        public string Mail{ get; set; }
        public string Firstname{ get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            

            username.Text = Username = IT.Session.Users.Username();
            mail.Text  = Mail = IT.Session.Users.Mail();
            firstname.Text  = Firstname = IT.Session.Users.Firstname();
            lastname.Text =  Lastname = IT.Session.Users.Lastname();
            phone.Text = Phone = IT.Session.Users.Phone();

        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            int id = IT.Session.Users.UserId();

            if (
                !username.Text.Trim().Equals(Username) || 
                !mail.Text.Trim().Equals(Mail) || 
                !firstname.Text.Trim().Equals(Firstname) ||
                !lastname.Text.Trim().Equals(Lastname) ||
                !phone.Text.Trim().Equals(phone) ||
                !password.Text.Trim().Equals("")
                )
            {
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

                    db.SaveChanges();

                }
            }

        }
    }
}