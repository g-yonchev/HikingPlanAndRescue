namespace HikingPlanAndRescue.Web.Routes.Tests
{
    using System.Web.Routing;
    using NUnit.Framework;

    [TestFixture]
    public class PrivateTrainingsRouteTests
    {
        [Test]
        public void TestIndexRouteByStartAndEndDate()
        {
            var path = "~/private/trainings";
            RouteData routeData = Helpers.GetPrivateAreaRouteData(path);

            Assert.IsNotNull(routeData, "Should have found the route");
            Assert.AreEqual("Private", routeData.DataTokens["area"]);
            Assert.AreEqual("trainings", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);

            //Assert.AreEqual("", routeData.Values["id"]);
        }

        [Test]
        public void TestCreateRoute()
        {
            var path = "~/private/trainings/create";
            RouteData routeData = Helpers.GetPrivateAreaRouteData(path);

            Assert.IsNotNull(routeData, "Should have found the route");
            Assert.AreEqual("Private", routeData.DataTokens["area"]);
            Assert.AreEqual("trainings", routeData.Values["controller"]);
            Assert.AreEqual("create", routeData.Values["action"]);

            //Assert.AreEqual("", routeData.Values["id"]);
        }

        [Test]
        public void TestEditRoute()
        {
            var path = "~/private/trainings/edit";
            RouteData routeData = Helpers.GetPrivateAreaRouteData(path);

            Assert.IsNotNull(routeData, "Should have found the route");
            Assert.AreEqual("Private", routeData.DataTokens["area"]);
            Assert.AreEqual("trainings", routeData.Values["controller"]);
            Assert.AreEqual("edit", routeData.Values["action"]);

            //Assert.AreEqual("", routeData.Values["id"]);
        }

        [Test]
        public void TestDeleteRoute()
        {
            var path = "~/private/trainings/delete";
            RouteData routeData = Helpers.GetPrivateAreaRouteData(path);

            Assert.IsNotNull(routeData, "Should have found the route");
            Assert.AreEqual("Private", routeData.DataTokens["area"]);
            Assert.AreEqual("trainings", routeData.Values["controller"]);
            Assert.AreEqual("delete", routeData.Values["action"]);

            //Assert.AreEqual("", routeData.Values["id"]);
        }

        [Test]
        public void TestAjaxWatchRoute()
        {
            var path = "~/private/trainings/ajaxwatch";
            RouteData routeData = Helpers.GetPrivateAreaRouteData(path);

            Assert.IsNotNull(routeData, "Should have found the route");
            Assert.AreEqual("Private", routeData.DataTokens["area"]);
            Assert.AreEqual("trainings", routeData.Values["controller"]);
            Assert.AreEqual("ajaxwatch", routeData.Values["action"]);

            //Assert.AreEqual("", routeData.Values["id"]);
        }

        [Test]
        public void TestAjaxPredictRoute()
        {
            var path = "~/private/trainings/ajaxpredict";
            RouteData routeData = Helpers.GetPrivateAreaRouteData(path);

            Assert.IsNotNull(routeData, "Should have found the route");
            Assert.AreEqual("Private", routeData.DataTokens["area"]);
            Assert.AreEqual("trainings", routeData.Values["controller"]);
            Assert.AreEqual("ajaxpredict", routeData.Values["action"]);

            //Assert.AreEqual("", routeData.Values["id"]);
        }

        [Test]
        public void TestAjaxLoadNextTrainingsRoute()
        {
            var path = "~/private/trainings/AjaxLoadNextTrainings";
            RouteData routeData = Helpers.GetPrivateAreaRouteData(path);

            Assert.IsNotNull(routeData, "Should have found the route");
            Assert.AreEqual("Private", routeData.DataTokens["area"]);
            Assert.AreEqual("trainings", routeData.Values["controller"]);
            Assert.AreEqual("AjaxLoadNextTrainings", routeData.Values["action"]);

            //Assert.AreEqual("", routeData.Values["id"]);
        }
    }
}