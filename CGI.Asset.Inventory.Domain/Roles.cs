using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGI.Asset.Inventory.Domain
{
    public partial class Roles
    {
        public Roles()
        {
            Users = new HashSet<Users>();
        }

        [Key]
        public int RoleKey { get; set; }
        [StringLength(50)]
        public string RoleName { get; set; }
        [InverseProperty("UsersKeyNavigation")]
        public ICollection<Users> Users { get; set; }
    }
}
