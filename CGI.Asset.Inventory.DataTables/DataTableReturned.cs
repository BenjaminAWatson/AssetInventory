using System;
using System.Collections.Generic;
using System.Text;

namespace CGI.Asset.Inventory.DataTables
{
    public class DataTableReturned
    {
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public object data { get; set; }

        public DataTableReturned(DataTableSent sent)
        {
            this.draw = sent.draw;
        }
    }
}
