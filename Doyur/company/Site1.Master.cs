﻿using Doyur.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur.company
{
	public partial class Site1 : System.Web.UI.MasterPage
	{

		protected void Page_Init(object sender, EventArgs e)
		{
			IT.Session.Users.UserIsNotLoginRedirect();
			IT.Session.Users.CompanyIsNotLoginRedirect("/");

        }
		protected void Page_Load(object sender, EventArgs e)
		{
			if (IT.Session.Users.MsgType() != "" && IT.Session.Users.Msg() != "")
			{
				this.ShowMessage(IT.Session.Users.MsgType(), IT.Session.Users.Msg(), IT.Session.Users.MsgTitle());
				IT.Session.Users.RemoveSessionMsg();
			}
		}
	}
}