namespace HikingPlanAndRescue.Web.Controllers.Tests
{
    using Moq;

    using HikingPlanAndRescue.Data.Models;
    using HikingPlanAndRescue.Services.Data;
    using HikingPlanAndRescue.Web.Infrastructure.Mapping;
    using HikingPlanAndRescue.Web.ViewModels.Home;

    using NUnit.Framework;

    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class JokesControllerTests
    {
        [Test]
        public void ByIdShouldWorkCorrectly()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(JokesController).Assembly);
            const string JokeContent = "SomeContent";
            var jokesServiceMock = new Mock<IJokesService>();
            jokesServiceMock.Setup(x => x.GetById(It.IsAny<string>()))
                .Returns(new Joke { Content = JokeContent, Category = new JokeCategory { Name = "asda" } });
            var controller = new JokesController(jokesServiceMock.Object);
            controller.WithCallTo(x => x.ById("asdasasd"))
                .ShouldRenderView("ById")
                .WithModel<JokeViewModel>(
                    viewModel =>
                        {
                            Assert.AreEqual(JokeContent, viewModel.Content);
                        }).AndNoModelErrors();
        }
    }
}
