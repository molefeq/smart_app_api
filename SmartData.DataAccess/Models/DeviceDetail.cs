using System;

namespace SmartData.DataAccess.Models
{
    public partial class DeviceDetail
    {
        public long Id { get; set; }
        public string DeviceName { get; set; }
        public string SerailNumber { get; set; }
        public long? LinkedUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public long CreateUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedUserId { get; set; }
        public virtual Account CreateUser { get; set; }
        public virtual Account LinkedUser { get; set; }
        public virtual Account ModifiedUser { get; set; }
    }
}
