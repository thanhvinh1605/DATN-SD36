using System;
using System.Collections.Generic;

namespace ShoeShopProject.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string? Description { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
