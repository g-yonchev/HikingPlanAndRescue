namespace HikingPlanAndRescue.Services.Data.Common
{
    using System.Linq;
    using HikingPlanAndRescue.Data.Common.Models;

    public interface IBaseDataService<T>
        where T : class, IDeletableEntity, IAuditInfo
    {
        void Add(T training);

        void Delete(object id);

        IQueryable<T> GetAll();

        IQueryable<T> GetAllWithPaging(int page = 0, int pageSize = 10);

        T GetById(object id);

        void Save();

        void Dispose();
    }
}