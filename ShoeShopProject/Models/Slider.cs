using System;
using System.Collections.Generic;

namespace ShoeShopProject.Models
{
    public partial class Slider
    {
        public int Id { get; set; }
        public string Image { get; set; } = null!;
        public bool Status { get; set; }
    }
}
