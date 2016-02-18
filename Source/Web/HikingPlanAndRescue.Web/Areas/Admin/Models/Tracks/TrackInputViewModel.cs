namespace HikingPlanAndRescue.Web.Areas.Admin.Models.Tracks
{
    using System.Web.Mvc;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TrackInputViewModel : IMap<Track>
    {
        public string Title { get; set; }

        public double Length { get; set; }

        public double Ascent { get; set; }

        public double AscentLength { get; set; }
    }
}