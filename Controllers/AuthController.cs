using Microsoft.AspNetCore.Mvc;
using Aplikacja_na_BDwAI.Data;
using Aplikacja_na_BDwAI.Models;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Aplikacja_na_BDwAI.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl; 
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

      
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

    
                HttpContext.Session.SetString("UserRole", user.Role);
                HttpContext.Session.SetString("UserEmail", user.Email);

              
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

           
            ModelState.AddModelError("", "Nieprawidłowe dane logowania");
            return View(model);
        }

   
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}
