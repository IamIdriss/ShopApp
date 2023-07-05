using System.ComponentModel.DataAnnotations;

namespace Matgr.ShoppingCartAPI.Models
{

    public class CartDetails
    {

        [Key]
        public int CartDetailId { get; set; }

        public int CartHeaderId { get; set; }

        public CartHeader CartHeader { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Count { get; set; }
    }
}
