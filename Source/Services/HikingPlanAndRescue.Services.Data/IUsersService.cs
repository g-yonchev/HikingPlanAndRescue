using System.Linq;
using HikingPlanAndRescue.Data.Models;

namespace HikingPlanAndRescue.Services.Data
{
    public interface IUsersService
    {
        IQueryable<ApplicationUser> GetAll();
    }
}