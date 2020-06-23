using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGI.Asset.Inventory.Domain
{
    public partial class Manufacturer
    {
        public Manufacturer()
        {
            Asset = new HashSet<Asset>();
        }

        [Key]
        [Column("ManufacturerKey")]
        public int ManufacturerKey { get; set; }
        [Required]
        [StringLength(50)]
        public string ManufacturerName { get; set; }

        [InverseProperty("ManufacturerKeyNavigation")]
        public ICollection<Asset> Asset { get; set; }
    }
}
