using ChoreographyBuilder.Core.Models.FullChoreographyVerseChoreography;
using ChoreographyBuilder.Core.Models.VerseChoreographyFigure;

namespace ChoreographyBuilder.Core.Contracts
{
    public interface IVerseChoreographyFigureService
	{
		/// <summary>
		/// Returns the verse choreography figure replace model of a verse choreography figure with the selected id. 
		/// Throws an exception if there is no such verse choreography figure with this id.
		/// </summary>
		/// <param name="verseChoreographyFigureId">Id of the verse choreography figure</param>
		/// <returns></returns>
		Task<VerseChoreographyFigureReplaceViewModel> GetVerseChoreographyFigureForReplaceAsync(int verseChoreographyFigureId);

		/// <summary>
		/// Returns a collection with all figures that could replace the given verse choreography figure.
		/// Throws an exception if there is no such verse choreography figure with this id.
		/// </summary>
		/// <param name="verseChoreographyFigureId">Id of the verse choreography figure</param>
		/// <returns></returns>
		Task<IEnumerable<VerseChoreographyFigureViewModel>> GetPossibleReplacementsForVerseChoreographyFigureAsync(int verseChoreographyFigureId);

		/// <summary>
		/// Returns the verse choreography id of the given verse choreography figure. 
		/// Throws an exception if there is no such verse choreography figure with this id.
		/// </summary>
		/// <param name="verseChoreographyFigureId">>Id of the verse choreography figure</param>
		/// <returns></returns>
		Task<int> GetVerseChoreographyIdForVerseChoreographyFigureByIdAsync(int verseChoreographyFigureId);

		/// <summary>
		/// Returns true if the verse choreography figure is linked to a verse choreography that is for this user. 
		/// Returns false if the verse choreography figure doesn't exist at all, or if it is for another user.
		/// </summary>
		/// <param name="verseChoreographyFigureId">Id of the verse choreography figure</param>
		/// <param name="userId">Id of the user</param>
		/// <returns></returns>
		Task<bool> VerseChoreographyFigureExistForThisUserByIdAsync(int verseChoreographyFigureId, string userId);

        /// <summary>
        /// Returns true if the verse choreography figure record is the last in order in the verse choreography.
        /// Returns false if the verse choreography figure record doesn't exist at all, or if is not the last for the verse choreography.
        /// </summary>
        /// <param name="verseChoreographyFigureId">Id of the verse choreography figure mapping record</param>
        /// <returns></returns>
        Task<bool> FigureIsLastForVerseChoreographyByIdAsync(int verseChoreographyFigureId);

        /// <summary>
        /// Adds a new verse choreography figure record with data from the model.
        /// Throws an exception if a verse choreography with the given id doesn't exist or if the given figure option id doesn't exist.
        /// Throws an exception if the user of the verse choreography and the figure option are not the same.
        /// </summary>
        /// <param name="verseChoreographyId">Id of the verse choreography</param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task AddFigureToVerseChoreographyAsync(int verseChoreographyId, VerseChoreographyFigureOptionFormViewModel model);
    }
}
