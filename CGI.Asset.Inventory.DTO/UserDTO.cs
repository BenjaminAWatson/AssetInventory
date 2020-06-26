using System;
using System.Collections.Generic;
using System.Text;

namespace CGI.Asset.Inventory.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
    }
}
