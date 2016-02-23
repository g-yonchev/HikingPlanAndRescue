namespace HikingPlanAndRescue.Services.Data
{
    using System.Linq;
    using Common;
    using Contracts;
    using HikingPlanAndRescue.Data.Common;
    using HikingPlanAndRescue.Data.Models;
    using HikingPlanAndRescue.Web.Infrastructure.CustomExceptions;

    public class ResolutionsService : BaseDataWithCreatorService<Resolution>, IResolutionsService
    {
        public ResolutionsService(IDbRepository<Resolution> dataSet, IDbRepository<ApplicationUser> users)
            : base(dataSet, users)
        {
        }

        public void AddResolution(Resolution resolution)
        {
            var isDuplicateForSameTraining = this.Data.All().Any(x => x.UserId == resolution.UserId && x.TrainingId == resolution.TrainingId);
            if (isDuplicateForSameTraining)
            {
                throw new CustomServiceOperationException("Cannot add two resolutions for same training");
            }

            base.Add(resolution);
        }
    }
}