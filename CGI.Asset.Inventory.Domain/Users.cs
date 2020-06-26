using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGI.Asset.Inventory.Domain
{
    public partial class Users
    {
        [Key]
        public int UserId { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("Users")]
        public Roles UsersKeyNavigation { get; set; }
    }
}
