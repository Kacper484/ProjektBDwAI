using Microsoft.AspNetCore.Mvc;
using ProjektBDwAI.Data; 
using ProjektBDwAI.Models; 

namespace ProjektBDwAI.Controllers
{
    [Authorize] // Wszystkie metody wymagają logowania
    public class ProductController : Controller
    {
        public class ProductController : Controller
        {
            private readonly ApplicationDbContext _context;

            public ProductController(ApplicationDbContext context)
            {
                _context = context;
            }

            // GET: /Product
            public IActionResult Index()
            {
                var products = _context.Products.ToList();
                return View(products); // Lista produktów
            }

            // GET: /Product/Create
            [Authorize(Roles = "Admin")]
            public IActionResult Create()
            {
                return View(); // Formularz dodawania
            }

            // POST: /Product/Create
            [Authorize(Roles = "Admin")]
            [HttpPost]
            public IActionResult Create(Product product)
            {
                if (ModelState.IsValid)
                {
                    _context.Products.Add(product);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }

            // GET: /Product/Edit/{id}
            [Authorize(Roles = "Admin")]
            public IActionResult Edit(int id)
            {
                var product = _context.Products.Find(id);
                if (product == null)
                    return NotFound();

                return View(product); // Formularz edycji
            }

            // POST: /Product/Edit/{id}
            [Authorize(Roles = "Admin")]
            [HttpPost]
            public IActionResult Edit(int id, Product product)
            {
                var existingProduct = _context.Products.Find(id);
                if (existingProduct == null)
                    return NotFound();

                if (ModelState.IsValid)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Price = product.Price;
                    existingProduct.Quantity = product.Quantity;
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }

            // GET: /Product/Delete/{id}
            [Authorize(Roles = "Admin")]
            public IActionResult Delete(int id)
            {
                var product = _context.Products.Find(id);
                if (product == null)
                    return NotFound();

                return View(product); // Potwierdzenie usunięcia
            }

            // POST: /Product/Delete/{id}
            [Authorize(Roles = "Admin")]
            [HttpPost, ActionName("Delete")]
            public IActionResult DeleteConfirmed(int id)
            {
                var product = _context.Products.Find(id);
                if (product == null)
                    return NotFound();

                _context.Products.Remove(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
