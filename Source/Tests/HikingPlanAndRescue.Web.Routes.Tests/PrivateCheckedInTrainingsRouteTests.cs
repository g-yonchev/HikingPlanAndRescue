namespace HikingPlanAndRescue.Web.Routes.Tests
{
    using System.Web.Routing;
    using NUnit.Framework;

    [TestFixture]
    public class PrivateCheckedInTrainingsRouteTests
    {
        [Test]
        public void TestIndexRoute()
        {
            var path = "~/private/checkedintrainings";
            RouteData routeData = Helpers.GetPrivateAreaRouteData(path);

            Assert.IsNotNull(routeData, "Should have found the route");
            Assert.AreEqual("Private", routeData.DataTokens["area"]);
            Assert.AreEqual("checkedintrainings", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
        }

        [Test]
        public void TestAjaxLoadNextTrainingRoute()
        {
            var path = "~/private/AjaxLoadNextTraining";
            RouteData routeData = Helpers.GetPrivateAreaRouteData(path);

            Assert.IsNotNull(routeData, "Should have found the route");
            Assert.AreEqual("Private", routeData.DataTokens["area"]);
            Assert.AreEqual("AjaxLoadNextTraining", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
        }
    }
}