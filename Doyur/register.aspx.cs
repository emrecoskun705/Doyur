﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur
{
    public partial class register : System.Web.UI.Page
    {

        db.doyurEntities db = new db.doyurEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void CreateButton_Click(object sender, EventArgs e)
        {
  
            var getUser = (from p in db.Users
                           where p.Mail == mail.Text
                           || p.Name == username.Text
                           select p);
            //TODO: ADD VALIDATION
            if (getUser != null && getUser.Count() == 0)
            {
                db.Users newUser = new db.Users
                {
                    AccessId = 0,
                    Name = username.Text,
                    Firstname = firstname.Text,
                    Lastname = lastname.Text,
                    Phone = phone.Text,
                    Gsm = gsm.Text,
                    Mail = mail.Text,
                    Password = password1.Text,
                    Gender = (byte)(Convert.ToInt32(gender.SelectedValue)),
                    BrithDate = Convert.ToDateTime(birthdatetime.Text),
                    IsName = true,
                    IsPhone = true,
                    IsGsm = true,
                    IsPath = true,
                    IsActivation = true,
                    IsActive = true,
                };

                db.Users.Add(newUser);

                int count = db.SaveChanges();
                if(count > 0)
                {
					ShowMessage("Kaydınız başarılı bir şekilde oluşturuldu, lütfen devam etmek için giriş yapınız", "Success");
				} else
                {
					ShowMessage("Kaydetmede bir hata oluştu lütfen tekrar deneyiniz", "Danger");
				}
			} else
            {
				ShowMessage("Lütfen bilgilerinizi değiştirip tekrar deneyiniz", "Warning");
			}


        }

		protected void ShowMessage(string Message, string type)
		{
			ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "SendAlert('" + type+ "','" + Message + "');", true);
		}
	}

}