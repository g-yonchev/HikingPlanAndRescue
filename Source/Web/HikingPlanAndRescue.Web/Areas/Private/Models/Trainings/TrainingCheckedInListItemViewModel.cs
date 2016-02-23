namespace HikingPlanAndRescue.Web.Areas.Private.Models
{
    using System;
    using Data.Models;
    using Infrastructure.Mapping;
    using Web.ViewModels.Tracks;
    using Web.ViewModels.Users;

    public class TrainingCheckedInListItemViewModel : IMap<Training>
    {
        public int Id { get; set; }

        public UserPublicViewModel User { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime? CheckedInOn { get; set; }

        public TrackPublicViewModel Track { get; set; }

        public double HoursLate
        {
            get
            {
                return (DateTime.Now - this.EndDate).TotalHours;
            }
        }
    }
}