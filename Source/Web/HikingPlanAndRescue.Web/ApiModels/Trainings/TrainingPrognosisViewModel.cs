namespace HikingPlanAndRescue.Web.ApiModels.Trainings
{
    using System;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TrainingPrognosisViewModel : IMap<Training>
    {
        public DateTime EndDate { get; set; }

        public double Water { get; set; }

        public double Calories { get; set; }
    }
}