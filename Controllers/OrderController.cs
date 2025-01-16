using Aplikacja_na_BDwAI.Data;
using Aplikacja_na_BDwAI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Aplikacja_na_BDwAI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _context.Orders
                .Include(o => o.Product)
                .Include(o => o.User)
                .ToList();
            return View(orders);
        }

        public IActionResult Create()
        {
            var products = _context.Products.ToList();

            // Logowanie liczby produktów dla diagnostyki
            Console.WriteLine($"Number of products available: {products.Count}");

            // Przekazanie listy produktów do ViewData
            ViewData["Products"] = products;

            if (!products.Any())
            {
                ViewBag.ErrorMessage = "Brak dostêpnych produktów. Dodaj produkty przed tworzeniem zamówienia.";
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Ustawienie UserId (przyk³ad: na sta³e lub z sesji u¿ytkownika)
                    order.UserId = 1; // Dostosuj do rzeczywistego zalogowanego u¿ytkownika

                    // Ustawienie daty zamówienia
                    order.OrderDate = DateTime.Now;

                    // Dodanie zamówienia do bazy danych
                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    Console.WriteLine("Order saved successfully!");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while saving order: {ex.Message}");
                    ModelState.AddModelError("", "Wyst¹pi³ b³¹d podczas zapisywania zamówienia.");
                }
            }

            // Ponowne za³adowanie listy produktów w przypadku b³êdów
            ViewData["Products"] = _context.Products.ToList();
            return View(order);
        }
    }
}
