using System;
using System.Collections.Generic;

namespace ShoeShopProject.Models
{
    public partial class Admin
    {
        public Admin()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Image { get; set; } = null!;
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
