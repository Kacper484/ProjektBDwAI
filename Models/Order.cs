namespace Aplikacja_na_BDwAI.Models
{
    public class Order
    {
        public int Id { get; set; } // klucz główny
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CustomerId {  get; set; }


        public Product Product { get; set; } //Relacja z encją Product
        public Customer Customer { get; set; } // Relacja z encją Customer
        public Payment Payment { get; set; } // relacja z encją Payment

    }
}
