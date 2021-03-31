using Grand.Core;
using Grand.Core.Plugins;
using Grand.Framework.Menu;
using Grand.Services.Common;
using Grand.Services.Configuration;
using Grand.Services.Localization;
using System.Linq;
using System.Threading.Tasks;

namespace Grand.Plugin.Notifier.SMS
{
    public class SmsPluginMethod : BasePlugin, IMiscPlugin, IAdminMenuPlugin
    {
        #region Fields

        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        private readonly ILanguageService _languageService;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public SmsPluginMethod(ISettingService settingService, ILocalizationService localizationService, ILanguageService languageService,
            IWebHelper webHelper)
        {
            _settingService = settingService;
            _localizationService = localizationService;
            _languageService = languageService;
            _webHelper = webHelper;
        }

        #endregion

        #region Methods
        public override async Task Install()
        {
            await _settingService.SaveSetting(new SmsPluginSettings());

            await this.AddOrUpdatePluginLocaleResource(_localizationService, _languageService, "Plugins.Notifier.SMS.SendAdvertisementSms", "Send Advertisement SMS");
            await this.AddOrUpdatePluginLocaleResource(_localizationService, _languageService, "Plugins.Notifier.SMS.ConfigSmsPanel", "Config Advertisement");
            await this.AddOrUpdatePluginLocaleResource(_localizationService, _languageService, "Plugins.Notifier.SMS.NotifierRoot", "Khanoumi Notifier");
            await this.AddOrUpdatePluginLocaleResource(_localizationService, _languageService, "Plugins.Notifier.SMS.SendSms", "Messages sent");
            await this.AddOrUpdatePluginLocaleResource(_localizationService, _languageService, "Plugins.Notifier.SMS.KhanoumiRoot", "Khanoumi plugins");

            await base.Install();
        }

        public async Task ManageSiteMap(SiteMapNode rootNode)
        {
            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "khanoumi Team");
            if (pluginNode == null)
            {
                rootNode.ChildNodes.Add(new SiteMapNode() {
                    SystemName = "khanoumi Team",
                    Visible = true,
                    IconClass = "icon-puzzle",
                    ResourceName = "Plugins.Notifier.SMS.KhanoumiRoot"
                });

                pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "khanoumi Team");
            }

            var menu = new SiteMapNode();
            menu.ResourceName = "Plugins.Notifier.SMS.NotifierRoot";
            menu.Visible = true;
            menu.SystemName = "Notification Plugin";

            menu.ChildNodes.Add(new SiteMapNode() {
                ControllerName = "Notifier",
                ActionName = "Configure",
                Visible = true,
                SystemName = "Config SMS Panel",
                IconClass = "envelope-open",
                ResourceName = "Plugins.Notifier.SMS.ConfigSmsPanel"
            });

            menu.ChildNodes.Add(new SiteMapNode() {
                ControllerName = "Sms",
                ActionName = "Index",
                Visible = true,
                SystemName = "Send SMS Panel",
                IconClass = "pencil-square",
                ResourceName = "Plugins.Notifier.SMS.SendAdvertisementSms"
            });

            if (pluginNode != null)
                pluginNode.ChildNodes.Add(menu);
            else
                rootNode.ChildNodes.Add(menu);
        }

        public override async Task Uninstall()
        {
            await _settingService.DeleteSetting<SmsPluginSettings>();

            await this.DeletePluginLocaleResource(_localizationService, _languageService, "Plugins.Notifier.SMS.SendAdvertisementSms");
            await this.DeletePluginLocaleResource(_localizationService, _languageService, "Plugins.Notifier.SMS.ConfigSmsPanel");
            await this.DeletePluginLocaleResource(_localizationService, _languageService, "Plugins.Notifier.SMS.NotifierRoot");
            await this.DeletePluginLocaleResource(_localizationService, _languageService, "Plugins.Notifier.SMS.SendSms");
            await this.DeletePluginLocaleResource(_localizationService, _languageService, "Plugins.Notifier.SMS.KhanoumiRoot");

            await base.Uninstall();
        }
        #endregion
    }
}
