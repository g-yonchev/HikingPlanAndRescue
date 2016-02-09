using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HikingPlanAndRescue.Startup))]
namespace HikingPlanAndRescue
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
