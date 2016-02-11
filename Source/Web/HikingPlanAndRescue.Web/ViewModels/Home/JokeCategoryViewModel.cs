namespace HikingPlanAndRescue.Web.ViewModels.Home
{
    using HikingPlanAndRescue.Data.Models;
    using HikingPlanAndRescue.Web.Infrastructure.Mapping;

    public class JokeCategoryViewModel : IMapFrom<JokeCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
