namespace HikingPlanAndRescue.Web.Areas.Private.Models.Trainings
{
    using System;
    using System.Collections.Generic;

    public class TrainingsIndexViewModel
    {
        public IEnumerable<TrainingListItemViewModel> Trainings { get; set; }

        public TrainingsStatsViewModel Stats { get; set; }
    }
}