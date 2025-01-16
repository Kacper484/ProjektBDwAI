using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aplikacja_na_BDwAI.Models
{
    public class User
    {
        public int Id { get; set; } // Klucz główny

        [Required]
        public string Email { get; set; } = string.Empty; // Email użytkownika

        [Required]
        public string Password { get; set; } = string.Empty; // Hasło użytkownika

        public string Role { get; set; } = "User"; // Domyślna rola użytkownika

        // Relacja: użytkownik -> zamówienia
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
