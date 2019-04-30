using System;
using System.Collections.Generic;

namespace SmartData.DataAccess.Models
{
    public partial class Role
    {
        public Role()
        {
            Account = new HashSet<Account>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
