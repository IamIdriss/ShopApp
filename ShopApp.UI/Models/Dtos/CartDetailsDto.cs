using ShopApp.UI.Models.Dtos;

namespace ShopApp.UI.Models.Dto
{
    public class CartDetailsDto
    {


        public int CartDetailId { get; set; }

        public int CartHeaderId { get; set; }
        public virtual CartHeaderDto CartHeader { get; set; }

        public int ProductId { get; set; }
        public virtual ProductDto Product { get; set; }

        public double Count { get; set; }
    }
}
