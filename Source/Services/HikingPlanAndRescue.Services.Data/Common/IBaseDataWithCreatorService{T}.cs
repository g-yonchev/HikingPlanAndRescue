namespace HikingPlanAndRescue.Services.Data.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HikingPlanAndRescue.Data.Common.Models;

    public interface IBaseDataWithCreatorService<T> : IBaseDataService<T>
        where T : class, IDeletableEntity, IAuditInfo, IEntitiyWithCreator
    {
        void Delete(object id, string userId, bool isAdmin);

        IQueryable<T> GetAllByUserWithPaging(string userId, int page = 0, int pageSize = 10);

        IQueryable<T> GetAllByUser(string userId);
    }
}
