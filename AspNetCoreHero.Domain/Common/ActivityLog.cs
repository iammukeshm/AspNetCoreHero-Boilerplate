using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Domain.Common
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string IpAddress { get; set; }
        public DateTime DateTime { get; set; }

        public string Action { get; set; }
        public string Entity{ get; set; }

        public string EntityId { get; set; }
    }
}
