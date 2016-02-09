namespace HikingPlanAndRescue.Data.Contracts
{
    using Models;
    using Repositories;

    public interface IHikingPlanAndRescueData
    {
        IRepository<User> Users { get; }

        int SaveChanges();
    }
}