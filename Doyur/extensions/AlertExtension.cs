using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;

namespace Doyur.extensions
{
	public static class AlertExtension
	{
		public static void ShowMessage(this System.Web.UI.Page obj, string type, string message)
		{
			ScriptManager.RegisterStartupScript(obj, obj.GetType(), System.Guid.NewGuid().ToString(), "SendAlert('" + type + "','" + message + "');", true);

		}

		public static string Shorten(this string name, int chars)
		{
			if (name.ToCharArray().Count() > chars)
			{
				return name.Substring(0, chars) + "...";
			}
			else return name;
		}



	}
}