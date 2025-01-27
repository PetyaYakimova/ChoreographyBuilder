using AutoMapper;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Models.FullChoreographyVerseChoreography;
using ChoreographyBuilder.Core.Models.VerseChoreographyFigure;
using ChoreographyBuilder.Infrastructure.Data.Common;
using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Core.Services
{
    public class VerseChoreographyFigureService : IVerseChoreographyFigureService
    {
        private readonly ILogger<VerseChoreographyFigureService> logger;
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public VerseChoreographyFigureService(ILogger<VerseChoreographyFigureService> logger, IRepository repository, IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<VerseChoreographyFigureReplaceViewModel> GetVerseChoreographyFigureForReplaceAsync(int verseChoreographyFigureId)
        {
            var option = await repository.AllAsReadOnly<VerseChoreographyFigure>()
                .Include(vcf => vcf.FigureOption)
                    .ThenInclude(fo => fo.Figure)
                .Include(vcf => vcf.FigureOption)
                    .ThenInclude(fo => fo.StartPosition)
                .Include(vcf => vcf.FigureOption)
                    .ThenInclude(fo => fo.EndPosition)
                .FirstOrDefaultAsync(fo => fo.Id == verseChoreographyFigureId);

            if (option == null)
            {
                logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(VerseChoreographyFigure), verseChoreographyFigureId);
                throw new EntityNotFoundException();
            }

            return mapper.Map<VerseChoreographyFigureReplaceViewModel>(option);
        }

        public async Task<IEnumerable<VerseChoreographyFigureViewModel>> GetPossibleReplacementsForVerseChoreographyFigureAsync(int verseChoreographyFigureId)
        {
            var currentFigure = await repository.AllAsReadOnly<VerseChoreographyFigure>()
                .Include(vcf => vcf.VerseChoreography)
                .Include(vcf => vcf.FigureOption)
                .FirstOrDefaultAsync(vcf => vcf.Id == verseChoreographyFigureId);

            if (currentFigure == null)
            {
                logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(VerseChoreographyFigure), verseChoreographyFigureId);
                throw new EntityNotFoundException();
            }

            var result = await repository.AllAsReadOnly<FigureOption>()
                .Where(fo => fo.Figure.UserId == currentFigure.VerseChoreography.UserId)
                .Where(fo => fo.FigureId != currentFigure.FigureOption.FigureId)
                .Where(fo => fo.StartPositionId == currentFigure.FigureOption.StartPositionId)
                .Where(fo => fo.EndPositionId == currentFigure.FigureOption.EndPositionId)
                .Where(fo => fo.BeatCounts == currentFigure.FigureOption.BeatCounts)
                .Include(f => f.Figure)
                .Include(f => f.StartPosition)
                .Include(f => f.EndPosition)
                .ToListAsync();

            return mapper.Map<List<VerseChoreographyFigureViewModel>>(result);
        }

        public async Task<VerseChoreographyFigureDeleteViewModel> GetFigureForDeleteAsync(int verseChoreographyFigureId)
        {
            var figure = await repository.AllAsReadOnly<VerseChoreographyFigure>()
                .Include(vcf => vcf.VerseChoreography)
                .Include(vcf => vcf.FigureOption.Figure)
                .FirstOrDefaultAsync(vcf => vcf.Id == verseChoreographyFigureId);

            if (figure == null)
            {
                logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(VerseChoreographyFigure), verseChoreographyFigureId);
                throw new EntityNotFoundException();
            }

            return mapper.Map<VerseChoreographyFigureDeleteViewModel>(figure);
        }

        public async Task<int> GetVerseChoreographyIdForVerseChoreographyFigureByIdAsync(int verseChoreographyFigureId)
        {
            var verseChoreographyFigure = await repository.GetByIdAsync<VerseChoreographyFigure>(verseChoreographyFigureId);

            if (verseChoreographyFigure == null)
            {
                logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(VerseChoreographyFigure), verseChoreographyFigureId);
                throw new EntityNotFoundException();
            }

            return verseChoreographyFigure.VerseChoreographyId;
        }

        public async Task<bool> VerseChoreographyFigureExistForThisUserByIdAsync(int verseChoreographyFigureId, string userId)
        {
            VerseChoreographyFigure? verseChoreographyFigure = await repository.AllAsReadOnly<VerseChoreographyFigure>()
                .Include(vcf => vcf.VerseChoreography)
                .FirstOrDefaultAsync(vcf => vcf.Id == verseChoreographyFigureId);

            if (verseChoreographyFigure == null)
            {
                return false;
            }

            if (verseChoreographyFigure.VerseChoreography.UserId != userId)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> FigureIsLastForVerseChoreographyByIdAsync(int verseChoreographyFigureId)
        {
            VerseChoreographyFigure? figure = await repository.AllAsReadOnly<VerseChoreographyFigure>()
                .Include(vcf => vcf.VerseChoreography)
                .FirstOrDefaultAsync(vcf => vcf.Id == verseChoreographyFigureId);

            if (figure == null)
            {
                return false;
            }

            VerseChoreographyFigure? lastfigureForTheVerseChoreography = await repository.AllAsReadOnly<VerseChoreographyFigure>()
                .Where(vcf => vcf.VerseChoreographyId == figure.VerseChoreographyId)
                .OrderByDescending(vcf => vcf.FigureOrder)
                .FirstOrDefaultAsync();

            if (lastfigureForTheVerseChoreography == null || lastfigureForTheVerseChoreography.Id != verseChoreographyFigureId)
            {
                return false;
            }

            return true;
        }

        public async Task AddFigureToVerseChoreographyAsync(int verseChoreographyId, VerseChoreographyFigureOptionFormViewModel model)
        {
            var verseChoreography = await repository.GetByIdAsync<VerseChoreography>(verseChoreographyId);
            if (verseChoreography == null)
            {
                logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(VerseChoreography), verseChoreographyId);
                throw new EntityNotFoundException();
            }

            var figureOption = await repository.AllAsReadOnly<FigureOption>()
                .Include(fo => fo.Figure)
                .FirstOrDefaultAsync(fo => fo.Id == model.FigureOptionId);
            if (figureOption == null)
            {
                logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(FigureOption), model.FigureOptionId);
                throw new EntityNotFoundException();
            }

            if (verseChoreography.UserId != figureOption.Figure.UserId)
            {
                logger.LogError(UserForTheFigureAndForTheVerseChoreographyIsNotTheSameLoggerErrorMessage);
                throw new InvalidModelException(UserForTheFigureAndForTheVerseChoreographyIsNotTheSameLoggerErrorMessage);
            }

            VerseChoreographyFigure entity = mapper.Map<VerseChoreographyFigure>(model);
            entity.VerseChoreographyId = verseChoreographyId;

            await repository.AddAsync(entity);

            await repository.SaveChangesAsync();
        }

        public async Task DeleteFigureFromVerseChoreographyAsync(int verseChoreographyFigureId)
        {
            await repository.DeleteAsync<VerseChoreographyFigure>(verseChoreographyFigureId);
            await repository.SaveChangesAsync();
        }
    }
}
