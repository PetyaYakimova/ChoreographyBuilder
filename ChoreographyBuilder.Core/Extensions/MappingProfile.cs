using AutoMapper;
using ChoreographyBuilder.Core.Models.VerseType;
using ChoreographyBuilder.Infrastructure.Data.Models;

namespace ChoreographyBuilder.Core.Extensions
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<VerseType, VerseTypeViewModel>();
		}
	}
}
