namespace HikingPlanAndRescue.Services.Data
{
    using System;
    using System.Linq;
    using Common;
    using Contracts;
    using HikingPlanAndRescue.Data.Common;
    using HikingPlanAndRescue.Data.Models;
    using HikingPlanAndRescue.Web.Infrastructure.CustomExceptions;

    public class TrainingsService : BaseDataWithCreatorService<Training>, ITrainingsService
    {
        public TrainingsService(IDbRepository<Training> data, IDbRepository<ApplicationUser> users)
            : base(data, users)
        {
        }

        public IQueryable<Training> GetCheckedIn(int page, int pageSize)
        {
            return this.Data
                .All()
                .Where(x => x.CheckedInOn != null && x.CheckedOutOn == null)
                .OrderBy(x => x.EndDate)
                .Skip(page * pageSize)
                .Take(pageSize);
        }

        public Training UpdateCheckInOut(int trainingId, string command, string userId)
        {
            var training = this.Data.All().FirstOrDefault(x => x.Id == trainingId);
            if (training == null)
            {
                return null;
            }
            else if (training.UserId != userId)
            {
                return null;
            }
            else if (command == "checkin")
            {
                if (this.Data.All()
                    .Any(
                    x => x.CheckedInOn != null
                    && x.CheckedOutOn == null
                    && x.UserId == userId))
                {
                    throw new CustomServiceOperationException("Cannot Check In more than one training at a time.");
                }

                training.CheckedInOn = DateTime.Now;
            }
            else if (command == "checkout")
            {
                training.CheckedOutOn = DateTime.Now;
                training.PredictedEndDate = training.EndDate;
                training.EndDate = DateTime.Now;
            }
            else if (command == "reset")
            {
                training.CheckedInOn = null;
                training.CheckedOutOn = null;
                training.EndDate = training.PredictedEndDate.Value;
            }

            this.Data.Save();
            return training;
        }

        public IQueryable<Training> GetAllByUserWithPagingAndFiltering(string userId, int page, int pageSize, DateTime? fromDate, DateTime? toDate)
        {
            return this.Data
                .All()
                .Where(
                    x => x.UserId == userId
                        && (fromDate == null || x.StartDate >= fromDate)
                        && (toDate == null || x.EndDate <= toDate))
                .OrderByDescending(x => x.EndDate)
                .Skip(page * pageSize)
                .Take(pageSize);
        }

    }
}