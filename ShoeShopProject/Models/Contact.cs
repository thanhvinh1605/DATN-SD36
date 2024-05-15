using System;
using System.Collections.Generic;

namespace ShoeShopProject.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string ContactName { get; set; } = null!;
        public string? ContactDescription { get; set; }
        public string? ImageUrl { get; set; }
    }
}
