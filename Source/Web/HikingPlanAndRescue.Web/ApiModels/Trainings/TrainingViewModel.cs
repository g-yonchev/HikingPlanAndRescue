namespace HikingPlanAndRescue.Web.ApiModels.Trainings
{
    using System;
    using Data.Models;
    using Infrastructure.Mapping;
    using Tracks;

    public class TrainingViewModel : IMap<Training>
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime? PredictedEndDate { get; set; }

        public string Title { get; set; }

        public double Water { get; set; }

        public double Calories { get; set; }

        public virtual TrackViewModel Track { get; set; }
    }
}