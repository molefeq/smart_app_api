using System;

namespace SmartData.Data.ViewModels
{
    public class AccountModel
    {
        public long Id { get; set; }
        public string ContactNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreateUserId { get; set; }
        public DateTime? DisableDate { get; set; }
        public string EmailAddress { get; set; }
        public string Firstname { get; set; }
        public bool IsFirstTimeLogin { get; set; }
        public string LastName { get; set; }
        public byte[] Password { get; set; }
        public Guid? PasswordResetKey { get; set; }
        public byte[] PasswordSalt { get; set; }
        public long RoleId { get; set; }
        public long CountryId { get; set; }
        public string Username { get; set; }
    }
}
