using Microsoft.AspNetCore.Mvc;
using Aplikacja_na_BDwAI.Data;
using Aplikacja_na_BDwAI.Models;
using System.Linq;

namespace Aplikacja_na_BDwAI.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Auth/Login
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        // POST: /Auth/Login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserRole", user.Role);
                return RedirectToAction("Index", "Product");
            }

            ModelState.AddModelError("", "Nieprawidłowe dane logowania");
            return View(model);
        }

        // GET: /Auth/Register
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        // POST: /Auth/Register
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("", "Użytkownik z podanym adresem email już istnieje.");
                return View(model);
            }

            var newUser = new User
            {
                Email = model.Email,
                Password = model.Password, // TODO: Implement password hashing
                Role = "User"
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}
