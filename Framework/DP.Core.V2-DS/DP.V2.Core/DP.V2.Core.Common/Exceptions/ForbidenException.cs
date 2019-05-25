﻿namespace DP.V2.Core.Common.Exceptions
{
    /// <summary>
    /// Forbiden custom handling exception class
    /// </summary>
    public class ForbidenException : System.Exception
    {
        /// <summary>
        /// Request is ajax type or not
        /// </summary>
        public bool IsAjaxRequest;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isAjax">is ajax request or not</param>
        /// <param name="ex">Exception</param>
        public ForbidenException(bool isAjax, System.Exception ex): base()
        {
            IsAjaxRequest = isAjax;
        }
    }
}
