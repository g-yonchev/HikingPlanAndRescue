namespace HikingPlanAndRescue.Services.Data
{
    using System.Linq;
    using HikingPlanAndRescue.Data.Common.Models;

    public interface IBaseDataService<T> 
        where T : class, IDeletableEntity, IAuditInfo
    {
        void Add(T training);

        void Delete(object id);

        IQueryable<T> GetAll();

        T GetById(object id);

        void Update();
    }
}