using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CGI.Asset.Inventory.DataTables
{
    public static class DataTableHelpers
    {
        public static IEnumerable<T> GetOrderedPaged<T>(IEnumerable<T> customers, DataTableSent sent)
        {
            var prop = GetDataTableProp<T>(sent);
            IOrderedEnumerable<T> paged;

            if (isASC(sent))
                paged = customers.OrderBy(c => prop.GetValue(c, null));
            else
                paged = customers.OrderByDescending(c => prop.GetValue(c, null));

            return GetPaged(paged, sent);
        }

        public static IEnumerable<T> GetOrdered<T>(IEnumerable<T> list, DataTableSent sent)
        {
            var prop = GetDataTableProp<T>(sent);
            IOrderedEnumerable<T> ordered;

            if (isASC(sent))
                ordered = list.OrderBy(c => prop.GetValue(c, null));
            else
                ordered = list.OrderByDescending(c => prop.GetValue(c, null));

            return ordered;
        }

        public static IEnumerable<T> GetPaged<T>(IOrderedEnumerable<T> list, DataTableSent sent)
        {
            return list.Skip(sent.start).Take(sent.length);
        }
        public static IEnumerable<T> GetPagedWithoutOrder<T>(IEnumerable<T> list, DataTableSent sent)
        {
            return list.Skip(sent.start).Take(sent.length);
        }

        internal static PropertyInfo GetDataTableProp<T>(DataTableSent sent)
        {
            var order = sent.columns[sent.order.FirstOrDefault().column];
            return typeof(T).GetProperty(order.name);
        }

        public static bool isASC(DataTableSent sent)
        {
            if (sent.order != null)
            {
                var order = sent.order.FirstOrDefault();
                if (order != null)
                {
                    return order.dir == "asc";
                }
                else
                    return false;
            }
            return false;
        }
    }
}
