﻿using AutoMapper;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Infrastructure.Data.Common;
using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static ChoreographyBuilder.Core.Constants.LimitConstants;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Core.Services;

public class FigureService : IFigureService
{
	private readonly ILogger<FigureService> logger;
	private readonly IRepository repository;
	private readonly IMapper mapper;

	public FigureService(ILogger<FigureService> logger, IRepository repository, IMapper mapper)
	{
		this.logger = logger;
		this.repository = repository;
		this.mapper = mapper;
	}

	public async Task<FigureFormViewModel> GetFigureByIdAsync(int figureId)
	{
		Figure? figure = await repository.GetByIdAsync<Figure>(figureId);
		if (figure == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(Figure), figureId);
			throw new EntityNotFoundException();
		}

		return mapper.Map<FigureFormViewModel>(figure);
	}

	public async Task<string> GetFigureNameByIdAsync(int figureId)
	{
		Figure? figure = await repository.GetByIdAsync<Figure>(figureId);

		if (figure == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(Figure), figureId);
			throw new EntityNotFoundException();
		}

		return figure.Name;
	}

	public async Task<FigureForPreviewViewModel> GetFigureForDeleteAsync(int id)
	{
		var figure = await repository.GetByIdAsync<Figure>(id);

		if (figure == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(Figure), id);
			throw new EntityNotFoundException();
		}

		return mapper.Map<FigureForPreviewViewModel>(figure);
	}

	public async Task<FigureForCopyViewModel> GetFigureForCopyAsync(int id)
	{
		var figure = await repository.AllAsReadOnly<Figure>()
			.Include(f => f.FigureOptions)
			.Include(f => f.User)
			.FirstOrDefaultAsync(f => f.Id == id);

		if (figure == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(Figure), id);
			throw new EntityNotFoundException();
		}

		return mapper.Map<FigureForCopyViewModel>(figure);
	}

	public async Task<FigureQueryServiceModel> AllUserFiguresAsync(string userId, bool sharedFigures = false, string? searchTerm = null, int? searchedStartPositionId = null, int? searchedEndPositionId = null, int? searchedBeatsCount = null, DynamicsType? searchedDynamicsType = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage)
	{
		var figuresToShow = repository.AllAsReadOnly<Figure>();

		if (sharedFigures)
		{
			figuresToShow = figuresToShow
				.Where(f => f.UserId != userId)
				.Where(f => f.CanBeShared);
		}
		else
		{
			figuresToShow = figuresToShow
				.Where(f => f.UserId == userId);
		}

		if (searchTerm != null)
		{
			string normalizedSearchTerm = searchTerm.ToLower();
			figuresToShow = figuresToShow
				.Where(f => f.Name.ToLower().Contains(normalizedSearchTerm));
		}

		if (searchedStartPositionId != null)
		{
			figuresToShow = figuresToShow
				.Where(f => f.FigureOptions.Any(o => o.StartPositionId == searchedStartPositionId));
		}

		if (searchedEndPositionId != null)
		{
			figuresToShow = figuresToShow
				.Where(f => f.FigureOptions.Any(o => o.EndPositionId == searchedEndPositionId));
		}

		if (searchedBeatsCount != null)
		{
			figuresToShow = figuresToShow
				.Where(f => f.FigureOptions.Any(o => o.BeatCounts == searchedBeatsCount));
		}

		if (searchedDynamicsType != null)
		{
			figuresToShow = figuresToShow
				.Where(f => f.FigureOptions.Any(o => o.DynamicsType == searchedDynamicsType));
		}

		var figures = await figuresToShow
			.Include(f => f.FigureOptions)
			.ThenInclude(fo => fo.VerseChoreographyFigures)
			.Include(f => f.User)
			.OrderBy(f => f.Id)
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

	public async Task<IEnumerable<FigureForPreviewViewModel>> AllUserHighlightFiguresForChoreographiesAsync(string userId)
	{
		return await repository.AllAsReadOnly<Figure>()
			.Where(f => f.UserId == userId)
			.Where(f => f.IsHighlight)
			.Select(f => mapper.Map<FigureForPreviewViewModel>(f))
			.ToListAsync();
	}

	public async Task<bool> FigureExistForThisUserByIdAsync(int figureId, string userId)
	{
		var figure = await repository.GetByIdAsync<Figure>(figureId);
		if (figure == null)
		{
			return false;
		}

		if (figure.UserId != userId)
		{
			return false;
		}

		return true;
	}

	public async Task<bool> FigureExistAndCanBeCopiedByIdAsync(int figureId)
	{
		var figure = await repository.GetByIdAsync<Figure>(figureId);
		if (figure == null)
		{
			return false;
		}

		return figure.CanBeShared;
	}

	public async Task<bool> IsFigureUsedInChoreographiesAsync(int figureId)
	{
		Figure? figure = await repository.AllAsReadOnly<Figure>()
			.Include(f => f.FigureOptions)
				.ThenInclude(fo => fo.VerseChoreographyFigures)
			.FirstOrDefaultAsync(f => f.Id == figureId);

		if (figure == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(Figure), figureId);
			throw new EntityNotFoundException();
		}

		return figure.FigureOptions.Any(fo => fo.VerseChoreographyFigures.Any());
	}

	public async Task<int> AddFigureAsync(FigureFormViewModel model, string userId)
	{
		if (await repository.GetByIdAsync<IdentityUser>(userId) == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(IdentityUser), userId);
			throw new EntityNotFoundException();
		}

		Figure entity = mapper.Map<Figure>(model);
		entity.UserId = userId;

		await repository.AddAsync(entity);

		await repository.SaveChangesAsync();

		return entity.Id;
	}

	public async Task<int> CopyFigureForUserAsync(int figureId, string userId)
	{
		if (await repository.GetByIdAsync<IdentityUser>(userId) == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(IdentityUser), userId);
			throw new EntityNotFoundException();
		}

		Figure? existingFigure = await repository.AllAsReadOnly<Figure>()
			.Include(f => f.FigureOptions)
			.FirstOrDefaultAsync(f => f.Id == figureId);

		if (existingFigure == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(Figure), figureId);
			throw new EntityNotFoundException();
		}

		Figure newFigure = new Figure()
		{
			Name = existingFigure.Name + " - Copied",
			IsFavourite = existingFigure.IsFavourite,
			IsHighlight = existingFigure.IsHighlight,
			CanBeShared = existingFigure.CanBeShared,
			UserId = userId
		};

		await repository.AddAsync(newFigure);

		if (existingFigure.FigureOptions.Any())
		{
			foreach (FigureOption fo in existingFigure.FigureOptions)
			{
				FigureOption newOption = new FigureOption()
				{
					Figure = newFigure,
					DynamicsType = fo.DynamicsType,
					BeatCounts = fo.BeatCounts,
					EndPositionId = fo.EndPositionId,
					StartPositionId = fo.StartPositionId
				};

				await repository.AddAsync(newOption);
			}
		}

		await repository.SaveChangesAsync();

		return newFigure.Id;
	}

	public async Task EditFigureAsync(int figureId, FigureFormViewModel model)
	{
		var figure = await repository.GetByIdAsync<Figure>(figureId);

		if (figure == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(Figure), figureId);
			throw new EntityNotFoundException();
		}

		figure.Name = model.Name;
		figure.IsFavourite = model.IsFavourite;
		figure.IsHighlight = model.IsHighlight;
		figure.CanBeShared = model.CanBeShared;

		await repository.SaveChangesAsync();
	}

	public async Task DeleteFigureAsync(int id)
	{
		List<FigureOption> options = await repository.All<FigureOption>()
			.Where(o => o.FigureId == id)
			.ToListAsync();

		foreach (FigureOption option in options)
		{
			await repository.DeleteAsync<FigureOption>(option.Id);
		}

		await repository.DeleteAsync<Figure>(id);
		await repository.SaveChangesAsync();
	}
}
