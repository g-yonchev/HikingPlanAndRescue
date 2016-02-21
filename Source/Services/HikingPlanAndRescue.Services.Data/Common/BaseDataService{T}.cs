namespace HikingPlanAndRescue.Services.Data
{
    using System.Linq;
    using Common;
    using HikingPlanAndRescue.Data.Common;
    using HikingPlanAndRescue.Data.Common.Models;
    using HikingPlanAndRescue.Web.Infrastructure.CustomExceptions;

    public abstract class BaseDataService<T> : IBaseDataService<T>
        where T : class, IDeletableEntity, IAuditInfo
    {
        public BaseDataService(IDbRepository<T> dataSet)
        {
            this.Data = dataSet;
        }

        protected IDbRepository<T> Data { get; set; }

        public virtual void Add(T item)
        {
            this.Data.Add(item);
            this.Data.Save();
        }

        public virtual void Delete(object id)
        {
            var entity = this.Data.GetById(id);
            if (entity == null)
            {
                throw new CustomServiceOperationException("No object with provided id found.");
            }

            this.Data.Delete(entity);
            this.Data.Save();
        }

        public virtual IQueryable<T> GetAll()
        {
            return this.Data.All();
        }

        public virtual IQueryable<T> GetAllWithPaging(int page = 0, int pageSize = 10)
        {
            return this.Data
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .Skip(page * pageSize)
                .Take(pageSize);
        }

        public virtual T GetById(object id)
        {
            return this.Data.GetById(id);
        }

        public virtual void Save()
        {
            this.Data.Save();
        }

        public void Dispose()
        {
            this.Data.Dispose();
        }
    }
}