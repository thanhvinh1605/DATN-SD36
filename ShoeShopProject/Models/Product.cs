using System;
using System.Collections.Generic;

namespace ShoeShopProject.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductVariants = new HashSet<ProductVariant>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Desciption { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public string Thumbnail { get; set; } = null!;
        public string? ImagePath { get; set; }

        public virtual Brand Brand { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
