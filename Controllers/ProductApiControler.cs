using Aplikacja_na_BDwAI.Data;
using Aplikacja_na_BDwAI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ProjektBDwAI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class ProductApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        [Authorize] 
        public IActionResult GetAll()
        {
            var products = _context.Products.ToList();
            return Ok(products); // Zwraca listę produktów
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        [Authorize] 
        public IActionResult Get(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound(); 
            }
            return Ok(product); // Zwraca szczegóły produktu
        }

        // POST: api/Product
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Walidacja nieudana
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product); // Zwraca nowo utworzony produkt
        }

        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] 
        public IActionResult Update(int id, [FromBody] Product product)
        {
            var existingProduct = _context.Products.Find(id);
            if (existingProduct == null)
            {
                return NotFound(); 
            }

            if (ModelState.IsValid)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Quantity = product.Quantity;
                _context.SaveChanges();
                return NoContent(); // Aktualizacja udana
            }
            return BadRequest(ModelState); 
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] 
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound(); 
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent(); // Usunięcie udane
        }
    }
}
