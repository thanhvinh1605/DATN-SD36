using System;
using System.Collections.Generic;

namespace ShoeShopProject.Models
{
    public partial class Color
    {
        public Color()
        {
            ProductVariants = new HashSet<ProductVariant>();
        }

        public int Id { get; set; }
        public string Cname { get; set; } = null!;
        public string? Cvalue { get; set; }

        public virtual ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
