using Grand.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationManagement.Presentation.Api.GRPC.SDK;

namespace Grand.Plugin.Notifier.SMS
{
    public class Startup : IGrandStartup
    {
        public int Order {
            get { return 10; }
        }

        public void Configure(IApplicationBuilder application, IWebHostEnvironment webHostEnvironment)
        {
            
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.NotificationManagementServiceSdk(2, "http://localhost:810");
        }
    }
}
