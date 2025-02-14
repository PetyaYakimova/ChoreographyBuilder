using AutoMapper;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Models.FigureOption;
using ChoreographyBuilder.Infrastructure.Data.Common;
using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static ChoreographyBuilder.Core.Constants.LimitConstants;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Core.Services;

public class FigureOptionService : IFigureOptionService
{
    private readonly ILogger<FigureOptionService> logger;
    private readonly IRepository repository;
    private readonly IMapper mapper;

    public FigureOptionService(ILogger<FigureOptionService> logger, IRepository repository, IMapper mapper)
    {
        this.logger = logger;
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<FigureOptionFormViewModel> GetFigureOptionByIdAsync(int optionId)
    {
        FigureOption? option = await repository.GetByIdAsync<FigureOption>(optionId);
        if (option == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(FigureOption), optionId);
            throw new EntityNotFoundException();
        }

        return mapper.Map<FigureOptionFormViewModel>(option);
    }

    public async Task<FigureOptionDeleteViewModel> GetFigureOptionForDeleteAsync(int id)
    {
        var option = await repository.AllAsReadOnly<FigureOption>()
            .Include(fo => fo.Figure)
            .FirstOrDefaultAsync(fo => fo.Id == id);

        if (option == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(FigureOption), id);
            throw new EntityNotFoundException();
        }

        return mapper.Map<FigureOptionDeleteViewModel>(option);
    }

    public async Task<int> GetBeatsForFigureOptionAsync(int id)
    {
        var option = await repository.AllAsReadOnly<FigureOption>()
            .FirstOrDefaultAsync(fo => fo.Id == id);

        if (option == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(FigureOption), id);
            throw new EntityNotFoundException();
        }

        return option.BeatCounts;
    }

    public async Task<string> GetStartPositionNameForFigureOptionAsync(int id)
    {
        var option = await repository.AllAsReadOnly<FigureOption>()
            .Include(fo => fo.StartPosition)
            .FirstOrDefaultAsync(fo => fo.Id == id);

        if (option == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(FigureOption), id);
            throw new EntityNotFoundException();
        }

        return option.StartPosition.Name;
    }

    public async Task<FigureOptionQueryServiceModel> GetFigureOptionsAsync(int figureId, int? searchedStartPositionId = null, int? searchedEndPositionId = null, int? searchedBeatsCount = null, DynamicsType? searchedDynamicsType = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage)
    {
        var figure = await repository.GetByIdAsync<Figure>(figureId);

        if (figure == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(Figure), figureId);
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
            .OrderBy(o => o.Id)
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

    public async Task<bool> IsFigureOptionUsedInChoreographiesAsync(int optionId)
    {
        FigureOption? option = await repository.AllAsReadOnly<FigureOption>()
            .Include(fo => fo.VerseChoreographyFigures)
            .FirstOrDefaultAsync(o => o.Id == optionId);

        if (option == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(FigureOption), optionId);
            throw new EntityNotFoundException();
        }

        return option.VerseChoreographyFigures.Any();
    }

    public async Task<IEnumerable<FigureOptionWithFigureViewModel>> AllUserFiguresStartingWithPositionAndLessThanBeatsAsync(string userId, int remainingBeats, int? startPositionId = null)
    {
        var figuresToShow = repository.AllAsReadOnly<FigureOption>()
             .Where(fo => fo.Figure.UserId == userId)
             .Where(fo => fo.BeatCounts <= remainingBeats);

        if (startPositionId != null)
        {
            figuresToShow = figuresToShow
                .Where(fo => fo.StartPositionId == startPositionId);
        }

        var result = await figuresToShow
                 .Include(fo => fo.Figure)
                 .Include(fo => fo.VerseChoreographyFigures)
                 .Include(fo => fo.StartPosition)
                 .Include(fo => fo.EndPosition)
                .Select(fo => mapper.Map<FigureOptionWithFigureViewModel>(fo))
                .ToListAsync();

        return result;
    }

    public async Task AddFigureOptionAsync(FigureOptionFormViewModel model)
    {
        if (await repository.GetByIdAsync<Figure>(model.FigureId) == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(Figure), model.FigureId);
            throw new EntityNotFoundException();
        }

        await CheckFigureOptionFormViewModelIsValid(model);

        FigureOption entity = mapper.Map<FigureOption>(model);

        await repository.AddAsync(entity);

        await repository.SaveChangesAsync();
    }

    public async Task EditFigureOptionAsync(int optionId, FigureOptionFormViewModel model)
    {
        var option = await repository.GetByIdAsync<FigureOption>(optionId);

        if (option == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(FigureOption), optionId);
            throw new EntityNotFoundException();
        }

        await CheckFigureOptionFormViewModelIsValid(model);

        option.StartPositionId = model.StartPositionId;
        option.EndPositionId = model.EndPositionId;
        option.DynamicsType = model.DynamicsType;
        option.BeatCounts = model.BeatCounts;

        await repository.SaveChangesAsync();
    }

    public async Task DeleteFigureOptionAsync(int id)
    {
        await repository.DeleteAsync<FigureOption>(id);
        await repository.SaveChangesAsync();
    }

    /// <summary>
    /// Checks whether the given model is valid and if it is not - throws an exception.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <exception cref="InvalidModelException"></exception>
    private async Task CheckFigureOptionFormViewModelIsValid(FigureOptionFormViewModel model)
    {
        var positions = await repository.AllAsReadOnly<Position>().Select(p => p.Id).ToListAsync();

        if (!positions.Contains(model.StartPositionId))
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(Position), model.StartPositionId);
            throw new EntityNotFoundException();
        }

        if (!positions.Contains(model.EndPositionId))
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(Position), model.EndPositionId);
            throw new EntityNotFoundException();
        }

        if (model.BeatCounts % 2 != 0)
        {
            logger.LogError(BeatsCountIsNotEvenNumberLoggerErrorMessage, nameof(FigureOption));
            throw new InvalidModelException();
        }
    }
}
