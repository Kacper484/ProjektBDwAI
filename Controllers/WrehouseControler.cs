using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjektBDwAI.Data; 
using ProjektBDwAI.Models; 

namespace ProjektBDwAI.Controllers
{
    [Authorize(Roles = "Admin")] // Wszystkie metody wymagają roli Admin
    public class WarehouseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Warehouses.ToList()); // Lista magazynów
        }

        public IActionResult Create()
        {
            return View(); // Formularz dodawania magazynu
        }

        [HttpPost]
        public IActionResult Create(Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                _context.Warehouses.Add(warehouse);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }

        public IActionResult Delete(int id)
        {
            var warehouse = _context.Warehouses.Find(id);
            if (warehouse == null)
                return NotFound();

            return View(warehouse); // Potwierdzenie usunięcia
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var warehouse = _context.Warehouses.Find(id);
            if (warehouse == null)
                return NotFound();

            _context.Warehouses.Remove(warehouse);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
