namespace HikingPlanAndRescue.Web.Areas.Admin.Models.Tracks
{
    using System;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TrackListItemViewModel : IMap<Track>
    {
        public string Title { get; set; }

        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        //[HiddenInput(DisplayValue = false)]
        //public string UserId { get; set; }

        //public virtual UserViewModel User { get; set; }

        public double Length { get; set; }

        public double Ascent { get; set; }

        public double AscentLength { get; set; }

        //public virtual ICollection<TrainingViewModel> Trainings { get; set; }
    }
}