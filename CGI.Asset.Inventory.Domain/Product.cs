using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGI.Asset.Inventory.Domain
{
    public partial class Product
    {
        public Product()
        {
            Asset = new HashSet<Asset>();
        }

        [Key]
        public int ProductKey { get; set; }
        [StringLength(50)]
        public string ProductName { get; set; }

        [InverseProperty("ProductKeyNavigation")]
        public ICollection<Asset> Asset { get; set; }
    }
}
