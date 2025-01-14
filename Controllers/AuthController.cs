using Microsoft.AspNetCore.Mvc;
using Aplikacja_na_BDwAI.Data; 
using Aplikacja_na_BDwAI.Models; 

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
            return View(); // Formularz logowania
        }

        // POST: /Auth/Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserRole", user.Role);
                return RedirectToAction("Index", "Product");
            }
            ModelState.AddModelError("", "Nieprawidłowe dane logowania");
            return View();
        }
    }
}
