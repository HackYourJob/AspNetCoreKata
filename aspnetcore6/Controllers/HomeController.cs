using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using aspnetcore6.Models;
using Microsoft.AspNetCore.Authorization;

namespace aspnetcore6.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "PrivatePages")]
        public IActionResult Private()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
