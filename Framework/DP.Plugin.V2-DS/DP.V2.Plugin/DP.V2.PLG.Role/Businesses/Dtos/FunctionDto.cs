using System;
using System.Collections.Generic;
using System.Text;

namespace DP.V2.PLG.Role.Businesses.Dtos
{
    public class FunctionDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string ActionName { get; set; }
        public string FnCd { get; set; }
    }
}
