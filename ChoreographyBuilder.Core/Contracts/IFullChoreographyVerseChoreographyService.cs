using ChoreographyBuilder.Core.Models.FullChoreographyVerseChoreography;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IFullChoreographyVerseChoreographyService
	{
		Task AddVerseChoreographyToFullChoreographyAsync(int fullChoreographyId, FullChoreographyVerseChoreographyFormViewModel model);

		Task<bool> VerseChoreographyInFullChoreographyExistForThisUserByIdAsync(int figureChoreographyVerseChoreographyId, string userId);

		Task<bool> VerseChoreographyIsLastForFullChoreographyByIdAdync(int figureChoreographyVerseChoreographyId);

		Task<FullChoreographyVerseChoreographyDeleteViewModel?> GetVerseChoreographyForDeleteAsync(int figureChoreographyVerseChoreographyId);

		Task DeleteAsync(int figureChoreographyVerseChoreographyId);
	}
}
