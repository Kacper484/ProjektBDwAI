using Microsoft.EntityFrameworkCore;
using Aplikacja_na_BDwAI;
using Aplikacja_na_BDwAI.Models; // mój namespace do encji 

namespace Aplikacja_na_BDwAI.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        // DbSet dla każdej encji
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relacja: Product -> Warehouse (wiele prodóktów w magazynie)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Warehouse)
                .WithMany(w => w.Products)
                .HasForeignKey(p => p.WarehouseId);

            //Relacja: Order -> Customer (wiele zamówień od klienta)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            // Relacja: Order -> Payment (1:1)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)"); // Precyzja: 18 cyfr w tym 2 po przecinku

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)"); // Precyzja: 18 cyfr w tym 2 po przecinku

            base.OnModelCreating(modelBuilder);

            //Dodwanie danych testowych do bazy danych
            modelBuilder.Entity<Warehouse>().HasData(
                new Warehouse { Id = 1, Name = "Central Warehouse", Location = "Cracow"},
                new Warehouse { Id = 2, Name = "Secondary Warehouse", Location = "Warsaw"}
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 1200.50M, Quantity = 50, WarehouseId = 1},
                new Product { Id = 2, Name = "Smartphone", Price = 800.99M, Quantity = 100, WarehouseId = 2}
                );
        }
    }
}
