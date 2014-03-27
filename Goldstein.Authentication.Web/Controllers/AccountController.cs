using System.Threading.Tasks;
using System.Web.Mvc;
using Goldstein.Authentication.Services;
using Goldstein.Authentication.Services.Interfaces;
using Goldstein.Authentication.Web.Models;

namespace Goldstein.Authentication.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController()
        {
        }

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        //
        // GET: /Account/
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(AccountModel model, string returnUrl)
        {
            if (ModelState.IsValid && await _userService.AuthenticateUserAsync(model.UserName, model.Password))
            {
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
	}
}