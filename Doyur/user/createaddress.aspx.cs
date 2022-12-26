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
			if(Page.IsValid)
			{
				int userId = IT.Session.Users.UserId();

				var addrList = (from p in db.Address where p.UserId == userId && p.Type == (byte)0 select p).ToList();
				if (addrList != null && addrList.Count < 5)
				{
					//address creation is successfull
					var newAddr = new db.Address()
					{
						UserId = userId,
						Type = (byte)0,
						Name = aName.Text,
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
						this.ShowMessage("success", "Adres başarıyla eklendi", "Başarılı");
					} else
					{
                        this.ShowMessage("warning", "Adres eklenirken bir hata meydana geldi", "Hata");
                    }

				}
				else
				{
					this.ShowMessage("warning", "Adres eklenemedi, en fazla 5 adrese sahip olabilirsiniz.", "Hata");
				}
			}

        }
    }
}