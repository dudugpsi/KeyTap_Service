using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KeyTap_Service.Startup))]
namespace KeyTap_Service
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
