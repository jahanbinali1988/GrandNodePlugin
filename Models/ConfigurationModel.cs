using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Plugin.Notifier.SMS.Models
{
    public class ConfigurationModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsMale { get; set; }
    }
}
