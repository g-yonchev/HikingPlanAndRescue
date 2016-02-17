namespace HikingPlanAndRescue.Web.Areas.Admin.Models.Tracks
{
    using System;
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TrackEditViewModel : IMap<Track>
    {
        public string Title { get; set; }

        [HiddenInput(DisplayValue =true)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = true)]
        public DateTime CreatedOn { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }

        //public virtual UserViewModel User { get; set; }

        public double Length { get; set; }

        public double Ascent { get; set; }

        public double AscentLength { get; set; }

        //public virtual ICollection<TrainingViewModel> Trainings { get; set; }
    }
}