﻿using AutoMapper;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Core.Models.VerseChoreography;
using ChoreographyBuilder.Core.Models.VerseChoreographyFigure;
using ChoreographyBuilder.Infrastructure.Data.Common;
using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static ChoreographyBuilder.Core.Constants.LimitConstants;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Core.Services;

public class VerseChoreographyService : IVerseChoreographyService
{
    private readonly ILogger<VerseChoreographyService> logger;
    private readonly IRepository repository;
    private readonly IMapper mapper;

    private List<FigureOption> allUserNonHighlightFigureOptions;
    private List<List<FigureOption>> validOptionsForGenerateChoreographies;
    private int? startPositionForGenerateChoreographyId;

    public VerseChoreographyService(ILogger<VerseChoreographyService> logger, IRepository repository, IMapper mapper)
    {
        this.logger = logger;
        this.repository = repository;
        this.mapper = mapper;

        allUserNonHighlightFigureOptions = new List<FigureOption>();
        validOptionsForGenerateChoreographies = new List<List<FigureOption>>();
    }

    public async Task<VerseChoreographyDetailsViewModel> GetVerseChoreographyByIdAsync(int id)
    {
        var choreography = await repository.AllAsReadOnly<VerseChoreography>()
            .Include(c => c.VerseType)
            .Include(c => c.FullChoreographies)
            .Include(c => c.Figures)
                .ThenInclude(f => f.FigureOption.Figure)
            .Include(c => c.Figures)
                .ThenInclude(f => f.FigureOption.StartPosition)
            .Include(c => c.Figures)
                .ThenInclude(f => f.FigureOption.EndPosition)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (choreography == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(VerseChoreography), id);
            throw new EntityNotFoundException();
        }

        choreography.Figures = choreography.Figures.OrderBy(f => f.FigureOrder);

