namespace Aplikacja_na_BDwAI.Models
{
    public class Customer
    {
        public int Id { get; set; } // Klucz Główny
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Email { get; set; }
        public string Address { get; set; }


        public List<Order> Orders { get; set; } // Relacja jeden do wielu z encją Order

    }
}
