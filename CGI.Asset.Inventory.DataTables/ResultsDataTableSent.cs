using System;
using System.Collections.Generic;
using System.Text;

namespace CGI.Asset.Inventory.DataTables
{
    public class ResultsDataTableSent: DataTableSent
    {
        public int AssetTag { get; set; }
        public string InventoryOwner { get; set; }
        public int LocationKey { get; set; }
        public int ClientSiteKey { get; set; }
        public string KeywordSearch { get; set; }
    }
}
