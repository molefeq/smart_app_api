namespace SmartData.Data.ViewModels
{
    public class UserModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DeviceDetailModel Device { get; set; }
    }
}
