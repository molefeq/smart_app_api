namespace SmartData.Data.ViewModels
{
    public class DeviceDetailModel
    {
        public long Id { get; set; }
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string SerailNumber { get; set; }
        public long? LinkedUserId { get; set; }
        public long? LinkedUserName { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyType { get; set; }
        public string LastDeviceCheck { get; set; }
    }
}
