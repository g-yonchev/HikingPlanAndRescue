namespace HikingPlanAndRescue.Web.Routes.Tests
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Areas.Private;
    using Moq;

    public static class Helpers
    {
        internal static RouteData GetPrivateAreaRouteData(string route)
        {
            var routeCollection = new RouteCollection();
            var areaReg = new PrivateAreaRegistration();
            areaReg.RegisterArea(new AreaRegistrationContext(areaReg.AreaName, routeCollection));

            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns(route);

            // Get the RouteData based on the HttpContext
            var routeData = routeCollection.GetRouteData(context.Object);
            return routeData;
        }
    }
}