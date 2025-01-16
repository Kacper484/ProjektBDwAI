namespace Aplikacja_na_BDwAI.Models
{
    public class User
    {
        public int Id { get; set; } // Klucz główny
        public string Email { get; set; } = string.Empty; // Wymagana wartość domyślna
        public string Password { get; set; } = string.Empty; // Wymagana wartość domyślna
        public string Role { get; set; } = "User"; // Domyślna rola użytkownika ("User")

        // Relacja: użytkownik -> zamówienia
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
