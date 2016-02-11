namespace HikingPlanAndRescue.Services.Data
{
    using System.Linq;

    using HikingPlanAndRescue.Data.Models;

    public interface IJokesService
    {
        IQueryable<Joke> GetRandomJokes(int count);

        Joke GetById(string id);
    }
}
