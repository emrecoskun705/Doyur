using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Doyur.extensions;

namespace Doyur.user
{
	public partial class addresslist : System.Web.UI.Page
	{

		db.doyurEntities db = new db.doyurEntities();
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				LoadAddress();
			}
		}

		private void LoadAddress()
		{
			int userId = IT.Session.Users.UserId();

			var getAddressList = db.sp_GetAddress(userId, -1, 0, 2).ToList();

			if(getAddressList != null && getAddressList.Count() > 0)
			{
				gList.DataSource= getAddressList;
			} else
			{
				// empty list
			}

			gList.DataBind();
		}


		protected void gList_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if(e.CommandName == "Edit")
			{
				int addressId = Convert.ToInt32(e.CommandArgument);
				Response.Redirect("/user/address.aspx?AddressId=" + addressId);

			}
			else if(e.CommandName == "DeleteAddress")
			{
				int userId = IT.Session.Users.UserId();
				int addressId = Convert.ToInt32(e.CommandArgument);
				var getAddress = (from p in db.Address where p.AddressId == addressId && p.UserId == userId select p).FirstOrDefault();

				if(getAddress != null)
				{
					db.Address.Remove(getAddress);
					if(db.SaveChanges() > 0)
					{
						this.ShowMessage("success", "Adres başarıyla silindi", "Başarılı");
						LoadAddress();
					} else
					{
						this.ShowMessage("warning", "Adres silinirken bir hata oluştu", "Hata");
					}
				} else
				{
					this.ShowMessage("warning", "Adres bulunamadı", "Hata");
				}
			}

		}
	}
}