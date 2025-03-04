using AutoMapper;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Models.FullChoreography;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Infrastructure.Data.Common;
using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static ChoreographyBuilder.Core.Constants.LimitConstants;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Core.Services;

public class FullChoreographyService : IFullChoreographyService
{
	private readonly ILogger<FullChoreographyService> logger;
	private readonly IRepository repository;
	private readonly IMapper mapper;

	public FullChoreographyService(ILogger<FullChoreographyService> logger, IRepository repository, IMapper mapper)
	{
		this.logger = logger;
		this.repository = repository;
		this.mapper = mapper;
	}

	public async Task<FullChoreographyDetailsViewModel> GetChoreographyDetailsByIdAsync(int id)
	{
		var choreography = await repository.AllAsReadOnly<FullChoreography>()
			.Include(c => c.VerseChoreographies)
				.ThenInclude(vc => vc.VerseChoreography.VerseType)
			.Include(c => c.VerseChoreographies)
				.ThenInclude(vc => vc.VerseChoreography.Figures)
					.ThenInclude(f => f.FigureOption.Figure)
			.Include(c => c.VerseChoreographies)
				.ThenInclude(vc => vc.VerseChoreography.Figures)
					.ThenInclude(f => f.FigureOption.StartPosition)
			.Include(c => c.VerseChoreographies)
				.ThenInclude(vc => vc.VerseChoreography.Figures)
					.ThenInclude(f => f.FigureOption.EndPosition)
			.FirstOrDefaultAsync(c => c.Id == id);

		if (choreography == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(FullChoreography), id);
			throw new EntityNotFoundException();
		}

		choreography.VerseChoreographies = choreography.VerseChoreographies.OrderBy(vs => vs.VerseChoreographyOrder);

		return mapper.Map<FullChoreographyDetailsViewModel>(choreography);
	}

	public async Task<FullChoreographyFormViewModel> GetChoreographyForEditByIdAsync(int id)
	{
		FullChoreography? choreography = await repository.GetByIdAsync<FullChoreography>(id);

		if (choreography == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(FullChoreography), id);
			throw new EntityNotFoundException();
		}

		return mapper.Map<FullChoreographyFormViewModel>(choreography);
	}

	public async Task<FullChoreographyTableViewModel> GetFullChoreographyForDeleteAsync(int id)
	{
		var choreography = await repository.AllAsReadOnly<FullChoreography>()
			.Include(vc => vc.VerseChoreographies)
			.FirstOrDefaultAsync(fc => fc.Id == id);

		if (choreography == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(FullChoreography), id);
			throw new EntityNotFoundException();
		}

		return mapper.Map<FullChoreographyTableViewModel>(choreography);
	}

	public async Task<PositionForPreviewViewModel?> GetLastVerseChoreographyEndPositionAsync(int fullChoreographyId)
	{
		PositionForPreviewViewModel? lastEndPosition = null;

		FullChoreographyVerseChoreography? lastVerseChoreography = await repository.AllAsReadOnly<FullChoreographyVerseChoreography>()
			.Where(fcvc => fcvc.FullChoreographyId == fullChoreographyId)
			.Include(vs => vs.VerseChoreography.Figures)
				.ThenInclude(vs => vs.FigureOption.EndPosition)
			.OrderByDescending(fcvc => fcvc.VerseChoreographyOrder)
			.FirstOrDefaultAsync();

		if (lastVerseChoreography != null)
		{
			lastEndPosition = lastVerseChoreography.VerseChoreography.Figures.OrderByDescending(f => f.FigureOrder).Select(f => mapper.Map<PositionForPreviewViewModel>(f.FigureOption.EndPosition)).FirstOrDefault();
		}

		return lastEndPosition;
	}

	public async Task<int> GetNumberOfVerseChoreographiesForFullChoreographyAsync(int fullChoreographyId)
	{
		FullChoreography? choreography = await repository.AllAsReadOnly<FullChoreography>()
			.Include(c => c.VerseChoreographies)
			.FirstOrDefaultAsync(c => c.Id == fullChoreographyId);

		if (choreography == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(FullChoreography), fullChoreographyId);
			throw new EntityNotFoundException();
		}

		return choreography.VerseChoreographies.Count();
	}

	public async Task<FullChoreographyQueryServiceModel> AllUserFullChoreographiesAsync(string userId, string? searchTerm = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage)
	{
		var choreographiesToShow = repository.AllAsReadOnly<FullChoreography>()
			.Where(f => f.UserId == userId);

		if (searchTerm != null)
		{
			string normalizedSearchTerm = searchTerm.ToLower();
			choreographiesToShow = choreographiesToShow
				.Where(f => f.Name.ToLower().Contains(normalizedSearchTerm));
		}

		var choreographies = await choreographiesToShow
			.Include(c => c.VerseChoreographies)
			.OrderBy(c => c.Id)
			.Skip((currentPage - 1) * itemsPerPage)
			.Take(itemsPerPage)
			.Select(f => mapper.Map<FullChoreographyTableViewModel>(f))
			.ToListAsync();

		int totalChoreographiesToShow = await choreographiesToShow.CountAsync();

		return new FullChoreographyQueryServiceModel()
		{
			TotalCount = totalChoreographiesToShow,
			Entities = choreographies
		};
	}

	public async Task<bool> FullChoreographyExistForThisUserByIdAsync(int id, string userId)
	{
		var fullChoreography = await repository.GetByIdAsync<FullChoreography>(id);
		if (fullChoreography == null)
		{
			return false;
		}

		if (fullChoreography.UserId != userId)
		{
			return false;
		}

		return true;
	}

	public async Task<int> AddFullChoreographyAsync(FullChoreographyFormViewModel model, string userId)
	{
		if (await repository.GetByIdAsync<IdentityUser>(userId) == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(IdentityUser), userId);
			throw new EntityNotFoundException();
		}

		FullChoreography entity = mapper.Map<FullChoreography>(model);
		entity.UserId = userId;

		await repository.AddAsync(entity);

		await repository.SaveChangesAsync();

		return entity.Id;
	}

	public async Task EditFullChoreographyAsync(int id, FullChoreographyFormViewModel model)
	{
		var choreography = await repository.GetByIdAsync<FullChoreography>(id);

		if (choreography == null)
		{
			logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(FullChoreography), id);
			throw new EntityNotFoundException();
		}

		choreography.Name = model.Name;

		await repository.SaveChangesAsync();
	}

	public async Task DeleteFullChoreographyAsync(int id)
	{
		List<FullChoreographyVerseChoreography> verseChoreographies = await repository.All<FullChoreographyVerseChoreography>()
			.Where(fcvc => fcvc.FullChoreographyId == id)
			.ToListAsync();
		foreach (FullChoreographyVerseChoreography verseChoreography in verseChoreographies)
		{
			await repository.DeleteAsync<FullChoreographyVerseChoreography>(verseChoreography.Id);
		}

		await repository.DeleteAsync<FullChoreography>(id);
		await repository.SaveChangesAsync();
	}
}
