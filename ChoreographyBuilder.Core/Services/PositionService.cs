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

		public async Task<PositionQueryServiceModel> AllPositionsAsync(string? searchTerm = null, int currentPage = 1, int itemsPerPage = 3)
		{
			var positionsToShow = repository.AllAsReadOnly<Position>();

			if (searchTerm != null)
			{
				string normalizedSearchTerm = searchTerm.ToLower();
				positionsToShow = positionsToShow
					.Where(p => p.Name.ToLower().Contains(normalizedSearchTerm));
			}

			var positions = await positionsToShow
				.Include(p => p.FiguresWithStartPosition)
				.Include(p => p.FiguresWithEndPosition)
				.Skip((currentPage - 1) * itemsPerPage)
				.Take(itemsPerPage)
				.Select(p => mapper.Map<PositionTableViewModel>(p))
				.ToListAsync();

			int totalPositionsToShow = await positionsToShow.CountAsync();

			return new PositionQueryServiceModel()
			{
				TotalCount = totalPositionsToShow,
				Positions = positions
			};
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
