using Doyur;
using Doyur.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BaseClass
/// </summary>
namespace IT
{
    public class Session
    {
        public static string PostUrlRef(string urlreferrer, bool addurlref)
        {
            if (addurlref)
            {
                HttpContext.Current.Session.Add("PostUrlRef", urlreferrer);
                return null;
            }
            else
            {
                string num1 = HttpContext.Current.Session["PostUrlRef"].ToString();
                HttpContext.Current.Session.Remove("PostUrlRef");
                return num1;
            }
        }

        public class Users
        {
            public static void AddLoginSessionList(int UserId, int AccessId, string Firstname, 
                string Lastname, string Username, string Phone, string Email)
            {
                HttpContext.Current.Session.Add("UserId", UserId);
                HttpContext.Current.Session.Add("AccessId", AccessId);
                HttpContext.Current.Session.Add("Firstame", Firstname);
                HttpContext.Current.Session.Add("Lastname", Lastname);
                HttpContext.Current.Session.Add("Username", Username);
                HttpContext.Current.Session.Add("Phone", Phone);
                HttpContext.Current.Session.Add("Email", Email);
            }

            public static void AddLoginSessionListCompany(int UserId, int AccessId, string Firstname,
                string Lastname, string Username, string Phone, string Email, int companyId)
            {
                HttpContext.Current.Session.Add("UserId", UserId);
                HttpContext.Current.Session.Add("CompanyId", companyId);
                HttpContext.Current.Session.Add("AccessId", AccessId);
                HttpContext.Current.Session.Add("Firstame", Firstname);
                HttpContext.Current.Session.Add("Lastname", Lastname);
                HttpContext.Current.Session.Add("Username", Username);
                HttpContext.Current.Session.Add("Phone", Phone);
                HttpContext.Current.Session.Add("Email", Email);
            }

            public static void AddMessageSession(string type, string msg, string msgTitle)
            {
				HttpContext.Current.Session.Add("msgType", type);
				HttpContext.Current.Session.Add("msg", msg);
				HttpContext.Current.Session.Add("msgTitle", msgTitle);
			}

			public static void RemoveSessionMsg()
			{
				HttpContext.Current.Session.Remove("msgType");
				HttpContext.Current.Session.Remove("msg");
                HttpContext.Current.Session.Remove("msgTitle");
            }


			public static void RevomeSessionList()
            {
                HttpContext.Current.Session.Remove("UserId");
                HttpContext.Current.Session.Remove("AccessId");
                HttpContext.Current.Session.Remove("Firstame");
                HttpContext.Current.Session.Remove("Lastname");
                HttpContext.Current.Session.Remove("Username");
                HttpContext.Current.Session.Remove("Phone");
                HttpContext.Current.Session.Remove("Email");
                HttpContext.Current.Session.Remove("CompanyId");
            }


			public static void AddLoginSessionDebug()
            {
                HttpContext.Current.Session.Add("UserId", 3);
                HttpContext.Current.Session.Add("AccessId", 2);
                HttpContext.Current.Session.Add("Firstame", "Emre");
                HttpContext.Current.Session.Add("Lastname", "Coskun");
                HttpContext.Current.Session.Add("Username", "emreuser");
                HttpContext.Current.Session.Add("Phone", "5458413575");
                HttpContext.Current.Session.Add("Email", "user@user.com");
            }

            public static void AddLoginSessionCompanyDebug()
            {
                HttpContext.Current.Session.Add("UserId", 1);
                HttpContext.Current.Session.Add("AccessId", 3);
                HttpContext.Current.Session.Add("CompanyId", 1);
                HttpContext.Current.Session.Add("Firstame", "Emre");
                HttpContext.Current.Session.Add("Lastname", "Coskun");
                HttpContext.Current.Session.Add("Username", "emrec");
                HttpContext.Current.Session.Add("Phone", "5458413575");
                HttpContext.Current.Session.Add("Email", "emre@emre.com");
            }


