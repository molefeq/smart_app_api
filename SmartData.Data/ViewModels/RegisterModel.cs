namespace SmartData.Data.ViewModels
{
    public class RegisterModel
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public long CountryId { get; set; }
    }
}
