using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGI.Asset.Inventory.Domain
{
    public partial class Model
    {
        public Model()
        {
            Asset = new HashSet<Asset>();
        }

        [Key]
        public int ModelKey { get; set; }
        [Required]
        [StringLength(50)]
        public string ModelName { get; set; }

        [InverseProperty("ModelKeyNavigation")]
        public ICollection<Asset> Asset { get; set; }
    }
}
