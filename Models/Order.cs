namespace Aplikacja_na_BDwAI.Models
{
    public class Order
    {
        public int Id { get; set; } // Klucz główny
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; } // Id użytkownika
        public User User { get; set; } = default!; // Relacja z encją User
        public Product Product { get; set; } = default!; // Relacja z encją Product
    }
}
