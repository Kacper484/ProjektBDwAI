using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Aplikacja_na_BDwAI.Models
{
    public class Product
    {
        public int Id { get; set; } //Klucz główny
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int WarehouseId {  get; set; } // Klucz obcy do encji Warehouse
        public Warehouse Warehouse { get; set; } //Relacja z encją Warehouse
    }
}
