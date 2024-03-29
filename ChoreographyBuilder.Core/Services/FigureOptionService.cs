﻿using AutoMapper;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Models.FigureOption;
using ChoreographyBuilder.Infrastructure.Data.Common;
using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Services
{
	public class FigureOptionService : IFigureOptionService
	{
		private readonly IRepository repository;
		private readonly IMapper mapper;

		public FigureOptionService(IRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		public async Task AddFigureOptionAsync(FigureOptionFormViewModel model)
		{
			FigureOption entity = mapper.Map<FigureOption>(model);

			await repository.AddAsync(entity);

			await repository.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			await repository.DeleteAsync<FigureOption>(id);
			await repository.SaveChangesAsync();
		}

		public async Task EditFigureOptionAsync(int optionId, FigureOptionFormViewModel model)
		{
			var option = await repository.GetByIdAsync<FigureOption>(optionId);

			if (option == null)
			{
				throw new EntityNotFoundException();
			}

			option.StartPositionId = model.StartPositionId;
			option.EndPositionId = model.EndPositionId;
			option.DynamicsType = model.DynamicsType;
			option.BeatCounts = model.BeatCounts;

			await repository.SaveChangesAsync();
		}

		public async Task<bool> FigureOptionExistForThisUserByIdAsync(int optionId, string userId)
		{
			FigureOption? figureOption = await repository.AllAsReadOnly<FigureOption>()
				.Include(fo => fo.Figure)
				.FirstOrDefaultAsync(fo => fo.Id == optionId);

			if (figureOption == null)
			{
				return false;
			}

			if (figureOption.Figure.UserId != userId)
			{
				return false;
			}

			return true;
		}

		public async Task<FigureOptionFormViewModel?> GetFigureOptionByIdAsync(int optionId)
		{
			FigureOption? option = await repository.GetByIdAsync<FigureOption>(optionId);
			return mapper.Map<FigureOptionFormViewModel>(option);
		}

		public async Task<FigureOptionDeleteViewModel?> GetFigureOptionForDeleteAsync(int id)
		{
			var option = await repository.AllAsReadOnly<FigureOption>()
				.Include(fo => fo.Figure)
				.FirstOrDefaultAsync(fo => fo.Id == id);

			if (option == null)
			{
				throw new EntityNotFoundException();
			}

			return mapper.Map<FigureOptionDeleteViewModel?>(option);
		}

		public async Task<FigureOptionQueryServiceModel> GetFigureOptionsAsync(int figureId, int? searchedStartPositionId = null, int? searchedEndPositionId = null, int? searchedBeatsCount = null, DynamicsType? searchedDynamicsType = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage)
		{
			var figure = await repository.GetByIdAsync<Figure>(figureId);

			if (figure == null)
			{
				throw new EntityNotFoundException();
			}

			var optionsToShow = repository.AllAsReadOnly<FigureOption>()
				.Where(o => o.FigureId == figureId);

			if (searchedStartPositionId != null)
			{
				optionsToShow = optionsToShow
					.Where(o => o.StartPositionId == searchedStartPositionId);
			}

			if (searchedEndPositionId != null)
			{
				optionsToShow = optionsToShow
					.Where(o => o.EndPositionId == searchedEndPositionId);
			}

			if (searchedBeatsCount != null)
			{
				optionsToShow = optionsToShow
					.Where(o => o.BeatCounts == searchedBeatsCount);
			}

			if (searchedDynamicsType != null)
			{
				optionsToShow = optionsToShow
					.Where(o => o.DynamicsType == searchedDynamicsType);
			}
			var options = await optionsToShow
				.Include(o => o.StartPosition)
				.Include(o => o.EndPosition)
				.Include(o => o.VerseChoreographyFigures)
				.Skip((currentPage - 1) * itemsPerPage)
				.Take(itemsPerPage)
				.Select(o => mapper.Map<FigureOptionTableViewModel>(o))
				.ToListAsync();

			int totalNumberOfOptions = await optionsToShow.CountAsync();

			string figureName = figure.Name;

			return new FigureOptionQueryServiceModel()
			{
				FigureId = figureId,
				FigureName = figureName,
				TotalCount = totalNumberOfOptions,
				Entities = options
			};
		}

		public async Task<bool> IsFigureOptionUsedInChoreographiesAsync(int optionId)
		{
			FigureOption? option = await repository.AllAsReadOnly<FigureOption>()
				.Include(fo => fo.VerseChoreographyFigures)
				.FirstOrDefaultAsync(o => o.Id == optionId);

			if (option == null)
			{
				throw new EntityNotFoundException();
			}

			return option.VerseChoreographyFigures.Any();
		}
	}
}
