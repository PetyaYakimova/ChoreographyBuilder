using AutoMapper;
using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Exceptions;
using ChoreographyBuilder.Core.Models.FigureOption;
using ChoreographyBuilder.Core.Models.FullChoreographyVerseChoreography;
using ChoreographyBuilder.Infrastructure.Data.Common;
using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChoreographyBuilder.Core.Services
{
	public class FullChoreographyVerseChoreographyService : IFullChoreographyVerseChoreographyService
	{
		private readonly IRepository repository;
		private readonly IMapper mapper;

		public FullChoreographyVerseChoreographyService(IRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		public async Task AddVerseChoreographyToFullChoreographyAsync(int fullChoreographyId, FullChoreographyVerseChoreographyFormViewModel model)
		{
			FullChoreographyVerseChoreography entity = mapper.Map<FullChoreographyVerseChoreography>(model);
			entity.FullChoreographyId = fullChoreographyId;

			await repository.AddAsync(entity);

			await repository.SaveChangesAsync();
		}

		public async Task DeleteAsync(int figureChoreographyVerseChoreographyId)
		{
			await repository.DeleteAsync<FullChoreographyVerseChoreography>(figureChoreographyVerseChoreographyId);
			await repository.SaveChangesAsync();
		}

		public async Task<FullChoreographyVerseChoreographyDeleteViewModel?> GetVerseChoreographyForDeleteAsync(int figureChoreographyVerseChoreographyId)
		{
			var choreography = await repository.AllAsReadOnly<FullChoreographyVerseChoreography>()
				.Include(fcvc => fcvc.VerseChoreography)
				.Include(fcvc => fcvc.FullChoreography)
				.FirstOrDefaultAsync(fo => fo.Id == figureChoreographyVerseChoreographyId);

			if (choreography == null)
			{
				throw new EntityNotFoundException();
			}

			return mapper.Map<FullChoreographyVerseChoreographyDeleteViewModel?>(choreography);
		}

		public async Task<bool> VerseChoreographyInFullChoreographyExistForThisUserByIdAsync(int figureChoreographyVerseChoreographyId, string userId)
		{
			FullChoreographyVerseChoreography? verseChoreography = await repository.AllAsReadOnly<FullChoreographyVerseChoreography>()
				.Include(vs => vs.FullChoreography)
				.FirstOrDefaultAsync(vs => vs.Id == figureChoreographyVerseChoreographyId);

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

		public async Task<bool> VerseChoreographyIsLastForFullChoreographyByIdAdync(int figureChoreographyVerseChoreographyId)
		{
			FullChoreographyVerseChoreography? verseChoreography = await repository.AllAsReadOnly<FullChoreographyVerseChoreography>()
				.Include(vs => vs.FullChoreography)
				.FirstOrDefaultAsync(vs => vs.Id == figureChoreographyVerseChoreographyId);

			if (verseChoreography == null)
			{
				return false;
			}

			FullChoreographyVerseChoreography? lastVerseChoreographyForTheFullChoreography = await repository.AllAsReadOnly<FullChoreographyVerseChoreography>()
				.Where(vs => vs.FullChoreographyId == verseChoreography.FullChoreographyId)
				.OrderByDescending(vs => vs.VerseChoreographyOrder)
				.FirstOrDefaultAsync();

			if (lastVerseChoreographyForTheFullChoreography == null || lastVerseChoreographyForTheFullChoreography.Id != figureChoreographyVerseChoreographyId)
			{
				return false;
			}

			return true;
		}
	}
}
