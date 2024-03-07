using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.VerseType;
using ChoreographyBuilder.Infrastructure.Data.Common;
using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChoreographyBuilder.Core.Services
{
	public class VerseTypeService : IVerseTypeService
	{
		private readonly IRepository repository;
		private readonly IMapper mapper;

		public VerseTypeService(IRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		public async Task<IEnumerable<VerseTypeViewModel>> AllVerseTypesAsync()
		{
			var verseTypes = await repository.AllAsReadOnly<VerseType>().ToListAsync();

			return mapper.Map<IEnumerable<VerseTypeViewModel>>(verseTypes);
		}
	}
}
