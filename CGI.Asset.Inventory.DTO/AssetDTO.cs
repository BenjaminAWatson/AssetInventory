using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CGI.Asset.Inventory.DTO
{
     public class AssetDTO
    {
        [Required(ErrorMessage ="Please enter Asset Tag")]
        public int AssetTag { get; set; }
        public string Product { get; set; }
        public int ProductKey { get; set; }
        public string Manufacturer { get; set; }
        public int ManufacturerKey { get; set; }
        public string Model { get; set; }
        public int ModelKey { get; set; }
        public string Location { get; set; }
        public int LocationKey { get; set; }
        public string ClientSite { get; set; }
        public int ClientSiteKey { get; set; }
        public int? SerialNumber { get; set; }
        public string ItemName { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string InventoryOwner { get; set; }
        public string InventoriedBy { get; set; }
        public DateTime? InventoryDate { get; set; }
        public bool isDisposed { get; set; }
        public string KeywordSearch { get; set; }
    }
}
