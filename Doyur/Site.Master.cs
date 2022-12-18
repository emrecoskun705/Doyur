using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Doyur.extensions;

namespace Doyur
{
    public partial class Site : System.Web.UI.MasterPage
    {

		protected void Page_Init(object sender, EventArgs e)
		{
            //IT.Session.Users.AddLoginSessionDebug();
		}
		protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}