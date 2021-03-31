using Grand.Framework.Controllers;
using Grand.Framework.Mvc.Filters;
using Grand.Plugin.Notifier.SMS.Models;
using Grand.Services.Localization;
using Microsoft.AspNetCore.Mvc;
using NotificationManagement.Presentation.Api.GRPC.Proto;
using System;

namespace Grand.Plugin.Notifier.SMS.Controllers
{
    [Area("Admin")]
    [AuthorizeAdmin]
    public class SmsController : BasePluginController
    {
        #region Fields
        private readonly ILocalizationService _localizationService;
        private readonly NotoficationManagementService.NotoficationManagementServiceClient _notificationServiceClient;
        #endregion

        #region Ctor

        public SmsController(NotoficationManagementService.NotoficationManagementServiceClient notificationServiceClient,
            ILocalizationService localizationService)
        {
            _localizationService = localizationService;
            _notificationServiceClient = notificationServiceClient;
        }

        #endregion

        #region methods
        public IActionResult Index()
        {
            var model = new SmsModel();

            return View("~/Plugins/Notifier.SMS/Views/Index.cshtml", model);
        }

        [HttpPost]
        public IActionResult Index(SmsModel model)
        {
            var sendSmsRequest = new SendMessageRequest() {
                Message = new Message() {
                    Content = model.Content,
                    Title = model.Title
                }
            };
            sendSmsRequest.Message.UserIds.Add(Guid.NewGuid().ToString());

            _notificationServiceClient.SendMessageAsync(sendSmsRequest);
            SuccessNotification(_localizationService.GetResource("Plugins.Notifier.SMS.SendSms") + model.Title);
            return View("~/Plugins/Notifier.SMS/Views/Index.cshtml", model);
        }
        #endregion
    }
}
