namespace DP.V2.Core.Common.Exceptions
{
    /// <summary>
    /// Not found custom handling processing class
    /// </summary>
    public class NotFoundException : System.Exception
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
        public NotFoundException(bool isAjax, System.Exception ex) : base()
        {
            IsAjaxRequest = isAjax;
        }
    }
}
