namespace HikingPlanAndRescue.Web.Areas.Private.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TrainingListItemViewModel : IMap<Training>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string TrackTitle { get; set; }

        public double TrackLength { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Training, TrainingListItemViewModel>()
                .ForMember(x => x.TrackTitle, opt => opt.MapFrom(x => x.Track.Title));

            configuration.CreateMap<Training, TrainingListItemViewModel>()
                .ForMember(x => x.TrackLength, opt => opt.MapFrom(x => x.Track.Length));
        }
    }
}
