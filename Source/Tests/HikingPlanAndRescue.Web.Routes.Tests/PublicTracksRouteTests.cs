namespace HikingPlanAndRescue.Web.Routes.Tests
{
    using System.Web.Routing;
    using HikingPlanAndRescue.Web.Controllers;
    using MvcRouteTester;
    using NUnit.Framework;

    [TestFixture]
    public class PublicTracksRouteTests
    {
        [Test]
        public void TestRouteByPageAndPageSize()
        {
            const string Url = "/Tracks?page=1&pageSize=5";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(Url).To<TracksController>(c => c.Index(1, 5));
        }
    }
}