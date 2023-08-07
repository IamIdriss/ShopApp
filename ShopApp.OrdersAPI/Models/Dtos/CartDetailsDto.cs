namespace ShopApp.OrdersAPI.Models.Dto
{
    public class CartDetailsDto
    {


        public int CartDetailId { get; set; }

        public int CartHeaderId { get; set; }
        

        public int ProductId { get; set; }
        public virtual ProductsDto Product { get; set; }

        public int Count { get; set; }
    }
}
