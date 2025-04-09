using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    // Models/LoginLog.cs
    public class LoginLog
    {
        public string LogId { get; set; }
        public string Username { get; set; }
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime LoginTime { get; set; } = DateTime.Now;
    }

}
