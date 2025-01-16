using Aplikacja_na_BDwAI.Data;
using Aplikacja_na_BDwAI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aplikacja_na_BDwAI.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.Warehouse)
                .ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            ViewData["Warehouses"] = _context.Warehouse.ToList();
            return View(new Product()); // Tworzymy pusty model, aby uniknąć błędu w widoku
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData["Warehouses"] = _context.Warehouse.ToList(); // Ładujemy magazyny w przypadku błędów
            return View(product);
        }


        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            // Load warehouses for dropdown (if needed)
            ViewData["Warehouses"] = _context.Warehouse.ToList();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Products.Update(product);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Logowanie błędu (jeśli potrzeba)
                    ModelState.AddModelError(string.Empty, "Wystąpił błąd podczas zapisywania danych.");
                }
            }

            // Ponowne załadowanie listy magazynów w przypadku błędu
            ViewData["Warehouses"] = _context.Warehouse.ToList();
            return View(product);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products
                .Include(p => p.Warehouse)
                .FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
