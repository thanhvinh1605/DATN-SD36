using System;
using System.Collections.Generic;

namespace ShoeShopProject.Models
{
    public partial class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAmount { get; set; }

        public virtual Cart Cart { get; set; } = null!;
        public virtual ProductVariant Product { get; set; } = null!;
    }
}
