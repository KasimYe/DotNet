using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kasim.Core.InvoiceWebApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Kasim.Core.InvoiceWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            var user = TestUserStorage.UserList.FirstOrDefault(m => m.UserName == model.UserName && m.Password == model.Password);

            if (user != null)
            {
                Startup.HangfireAuthorizationFilter.bAuthorize = true;
                var identity = new ClaimsIdentity("Basic");
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return RedirectToAction("", "hangfire");
            }            
            return View("Index");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
