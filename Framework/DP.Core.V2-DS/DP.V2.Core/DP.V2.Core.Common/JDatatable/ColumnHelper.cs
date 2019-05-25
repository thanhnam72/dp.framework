using System;

namespace DP.Core.Common.JqueryDatatable
{
    /// <summary>
    /// List of column of datatable
    /// </summary>
    public class ColumnHelper
    {
        /// <summary>
        /// Name of column map with column's name in data object
        /// IF not, that is the order of the column in table on design
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Allow for Search
        /// </summary>
        public bool Searchable { get; set; }
        /// <summary>
        /// Allow for order
        /// </summary>
        public bool Orderable { get; set; }

        /// <summary>
        /// Column data in jquery datatable
        /// </summary>
        public ColumnHelper()
        {
            this.Data = "";
            this.Name = "";
            this.Searchable = false;
            this.Orderable = false;
        }
    }
}
