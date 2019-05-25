﻿
namespace DP.Core.Common.JqueryDatatable
{
    /// <summary>
    /// Use for some cases user want to have some variable included into parameter of jquery datatable
    /// </summary>
    public class CustomDataTableRequestHelper : DataTableRequestHelper
    {
        /// <summary>
        /// Custom table parameter string
        /// </summary>
        public string TableName { get; set; } = string.Empty;

        /// <summary>
        /// custom filter parameter string
        /// </summary>
        public string Filter { get; set; } = string.Empty;

        /// <summary>
        /// Custom parameter 1
        /// </summary>
        public string Parameter1 { get; set; } = string.Empty;

        /// <summary>
        /// Custom parameter 2
        /// </summary>
        public string Parameter2 { get; set; } = string.Empty;

        /// <summary>
        /// Custom parameter 3
        /// </summary>
        public string Parameter3 { get; set; } = string.Empty;

        /// <summary>
        /// Custom parameter 4
        /// </summary>
        public string Parameter4 { get; set; } = string.Empty;

        /// <summary>
        /// Custom parameter 5
        /// </summary>
        public string Parameter5 { get; set; } = string.Empty;

    }
}
