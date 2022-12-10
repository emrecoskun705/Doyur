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
			int userId = IT.Session.Users.UserId();

			var createAddress = db.sp_CreateAddress(userId, aName.Text.Trim(), aTown.Text.Trim(), aDistrict.Text.Trim(), aDescription.Text.Trim(), phone.Text.Trim(), IsActive.Checked, (decimal)24.2, (decimal)24.2).FirstOrDefault();

			if(createAddress != null && createAddress > 0)
			{
				//address creation is successfull
				this.ShowMessage("Success", "Adres başarıyla eklendi");
			} else
			{
				this.ShowMessage("Warning", "Adres eklenemedi, en fazla 5 adrese sahip olabilirsiniz.");
			}
        }
    }
}