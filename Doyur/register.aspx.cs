﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Doyur.extensions;

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

			if (!Page.IsValid) 
			{
				return;
			}
            
			var getUser = (from p in db.Users
							where p.Mail == mail.Text
							|| p.Name == username.Text
							|| p.Phone == phone.Text
							select p).FirstOrDefault();
				

			//TODO: ADD VALIDATION
			if (getUser == null)
			{
				Random rnd = new Random();

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
					Activation = "" + rnd.Next(0, 9) + rnd.Next(0, 9) + rnd.Next(0, 9) + rnd.Next(0, 9),
                    IsName = true,
					IsPhone = true,
					IsGsm = true,
					IsPath = true,
					IsActivation = false,
					IsActive = true,
				};

				db.Users.Add(newUser);

				int count = db.SaveChanges();
				if (count > 0)
				{
					IT.Session.Users.AddMessageSession("success", "Kaydınız başarılı bir şekilde oluşturuldu, lütfen devam etmek için giriş yapınız", "Başarılı");
					Response.Redirect("/login.aspx");
				}
				else
				{
					this.ShowMessage("error", "Kaydetmede bir hata oluştu lütfen tekrar deneyiniz", "Sunucu Hatası");
				}
			}
			else
			{
				this.ShowMessage("warning", "Lütfen bilgilerinizi değiştirip tekrar deneyiniz", "Hata");
			}


			
		}
		
	}

}