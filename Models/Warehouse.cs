using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aplikacja_na_BDwAI.Models
{
    public class Warehouse
    {
        public int Id { get; set; } 

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }


        public List<Product> Products { get; set; } = new List<Product>(); 
    }
}
