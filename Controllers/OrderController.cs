using Aplikacja_na_BDwAI.Data;
using Aplikacja_na_BDwAI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // Wy�wietlanie listy zam�wie�
        public IActionResult Index()
        {
            var orders = _context.Orders
                .Include(o => o.Product)
                .Include(o => o.User)
                .ToList();
            return View(orders);
        }

        // GET: Wy�wietlenie formularza dodawania zam�wienia
        public IActionResult Create()
        {
            ViewBag.Products = new SelectList(_context.Products.ToList(), "Id", "Name");
            return View();
        }

        // POST: Obs�uga dodawania zam�wienia
        [HttpPost]
        public IActionResult Create(int productId, int quantity)
        {
            if (productId <= 0 || quantity <= 0)
            {
                ModelState.AddModelError("", "Wszystkie pola s� wymagane i musz� by� poprawnie wype�nione.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var order = new Order
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        UserId = 1, // Dostosuj do aktualnie zalogowanego u�ytkownika
                        OrderDate = DateTime.Now
                    };

                    _context.Orders.Add(order);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Wyst�pi� b��d podczas zapisywania zam�wienia: " + ex.Message);
                }
            }

            ViewBag.Products = new SelectList(_context.Products.ToList(), "Id", "Name");
            return View();
        }

        // GET: Wy�wietlenie potwierdzenia usuni�cia zam�wienia
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var order = _context.Orders
                .Include(o => o.Product)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Usuni�cie zam�wienia
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            try
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Wyst�pi� b��d podczas usuwania zam�wienia: " + ex.Message);
                return RedirectToAction("Delete", new { id });
            }
        }
    }
}
