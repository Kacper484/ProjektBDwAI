using Aplikacja_na_BDwAI.Data;
using Aplikacja_na_BDwAI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // Akcja wyświetlająca listę produktów
        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.Warehouse)
                .ToList();
            return View(products);
        }

        // GET: Wyświetlenie formularza dodawania produktu
        public IActionResult Create()
        {
            // Załadowanie listy magazynów
            ViewBag.Warehouses = new SelectList(_context.Warehouse.ToList(), "Id", "Name");
            return View();
        }

        // POST: Obsługa dodawania produktu
        [HttpPost]
        public IActionResult Create(string name, decimal price, int quantity, int warehouseId)
        {
            // Walidacja danych
            if (string.IsNullOrEmpty(name) || price <= 0 || quantity <= 0 || warehouseId <= 0)
            {
                ModelState.AddModelError("", "Wszystkie pola są wymagane i muszą być poprawnie wypełnione.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = new Product
                    {
                        Name = name,
                        Price = price,
                        Quantity = quantity,
                        WarehouseId = warehouseId
                    };

                    _context.Products.Add(product);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Wystąpił błąd podczas dodawania produktu: " + ex.Message);
                }
            }

            // Ponowne załadowanie magazynów w przypadku błędu
            ViewBag.Warehouses = new SelectList(_context.Warehouse.ToList(), "Id", "Name");
            return View();
        }

        // GET: Wyświetlenie formularza edycji produktu
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            // Załadowanie listy magazynów
            ViewBag.Warehouses = new SelectList(_context.Warehouse.ToList(), "Id", "Name", product.WarehouseId);
            return View(product);
        }

        // POST: Obsługa edycji produktu
        [HttpPost]
        public IActionResult Edit(int id, string name, decimal price, int quantity, int warehouseId)
        {
            // Walidacja danych
            if (id <= 0 || string.IsNullOrEmpty(name) || price <= 0 || quantity <= 0 || warehouseId <= 0)
            {
                ModelState.AddModelError("", "Wszystkie pola są wymagane i muszą być poprawnie wypełnione.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var product = _context.Products.Find(id);
                    if (product == null)
                        return NotFound();

                    product.Name = name;
                    product.Price = price;
                    product.Quantity = quantity;
                    product.WarehouseId = warehouseId;

                    _context.Products.Update(product);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Wystąpił błąd podczas edycji produktu: " + ex.Message);
                }
            }

            // Ponowne załadowanie magazynów w przypadku błędu
            ViewBag.Warehouses = new SelectList(_context.Warehouse.ToList(), "Id", "Name", warehouseId);
            return View();
        }

        // GET: Wyświetlenie formularza usuwania produktu
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

        // POST: Potwierdzenie usunięcia produktu
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                if (product == null)
                    return NotFound();

                _context.Products.Remove(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Wystąpił błąd podczas usuwania produktu: " + ex.Message);
                return RedirectToAction("Delete", new { id });
            }
        }
    }
}
