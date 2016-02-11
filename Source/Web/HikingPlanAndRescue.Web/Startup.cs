using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(HikingPlanAndRescue.Web.Startup))]

namespace HikingPlanAndRescue.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
