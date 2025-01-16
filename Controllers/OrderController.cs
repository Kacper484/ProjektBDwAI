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
            ViewBag.Products = new SelectList(_context.Products.ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(int productId, int quantity)
        {
            if (productId <= 0 || quantity <= 0)
            {
                ModelState.AddModelError("", "Wszystkie pola s¹ wymagane i musz¹ byæ poprawnie wype³nione.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var order = new Order
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        UserId = 1, 
                        OrderDate = DateTime.Now
                    };

                    _context.Orders.Add(order);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Wyst¹pi³ b³¹d podczas zapisywania zamówienia: " + ex.Message);
                }
            }

            ViewBag.Products = new SelectList(_context.Products.ToList(), "Id", "Name");
            return View();
        }

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
                ModelState.AddModelError("", "Wyst¹pi³ b³¹d podczas usuwania zamówienia: " + ex.Message);
                return RedirectToAction("Delete", new { id });
            }
        }
    }
}
