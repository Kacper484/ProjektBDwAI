namespace Aplikacja_na_BDwAI.Models
{
    public class Payment
    {
        public int Id { get; set; } // Klucz główny
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }  
        public int OrderId { get; set; } //Klucz obcy do encji Order


        public Order Order { get; set; } // Relacja jeden do wielu z encją Order 
    }
}
