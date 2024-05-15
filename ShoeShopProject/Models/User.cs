using System;
using System.Collections.Generic;

namespace ShoeShopProject.Models
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set; }
        public DateTime? Birthday { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
