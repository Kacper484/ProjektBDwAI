using System.ComponentModel.DataAnnotations;

namespace Aplikacja_na_BDwAI.Models
{
    public class Product
    {
        public int Id { get; set; } // Klucz główny

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int WarehouseId { get; set; } // Klucz obcy do encji Warehouse

        // Relacja
        public Warehouse Warehouse { get; set; } // Relacja z encją Warehouse
    }
}
