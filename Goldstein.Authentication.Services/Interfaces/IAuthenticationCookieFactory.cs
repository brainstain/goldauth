using System.Web;

namespace Goldstein.Authentication.Services.Interfaces
{
    public interface IAuthenticationCookieFactory
    {
        HttpCookie GenerateAuthenticationCookie();
        HttpCookie ExpireAuthenticationCookie();
    }
}
