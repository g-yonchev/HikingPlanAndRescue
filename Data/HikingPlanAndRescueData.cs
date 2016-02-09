namespace HikingPlanAndRescue.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Contracts;
    using Models;

    public class HikingPlanAndRescueData : IHikingPlanAndRescueData
    {
        private readonly DbContext context;
        private readonly IDictionary<Type, object> repositories;

        // TODO: IoC injection
        public HikingPlanAndRescueData()
        {
            this.context = new HikingPlanAndRescueDbContext();
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users => this.GetRepository<User>();

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);

            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EfRepository<T>), this.context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}