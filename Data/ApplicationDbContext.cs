using Microsoft.EntityFrameworkCore;
using Aplikacja_na_BDwAI.Models;

namespace Aplikacja_na_BDwAI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacja: Product -> Warehouse (wiele produktów w magazynie)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Warehouse)
                .WithMany(w => w.Products)
                .HasForeignKey(p => p.WarehouseId);

            // Relacja: Order -> User (wiele zamówień dla jednego użytkownika)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacja: Order -> Product (jeden produkt na zamówienie)
            modelBuilder.Entity<Order>()
           .HasOne(o => o.Product)
           .WithMany()
           .HasForeignKey(o => o.ProductId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)"); // Precyzja

            base.OnModelCreating(modelBuilder);
        }
    }
}
