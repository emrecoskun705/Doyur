﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IT.Session.Users.AddMessageSession("success", "Hesabınızdan çıkış yaptınız", "Başarılı");
            IT.Session.Users.RevomeSessionList();
            Response.Redirect("/default.aspx");
        }
    }
}