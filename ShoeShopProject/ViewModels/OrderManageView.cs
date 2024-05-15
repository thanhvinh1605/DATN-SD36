namespace ShoeShopProject.ViewModels
{
    public class OrderManageView
    {
        public int orderId { get; set; }
        public string customerName { get; set; }
        public DateTime updateDate { get; set; }
        public int orderStatus { get; set; }
        public bool paymentStatus { get; set; }
        public decimal totalAmount { get; set; }
        public int? saleID { get; set; }
        public string saleName { get; set; }
    }
}
