using System.ComponentModel.DataAnnotations;

namespace ShopApp.ProductsAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Range (1, double.MaxValue)]
        public double Price { get; set; }

        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }


    }
}
