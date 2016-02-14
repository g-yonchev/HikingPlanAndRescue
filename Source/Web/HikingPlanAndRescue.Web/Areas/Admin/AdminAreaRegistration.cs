namespace HikingPlanAndRescue.Web.Areas.Admin
{
    using System.Web.Mvc;
    using HikingPlanAndRescue.Web.Infrastructure.Constraints;

    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                constraints: new { adminConstraint = new AdminRouteConstraint() },
                namespaces: new string[] { "HikingPlanAndRescue.Web.Areas.Admin.Controllers" }
            );
        }
    }
}