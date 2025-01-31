using Aplikacja_na_BDwAI.Data;
using Aplikacja_na_BDwAI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjektBDwAI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WarehouseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Warehouse.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                _context.Warehouse.Add(warehouse);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(warehouse);
        }


        public IActionResult Delete(int id)
        {
            var warehouse = _context.Warehouse.Find(id);
            if (warehouse == null)
                return NotFound();

            return View(warehouse);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var warehouse = _context.Warehouse.Find(id);
            if (warehouse == null)
                return NotFound();

            _context.Warehouse.Remove(warehouse);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
