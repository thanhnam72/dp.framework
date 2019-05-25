using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace DP.V2.Core.Common.Extensions
{
    public static class DataTableExt
    {
        public static List<T> ToList<T>(this DataTable tbl) where T : class
        {
            // define return list
            List<T> lst = new List<T>();

            // go through each row
            foreach (DataRow r in tbl.Rows)
            {
                // add to the list
                lst.Add(CreateItemFromRow<T>(r));
            }

            // return the list
            return lst;
        }

        private static T CreateItemFromRow<T>(DataRow row) where T : class
        {
            // create a new object
            T item = (T)Activator.CreateInstance(typeof(T));

            // set the item
            SetItemFromRow(item, row);

            // return 
            return item;
        }

        private static void SetItemFromRow<T>(T item, DataRow row) where T : class
        {
            // go through each column
            foreach (DataColumn c in row.Table.Columns)
            {
                // find the property for the column
                PropertyInfo p = item.GetType().GetProperty(c.ColumnName);

                // if exists, set the value
                if (p != null && row[c] != DBNull.Value)
                {
                    p.SetValue(item, row[c], null);
                }
            }
        }
    }
}
