using AutoMapper;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Core.Models.VerseType;
using ChoreographyBuilder.Infrastructure.Data.Common;
using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChoreographyBuilder.Core.Services
{
    public class PositionService : IPositionService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public PositionService(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task AddPositionAsync(PositionFormViewModel model)
        {
            Position entity = mapper.Map<Position>(model);

            await repository.AddAsync(entity);

            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<PositionForFigureViewModel>> AllActivePositionsAndSelectedPositionAsync(int? selectedPositionId)
        {
            return await repository.AllAsReadOnly<Position>()
                 .Where(p => p.IsActive || p.Id == selectedPositionId)
                 .Select(p => mapper.Map<PositionForFigureViewModel>(p))
                 .ToListAsync();
        }

        public async Task<IEnumerable<PositionTableViewModel>> AllPositionsAsync()
        {
            return await repository.AllAsReadOnly<Position>()
                .Include(p => p.FiguresWithStartPosition)
                .Include(p => p.FiguresWithEndPosition)
                .Select(p => mapper.Map<PositionTableViewModel>(p))
                .ToListAsync();
        }

        public async Task ChangePositionStatusAsync(int id)
        {
            Position? position = await repository.All<Position>()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (position == null)
            {
                throw new ArgumentNullException();
            }

            position.IsActive = !position.IsActive;

            await repository.SaveChangesAsync();
        }
    }
}
