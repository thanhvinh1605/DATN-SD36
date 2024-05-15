using System;
using System.Collections.Generic;

namespace ShoeShopProject.Models
{
    public partial class Size
    {
        public Size()
        {
            ProductVariants = new HashSet<ProductVariant>();
        }

        public int Id { get; set; }
        public int SizeVal { get; set; }

        public virtual ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
