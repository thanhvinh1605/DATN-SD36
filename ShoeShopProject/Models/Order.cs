using System;
using System.Collections.Generic;

namespace ShoeShopProject.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string OrderAddress { get; set; } = null!;
        public string OrderPhone { get; set; }
        public string OrderName { get; set; }
        public int PaymentMethod { get; set; }
        public bool PaymentStatus { get; set; }
        public int OrderStatus { get; set; }
        public DateTime UpdateDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? OrderDesc { get; set; }
        public int? SaleId { get; set; }

        public virtual Payment PaymentMethodNavigation { get; set; } = null!;
        public virtual Admin? Sale { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
