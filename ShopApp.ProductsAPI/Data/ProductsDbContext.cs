using Microsoft.EntityFrameworkCore;
using ShopApp.ProductsAPI.Models;

namespace ShopApp.ProductsAPI.Data
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "MSI GF63 Thin Gaming Laptop",
                Price = 7800,
                Description = "MSI GF63 Thin Gaming Laptop - Intel Core I5 - 8GB RAM - 256GB SSD - 15.6-inch FHD - 4GB GPU - Windows 10 - Black (English Keyboard).",
                ImageUrl = "/images/products/1.jpg",
                CategoryName = "Laptops"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "DELL G3 15-3500 Gaming Laptop",
                Price = 1240,
                Description = "DELL G3 15-3500 Gaming Laptop - Intel Core I5-10300H - 8GB RAM - 256GB SSD + 1TB HDD - 15.6-inch FHD - 4GB GTX 1650 GPU - Ubuntu - Black.",
                ImageUrl = "/images/products/2.jpg",
                CategoryName = "Laptops"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Samsung UA43T5300",
                Price = 650,
                Description = "Samsung UA43T5300 - 43-inch Full HD Smart TV With Built-In Receiver.",
                ImageUrl = "/images/products/3.jpg",
                CategoryName = "Televisions"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "LG 43LM6370PVA",
                Price = 620,
                Description = "LG 43LM6370PVA - 43-inch Full HD Smart TV With Built-in Receiver.",
                ImageUrl = "/images/products/4.jpg",
                CategoryName = "Televisions"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 5,
                Name = "Samsung Galaxy A12",
                Price = 285,
                Description = "Samsung Galaxy A12 - 6.4-inch 128GB/4GB Dual SIM Mobile Phone - White.",
                ImageUrl = "/images/products/5.jpg",
                CategoryName = "Mobile Phones"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 6,
                Name = "Apple iPhone 12 Pro Max",
                Price = 2250,
                Description = "Apple iPhone 12 Pro Max Dual SIM with FaceTime - 256GB - Pacific Blue.",
                ImageUrl = "/images/products/6.jpg",
                CategoryName = "Mobile Phones"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 7,
                Name = "OPPO Realme 8 Pro Case",
                Price = 15,
                Description = "OPPO Realme 8 Pro Case, Dual Layer PC Back TPU Bumper Hybrid No-Slip Shockproof Cover For OPPO Realme 8 / Realme 8 Pro 4G.",
                ImageUrl = "/images/products/7.jpg",
                CategoryName = "Mobile Accessories"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 8,
                Name = "Galaxy Z Flip3 5G Case",
                Price = 25,
                Description = "Galaxy Z Flip3 5G Case, Slim Luxury Electroplate Frame Crystal Clear Back Protective Case Cover For Samsung Galaxy Z Flip 3 5G Purple.",
                ImageUrl = "/images/products/8.jpg",
                CategoryName = "Mobile Accessories"
            });
        }
    }
}
