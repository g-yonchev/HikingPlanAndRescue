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
        public int Id { get; set; }

        public DateTime? CheckedInOn { get; set; }

        public DateTime? CheckedOutOn { get; set; }

        public string Title { get; set; }

        public string TrackTitle { get; set; }

        public double TrackLength { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Training, TrainingListItemViewModel>()
                .ForMember(x => x.TrackTitle, opt => opt.MapFrom(x => x.Track.Title));

            configuration.CreateMap<Training, TrainingListItemViewModel>()
                .ForMember(x => x.TrackLength, opt => opt.MapFrom(x => x.Track.Length));
        }
    }
}
