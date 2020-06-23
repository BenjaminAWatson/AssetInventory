using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGI.Asset.Inventory.Domain
{
    public partial class ClientSite
    {
        public ClientSite()
        {
            Asset = new HashSet<Asset>();
        }

        [Key]
        public int ClientSiteKey { get; set; }
        [Required]
        [StringLength(50)]
        public string ClientSiteName { get; set; }

        [InverseProperty("ClientSiteKeyNavigation")]
        public ICollection<Asset> Asset { get; set; }
    }
}
