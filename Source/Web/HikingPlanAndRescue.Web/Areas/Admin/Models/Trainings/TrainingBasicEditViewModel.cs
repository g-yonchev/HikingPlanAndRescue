namespace HikingPlanAndRescue.Web.Areas.Admin.Models.Trainings
{
    using System;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TrainingBasicEditViewModel : IMap<Training>
    {
        public int Id { get; set; }

        public DateTime? CheckedInOn { get; set; }

        public DateTime? CheckedOutOn { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime? PredictedEndDate { get; set; }

        public string Title { get; set; }

        public double Water { get; set; }

        public double Calories { get; set; }

        public string UserId { get; set; }

        public int TrackId { get; set; }

        //public virtual Track Track { get; set; }
    }
}