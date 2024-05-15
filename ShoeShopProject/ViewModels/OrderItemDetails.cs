namespace ShoeShopProject.ViewModels
{
    public class OrderItemDetails
    {
        public int Id { get; set; }
        public int productId { get; set; }
        public string productName { get; set; }
        public string? colorName { get; set; }
        public int? size { get; set; }
        public int quantity { get; set; }
        public decimal priceAmout { get; set; }
    }
}
