using System;
using System.Collections.Generic;

namespace DP.V2.PLG.Role.Businesses.Dtos
{
    public class RoleDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Descriptions { get; set; }
        public IList<PermissionDto> PermissionData { get; set; }
    }
}
