using System.ComponentModel.DataAnnotations;

namespace Aplikacja_na_BDwAI.Models
{
    public class Product
    {
        public int Id { get; set; } 

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int WarehouseId { get; set; } 

        public Warehouse Warehouse { get; set; } 
    }
}
