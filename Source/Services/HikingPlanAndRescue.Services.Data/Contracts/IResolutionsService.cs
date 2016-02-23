namespace HikingPlanAndRescue.Services.Data.Contracts
{
    using HikingPlanAndRescue.Data.Models;
    using HikingPlanAndRescue.Services.Data.Common;

    public interface IResolutionsService : IBaseDataWithCreatorService<Resolution>
    {
        void AddResolution(Resolution resolution);
    }
}