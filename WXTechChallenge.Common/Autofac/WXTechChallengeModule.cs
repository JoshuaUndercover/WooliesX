using System.Linq;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using WXTechChallenge.Common.Settings;
using Module = Autofac.Module;

namespace WXTechChallenge.Common.Autofac
{
    public class WXTechChallengeModule : Module
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
            builder.RegisterInstance(_configuration.GetSection("WooliesXApiSettings").Get<WooliesXApiSettings>());
        }
    }
}
