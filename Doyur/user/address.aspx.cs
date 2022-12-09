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

			// func id 2 gets all the address list for given user
			var getAddressList = (from p in db.sp_GetAddress(userId, 2) select p).ToList();

			if (getAddressList != null && getAddressList.Count > 0)
			{
				addressDL.DataSource = getAddressList;
				addressDL.DataTextField= "Name";
				addressDL.DataValueField= "AddressId";
				
				var selectedValue = getAddressList.Find(x => x.IsActive);
				if(selectedValue != null)
				{
					addressDL.SelectedValue = Convert.ToString(selectedValue.AddressId);
				}
				addressDL.DataBind();

				// if there is no active address for user show select value
				ListItem emptyItem = new ListItem("Lütfen adres seçiniz", "0");
				addressDL.Items.Insert(0, emptyItem);
				addressDL.Items[0].Attributes["disabled"] = "disabled";
				if(selectedValue == null)
				{
					addressDL.SelectedIndex = 0;
				}
			}
		}

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
			var createUser = db.sp_CreateAddress(
				IT.Session.Users.UserId(),
					aName.Text.Trim(),
					aTown.Text.Trim(),
					aDistrict.Text.Trim(),
					aDescription.Text.Trim(),
					phone.Text.Trim(),
					(decimal)37.171379,
					(decimal)28.399133
				).ToList();

			if(createUser != null && createUser.Count() > 0 && createUser.First() > 0)
			{
				BindAddressData();
				this.ShowMessage("Success", "Adres oluşturuldu lütfen kayıtlı adreslerden kullanmak istediğiniz adresi seçiniz.");
			}
        }

		protected void addressDL_SelectedIndexChanged(object sender, EventArgs e)
		{
			int userId = IT.Session.Users.UserId();
			int addressId = Convert.ToInt32(addressDL.SelectedValue);

			var updateAddress = db.sp_UpdateAddressActive(userId, addressId).ToList();

			if(updateAddress.Count() > 0 && updateAddress.First() > 0)
			{
				IT.Session.Users.AddAddressSession(addressId);
				this.ShowMessage("Success", "Adres başarıyla seçildi alışverişe başlayabilirsiniz.");
			}

		}
	}
}