        return mapper.Map<VerseChoreographyDetailsViewModel>(choreography);
    }

    public async Task<VerseChoreographyDeleteViewModel> GetVerseChoreographyForDeleteAsync(int id)
    {
        var choreography = await repository.AllAsReadOnly<VerseChoreography>()
            .Include(vc => vc.Figures)
            .FirstOrDefaultAsync(vc => vc.Id == id);

        if (choreography == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(VerseChoreography), id);
            throw new EntityNotFoundException();
        }

        return mapper.Map<VerseChoreographyDeleteViewModel>(choreography);
    }

    public async Task<PositionForPreviewViewModel?> GetLastFigureEndPositionAsync(int verseChoreographyId)
    {
        PositionForPreviewViewModel? lastEndPosition = null;

        VerseChoreographyFigure? lastfigure = await repository.AllAsReadOnly<VerseChoreographyFigure>()
            .Where(vcf => vcf.VerseChoreographyId == verseChoreographyId)
            .Include(vcf => vcf.FigureOption.Figure)
            .Include(vcf => vcf.FigureOption.EndPosition)
            .OrderByDescending(vcf => vcf.FigureOrder)
            .FirstOrDefaultAsync();

        if (lastfigure != null)
        {
            lastEndPosition = mapper.Map<PositionForPreviewViewModel>(lastfigure.FigureOption.EndPosition);
        }

        return lastEndPosition;
    }

    public async Task<int> GetNumberOfFiguresForVerseChoreographyAsync(int verseChoreorgaphyId)
    {
        VerseChoreography? choreography = await repository.AllAsReadOnly<VerseChoreography>()
            .Include(c => c.Figures)
            .FirstOrDefaultAsync(c => c.Id == verseChoreorgaphyId);

        if (choreography == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(VerseChoreography), verseChoreorgaphyId);
            throw new EntityNotFoundException();
        }

        return choreography.Figures.Count();
    }

    public async Task<int> GetNumberOfRemainingBeatsForVerseChoreographyAsync(int verseChoreorgaphyId)
    {
        VerseChoreography? choreography = await repository.AllAsReadOnly<VerseChoreography>()
            .Include(c => c.Figures)
                .ThenInclude(f => f.FigureOption)
            .Include(c => c.VerseType)
            .FirstOrDefaultAsync(c => c.Id == verseChoreorgaphyId);

        if (choreography == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(VerseChoreography), verseChoreorgaphyId);
            throw new EntityNotFoundException();
        }

        return choreography.VerseType.BeatCounts - (choreography.Figures.Sum(f => f.FigureOption.BeatCounts));
    }

    public async Task<string?> GetStartPositionNameForVerseChoreographyAsync(int verseChoreorgaphyId)
    {
        VerseChoreography? choreography = await repository.AllAsReadOnly<VerseChoreography>()
            .Include(c => c.Figures)
                .ThenInclude(f => f.FigureOption.StartPosition)
            .FirstOrDefaultAsync(c => c.Id == verseChoreorgaphyId);

        if (choreography == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(VerseChoreography), verseChoreorgaphyId);
            throw new EntityNotFoundException();
        }

        return choreography.Figures.OrderBy(f => f.FigureOrder).FirstOrDefault()?.FigureOption.StartPosition.Name;
    }

    public async Task<VerseChoreographyQueryServiceModel> AllUserVerseChoreographiesAsync(string userId, string? searchTerm = null, int? searchedVerseTypeId = null, int? searchedStartPositionId = null, int? searchedEndPositionId = null, int? searchedFinalFigureId = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage)
    {
        var choreographiesToShow = repository.AllAsReadOnly<VerseChoreography>()
             .Where(c => c.UserId == userId);

        if (searchTerm != null)
        {
            string normalizedSearchTerm = searchTerm.ToLower();
            choreographiesToShow = choreographiesToShow
                .Where(f => f.Name.ToLower().Contains(normalizedSearchTerm));
        }

        if (searchedVerseTypeId != null)
        {
            choreographiesToShow = choreographiesToShow
                .Where(c => c.VerseTypeId == searchedVerseTypeId);
        }

        if (searchedStartPositionId != null)
        {
            choreographiesToShow = choreographiesToShow
                .Where(c => c.Figures.OrderBy(f => f.FigureOrder).Select(f => f.FigureOption.StartPositionId == searchedStartPositionId).FirstOrDefault());
        }

        if (searchedEndPositionId != null)
        {
            choreographiesToShow = choreographiesToShow
                 .Where(c => c.Figures.OrderByDescending(f => f.FigureOrder).Select(f => f.FigureOption.EndPositionId == searchedEndPositionId).FirstOrDefault());
        }

        if (searchedFinalFigureId != null)
        {
            choreographiesToShow = choreographiesToShow
                 .Where(c => c.Figures.OrderByDescending(f => f.FigureOrder).Select(f => f.FigureOption.FigureId == searchedFinalFigureId).FirstOrDefault());
        }

        var choreographies = await choreographiesToShow
             .Include(c => c.VerseType)
             .Include(c => c.FullChoreographies)
             .Include(c => c.Figures)
                .ThenInclude(cf => cf.FigureOption.Figure)
             .Include(c => c.Figures)
                .ThenInclude(cf => cf.FigureOption.StartPosition)
             .Include(c => c.Figures)
                .ThenInclude(cf => cf.FigureOption.EndPosition)
            .OrderBy(c => c.Id)
            .Skip((currentPage - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .Select(f => mapper.Map<VerseChoreographyTableViewModel>(f))
            .ToListAsync();

        int totalChoreographiesToShow = await choreographiesToShow.CountAsync();

        return new VerseChoreographyQueryServiceModel()
        {
            TotalCount = totalChoreographiesToShow,
            Entities = choreographies
        };
    }

    public async Task<IEnumerable<VerseChoreographyTableViewModel>> AllUserCompleteVerseChoreographiesStartingWithPositionAsync(string userId, int? startPositionId = null)
    {
        var choreographiesToShow = repository.AllAsReadOnly<VerseChoreography>()
             .Where(c => c.UserId == userId)
             .Where(c => (c.Figures.Sum(f => f.FigureOption.BeatCounts) == c.VerseType.BeatCounts));

        if (startPositionId != null)
        {
            choreographiesToShow = choreographiesToShow
                .Where(c => c.Figures.OrderBy(f => f.FigureOrder).Select(f => f.FigureOption.StartPositionId).FirstOrDefault() == startPositionId);
        }

        var result = await choreographiesToShow
                 .Include(c => c.VerseType)
                 .Include(c => c.FullChoreographies)
                 .Include(c => c.Figures)
                    .ThenInclude(cf => cf.FigureOption.Figure)
                 .Include(c => c.Figures)
                    .ThenInclude(cf => cf.FigureOption.StartPosition)
                 .Include(c => c.Figures)
                    .ThenInclude(cf => cf.FigureOption.EndPosition)
                .Select(c => mapper.Map<VerseChoreographyTableViewModel>(c))
                .ToListAsync();

        return result;
    }

    public async Task<bool> VerseChoreographyExistForThisUserByIdAsync(int id, string userId)
    {
        var verseChoreography = await repository.GetByIdAsync<VerseChoreography>(id);
        if (verseChoreography == null)
        {
            return false;
        }

        if (verseChoreography.UserId != userId)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> IsVerseChoreographyUsedInFullChoreographiesAsync(int id)
    {
        VerseChoreography? choreography = await repository.AllAsReadOnly<VerseChoreography>()
            .Include(vc => vc.FullChoreographies)
            .FirstOrDefaultAsync(vc => vc.Id == id);

        if (choreography == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(VerseChoreography), id);
            throw new EntityNotFoundException();
        }

        return choreography.FullChoreographies.Any();
    }

    public async Task<bool> IsVerseChoreographyCompleteAsync(int id)
    {
        VerseChoreography? choreography = await repository.AllAsReadOnly<VerseChoreography>()
            .Include(vc => vc.Figures)
                .ThenInclude(vcf => vcf.FigureOption)
            .Include(vc => vc.VerseType)
            .FirstOrDefaultAsync(vc => vc.Id == id);

        if (choreography == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(VerseChoreography), id);
            throw new EntityNotFoundException();
        }

        return choreography.Figures.Sum(f => f.FigureOption.BeatCounts) == choreography.VerseType.BeatCounts;
    }

    public async Task<bool> IsVerseChoreographyForFigureCompleteAsync(int verseChoreographyFigureId)
    {
        VerseChoreography? choreography = await repository.AllAsReadOnly<VerseChoreography>()
            .Include(vc => vc.Figures)
                .ThenInclude(vcf => vcf.FigureOption)
            .Include(vc => vc.VerseType)
            .FirstOrDefaultAsync(vc => vc.Figures.Any(f => f.Id == verseChoreographyFigureId));

        if (choreography == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(VerseChoreographyFigure), verseChoreographyFigureId);
            throw new EntityNotFoundException();
        }

        return choreography.Figures.Sum(f => f.FigureOption.BeatCounts) == choreography.VerseType.BeatCounts;
    }

    public async Task<int> AddVerseChoreographyAsync(VerseChoreographyFormViewModel model, string userId)
    {
        if (await repository.GetByIdAsync<IdentityUser>(userId) == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(IdentityUser), userId);
            throw new EntityNotFoundException();
        }

        VerseChoreography entity = mapper.Map<VerseChoreography>(model);
        entity.UserId = userId;

        await repository.AddAsync(entity);

        await repository.SaveChangesAsync();

        return entity.Id;
    }

    public async Task SaveVerseChoreographyAsync(VerseChoreographySaveViewModel model, string userId)
    {
        if (await repository.GetByIdAsync<IdentityUser>(userId) == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(IdentityUser), userId);
            throw new EntityNotFoundException();
        }

        var verseTypes = await repository.AllAsReadOnly<VerseType>().Select(v => v.Id).ToListAsync();

        if (!verseTypes.Contains(model.VerseTypeId))
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(VerseType), model.VerseTypeId);
            throw new EntityNotFoundException();
        }

        var figureOptionsForThisUser = await repository.AllAsReadOnly<FigureOption>().Where(o => o.Figure.UserId == userId).Select(o => o.Id).ToListAsync();

        if (model.Figures.Any(f => !figureOptionsForThisUser.Contains(f.FigureOptionId)))
        {
            logger.LogError(InvalidFigureOptionIdWhenSavingVerseChoreographyErrorMessage);
            throw new EntityNotFoundException();
        }

        VerseChoreography entity = mapper.Map<VerseChoreography>(model);
        entity.UserId = userId;

        await repository.AddAsync(entity);

        await repository.SaveChangesAsync();
    }

    public async Task ChangeFigureInVerseChoreographyAsync(int id, VerseChoreographyFigureSelectedReplacementServiceModel newFigure)
    {
        var choreography = await repository.AllAsReadOnly<VerseChoreography>()
            .Include(vc => vc.Figures)
                .ThenInclude(f => f.FigureOption)
            .FirstOrDefaultAsync(vc => vc.Id == id);

        if (choreography == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(VerseChoreography), id);
            throw new EntityNotFoundException();
        }

        var newFigureOption = await repository.AllAsReadOnly<FigureOption>()
            .Include(f => f.Figure)
            .FirstOrDefaultAsync(fo => fo.Id == newFigure.FigureOptionId);

        if (newFigureOption == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(FigureOption), newFigure.FigureOptionId);
            throw new EntityNotFoundException();
        }

        if (choreography.UserId != newFigureOption.Figure.UserId)
        {
            logger.LogError(InvalidFigureOptionIdWhenSavingVerseChoreographyErrorMessage);
            throw new EntityNotFoundException();
        }

        var currentFigureOption = await repository.All<VerseChoreographyFigure>()
            .Include(vcf => vcf.FigureOption)
            .Where(vcf => vcf.VerseChoreographyId == id)
            .FirstOrDefaultAsync(vcf => vcf.FigureOrder == newFigure.FigureOrder);

        if (currentFigureOption == null)
        {
            logger.LogError(InvalidFigureOrderWhenUpdatingVerseChoreographyErrorMessage);
            throw new EntityNotFoundException();
        }

        if ((newFigureOption.StartPositionId != currentFigureOption.FigureOption.StartPositionId)
            || (newFigureOption.EndPositionId != currentFigureOption.FigureOption.EndPositionId)
            || (newFigureOption.BeatCounts != currentFigureOption.FigureOption.BeatCounts))
        {
            logger.LogError(NewFigureDoesNotMatchOldFigureDataErrorMessage);
            throw new InvalidModelException();
        }

        currentFigureOption.FigureOptionId = newFigureOption.Id;
        await repository.SaveChangesAsync();
    }

    public async Task DeleteVerseChoreographyAsync(int id)
    {
        List<VerseChoreographyFigure> figures = await repository.All<VerseChoreographyFigure>()
            .Where(vcf => vcf.VerseChoreographyId == id)
            .ToListAsync();
        foreach (VerseChoreographyFigure figure in figures)
        {
            await repository.DeleteAsync<VerseChoreographyFigure>(figure.Id);
        }

        await repository.DeleteAsync<VerseChoreography>(id);
        await repository.SaveChangesAsync();
    }

    public async Task<IList<VerseChoreographySaveViewModel>> GenerateChoreographies(VerseChoreographyGenerateModel query, string userId)
    {
        var verseType = await repository.GetByIdAsync<VerseType>(query.VerseTypeId);

        if (verseType == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(VerseType), query.VerseTypeId);
            throw new EntityNotFoundException();
        }

        int totalNumberOfBeats = verseType.BeatCounts;

        var finalFigure = await repository.AllAsReadOnly<Figure>()
            .Include(f => f.FigureOptions)
                .ThenInclude(fo => fo.StartPosition)
            .Include(f => f.FigureOptions)
                .ThenInclude(fo => fo.EndPosition)
            .FirstOrDefaultAsync(f => f.Id == query.FinalFigureId);

        if (finalFigure == null)
        {
            logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(Figure), query.FinalFigureId);
            throw new EntityNotFoundException();
        }

        if (query.StartPositionId != null)
        {
            startPositionForGenerateChoreographyId = query.StartPositionId;
        }

        allUserNonHighlightFigureOptions = await repository.AllAsReadOnly<FigureOption>()
            .Include(fo => fo.Figure)
            .Include(fo => fo.StartPosition)
            .Include(fo => fo.EndPosition)
            .Where(fo => fo.Figure.UserId == userId)
            .Where(fo => fo.Figure.IsHighlight == false)
            .ToListAsync();

        //Generate all posible choreographies and save them in the list validOptionsForGenerateChoreographies
        List<FigureOption> finalFigureOptions = finalFigure.FigureOptions
            .Where(fr => fr.BeatCounts <= totalNumberOfBeats)
            .ToList();
        foreach (FigureOption finalFigureOption in finalFigureOptions)
        {
            List<FigureOption> currentOption = new List<FigureOption>();
            currentOption.Add(finalFigureOption);
            int remainingNumberOfBeats = totalNumberOfBeats - finalFigureOption.BeatCounts;
            int nextFigureId = finalFigureOption.FigureId;
            GetPreviousFigure(currentOption, finalFigureOption.StartPositionId, remainingNumberOfBeats, nextFigureId);
        }

        //Calculate score of each of the generated choreographies and leave top options
        IList<VerseChoreographyWithScoreServiceModel> topOptionsWithScore = LeaveOnlyTopRatedSuggestedChoreographiesWithOrderedFigures(MaxNumberOfSuggestedChoreographies);

        //Make projection for the left choreographies
        List<VerseChoreographySaveViewModel> suggestedOptions = new List<VerseChoreographySaveViewModel>();
        for (int i = 0; i < topOptionsWithScore.Count; i++)
        {
            VerseChoreographySaveViewModel suggestedOption = new VerseChoreographySaveViewModel()
            {
                VerseTypeId = verseType.Id,
                Score = topOptionsWithScore[i].Score,
                Figures = new List<VerseChoreographyFigureViewModel>()
            };

            for (int j = 0; j < topOptionsWithScore[i].Choreography.Count; j++)
            {
                VerseChoreographyFigureViewModel figure = mapper.Map<VerseChoreographyFigureViewModel>(topOptionsWithScore[i].Choreography[j]);
                figure.FigureOrder = j + 1;

                suggestedOption.Figures.Add(figure);
            }

            suggestedOptions.Add(suggestedOption);
        }

        return suggestedOptions;
    }

    /// <summary>
    /// A recursively used method to generate options for previous figure in a verse when generating suggestions for choreographies.
    /// </summary>
    /// <param name="currentOption">The list of the selected already figure options for this current option for choreography.</param>
    /// <param name="endPositionId">The start position of the last figure option, which is end position for the searched previous figure option.</param>
    /// <param name="remainingNumberOfBeats">The remaining number of beats in the verse.</param>
    /// <param name="nextFigureId">The last selected figure for the verse, which will be the next figure from the searched previous figure.</param>
    private void GetPreviousFigure(List<FigureOption> currentOption, int endPositionId, int remainingNumberOfBeats, int nextFigureId)
    {
        //This is how the recursion ends
        if (remainingNumberOfBeats == 0 &&
            (startPositionForGenerateChoreographyId == null || startPositionForGenerateChoreographyId == endPositionId))
        {
            validOptionsForGenerateChoreographies.Add(currentOption);
        }

        List<FigureOption> validOptionsForPreviousFigure = allUserNonHighlightFigureOptions
            .Where(fo => fo.EndPositionId == endPositionId)
            .Where(fo => fo.BeatCounts <= remainingNumberOfBeats)
            .Where(fo => fo.FigureId != nextFigureId)
            .ToList();

        foreach (FigureOption validOptionForPreviousFigure in validOptionsForPreviousFigure)
        {
            List<FigureOption> newCurrentOption = new List<FigureOption>();
            foreach (FigureOption option in currentOption)
            {
                newCurrentOption.Add(option);
            }

            newCurrentOption.Add(validOptionForPreviousFigure);
            int newEndPositionId = validOptionForPreviousFigure.StartPositionId;
            int newRemainingNumberOfBeats = remainingNumberOfBeats - validOptionForPreviousFigure.BeatCounts;
            int newNextFigureId = validOptionForPreviousFigure.FigureId;

            GetPreviousFigure(newCurrentOption, newEndPositionId, newRemainingNumberOfBeats, newNextFigureId);
        }
    }

    /// <summary>
    /// Returns only the top scored verse choreographies with theirs scores as a collection.
    /// </summary>
    /// <param name="maxNumberOfOptions">The max number of choreographies that should remain.</param>
    /// <returns></returns>
    private IList<VerseChoreographyWithScoreServiceModel> LeaveOnlyTopRatedSuggestedChoreographiesWithOrderedFigures(int maxNumberOfOptions)
    {
        List<VerseChoreographyWithScoreServiceModel> choreographies = new List<VerseChoreographyWithScoreServiceModel>();
        foreach (List<FigureOption> choreography in validOptionsForGenerateChoreographies)
        {
            int score = CalculateChoreographyScore(choreography);

            VerseChoreographyWithScoreServiceModel currentOption = new VerseChoreographyWithScoreServiceModel(score, choreography);
            choreographies.Add(currentOption);
        }

        choreographies = choreographies.OrderByDescending(c => c.Score).ThenBy(c => c.Choreography.Count()).Take(maxNumberOfOptions).ToList();
        for (int i = 0; i < choreographies.Count; i++)
        {
            choreographies[i].Choreography = choreographies[i].Choreography.Reverse().ToList();
        }

        return choreographies;
    }

    /// <summary>
    /// Calculates a score for a choreography based on various criteria.
    /// </summary>
    /// <param name="choreography">Choreography as a list of figure options</param>
    /// <returns></returns>
    private int CalculateChoreographyScore(List<FigureOption> choreography)
    {
        int score = 0;
        score += choreography.Where(fo => fo.Figure.IsFavourite).Count();

        int numberOfSlowFigures = choreography.Where(fo => fo.DynamicsType == DynamicsType.Slow).Count();
        int numberOfFastFigures = choreography.Where(fo => fo.DynamicsType == DynamicsType.Fast).Count();

        if (numberOfSlowFigures >= 2)
        {
            score -= 1;
        }

        if (numberOfFastFigures >= 3)
        {
            score -= 1;
        }

        if (numberOfFastFigures >= 1 && numberOfSlowFigures >= 1)
        {
            score += 3;
        }

        int numberOfNonStandartFigures = choreography.Where(fo => fo.BeatCounts < 6 || fo.BeatCounts > 8).Count();
        if (numberOfNonStandartFigures > 2)
        {
            score -= 1;
        }

        int numberOfDifferentStartPositions = choreography.Select(fo => fo.StartPositionId).Distinct().Count();
        int numberOfDifferentEndPositions = choreography.Select(fo => fo.EndPositionId).Distinct().Count();

        score += Math.Min(numberOfDifferentStartPositions, 3);
        score += Math.Min(numberOfDifferentEndPositions, 3);

        return score;
    }
}
