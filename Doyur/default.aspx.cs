using Doyur.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Doyur
{
    public partial class _default : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IT.Session.Users.MsgType() != "" && IT.Session.Users.Msg() != "")
            {
                this.ShowMessage(IT.Session.Users.MsgType(), IT.Session.Users.Msg());
                IT.Session.Users.RemoveSessionMsg();
            }
            if (!IsPostBack)
            {

            }
        }
    }
}