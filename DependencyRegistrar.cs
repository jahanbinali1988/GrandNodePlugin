using Grand.Core.Configuration;
using Grand.Core.TypeFinders;
using Microsoft.Extensions.DependencyInjection;

namespace Grand.Plugin.Notifier.SMS
{
    public class DependencyRegistrar : Grand.Core.DependencyInjection.IDependencyInjection
    {
        public virtual void Register(IServiceCollection serviceCollection, ITypeFinder typeFinder, GrandConfig config)
        {
            serviceCollection.AddScoped<SmsPluginMethod>();
        }

        public int Order {
            get { return 10; }
        }
    }
}
