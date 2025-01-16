using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aplikacja_na_BDwAI.Models
{
    public class User
    {
        public int Id { get; set; } 

        [Required]
        public string Email { get; set; } = string.Empty; 

        [Required]
        public string Password { get; set; } = string.Empty; 

        public string Role { get; set; } = "User"; 

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
