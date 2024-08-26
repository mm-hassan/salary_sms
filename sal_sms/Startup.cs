using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sal_sms.Startup))]
namespace sal_sms
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
