﻿using AutoMapper;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Infrastructure.Data.Common;
using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static ChoreographyBuilder.Core.Constants.LimitConstants;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Core.Services;

public class PositionService : IPositionService
{
	private readonly ILogger<PositionService> logger;
	private readonly IRepository repository;
	private readonly IMapper mapper;

	public PositionService(ILogger<PositionService> logger, IRepository repository, IMapper mapper)
	{
		this.logger = logger;
		this.repository = repository;
		this.mapper = mapper;
	}

	public async Task<PositionFormViewModel> GetPositionByIdAsync(int id)
	{
		Position? position = await repository.GetByIdAsync<Position>(id);
		if (position == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(Position), id);
			throw new EntityNotFoundException();
		}

		return mapper.Map<PositionFormViewModel>(position);
	}

	public async Task<PositionForPreviewViewModel> GetPositionForDeleteAsync(int id)
	{
		var position = await repository.GetByIdAsync<Position>(id);

		if (position == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(Position), id);
			throw new EntityNotFoundException();
		}

		return mapper.Map<PositionForPreviewViewModel>(position);
	}

	public async Task<PositionQueryServiceModel> AllPositionsAsync(string? searchTerm = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage)
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
			.OrderBy(p => p.Id)
			.Skip((currentPage - 1) * itemsPerPage)
			.Take(itemsPerPage)
			.Select(p => mapper.Map<PositionTableViewModel>(p))
			.ToListAsync();

		int totalPositionsToShow = await positionsToShow.CountAsync();

		return new PositionQueryServiceModel()
		{
			TotalCount = totalPositionsToShow,
			Entities = positions
		};
	}

	public async Task<IEnumerable<PositionForPreviewViewModel>> AllActivePositionsAndSelectedPositionAsync(int? selectedPositionId = null)
	{
		return await repository.AllAsReadOnly<Position>()
			 .Where(p => p.IsActive || p.Id == selectedPositionId)
			 .Select(p => mapper.Map<PositionForPreviewViewModel>(p))
			 .ToListAsync();
	}

	public async Task<bool> PositionExistByIdAsync(int id)
	{
		var position = await repository.GetByIdAsync<Position>(id);
		if (position == null)
		{
			return false;
		}

		return true;
	}

	public async Task<bool> IsPositionUsedInFiguresAsync(int id)
	{
		Position? position = await repository.AllAsReadOnly<Position>()
			.Include(p => p.FiguresWithStartPosition)
			.Include(p => p.FiguresWithEndPosition)
			.FirstOrDefaultAsync(p => p.Id == id);

		if (position == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(Position), id);
			throw new EntityNotFoundException();
		}

		return position.FiguresWithStartPosition.Any() || position.FiguresWithEndPosition.Any();
	}

	public async Task AddPositionAsync(PositionFormViewModel model)
	{
		Position entity = mapper.Map<Position>(model);

		await repository.AddAsync(entity);

		await repository.SaveChangesAsync();
	}

	public async Task ChangePositionStatusAsync(int id)
	{
		Position? position = await repository.GetByIdAsync<Position>(id);

		if (position == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(Position), id);
			throw new EntityNotFoundException();
		}

		position.IsActive = !position.IsActive;

		await repository.SaveChangesAsync();
	}

	public async Task EditPositionAsync(int positionId, PositionFormViewModel model)
	{
		var position = await repository.GetByIdAsync<Position>(positionId);

		if (position == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(Position), positionId);
			throw new EntityNotFoundException();
		}

		position.Name = model.Name;

		await repository.SaveChangesAsync();
	}

	public async Task DeletePositionAsync(int id)
	{
		await repository.DeleteAsync<Position>(id);
		await repository.SaveChangesAsync();
	}
}
