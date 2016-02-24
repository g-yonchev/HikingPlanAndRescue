namespace HikingPlanAndRescue.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HikingPlanAndRescue.Data.Common;
    using HikingPlanAndRescue.Data.Models;
    using HikingPlanAndRescue.Web.Infrastructure.CustomExceptions;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class TrainingsServiceTests
    {
        [Test]
        public void GetCheckedInTrainingsShouldWork()
        {
            var trainingsRepo = new Mock<IDbRepository<Training>>();
            trainingsRepo.Setup(
                x =>
                x.All())
                .Returns(new List<Training>()
                {
                    new Training()
                    {
                        CheckedInOn = DateTime.Now,
                        CheckedOutOn = null
                    },
                    new Training()
                    {
                    }
                }.AsQueryable());

            var usersRepo = new Mock<IDbRepository<ApplicationUser>>();

            var trainings = new TrainingsService(trainingsRepo.Object, usersRepo.Object);
            var result = trainings.GetCheckedIn(0, 1);

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void GetCheckedInTrainingsPagingShouldWork()
        {
            var trainingsRepo = new Mock<IDbRepository<Training>>();
            trainingsRepo.Setup(
                x =>
                x.All())
                .Returns(new List<Training>()
                {
                    new Training()
                    {
                        CheckedInOn = DateTime.Now,
                        CheckedOutOn = null
                    },
                    new Training()
                    {
                        CheckedInOn = DateTime.Now,
                        CheckedOutOn = null
                    },
                    new Training()
                    {
                    }
                }.AsQueryable());

            var usersRepo = new Mock<IDbRepository<ApplicationUser>>();

            var trainings = new TrainingsService(trainingsRepo.Object, usersRepo.Object);
            var result = trainings.GetCheckedIn(0, 1);

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void GetAllByUserWithPagingAndFilteringWithoutDateRangeShouldWork()
        {
            string userId = "userId";
            string wrongUserId = "wrongUserId";
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddDays(5);

            var trainingsRepo = new Mock<IDbRepository<Training>>();
            trainingsRepo.Setup(
                x =>
                x.All())
                .Returns(new List<Training>()
                {
                    new Training()
                    {
                        UserId = userId,
                        StartDate = startDate,
                        EndDate = endDate
                    },
                    new Training()
                    {
                        UserId = wrongUserId,
                        StartDate = startDate,
                        EndDate = endDate
                    }
                }.AsQueryable());

            var usersRepo = new Mock<IDbRepository<ApplicationUser>>();

            var trainings = new TrainingsService(trainingsRepo.Object, usersRepo.Object);
            var result = trainings.GetAllByUserWithPagingAndFiltering(userId , 0, 2, null, null);

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void GetAllByUserWithPagingAndFilteringWithDateRangeShouldWork()
        {
            string userId = "userId";
            string wrongUserId = "wrongUserId";
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddDays(5);
            DateTime wrongEndDate = startDate.AddDays(10);

            var trainingsRepo = new Mock<IDbRepository<Training>>();
            trainingsRepo.Setup(
                x =>
                x.All())
                .Returns(new List<Training>()
                {
                    new Training()
                    {
                        UserId = userId,
                        StartDate = startDate,
                        EndDate = endDate
                    },
                    new Training()
                    {
                        UserId = userId,
                        StartDate = startDate,
                        EndDate = wrongEndDate
                    }
                }.AsQueryable());

            var usersRepo = new Mock<IDbRepository<ApplicationUser>>();

            var trainings = new TrainingsService(trainingsRepo.Object, usersRepo.Object);
            var result = trainings.GetAllByUserWithPagingAndFiltering(userId, 0, 2, startDate, endDate);

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void UpdateCheckInOutWithWrongTrainingIdShouldReturnNull()
        {
            int trainingId = 1;

            var trainingsRepo = new Mock<IDbRepository<Training>>();
            trainingsRepo.Setup(
                x =>
                x.All())
                .Returns(new List<Training>()
                {
                    new Training()
                    {
                        Id = trainingId
                    },
                    new Training()
                    {
                        Id = trainingId + 1
                    }
                }.AsQueryable());

            var usersRepo = new Mock<IDbRepository<ApplicationUser>>();

            var trainings = new TrainingsService(trainingsRepo.Object, usersRepo.Object);
            var result = trainings.UpdateCheckInOut(2, "fake", "fake");

            Assert.AreEqual(null, result);
        }

        [Test]
        public void UpdateCheckInOutWithWrongUserIdShouldReturnNull()
        {
            int trainingId = 1;
            string userId = "user1";

            var trainingsRepo = new Mock<IDbRepository<Training>>();
            trainingsRepo.Setup(
                x =>
                x.All())
                .Returns(new List<Training>()
                {
                    new Training()
                    {
                        Id = trainingId,
                        UserId = userId
                    },
                    new Training()
                    {
                        Id = trainingId + 1,
                        UserId = userId
                    }
                }.AsQueryable());

            var usersRepo = new Mock<IDbRepository<ApplicationUser>>();

            var trainings = new TrainingsService(trainingsRepo.Object, usersRepo.Object);
            var result = trainings.UpdateCheckInOut(2, "fake", "fake");

            Assert.AreEqual(null, result);
        }

        [Test]
        public void UpdateCheckInOutWithCheckInCommandAndLegalRequestShouldWork()
        {
            int trainingId = 1;
            string userId = "user1";
            string command = "checkin";

            var trainingsRepo = new Mock<IDbRepository<Training>>();
            trainingsRepo.Setup(
                x =>
                x.All())
                .Returns(new List<Training>()
                {
                    new Training()
                    {
                        Id = trainingId,
                        UserId = userId
                    },
                    new Training()
                    {
                        Id = trainingId + 1,
                        UserId = userId
                    }
                }.AsQueryable());

            var usersRepo = new Mock<IDbRepository<ApplicationUser>>();

            var trainings = new TrainingsService(trainingsRepo.Object, usersRepo.Object);
            var result = trainings.UpdateCheckInOut(trainingId, command, userId);

            Assert.AreNotEqual(null, result.CheckedInOn);
        }

        [Test]
        [ExpectedException(typeof(CustomServiceOperationException))]
        public void UpdateCheckInOutWithCheckInCommandAndIllegalRequestShouldThrow()
        {
            int trainingId = 1;
            string userId = "user1";
            string command = "checkin";

            var trainingsRepo = new Mock<IDbRepository<Training>>();
            trainingsRepo.Setup(
                x =>
                x.All())
                .Returns(new List<Training>()
                {
                    new Training()
                    {
                        Id = trainingId,
                        UserId = userId,
                        CheckedInOn = DateTime.Now
                    },
                    new Training()
                    {
                        Id = trainingId + 1,
                        UserId = userId
                    }
                }.AsQueryable());

            var usersRepo = new Mock<IDbRepository<ApplicationUser>>();

            var trainings = new TrainingsService(trainingsRepo.Object, usersRepo.Object);
            var result = trainings.UpdateCheckInOut(trainingId, command, userId);
        }
    }
}
