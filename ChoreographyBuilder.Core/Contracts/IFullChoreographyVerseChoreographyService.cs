using ChoreographyBuilder.Core.Models.FullChoreographyVerseChoreography;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IFullChoreographyVerseChoreographyService
	{
		/// <summary>
		/// Returns VerseCHoreographyFullChoreographyDeleteViewModel of a full choreography verse choreography mapping record with the selected id. 
		/// Throws an exception if there is no such full choreography verse choreorgaphy record with this id.
		/// </summary>
		/// <param name="fullChoreographyVerseChoreographyId">Id of the full choreography verse choreorgaphy mapping record</param>
		/// <returns></returns>
		Task<FullChoreographyVerseChoreographyDeleteViewModel> GetVerseChoreographyForDeleteAsync(int fullChoreographyVerseChoreographyId);

		/// <summary>
		/// Returns true if the full choreography verse choreography record is for full choreography that is for this user.
		/// Returns false if the full choreography verse choreography record doesn't exist at all, or if the full choreorgaphy is for another user.
		/// </summary>
		/// <param name="fullChoreographyVerseChoreographyId">Id of the full choreography verse choreorgaphy mapping record</param>
		/// <param name="userId">Id of the user</param>
		/// <returns></returns>
		Task<bool> VerseChoreographyInFullChoreographyExistForThisUserByIdAsync(int fullChoreographyVerseChoreographyId, string userId);

		/// <summary>
		/// Returns true if the full choreography verse choreography record is the last in order in the full choreography.
		/// Returns false if the full choreography verse choreography record doesn't exist at all, or if is not the last for the full choreography.
		/// </summary>
		/// <param name="fullChoreographyVerseChoreographyId">Id of the full choreography verse choreorgaphy mapping record</param>
		/// <returns></returns>
		Task<bool> VerseChoreographyIsLastForFullChoreographyByIdAdync(int fullChoreographyVerseChoreographyId);

		/// <summary>
		/// Adds a new full choreorgaphy verse choreography record with data from the model.
		/// Throws an exception if a full choreography with the given id doesn't exist.
		/// </summary>
		/// <param name="fullChoreographyId">Id of the full choreography</param>
		/// <param name="model"></param>
		/// <returns></returns>
		Task AddVerseChoreographyToFullChoreographyAsync(int fullChoreographyId, FullChoreographyVerseChoreographyFormViewModel model);

		/// <summary>
		/// Deletes a full choreography verse choreography record by its id.
		/// </summary>
		/// <param name="fullChoreographyVerseChoreographyId">Id of the full choreography verse choreorgaphy mapping record</param>
		/// <returns></returns>
		Task DeleteVerseChoreographyFromFullChoreographyAsync(int fullChoreographyVerseChoreographyId);
	}
}
