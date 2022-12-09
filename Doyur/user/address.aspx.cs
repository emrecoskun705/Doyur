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

			var getAddressList = (from p in db.sp_GetAddress(userId, addressId, 3) select p).ToList();
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
			int userId = IT.Session.Users.UserId();
			int addressId = Convert.ToInt32(Request.QueryString["AddressId"]);
			db.Address updateAddress = (from p in db.Address where p.AddressId == addressId && p.UserId == userId select p).FirstOrDefault();
			
			if(updateAddress != null)
			{
				updateAddress.Name = aName.Text.Trim();
				updateAddress.Town= aTown.Text.Trim();
				updateAddress.District= aDistrict.Text.Trim();
				updateAddress.Description= aDescription.Text.Trim();
				updateAddress.Phone = phone.Text.Trim();
				updateAddress.IsActive = IsActive.Checked;
				
			}

			if(db.SaveChanges() > 0)
			{
				BindAddressData();
				this.ShowMessage("Success", "Adres başarıyla güncellendi");
			} else
			{
				this.ShowMessage("Warning", "Adres güncellenemedi.");
			}
        }

	}
}