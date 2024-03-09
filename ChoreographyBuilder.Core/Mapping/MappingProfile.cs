using AutoMapper;
using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.VerseType;
using ChoreographyBuilder.Infrastructure.Data.Models;

namespace ChoreographyBuilder.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Verse type models
            CreateMap<VerseType, VerseTypeTableViewModel>()
                .ForMember(d => d.HasChoreographies, act => act.MapFrom(src => src.VerseChoreographies.Any()));

            CreateMap<VerseType, VerseTypeForChoreographiesViewModel>()
                .ForMember(d => d.Name, act => act.MapFrom(src => $"{src.Name} ({src.BeatCounts})"));

            CreateMap<VerseTypeFormViewModel, VerseType>();
			CreateMap<VerseType, VerseTypeFormViewModel>();

			//Figure models
			CreateMap<Figure, FigureViewModel>()
                .ForMember(d => d.FigureOptionsCount, act => act.MapFrom(src => src.FigureOptions.Count()));
        }
    }
}
