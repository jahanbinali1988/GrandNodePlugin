using Grand.Framework.Controllers;
using Grand.Framework.Mvc.Filters;
using Grand.Plugin.Notifier.SMS.Models;
using Grand.Services.Configuration;
using Grand.Services.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Grand.Plugin.Notifier.SMS.Controllers
{
    [Area("Admin")]
    [AuthorizeAdmin]
    public class NotifierController : BasePluginController
    {
        #region Fields

        private readonly SmsPluginSettings _settings;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        #endregion

        #region Ctor

        public NotifierController(SmsPluginSettings settings, ISettingService settingService,
            ILocalizationService localizationService)
        {
            _settings = settings;
            _settingService = settingService;
            _localizationService = localizationService;
        }

        #endregion

        #region Methods
        public IActionResult Configure()
        {
            var model = new ConfigurationModel();
            model.IsMale = _settings.IsMale;
            model.Login = _settings.Login;
            model.Password = _settings.Password;
            return View("~/Plugins/Notifier.SMS/Views/Configure.cshtml", model);
        }
        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            model.IsMale = _settings.IsMale;
            model.Login = _settings.Login;
            model.Password = _settings.Password;

            _settingService.SaveSetting(_settings); //save our settings
            _settingService.ClearCache();
            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }
        #endregion
    }
}
