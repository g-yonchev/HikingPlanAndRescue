using System.Web.Mvc;

namespace HikingPlanAndRescue.Web.Areas.Private
{
    public class PrivateAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Private";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Private_default",
                "Private/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "HikingPlanAndRescue.Web.Areas.Private.Controllers" }
            );
        }
    }
}