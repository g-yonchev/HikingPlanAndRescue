namespace HikingPlanAndRescue.Services.Data
{
    using System.Linq;
    using HikingPlanAndRescue.Data.Common;
    using HikingPlanAndRescue.Data.Common.Models;
    using HikingPlanAndRescue.Web.Infrastructure.CustomExceptions;

    public abstract class BaseDataService<T> : IBaseDataService<T>
        where T : class, IDeletableEntity, IAuditInfo
    {
        protected readonly IDbRepository<T> data;

        public BaseDataService(IDbRepository<T> dataSet)
        {
            this.data = dataSet;
        }

        public virtual void Add(T training)
        {
            this.data.Add(training);
            this.data.Save();
        }

        public virtual void Delete(object id)
        {
            var entity = this.data.GetById(id);
            if (entity == null)
            {
                throw new CustomServiceOperationException("No such training found.");
            }

            this.data.Delete(entity);
            this.data.Save();
        }

        public virtual IQueryable<T> GetAll()
        {
            return this.data.All().OrderBy(x => x.CreatedOn);
        }

        public virtual T GetById(object id)
        {
            return this.data.GetById(id);
        }

        public virtual void Update()
        {
            this.data.Save();
        }
    }
}