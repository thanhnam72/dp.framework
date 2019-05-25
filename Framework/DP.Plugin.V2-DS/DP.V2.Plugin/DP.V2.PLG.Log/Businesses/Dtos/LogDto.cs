using System;

namespace DP.V2.PLG.Log.Businesses.Dtos
{
    public class LogDto
    {
        public Guid? Id { get; set; }
        public string Controller { get; set; }
        public string ActionName { get; set; }
        public string Descriptions { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
