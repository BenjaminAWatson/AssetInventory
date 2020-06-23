using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGI.Asset.Inventory.Domain
{
    public partial class Location
    {
        public Location()
        {
            Asset = new HashSet<Asset>();
        }

        [Key]
        public int LocationKey { get; set; }
        [Required]
        [StringLength(50)]
        public string LocationName { get; set; }

        [InverseProperty("LocationKeyNavigation")]
        public ICollection<Asset> Asset { get; set; }
    }
}
