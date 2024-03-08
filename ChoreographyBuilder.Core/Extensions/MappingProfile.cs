using AutoMapper;
using ChoreographyBuilder.Core.Models.VerseType;
using ChoreographyBuilder.Infrastructure.Data.Models;

namespace ChoreographyBuilder.Core.Extensions
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<VerseType, VerseTypeTableViewModel>()
				.ForMember(d => d.HasChoreographies, act => act.MapFrom(src => src.VerseChoreographies.Any()));

			CreateMap<VerseType, VerseTypeForChoreographiesViewModel>()
				.ForMember(d => d.Name, act => act.MapFrom(src => $"{src.Name} ({src.BeatCounts})"));
		}
	}
}
