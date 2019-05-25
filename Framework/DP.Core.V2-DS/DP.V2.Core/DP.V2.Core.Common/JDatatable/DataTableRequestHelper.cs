using System.Collections.Generic;

namespace DP.Core.Common.JqueryDatatable
{
    /// <summary>
    /// Jquery datatable Request from client
    /// </summary>
    public class DataTableRequestHelper
    {
        /// <summary>
        /// draw
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// start
        /// </summary>
        public int start { get; set; }

        /// <summary>
        /// length
        /// </summary>
        public int length { get; set; }

        /// <summary>
        /// order
        /// </summary>
        public List<OrderingHelper> order { get; set; }

        /// <summary>
        /// search
        /// </summary>
        public SearchHelper search { get; set; }

        /// <summary>
        /// columns
        /// </summary>
        public List<ColumnHelper> columns { get; set; }
    }
}
