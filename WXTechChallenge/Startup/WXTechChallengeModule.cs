using Autofac;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Reflection;
using WXTechChallenge.Settings;

namespace WXTechChallenge.Startup
{
    public class WXTechChallengeModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;
        public WXTechChallengeModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.GetInterfaces().Any())
                .AsImplementedInterfaces();

            builder.RegisterInstance(_configuration.GetSection("UserSettings").Get<UserSettings>());
        }
    }
}
