using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Doyur.extensions;

namespace Doyur.user
{
	public partial class createaddress : System.Web.UI.Page
	{

		db.doyurEntities db = new db.doyurEntities();

		protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
			if(!Page.IsValid)
			{
				return;
			}
			
			int userId = IT.Session.Users.UserId();

			var addrList = (from p in db.Address where p.UserId == userId && p.Type == (byte)0 select p).ToList();
			// if addrList is null or count is greater or equal to 5 than give warning
			if (addrList == null || addrList.Count >= 5)
			{
				this.ShowMessage("warning", "Adres eklenemedi, en fazla 5 adrese sahip olabilirsiniz.", "Uyarı");
				return;
			}

			//address creation is successfull
			var newAddr = new db.Address()
			{
				UserId = userId,
				Type = (byte)0,
				Name = aName.Text,
				Firstname = aFirstname.Text,
				Lastname = aLastname.Text,
				Town = aTown.Text,
				District= aDistrict.Text,
				Description= aDescription.Text,
				Phone = phone.Text,
				IsActive = IsActive.Checked,
			};

			if(IsActive.Checked )
			{
				foreach(var addr in addrList )
				{
					addr.IsActive = false;
				}
			}

			db.Address.Add(newAddr);

			if(db.SaveChanges() > 0)
			{
				if (Request.QueryString["redirect"] == "payment")
				{
					IT.Session.Users.AddMessageSession("success", "Adres başarıyla eklendi", "Başarılı");
					Response.Redirect("/order/payment");
				}
				this.ShowMessage("success", "Adres başarıyla eklendi", "Başarılı");
			} else
			{
                this.ShowMessage("error", "Adres eklenirken bir hata meydana geldi", "Hata");
            }



			

        }
    }
}