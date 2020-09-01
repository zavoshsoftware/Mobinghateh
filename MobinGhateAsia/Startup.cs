using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MobinGhateAsia.Startup))]
namespace MobinGhateAsia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
