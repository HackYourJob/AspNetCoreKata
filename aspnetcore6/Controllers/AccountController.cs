using System.Security.Claims;
using System.Threading.Tasks;
using aspnetcore6.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore6.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            var user = TryToGetUser(userName, password);
            if (user == null) return View();

            await HttpContext.SignInAsync(user);

            return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl : "/");
        }

        private ClaimsPrincipal TryToGetUser(string userName, string password)
        {
            if (userName == "joe" && password == "bob")
            {
                return CreateClaimsPrincipalFor(1, userName, Permissions.CanViewPublicPage);
            }
            else if (userName == "joe" && password == "private")
            {
                return CreateClaimsPrincipalFor(2, userName, Permissions.CanViewPublicPage, Permissions.CanViewPrivatePage);
            }
            else
            {
                return null;
            }
        }

        private ClaimsPrincipal CreateClaimsPrincipalFor(int id, string userName, params Permissions[] permissions)
        {
            return new ClaimsPrincipal(new User(id, userName, permissions));
        }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}