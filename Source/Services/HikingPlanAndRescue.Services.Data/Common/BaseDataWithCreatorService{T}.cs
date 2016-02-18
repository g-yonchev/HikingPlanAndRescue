namespace HikingPlanAndRescue.Services.Data.Common
{
    using System.Linq;
    using HikingPlanAndRescue.Data.Common;
    using HikingPlanAndRescue.Data.Common.Models;
    using HikingPlanAndRescue.Data.Models;
    using HikingPlanAndRescue.Web.Infrastructure.CustomExceptions;

    public class BaseDataWithCreatorService<T> : BaseDataService<T>
        where T : class, IDeletableEntity, IAuditInfo, IEntitiyWithCreator
    {
        public BaseDataWithCreatorService(IDbRepository<T> dataSet, IDbRepository<ApplicationUser> users)
            : base(dataSet)
        {
            this.Users = users;
        }

        protected IDbRepository<ApplicationUser> Users { get; set; }

        public IQueryable<T> GetAllByUserWithPaging(string userId, int page = 0, int pageSize = 10)
        {
            return this.Data
                .All()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Skip(page * pageSize)
                .Take(pageSize);
        }

        public IQueryable<T> GetAllByUser(string userId)
        {
            return this.Data
                .All()
                .Where(x => x.UserId == userId);
        }

        public void Delete(object id, string userId, bool isAdmin)
        {
            var user = this.Users.GetById(userId);
            var training = this.Data.GetById(id);
            if (training == null)
            {
                throw new CustomServiceOperationException("No such training found.");
            }

            if (training.UserId != userId && !isAdmin)
            {
                throw new CustomServiceOperationException("Cannot delete trainings you do not own.");
            }

            this.Data.Delete(training);
            this.Data.Save();
        }
    }
}