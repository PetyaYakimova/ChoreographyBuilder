using AutoMapper;
using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.FigureOption;
using ChoreographyBuilder.Core.Models.FullChoreography;
using ChoreographyBuilder.Core.Models.FullChoreographyVerseChoreography;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Core.Models.VerseChoreography;
using ChoreographyBuilder.Core.Models.VerseChoreographyFigure;
using ChoreographyBuilder.Core.Models.VerseType;
using ChoreographyBuilder.Infrastructure.Data.Models;

namespace ChoreographyBuilder.Core.Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Verse type models
        CreateMap<VerseType, VerseTypeTableViewModel>()
            .ForMember(d => d.HasChoreographies, act => act.MapFrom(src => src.VerseChoreographies.Any()));

        CreateMap<VerseType, VerseTypeForPreviewViewModel>()
            .ForMember(d => d.Name, act => act.MapFrom(src => $"{src.Name} ({src.BeatCounts})"));

        CreateMap<VerseType, VerseTypeFormViewModel>();
        CreateMap<VerseTypeFormViewModel, VerseType>();

        //Position models
        CreateMap<Position, PositionTableViewModel>()
            .ForMember(d => d.HasFigures, act => act.MapFrom(src => src.FiguresWithStartPosition.Any() || src.FiguresWithEndPosition.Any()));

        CreateMap<Position, PositionForPreviewViewModel>();

        CreateMap<Position, PositionFormViewModel>();
        CreateMap<PositionFormViewModel, Position>();

        //Figure models
        CreateMap<Figure, FigureTableViewModel>()
            .ForMember(d => d.FigureOptionsCount, act => act.MapFrom(src => src.FigureOptions.Count()))
            .ForMember(d => d.FigureUsedInChoreographies, act => act.MapFrom(src => src.FigureOptions.Any(fo => fo.VerseChoreographyFigures.Any())))
            .ForMember(d => d.UserEmailAddress, act => act.MapFrom(src => src.User.Email));

        CreateMap<Figure, FigureFormViewModel>();
        CreateMap<FigureFormViewModel, Figure>();

        CreateMap<Figure, FigureForPreviewViewModel>();

        CreateMap<Figure, FigureForCopyViewModel>()
            .ForMember(d => d.FigureOptionsCount, act => act.MapFrom(src => src.FigureOptions.Count()))
            .ForMember(d => d.UserEmailAddress, act => act.MapFrom(src => src.User.Email));

        //Figure option models
        CreateMap<FigureOption, FigureOptionTableViewModel>()
            .ForMember(d => d.StartPositionName, act => act.MapFrom(src => src.StartPosition.Name))
            .ForMember(d => d.EndPositionName, act => act.MapFrom(src => src.EndPosition.Name))
            .ForMember(d => d.DynamicsTypeName, act => act.MapFrom(src => src.DynamicsType.ToString()))
            .ForMember(d => d.UsedInChoreographies, act => act.MapFrom(src => src.VerseChoreographyFigures.Any()));

        CreateMap<FigureOption, FigureOptionWithFigureViewModel>()
            .ForMember(d => d.StartPositionName, act => act.MapFrom(src => src.StartPosition.Name))
            .ForMember(d => d.EndPositionName, act => act.MapFrom(src => src.EndPosition.Name))
            .ForMember(d => d.DynamicsTypeName, act => act.MapFrom(src => src.DynamicsType.ToString()))
            .ForMember(d => d.UsedInChoreographies, act => act.MapFrom(src => src.VerseChoreographyFigures.Any()))
            .ForMember(d => d.FigureName, act => act.MapFrom(src => src.Figure.Name));

        CreateMap<FigureOption, FigureOptionFormViewModel>()
            .ForMember(d => d.FigureName, act => act.MapFrom(src => src.Figure.Name));
        CreateMap<FigureOptionFormViewModel, FigureOption>();

        CreateMap<FigureOption, FigureOptionDeleteViewModel>()
            .ForMember(d => d.FigureName, act => act.MapFrom(src => src.Figure.Name));

        //Verse choreography models
        CreateMap<VerseChoreography, VerseChoreographyTableViewModel>()
            .ForMember(d => d.VerseTypeName, act => act.MapFrom(src => $"{src.VerseType.Name} ({src.VerseType.BeatCounts})"))
            .ForMember(d => d.StartPositionName, act => act.MapFrom(src => src.Figures.OrderBy(f => f.FigureOrder).Select(f => f.FigureOption.StartPosition.Name).FirstOrDefault()))
            .ForMember(d => d.EndPositionName, act => act.MapFrom(src => src.Figures.OrderByDescending(f => f.FigureOrder).Select(f => f.FigureOption.EndPosition.Name).FirstOrDefault()))
            .ForMember(d => d.NumberOfFigures, act => act.MapFrom(src => src.Figures.Count()))
            .ForMember(d => d.FinalFigureName, act => act.MapFrom(src => src.Figures.OrderByDescending(f => f.FigureOrder).Select(f => f.FigureOption.Figure.Name).FirstOrDefault()))
            .ForMember(d => d.UsedInFullChoreographies, act => act.MapFrom(src => src.FullChoreographies.Any()))
            .ForMember(d => d.HasEnoughFigures, act => act.MapFrom(src => src.Figures.Sum(f => f.FigureOption.BeatCounts) >= src.VerseType.BeatCounts));

        CreateMap<VerseChoreography, VerseChoreographyDetailsViewModel>()
            .ForMember(d => d.VerseTypeName, act => act.MapFrom(src => $"{src.VerseType.Name} ({src.VerseType.BeatCounts})"))
            .ForMember(d => d.StartPositionName, act => act.MapFrom(src => src.Figures.OrderBy(f => f.FigureOrder).Select(f => f.FigureOption.StartPosition.Name).FirstOrDefault()))
            .ForMember(d => d.EndPositionName, act => act.MapFrom(src => src.Figures.OrderByDescending(f => f.FigureOrder).Select(f => f.FigureOption.EndPosition.Name).FirstOrDefault()))
            .ForMember(d => d.NumberOfFigures, act => act.MapFrom(src => src.Figures.Count()))
            .ForMember(d => d.FinalFigureName, act => act.MapFrom(src => src.Figures.OrderByDescending(f => f.FigureOrder).Select(f => f.FigureOption.Figure.Name).FirstOrDefault()))
            .ForMember(d => d.UsedInFullChoreographies, act => act.MapFrom(src => src.FullChoreographies.Any()))
            .ForMember(d => d.Figures, act => act.MapFrom(src => src.Figures))
            .ForMember(d => d.HasEnoughFigures, act => act.MapFrom(src => src.Figures.Sum(f => f.FigureOption.BeatCounts) >= src.VerseType.BeatCounts));

        CreateMap<VerseChoreographySaveViewModel, VerseChoreography>();

        CreateMap<VerseChoreographyFormViewModel, VerseChoreography>();

        CreateMap<VerseChoreography, VerseChoreographyDeleteViewModel>()
            .ForMember(d => d.NumberOfFigures, act => act.MapFrom(src => src.Figures.Count()));

        //Verse choreography figure models
        CreateMap<VerseChoreographyFigure, VerseChoreographyFigureViewModel>()
            .ForMember(d => d.FigureName, act => act.MapFrom(src => src.FigureOption.Figure.Name))
            .ForMember(d => d.IsFavourite, act => act.MapFrom(src => src.FigureOption.Figure.IsFavourite))
            .ForMember(d => d.IsHighlight, act => act.MapFrom(src => src.FigureOption.Figure.IsHighlight))
            .ForMember(d => d.StartPosition, act => act.MapFrom(src => src.FigureOption.StartPosition.Name))
            .ForMember(d => d.EndPosition, act => act.MapFrom(src => src.FigureOption.EndPosition.Name))
            .ForMember(d => d.BeatsCount, act => act.MapFrom(src => src.FigureOption.BeatCounts))
            .ForMember(d => d.DynamicsType, act => act.MapFrom(src => src.FigureOption.DynamicsType.ToString()));
        CreateMap<VerseChoreographyFigureViewModel, VerseChoreographyFigure>();

        CreateMap<VerseChoreographyFigure, VerseChoreographyFigureReplaceViewModel>()
            .ForMember(d => d.FigureName, act => act.MapFrom(src => src.FigureOption.Figure.Name))
            .ForMember(d => d.IsFavourite, act => act.MapFrom(src => src.FigureOption.Figure.IsFavourite))
            .ForMember(d => d.IsHighlight, act => act.MapFrom(src => src.FigureOption.Figure.IsHighlight))
            .ForMember(d => d.StartPosition, act => act.MapFrom(src => src.FigureOption.StartPosition.Name))
            .ForMember(d => d.EndPosition, act => act.MapFrom(src => src.FigureOption.EndPosition.Name))
            .ForMember(d => d.BeatsCount, act => act.MapFrom(src => src.FigureOption.BeatCounts))
            .ForMember(d => d.DynamicsType, act => act.MapFrom(src => src.FigureOption.DynamicsType.ToString()));

        CreateMap<FigureOption, VerseChoreographyFigureViewModel>()
            .ForMember(d => d.Id, act => act.Ignore())
            .ForMember(d => d.FigureOptionId, act => act.MapFrom(src => src.Id))
            .ForMember(d => d.FigureName, act => act.MapFrom(src => src.Figure.Name))
            .ForMember(d => d.IsFavourite, act => act.MapFrom(src => src.Figure.IsFavourite))
            .ForMember(d => d.IsHighlight, act => act.MapFrom(src => src.Figure.IsHighlight))
            .ForMember(d => d.StartPosition, act => act.MapFrom(src => src.StartPosition.Name))
            .ForMember(d => d.EndPosition, act => act.MapFrom(src => src.EndPosition.Name))
            .ForMember(d => d.BeatsCount, act => act.MapFrom(src => src.BeatCounts))
            .ForMember(d => d.DynamicsType, act => act.MapFrom(src => src.DynamicsType.ToString()));

        CreateMap<VerseChoreographyFigureOptionFormViewModel, VerseChoreographyFigure>();

        CreateMap<VerseChoreographyFigure, VerseChoreographyFigureDeleteViewModel>()
           .ForMember(d => d.VerseChoreographyName, act => act.MapFrom(src => src.VerseChoreography.Name))
           .ForMember(d => d.FigureName, act => act.MapFrom(src => src.FigureOption.Figure.Name));

        //Full choreography models
        CreateMap<FullChoreography, FullChoreographyTableViewModel>()
            .ForMember(d => d.NumberOfVerses, act => act.MapFrom(src => src.VerseChoreographies.Count()));

        CreateMap<FullChoreography, FullChoreographyFormViewModel>();
        CreateMap<FullChoreographyFormViewModel, FullChoreography>();

        CreateMap<FullChoreography, FullChoreographyDetailsViewModel>()
            .ForMember(d => d.NumberOfVerses, act => act.MapFrom(src => src.VerseChoreographies.Count()))
            .ForMember(d => d.Verses, act => act.MapFrom(src => src.VerseChoreographies));

        //Full choreography verse choreography models
        CreateMap<FullChoreographyVerseChoreography, FullChoreographyVerseChoreographyViewModel>();

        CreateMap<FullChoreographyVerseChoreographyFormViewModel, FullChoreographyVerseChoreography>();

        CreateMap<FullChoreographyVerseChoreography, FullChoreographyVerseChoreographyDeleteViewModel>()
            .ForMember(d => d.VerseChoreographyName, act => act.MapFrom(src => src.VerseChoreography.Name))
            .ForMember(d => d.FullChoreographyName, act => act.MapFrom(src => src.FullChoreography.Name));
    }
}
