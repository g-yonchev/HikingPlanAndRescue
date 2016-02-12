namespace HikingPlanAndRescue.Web.Areas.Private
{
    using System.Web.Mvc;
    using Constraints;

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
                name: "Private_default",
                url: "Private/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                constraints: new { authConstraint = new AuthenticationRouteConstraint() },
                namespaces: new string[] { "HikingPlanAndRescue.Web.Areas.Private.Controllers" }
            );
        }
    }
}