namespace HikingPlanAndRescue.Web.Areas.Admin.Models.Tracks
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TrackListItemViewModel : IMap<Track>
    {
        public string Title { get; set; }

        public int Id { get; set; }

        public double Length { get; set; }

        public double Ascent { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public double AscentLength { get; set; }
    }
}