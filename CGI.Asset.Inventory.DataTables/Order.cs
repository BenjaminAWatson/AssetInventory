using System;
using System.Collections.Generic;
using System.Text;

namespace CGI.Asset.Inventory.DataTables
{
    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }

        public bool IsASC()
        {
            return this.dir == "asc";
        }
    }
}
