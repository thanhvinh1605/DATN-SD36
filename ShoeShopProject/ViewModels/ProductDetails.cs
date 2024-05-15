namespace ShoeShopProject.ViewModels
{
    public class ProductDetails
    {
        public int pId { get; set; }
        public int colorID { get; set; }
        public string? colorVal { get; set; }
        public string colorName { get; set; } = String.Empty;
        public int sizeId { get; set; }
        public int size { get; set; }
        public int quantity { get; set; }
    }
}
