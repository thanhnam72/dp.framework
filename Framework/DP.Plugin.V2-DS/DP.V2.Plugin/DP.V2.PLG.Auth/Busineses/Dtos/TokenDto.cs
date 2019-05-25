using System;

namespace DP.V2.PLG.Auth.Busineses.Dtos
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime Expired { get; set; }
        public string Username { get; set; }
        public string Id { get; set; }
    }
}
