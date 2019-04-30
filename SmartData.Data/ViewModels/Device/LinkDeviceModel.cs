using System;
using System.Collections.Generic;
using System.Text;

namespace SmartData.Data.ViewModels.Device
{
    public class LinkDeviceModel
    {
        public long Id { get; set; }
        public long LinkUserId { get; set; }
        public long UserId { get; set; }
        public string EmailAddress { get; set; }
        public string DeviceName { get; set; }
        public string SerialNumber { get; set; }
        public string TerminalPassword { get; set; }
    }
}
