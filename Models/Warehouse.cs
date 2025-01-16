using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aplikacja_na_BDwAI.Models
{
    public class Warehouse
    {
        public int Id { get; set; } // Klucz główny

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        // Relacja
        public List<Product> Products { get; set; } = new List<Product>(); // Relacja jeden do wielu z encją Product
    }
}
