using Core.Utilities.Helpers;
using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Identity
{
    public class IdentityModule : ICoreModule
    {
        IConfiguration Configuration;

        public IdentityModule(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IEmailSender, SmtpEmailSender>(i => 
                new SmtpEmailSender(
                    Configuration["EmailSender:Host"],
                    Configuration.GetValue<int>("EmailSender:Port"),
                    Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    Configuration["EmailSender:UserName"],
                    Configuration["EmailSender:Password"]));
        }
    }
}
