using System;

namespace DP.V2.PLG.Identity.Businesses.Dtos
{
    public class UserDto
    {
        public Guid? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public Guid? RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
