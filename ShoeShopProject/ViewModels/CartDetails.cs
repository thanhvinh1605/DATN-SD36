namespace ShoeShopProject.ViewModels
{
    public class CartDetails
    {
        public int cartItemId { get; set; }
        public int productId { get; set; }
        public int productVariantId { get; set; }
        public string productName { get; set; }
        public string thumbnail { get; set; }
        public string colorName { get; set; }
        public int size { get; set; }
        public decimal unitPrice { get; set; }
        public int quantity { get; set; }
        public decimal priceAmout { get; set; }
    }
}
