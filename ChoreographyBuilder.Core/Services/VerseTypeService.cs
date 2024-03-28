using AutoMapper;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Models.VerseType;
using ChoreographyBuilder.Infrastructure.Data.Common;
using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

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

        public async Task<IEnumerable<VerseTypeForPreviewViewModel>> AllActiveVerseTypesOrSelectedVerseTypeAsync(int? selectedVerseTypeId = null)
        {
            return await repository.AllAsReadOnly<VerseType>()
                .Where(vt => vt.IsActive || vt.Id == selectedVerseTypeId)
                .Select(vt => mapper.Map<VerseTypeForPreviewViewModel>(vt))
                .ToListAsync();
        }

        public async Task<VerseTypeQueryServiceModel> AllVerseTypesAsync(string? searchTerm = null, int? searchedBeatsCount = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage)
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
            VerseType? verseType = await repository.GetByIdAsync<VerseType>(id);

            if (verseType == null)
            {
				throw new EntityNotFoundException();
			}

            verseType.IsActive = !verseType.IsActive;

            await repository.SaveChangesAsync();
        }

        public async Task EditVerseTypeAsync(int verseTypeId, VerseTypeFormViewModel model)
        {
            var verseType = await repository.GetByIdAsync<VerseType>(verseTypeId);

            if (verseType == null)
            {
				throw new EntityNotFoundException();
			}

            verseType.Name = model.Name;
            verseType.BeatCounts = model.BeatCounts;

            await repository.SaveChangesAsync();
        }

        public async Task<VerseTypeForPreviewViewModel?> GetVerseTypeForDeleteAsync(int id)
        {
            var verseType = await repository.GetByIdAsync<VerseType>(id);

            if (verseType == null)
            {
				throw new EntityNotFoundException();
			}

            return mapper.Map<VerseTypeForPreviewViewModel>(verseType);
        }

        public async Task<VerseTypeFormViewModel?> GetVerseTypeById(int id)
        {
            VerseType? verseType = await repository.GetByIdAsync<VerseType>(id);
            return mapper.Map<VerseTypeFormViewModel?>(verseType);
        }

        public async Task<bool> IsVerseTypeUsedInChoreographiesAsync(int id)
        {
            VerseType? verseType = await repository.AllAsReadOnly<VerseType>()
                .Include(vt => vt.VerseChoreographies)
                .FirstOrDefaultAsync(vt => vt.Id == id);

            if (verseType == null)
            {
				throw new EntityNotFoundException();
			}

            return verseType.VerseChoreographies.Any();
        }

        public async Task<bool> VerseTypeExistByIdAsync(int id)
        {
            var verseType = await repository.GetByIdAsync<VerseType>(id);
            if (verseType == null)
            {
                return false;
            }

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            await repository.DeleteAsync<VerseType>(id);
            await repository.SaveChangesAsync();
        }
    }
}
