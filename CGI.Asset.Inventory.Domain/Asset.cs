using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGI.Asset.Inventory.Domain
{
    public partial class Asset
    {
        [Key]
        public int AssetKey { get; set; }
        public int ProductKey { get; set; }
        public int ManufacturerKey { get; set; }
        public int ModelKey { get; set; }
        public int LocationKey { get; set; }
        public int ClientSiteKey { get; set; }
        public int? SerialNumber { get; set; }
        [StringLength(50)]
        public string ItemName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PurchaseDate { get; set; }
        [StringLength(50)]
        public string InventoryOwner { get; set; }
        [StringLength(50)]
        public string InventoriedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InventoryDate { get; set; }
        public bool IsDisposed { get; set; }

        [ForeignKey("ClientSiteKey")]
        [InverseProperty("Asset")]
        public ClientSite ClientSiteKeyNavigation { get; set; }
        [ForeignKey("LocationKey")]
        [InverseProperty("Asset")]
        public Location LocationKeyNavigation { get; set; }
        [ForeignKey("ManufacturerKey")]
        [InverseProperty("Asset")]
        public Manufacturer ManufacturerKeyNavigation { get; set; }
        [ForeignKey("ModelKey")]
        [InverseProperty("Asset")]
        public Model ModelKeyNavigation { get; set; }
        [ForeignKey("ProductKey")]
        [InverseProperty("Asset")]
        public Product ProductKeyNavigation { get; set; }
    }
}
