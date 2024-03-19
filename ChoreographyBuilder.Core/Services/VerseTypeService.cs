using AutoMapper;
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

		public async Task AddVerseTypeAsync(VerseTypeFormViewModel model)
		{
			VerseType entity = mapper.Map<VerseType>(model);

			await repository.AddAsync(entity);

			await repository.SaveChangesAsync();
		}

		public async Task<IEnumerable<VerseTypeForChoreographiesViewModel>> AllActiveVerseTypesOrSelectedVerseTypeAsync(int selectedVerseTypeId)
		{
			return await repository.AllAsReadOnly<VerseType>()
				.Where(vt => vt.IsActive || vt.Id == selectedVerseTypeId)
				.Select(vt => mapper.Map<VerseTypeForChoreographiesViewModel>(vt))
				.ToListAsync();
		}

		public async Task<VerseTypeQueryServiceModel> AllVerseTypesAsync(string? searchTerm = null, int? searchedBeatsCount = null, int currentPage = 1, int itemsPerPage = 10)
		{
			var verseTypesToShow = repository.AllAsReadOnly<VerseType>();

			if (searchTerm != null)
			{
				string normalizedSearchTerm = searchTerm.ToLower();
				verseTypesToShow = verseTypesToShow
					.Where(vt => vt.Name.ToLower().Contains(normalizedSearchTerm));
			}

			if (searchedBeatsCount != null)
			{
				verseTypesToShow = verseTypesToShow
					.Where(vt => vt.BeatCounts == searchedBeatsCount);
			}

			var verseTypes = await verseTypesToShow
				.Include(vt => vt.VerseChoreographies)
				.Skip((currentPage - 1) * itemsPerPage)
				.Take(itemsPerPage)
				.Select(p => mapper.Map<VerseTypeTableViewModel>(p))
				.ToListAsync();

			int totalVerseTypesToShow = await verseTypesToShow.CountAsync();

			return new VerseTypeQueryServiceModel()
			{
				TotalCount = totalVerseTypesToShow,
				Entities = verseTypes
			};
		}

		public async Task ChangeVerseTypeStatusAsync(int id)
		{
			VerseType? verseType = await repository.All<VerseType>()
				.FirstOrDefaultAsync(vt => vt.Id == id);

			if (verseType == null)
			{
				throw new ArgumentNullException();
			}

			verseType.IsActive = !verseType.IsActive;

			await repository.SaveChangesAsync();
		}
	}
}