            public static string MsgTitle()
            {
                if (HttpContext.Current.Session["msgTitle"] == null)
                    return "";
                else
                    return HttpContext.Current.Session["msgTitle"].ToString();
            }
            public static string MsgType()
            {
				if (HttpContext.Current.Session["msgType"] == null)
					return "";
				else
					return HttpContext.Current.Session["msgType"].ToString();
			}

			public static string Msg()
			{
				if (HttpContext.Current.Session["msg"] == null)
					return "";
				else
					return HttpContext.Current.Session["msg"].ToString();
			}

			public static string Phone()
            {
                if (HttpContext.Current.Session["Phone"] == null)
                    return "";
                else
                    return HttpContext.Current.Session["Phone"].ToString();
            }

            public static string Mail()
            {
                if (HttpContext.Current.Session["Email"] == null)
                    return "";
                else
                    return HttpContext.Current.Session["Email"].ToString();
            }

            public static string Firstname()
            {
                if (HttpContext.Current.Session["Firstame"] == null)
                    return "";
                else
                    return HttpContext.Current.Session["Firstame"].ToString();
            }

            public static string Lastname()
            {
                if (HttpContext.Current.Session["Lastname"] == null)
                    return "";
                else
                    return HttpContext.Current.Session["Lastname"].ToString();
            }

            public static string Username()
            {
                if (HttpContext.Current.Session["Username"] == null)
                    return "";
                else
                    return HttpContext.Current.Session["Username"].ToString();
            }

            public static int UserId()
            {
                if (HttpContext.Current.Session["UserId"] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString());
            }

            public static int CompanyId()
            {
                if (HttpContext.Current.Session["CompanyId"] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session["CompanyId"].ToString());
            }


            public static int AccessId()
            {
                if (HttpContext.Current.Session["AccessId"] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session["AccessId"].ToString());
            }

            public static int SecurityId()
            {
                if (HttpContext.Current.Session["SecurityId"] == null)
                    return 0;
                else
                    return Convert.ToInt32(HttpContext.Current.Session["SecurityId"].ToString());
            }

            public static Guid RelationId()
            {
                if (HttpContext.Current.Session["RelationId"] == null)
                    return Guid.Empty;
                else
                    return Guid.Parse(HttpContext.Current.Session["RelationId"].ToString());
            }


            public static void UserIsNotLoginRedirect()
            {
                if (HttpContext.Current.Session["AccessId"] == null)
                {
                    HttpContext.Current.Response.Redirect("/login.aspx");
                }
            }

            public static void UserIsNotLoginRedirect(string Url)
            {
                if (HttpContext.Current.Session["AccessId"] == null)
                {
                    HttpContext.Current.Response.Redirect(Url);
                }
            }


            public static void CompanyIsNotLoginRedirect(string Url)
            {
                if (HttpContext.Current.Session["AccessId"] == null)
                {
                    HttpContext.Current.Response.Redirect(Url);
                } else
                {
                    if(Convert.ToInt32(HttpContext.Current.Session["AccessId"]) != 3)
                    {
                        HttpContext.Current.Response.Redirect(Url);
                    }
                }
            }

			public static void AdminIsNotLoginRedirect(string Url)
			{
				if (HttpContext.Current.Session["AccessId"] == null)
				{
					HttpContext.Current.Response.Redirect(Url);
				}
				else
				{
					if (Convert.ToInt32(HttpContext.Current.Session["AccessId"]) != 255)
					{
						HttpContext.Current.Response.Redirect(Url);
					}
				}
			}


		}

        public class UserCookie
        {
            /// <summary>
            /// Kullanicinin cookie si okunuyor ve sessiona ataniyor.
            /// </summary>
            /// <param name="BasketCookies"></param>
            public static void LoadBasketCookieList(string BasketCookies)
            {
                HttpContext.Current.Session.Add("BasketCookies", BasketCookies);
            }
            /// <summary>
            /// Kullancinin cookie sindeki guid id si var.
            /// </summary>
            /// <returns></returns>
            public static Guid BasketCookiesID()
            {
                if (HttpContext.Current.Session["BasketCookies"] != null)
                {
                    return (new Guid(HttpContext.Current.Session["BasketCookies"].ToString()));
                }
                return Guid.Empty;
            }
        }
    }
}