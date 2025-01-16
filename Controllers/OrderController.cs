using Aplikacja_na_BDwAI.Data;
using Aplikacja_na_BDwAI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
                .ToList();
            return View(orders);
        }

        public IActionResult Create()
        {
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                // Dodaj zamówienie do bazy
                _context.Orders.Add(order);
                _context.SaveChanges();

                // Przekierowanie do listy zamówień
                return RedirectToAction("Index");
            }

            // W razie błędu ponownie załaduj listę produktów
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            return View(order);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var order = _context.Orders
                .Include(o => o.Product)
                .FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
                return NotFound();

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
