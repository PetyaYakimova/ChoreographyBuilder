using AutoMapper;
using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.Position;
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

			CreateMap<VerseType, VerseTypeFormViewModel>();
			CreateMap<VerseTypeFormViewModel, VerseType>();

			//Position models
			CreateMap<Position, PositionTableViewModel>()
                .ForMember(d => d.HasFigures, act => act.MapFrom(src => src.FiguresWithStartPosition.Any() || src.FiguresWithEndPosition.Any()));

            CreateMap<Position, PositionForFigureViewModel>();

            CreateMap<Position, PositionFormViewModel>();
            CreateMap<PositionFormViewModel, Position>();

			//Figure models
			CreateMap<Figure, FigureViewModel>()
                .ForMember(d => d.FigureOptionsCount, act => act.MapFrom(src => src.FigureOptions.Count()));
        }
    }
}
