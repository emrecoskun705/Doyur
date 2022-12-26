using Doyur.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.user
{
	public partial class address : System.Web.UI.Page
	{
		
		db.doyurEntities db = new db.doyurEntities();

		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				BindAddressData();
			}

			
		}

		private void BindAddressData()
		{
			int userId = IT.Session.Users.UserId();
			var addressId = Convert.ToInt32(Request.QueryString["AddressId"]);

			var getAddressList = (from p in db.sp_GetAddress(userId, addressId, 0, 3) select p).ToList();
			if(getAddressList != null && getAddressList.Count() > 0)
			{
				var getAddress = getAddressList.First();
				aName.Text = getAddress.Name.Trim();
				aTown.Text = getAddress.Town.Trim();
				aDistrict.Text = getAddress.District.Trim();
				aDescription.Text = getAddress.Description.Trim();
				phone.Text = getAddress.Phone.Trim();
				IsActive.Checked = getAddress.IsActive;
			}

		}

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
			if(Page.IsValid)
			{
                int userId = IT.Session.Users.UserId();
                int addressId = Convert.ToInt32(Request.QueryString["AddressId"]);
                var addrList = (from p in db.Address where p.UserId == userId && p.Type == 0 select p).ToList();

                var updateAddress = addrList.Where(x => x.AddressId == addressId).FirstOrDefault();

                if (updateAddress != null)
                {
					if(IsActive.Checked)
					{
						foreach (var addr in addrList)
						{
							addr.IsActive = false;
						}
					}

                    updateAddress.Name = aName.Text.TrimEnd();
                    updateAddress.Town = aTown.Text.TrimEnd();
                    updateAddress.District = aDistrict.Text.TrimEnd();
                    updateAddress.Description = aDescription.Text.TrimEnd();
                    updateAddress.Phone = phone.Text.TrimEnd();
                    updateAddress.IsActive = IsActive.Checked;

                }

                if (db.SaveChanges() > 0)
                {
                    BindAddressData();
                    this.ShowMessage("success", "Adres başarıyla güncellendi", "Başarılı");
                }
                else
                {
                    this.ShowMessage("warning", "Adres güncellenemedi.", "Hata");
                }
            }

        }

	}
}