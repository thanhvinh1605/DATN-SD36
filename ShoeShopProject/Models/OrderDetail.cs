using System;
using System.Collections.Generic;

namespace ShoeShopProject.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal AmountPrice { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual ProductVariant Product { get; set; } = null!;
    }
}
