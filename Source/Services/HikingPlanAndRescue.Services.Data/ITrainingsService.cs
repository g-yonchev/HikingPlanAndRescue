namespace HikingPlanAndRescue.Services.Data
{
    using System.Linq;
    using HikingPlanAndRescue.Data.Models;

    public interface ITrainingsService
    {
        IQueryable<Training> GetByUser(string userId, int page = 0, int pageSize = 10);

        void AddTraining(Training training);

        Training UpdateWatch(int trainingId, string watch, string userId);

        Training GetById(int id);

        void Update();

        void Delete(int id, string userId);

        IQueryable<Training> GetCheckedIn(int page, int pageSize);
        IQueryable<Training> GetAll();
    }
}