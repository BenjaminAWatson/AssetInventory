using System;
using System.Collections.Generic;
using System.Text;

namespace CGI.Asset.Inventory.DataTables
{
    public class DataTableSent
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public List<Order> order { get; set; }
    }
}
