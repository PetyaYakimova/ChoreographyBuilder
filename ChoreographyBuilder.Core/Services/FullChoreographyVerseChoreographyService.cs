using AutoMapper;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Models.FullChoreographyVerseChoreography;
using ChoreographyBuilder.Infrastructure.Data.Common;
using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static ChoreographyBuilder.Core.Constants.MessageConstants;

namespace ChoreographyBuilder.Core.Services
{
	public class FullChoreographyVerseChoreographyService : IFullChoreographyVerseChoreographyService
	{
		private readonly ILogger<FullChoreographyVerseChoreographyService> logger;
		private readonly IRepository repository;
		private readonly IMapper mapper;

		public FullChoreographyVerseChoreographyService(ILogger<FullChoreographyVerseChoreographyService> logger, IRepository repository, IMapper mapper)
		{
			this.logger = logger;
			this.repository = repository;
			this.mapper = mapper;
		}

		public async Task<FullChoreographyVerseChoreographyDeleteViewModel> GetVerseChoreographyForDeleteAsync(int fullChoreographyVerseChoreographyId)
		{
			var choreography = await repository.AllAsReadOnly<FullChoreographyVerseChoreography>()
				.Include(fcvc => fcvc.VerseChoreography)
				.Include(fcvc => fcvc.FullChoreography)
				.FirstOrDefaultAsync(fo => fo.Id == fullChoreographyVerseChoreographyId);

			if (choreography == null)
			{
				logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(FullChoreographyVerseChoreography), fullChoreographyVerseChoreographyId);
				throw new EntityNotFoundException();
			}

			return mapper.Map<FullChoreographyVerseChoreographyDeleteViewModel>(choreography);
		}

		public async Task<bool> VerseChoreographyInFullChoreographyExistForThisUserByIdAsync(int fullChoreographyVerseChoreographyId, string userId)
		{
			FullChoreographyVerseChoreography? verseChoreography = await repository.AllAsReadOnly<FullChoreographyVerseChoreography>()
				.Include(vs => vs.FullChoreography)
				.FirstOrDefaultAsync(vs => vs.Id == fullChoreographyVerseChoreographyId);

			if (verseChoreography == null)
			{
				return false;
			}

			if (verseChoreography.FullChoreography.UserId != userId)
			{
				return false;
			}

			return true;
		}

		public async Task<bool> VerseChoreographyIsLastForFullChoreographyByIdAsync(int fullChoreographyVerseChoreographyId)
		{
			FullChoreographyVerseChoreography? verseChoreography = await repository.AllAsReadOnly<FullChoreographyVerseChoreography>()
				.Include(vs => vs.FullChoreography)
				.FirstOrDefaultAsync(vs => vs.Id == fullChoreographyVerseChoreographyId);

			if (verseChoreography == null)
			{
				return false;
			}

			FullChoreographyVerseChoreography? lastVerseChoreographyForTheFullChoreography = await repository.AllAsReadOnly<FullChoreographyVerseChoreography>()
				.Where(vs => vs.FullChoreographyId == verseChoreography.FullChoreographyId)
				.OrderByDescending(vs => vs.VerseChoreographyOrder)
				.FirstOrDefaultAsync();

			if (lastVerseChoreographyForTheFullChoreography == null || lastVerseChoreographyForTheFullChoreography.Id != fullChoreographyVerseChoreographyId)
			{
				return false;
			}

			return true;
		}

		public async Task AddVerseChoreographyToFullChoreographyAsync(int fullChoreographyId, FullChoreographyVerseChoreographyFormViewModel model)
		{
			if (await repository.GetByIdAsync<FullChoreography>(fullChoreographyId) == null)
			{
				logger.LogError(EntityWithIdWasNotFoundLoggerErrorMessage, nameof(FullChoreography), fullChoreographyId);
				throw new EntityNotFoundException();
			}

			FullChoreographyVerseChoreography entity = mapper.Map<FullChoreographyVerseChoreography>(model);
			entity.FullChoreographyId = fullChoreographyId;

			await repository.AddAsync(entity);

			await repository.SaveChangesAsync();
		}

		public async Task DeleteVerseChoreographyFromFullChoreographyAsync(int fullChoreographyVerseChoreographyId)
		{
			await repository.DeleteAsync<FullChoreographyVerseChoreography>(fullChoreographyVerseChoreographyId);
			await repository.SaveChangesAsync();
		}
	}
}
