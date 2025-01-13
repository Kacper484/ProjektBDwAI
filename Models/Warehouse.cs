namespace Aplikacja_na_BDwAI.Models
{
    public class Warehouse
    {
        public int Id { get; set; } // Klucz głóny
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Product> Products { get; set; } // relacja jeden do wielu z encją Product
    }
}
