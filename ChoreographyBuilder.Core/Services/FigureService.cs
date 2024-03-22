using AutoMapper;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Infrastructure.Data.Common;
using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Services
{
	public class FigureService : IFigureService
	{
		private readonly IRepository repository;
		private readonly IMapper mapper;

		public FigureService(IRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		public async Task<int> AddFigureAsync(FigureFormViewModel model, string userId)
		{
			Figure entity = mapper.Map<Figure>(model);
			entity.UserId = userId;

			await repository.AddAsync(entity);

			await repository.SaveChangesAsync();

			return entity.Id;
		}

		public async Task<FigureQueryServiceModel> AllUserFiguresAsync(string userId, string? searchTerm = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage)
		{
			var figuresToShow = repository.AllAsReadOnly<Figure>()
				.Where(f => f.UserId == userId);

			if (searchTerm != null)
			{
				string normalizedSearchTerm = searchTerm.ToLower();
				figuresToShow = figuresToShow
					.Where(f => f.Name.ToLower().Contains(normalizedSearchTerm));
			}

			var figures = await figuresToShow
				.Include(f => f.FigureOptions)
				.ThenInclude(fo => fo.VerseChoreographyFigures)
				.Skip((currentPage - 1) * itemsPerPage)
				.Take(itemsPerPage)
				.Select(f => mapper.Map<FigureTableViewModel>(f))
				.ToListAsync();

			int totalFiguresToShow = await figuresToShow.CountAsync();

			return new FigureQueryServiceModel()
			{
				TotalCount = totalFiguresToShow,
				Entities = figures
			};
		}

		public async Task EditFigureAsync(int figureId, FigureFormViewModel model)
		{
			var figure = await repository.GetByIdAsync<Figure>(figureId);

			if (figure == null)
			{
				//Check if this is the correct exception to throw
				throw new ArgumentNullException();
			}

			figure.Name = model.Name;
			figure.IsFavourite = model.IsFavourite;
			figure.IsHighlight = model.IsHighlight;

			await repository.SaveChangesAsync();
		}

		public async Task<FigureFormViewModel?> GetFigureByIdAsync(int figureId)
		{
			Figure? figure = await repository.GetByIdAsync<Figure>(figureId);
			return mapper.Map<FigureFormViewModel>(figure);
		}

		public async Task<string> GetFigureNameByIdAsync(int figureId)
		{
			Figure? figure = await repository.GetByIdAsync<Figure>(figureId);

			if (figure == null)
			{
				//Check if this is the correct exception to be thrown
				throw new ArgumentNullException();
			}

			return figure.Name;
		}

		public async Task<string> GetUserIdForFigureByIdAsync(int figureId)
		{
			Figure? figure = await repository.GetByIdAsync<Figure>(figureId);

			if (figure == null)
			{
				//Check if this is the correct exception to be thrown
				throw new ArgumentNullException();
			}

			return figure.UserId;
		}

		public async Task<bool> IsFigureUsedInChoreographiesAsync(int figureId)
		{
			Figure? figure = await repository.AllAsReadOnly<Figure>()
				.Include(f => f.FigureOptions)
					.ThenInclude(fo => fo.VerseChoreographyFigures)
				.FirstOrDefaultAsync(f => f.Id == figureId);

			if (figure == null)
			{
				//Check if this is the correct exception to be thrown
				throw new ArgumentNullException();
			}

			return figure.FigureOptions.Any(fo => fo.VerseChoreographyFigures.Any());
		}
	}
}
