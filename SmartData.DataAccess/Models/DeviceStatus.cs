using System;
using System.Collections.Generic;
using System.Text;

namespace SmartData.DataAccess.Models
{
    public partial class DeviceStatus
    {
        public DeviceStatus()
        {
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

    }
}
