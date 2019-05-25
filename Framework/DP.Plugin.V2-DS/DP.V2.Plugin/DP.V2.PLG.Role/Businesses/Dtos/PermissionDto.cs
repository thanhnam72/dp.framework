namespace DP.V2.PLG.Role.Businesses.Dtos
{
    public class PermissionDto
    {
        public string FnCd { get; set; }
        public bool View { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Remove { get; set; }
        public bool Import { get; set; }
        public bool Export { get; set; }
        public string Other { get; set; }
    }
}
