
using System.Threading.Tasks;
using ePizza.Entities.Concrete;
using ePizza.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ePizza.Services.Interfaces;

namespace ePizza.WebUI.Controllers
{
   
    public class AccountController : Controller
    {
        IAuthenticationService _authService;
        public AccountController(IAuthenticationService authService)
        {
            _authService = authService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _authService.AuthenticateUser(model.Email, model.Password);
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    if (user.Roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                        //return RedirectToAction("Index", "Dashboard","Admin/Index");
                    }
                    else if (user.Roles.Contains("User"))
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "User" });

                        /*return RedirectToAction("Index", "Dashboard","Admin/User");*/
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Eposta Adresiniz veya Şifreniz Yanlış");
                    ViewBag.Error = "Eposta Adresiniz veya Şifreniz Yanlış";
                    return View("Login");
                }
            }
            else
            {
                ModelState.AddModelError("", "Eposta Adresiniz veya Şifreniz Yanlış");

            }
            return View("Login");
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                bool result = _authService.CreateUser(user, model.Password);
                if (result)
                {
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _authService.SignOut();
            return RedirectToAction("LogOutComplete");
        }

        public IActionResult LogOutComplete()
        {
            return View();
        }
        public IActionResult Unauthorize()
        {
            return View();
        }
    }
}
