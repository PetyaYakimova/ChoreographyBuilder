using AutoMapper;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
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
                .Where(fo=>fo.FigureId!=currentFigure.FigureOption.FigureId)
				.Where(fo => fo.StartPositionId == currentFigure.FigureOption.StartPositionId)
				.Where(fo => fo.EndPositionId == currentFigure.FigureOption.EndPositionId)
				.Where(fo => fo.BeatCounts == currentFigure.FigureOption.BeatCounts)
				.Include(f => f.Figure)
				.Include(f => f.StartPosition)
				.Include(f => f.EndPosition)
				.ToListAsync();

			return mapper.Map<List<VerseChoreographyFigureViewModel>>(result);
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
	}
}
