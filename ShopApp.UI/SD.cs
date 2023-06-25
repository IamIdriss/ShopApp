namespace ShopApp.UI
{
    public static class SD
    {
        public static string ProductsAPIUrl { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,    
            DELETE
        }
    }
}
