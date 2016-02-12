namespace HikingPlanAndRescue.Web.Infrastructure.Constraints
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Routing;

    public class AuthenticationRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return httpContext.User.Identity.IsAuthenticated;
        }
    }
}
