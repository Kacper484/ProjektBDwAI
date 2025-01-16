using System;
using System.ComponentModel.DataAnnotations;

namespace Aplikacja_na_BDwAI.Models
{
    public class Order
    {
        public int Id { get; set; } 


        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int UserId { get; set; } 

        
        public User User { get; set; } = default!; 
        public Product Product { get; set; } = default!; 
    }
}
