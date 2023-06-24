namespace ShopApp.ProductsAPI.Models.Dtos
{
    public class ProductResponseDto
    {
        public bool IsSucess { get; set; } = true;
        public object Result { get; set; }
        public string DisplayMessage { get; set; } = string.Empty;
        public List<string> ErrorMessages { get; set; }
    }
}
