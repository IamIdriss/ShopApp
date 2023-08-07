using System.ComponentModel.DataAnnotations;

namespace ShopApp.OrdersAPI.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailId { get; set; }

        public int OrderHeaderId { get; set; }

        public OrderHeader CartHeader { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public double Price { get; set; }

        public int Count { get; set; }
    }
}
