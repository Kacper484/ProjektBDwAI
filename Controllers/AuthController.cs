using Aplikacja_na_BDwAI.Data;
using Microsoft.AspNetCore.Mvc;
using ProjektBDwAI.Data; 
using ProjektBDwAI.Models; 

namespace ProjektBDwAI.Controllers
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
            return View(s); // Formularz logowania
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
