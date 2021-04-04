using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HrManagementAlpha.Startup))]
namespace HrManagementAlpha
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
