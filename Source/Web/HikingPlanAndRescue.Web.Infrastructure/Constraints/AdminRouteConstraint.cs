namespace HikingPlanAndRescue.Web.Infrastructure.Constraints
{
    using System.Web;
    using System.Web.Routing;

    public class AdminRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return httpContext.User.IsInRole("Admin");
        }
    }
}