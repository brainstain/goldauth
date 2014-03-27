using System;
using System.Web;
using System.Web.Security;
using Goldstein.Authentication.Services.Interfaces;

namespace Goldstein.Authentication.Services
{
    public class FormsAuthenticationCookieFactory : IAuthenticationCookieFactory
    {
        public HttpCookie GenerateAuthenticationCookie()
        {
            var expiration = DateTime.Now.Add(FormsAuthentication.Timeout);

            var ticket = new FormsAuthenticationTicket(2,
                FormsAuthentication.FormsCookieName,
                DateTime.Now,
                expiration,
                true,
                String.Empty,
                FormsAuthentication.FormsCookiePath);

            return new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket))
            {
                Path = FormsAuthentication.FormsCookiePath,
                Expires = expiration,
                HttpOnly = true
            };
        }

        public HttpCookie ExpireAuthenticationCookie()
        {
            return new HttpCookie(FormsAuthentication.FormsCookieName, String.Empty)
                {
                    Expires = DateTime.Now.AddYears(-1)
                };
        }
    }
}
