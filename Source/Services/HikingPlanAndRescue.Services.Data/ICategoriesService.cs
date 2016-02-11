namespace HikingPlanAndRescue.Services.Data
{
    using System.Linq;

    using HikingPlanAndRescue.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<JokeCategory> GetAll();

        JokeCategory EnsureCategory(string name);
    }
}
