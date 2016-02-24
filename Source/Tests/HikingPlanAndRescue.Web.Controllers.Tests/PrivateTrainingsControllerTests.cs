namespace HikingPlanAndRescue.Web.Controllers.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading;
    using System.Web.Mvc;
    using Areas.Private.Controllers;
    using Areas.Private.Models;
    using Areas.Private.Models.Trainings;
    using Data.Models;
    using HikingPlanAndRescue.Web.Infrastructure.Mapping;
    using Moq;
    using NUnit.Framework;
    using Services.Data.Contracts;
    using Services.Predictions;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class PrivateTrainingsControllerTests
    {
        [Test]
        public void GetAllByUserWithPageingAndFilteringShouldWorkCorrectly()
        {
            var caloriesBurned = 1500;

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(TrainingsController).Assembly);

            var trainingsServiceMock = new Mock<ITrainingsService>();

            trainingsServiceMock.Setup(
                x =>
                x.GetAllByUserWithPagingAndFiltering(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>()))
                .Returns(new List<Training>()
                {
                    new Training()
                    {
                        Calories = caloriesBurned,
                        Track = new Track()
                        {
                            Ascent = 2500,
                            Length = 50
                        }
                    }
                }.AsQueryable());

            var predictionsServiceMock = new Mock<ITrainingPrediction>();
            //predictionsServiceMock.Setup()

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("anon");
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = new TrainingsController(trainingsServiceMock.Object, predictionsServiceMock.Object);
            controller.ControllerContext = mock.Object;
            controller.WithCallTo(x => x.Index(null, null))
                .ShouldRenderView("Index")
                .WithModel<TrainingsIndexViewModel>(
                    viewModel =>
                        {
                            Assert.AreEqual(caloriesBurned, viewModel.Stats.TotalCalories);
                        }).AndNoModelErrors();
        }

        [Test]
        public void AjaxLoadNextTrainingsShouldWorkCorrectly()
        {
            var caloriesBurned = 1500;

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(TrainingsController).Assembly);

            var trainingsServiceMock = new Mock<ITrainingsService>();

            trainingsServiceMock.Setup(
                x =>
                x.GetAllByUserWithPagingAndFiltering(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>()))
                .Returns(new List<Training>()
                {
                    new Training()
                    {
                        Calories = caloriesBurned,
                        Track = new Track()
                        {
                            Ascent = 2500,
                            Length = 50
                        }
                    }
                }.AsQueryable());

            var predictionsServiceMock = new Mock<ITrainingPrediction>();
            //predictionsServiceMock.Setup()

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("anon");
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = new TrainingsController(trainingsServiceMock.Object, predictionsServiceMock.Object);
            controller.ControllerContext = mock.Object;
            controller.WithCallTo(x => x.AjaxLoadNextTrainings(null, null, 0, 15))
                .ShouldRenderPartialView("_TrainingsList")
                .WithModel<List<TrainingListItemViewModel>>(
                    viewModel =>
                    {
                        Assert.AreEqual(1, viewModel.Count);
                    }).AndNoModelErrors();
        }

        [Test]
        public void CreateShouldRedirectToHomeWhenValidModelCorrectly()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(TrainingsController).Assembly);

            var trainingsServiceMock = new Mock<ITrainingsService>();

            var predictionsServiceMock = new Mock<ITrainingPrediction>();

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("anon");
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = new TrainingsController(trainingsServiceMock.Object, predictionsServiceMock.Object);
            controller.ControllerContext = mock.Object;
            controller.WithCallTo(x => x.Create(new TrainingCreateViewModel()
            {
                Calories = 1500,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                Title = "Test",
                Water = 3,
                Track = new TrackCreateViewModel()
                {
                    Ascent = 3000,
                    AscentLength = 25,
                    Length = 80,
                    Title = "test track 1"
                },
            }))
                .ShouldRedirectToRoute("");
        }

        [Test]
        public void CreateShouldRedirectToCreateWhenInvalidModel()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(TrainingsController).Assembly);

            var trainingsServiceMock = new Mock<ITrainingsService>();

            var predictionsServiceMock = new Mock<ITrainingPrediction>();

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("anon");
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = new TrainingsController(trainingsServiceMock.Object, predictionsServiceMock.Object);
            controller.ControllerContext = mock.Object;
            controller.ModelState.AddModelError("FirstName", "First Name is Required");
            controller.WithCallTo(x => x.Create(new TrainingCreateViewModel()))
                .ShouldRenderView("Create");
        }

        [Test]
        public void AjaxPredictShouldWorkCorrectly()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(TrainingsController).Assembly);

            var trainingsServiceMock = new Mock<ITrainingsService>();

            var resultDate = DateTime.Now;

            var predictionsServiceMock = new Mock<ITrainingPrediction>();
            predictionsServiceMock.Setup(
                x =>
                x.Predict(It.IsAny<Training>()))
                .Returns(new Training()
                {
                    EndDate = resultDate
                });

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("anon");
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = new TrainingsController(trainingsServiceMock.Object, predictionsServiceMock.Object);
            controller.ControllerContext = mock.Object;
            controller.WithCallTo(x => x.AjaxPredict(new TrainingAjaxPredictViewModel()))
                .ShouldReturnJson(
                x =>
                {
                    var content = x as TrainingAjaxPredictViewModel;
                    Assert.AreEqual(content.EndDate, resultDate);
                });
        }

        [Test]
        public void EditShouldWorkCorrectly()
        {
            var caloriesBurned = 1500;

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(TrainingsController).Assembly);

            var trainingsServiceMock = new Mock<ITrainingsService>();

            trainingsServiceMock.Setup(
                x =>
                x.GetById(It.IsAny<object>()))
                .Returns(
                    new Training()
                    {
                        Calories = caloriesBurned,
                        Track = new Track()
                        {
                            Ascent = 2500,
                            Length = 50
                        }
                    }
                );

            var predictionsServiceMock = new Mock<ITrainingPrediction>();
            //predictionsServiceMock.Setup()

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("anon");
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = new TrainingsController(trainingsServiceMock.Object, predictionsServiceMock.Object);
            controller.ControllerContext = mock.Object;
            controller.WithCallTo(x => x.Edit(It.IsAny<int>()))
                .ShouldRenderView("Edit")
                .WithModel<TrainingEditViewModel>(
                    viewModel =>
                    {
                        Assert.AreEqual(caloriesBurned, viewModel.Calories);
                    }).AndNoModelErrors();
        }

        [Test]
        public void EditShouldWorkCorrectlyWithInvalidModelState()
        {
            var caloriesBurned = 1500;

            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(TrainingsController).Assembly);

            var trainingsServiceMock = new Mock<ITrainingsService>();

            var training = new TrainingEditViewModel()
            {
                Calories = caloriesBurned,
            };

            var predictionsServiceMock = new Mock<ITrainingPrediction>();
            //predictionsServiceMock.Setup()

            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("anon");
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = new TrainingsController(trainingsServiceMock.Object, predictionsServiceMock.Object);
            controller.ControllerContext = mock.Object;
            controller.ModelState.AddModelError("FirstName", "First Name is Required");
            controller.WithCallTo(x => x.Edit(training))
                .ShouldRenderView("Edit")
                .WithModel<TrainingEditViewModel>(
                    viewModel =>
                    {
                        Assert.AreEqual(caloriesBurned, viewModel.Calories);
                    });
        }
    }
}