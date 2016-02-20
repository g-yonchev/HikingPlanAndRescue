namespace HikingPlanAndRescue.Web.ApiModels.Tracks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TrackViewModel : IMap<Track>
    {
        public string Title { get; set; }

        public double Length { get; set; }

        public double Ascent { get; set; }

        public double AscentLength { get; set; }
    }
}
