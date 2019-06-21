using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Presentation.Models;
using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Presentation.Helpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using System;

namespace EmployeeManagement.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly IValidationService _validationService;
        private readonly IPersonsBL _personsService;
        public LoginController(IValidationService validationService, IPersonsBL personsService)
        {
            _validationService = validationService;
            _personsService = personsService;
        }

        public async Task<IActionResult> Index(string returnUrl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return View(new UserModel() { ReturnUrl = returnUrl });
        }

        public async Task<IActionResult> Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var loginIsValid = _personsService.Login(model.user, model.password);

                if (loginIsValid)
                {
                    await LoginAsync(model);

                    if (IsUrlValid(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Persons");
                }

                ModelState.AddError(_validationService);
            }

            return View("Index");
        }

        public async Task<IActionResult> Logout(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            return RedirectToAction("Index", "Login", new UserModel() { ReturnUrl = returnUrl });
        }

        private async Task LoginAsync(UserModel user)
        {
            var claims = new List<Claim>
            {                
                new Claim(ClaimTypes.Name, user.user)               
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);
        }

        private static bool IsUrlValid(string returnUrl)
        {
            return !string.IsNullOrWhiteSpace(returnUrl)
                   && Uri.IsWellFormedUriString(returnUrl, UriKind.Relative);
        }        
    }
}
