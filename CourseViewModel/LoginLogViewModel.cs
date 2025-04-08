using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesViewModel
{
    // ViewModels/LoginLogViewModel.cs
    public class LoginLogViewModel
    {
        public string LogId { get; set; }
        public string Username { get; set; }
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
        public string LoginTimeFormatted => LoginTime.ToString("yyyy-MM-dd HH:mm:ss");

        public DateTime LoginTime { get; set; }
    }

}
