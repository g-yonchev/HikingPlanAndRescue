namespace HikingPlanAndRescue.Data.Models
{
    using System;
    using System.Collections.Generic;
    using HikingPlanAndRescue.Data.Common.Models;
    using Models;

    public class Training : BaseModel<int>
    {
        public Training()
        {
        }

        public DateTime? CheckedInOn { get; set; }

        public DateTime? CheckedOutOn { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Title { get; set; }

        public double Water { get; set; }

        public double Calories { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int TrackId { get; set; }

        public virtual Track Track { get; set; }

        // weather
        // equioment
    }
}