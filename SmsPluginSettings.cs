using Grand.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grand.Plugin.Notifier.SMS
{
    public class SmsPluginSettings : ISettings
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsMale { get; set; }
    }
}
