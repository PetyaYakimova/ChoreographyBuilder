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

        public async Task<IEnumerable<VerseTypeTableViewModel>> AllVerseTypesAsync()
        {
            return await repository.AllAsReadOnly<VerseType>()
                .Include(vt => vt.VerseChoreographies)
                .Select(vt => mapper.Map<VerseTypeTableViewModel>(vt))
                .ToListAsync();
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
