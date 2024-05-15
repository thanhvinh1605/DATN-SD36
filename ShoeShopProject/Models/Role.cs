using System;
using System.Collections.Generic;

namespace ShoeShopProject.Models
{
    public partial class Role
    {
        public Role()
        {
            Admins = new HashSet<Admin>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Admin> Admins { get; set; }
    }
}
