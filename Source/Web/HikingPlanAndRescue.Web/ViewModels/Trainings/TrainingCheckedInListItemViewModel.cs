namespace HikingPlanAndRescue.Web.ViewModels.Trainings
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;
    using Tracks;
    using Users;

    public class TrainingCheckedInListItemViewModel : IMap<Training>
    {
        public int Id { get; set; }

        public UserPublicViewModel User { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime? CheckedInOn { get; set; }

        public TrackPublicViewModel Track { get; set; }
    }
}