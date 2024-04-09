using AutoMapper;
using ChoreographyBuilder.Core.Infrastructure;

namespace ChoreographyBuilder.Tests.Mocks
{
	public static class MapperMock
	{
		public static IMapper Instance
		{
			get
			{
				var mapperConfiguration = new MapperConfiguration(config =>
				{
					config.AddProfile<MappingProfile>();
				});

				return new Mapper(mapperConfiguration);
			}
		}
	}
}
