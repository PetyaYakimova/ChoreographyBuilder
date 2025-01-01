using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Core.Models.VerseChoreography;
using ChoreographyBuilder.Core.Models.VerseChoreographyFigure;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Contracts
{
    public interface IVerseChoreographyService
    {
        /// <summary>
        /// Returns VerseChoreographyDetailsViewModel for the verse choreography with the selected id.
        /// Throws an exception if there is no such verse choreography with this id.
        /// </summary>
        /// <param name="id">Id of the verse choreography</param>
        /// <returns></returns>
        Task<VerseChoreographyDetailsViewModel> GetVerseChoreographyByIdAsync(int id);

        /// <summary>
        /// Returns VerseChoreographyDeleteViewModel for the verse choreography with the selected id.
        /// Throws an exception if there is no such verse choreography with this id.
        /// </summary>
        /// <param name="id">Id of the verse choreography</param>
        /// <returns></returns>
        Task<VerseChoreographyDeleteViewModel> GetVerseChoreographyForDeleteAsync(int id);

        /// <summary>
        /// Returns the end position of the last figure in the verse choreography. 
        /// Returns null if the verse choreography doesn't have any figures or if the verse choreography doesn't exist at all.
        /// </summary>
        /// <param name="verseChoreographyId">Id of the verse choreography</param>
        /// <returns></returns>
        Task<PositionForPreviewViewModel?> GetLastFigureEndPositionAsync(int verseChoreographyId);

        /// <summary>
        /// Returns the number of figures in the verse choreography.
        /// Throws an exception if there is no such choreography with this id.
        /// </summary>
        /// <param name="verseChoreographyId">Id of the verse choreogrpahy</param>
        /// <returns></returns>
        Task<int> GetNumberOfFiguresForVerseChoreographyAsync(int verseChoreographyId);

        /// <summary>
        /// Gets the verse choreographies for the user by the selected search criteria and returns only those of them that should be displayed on the given page.
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="searchTerm"></param>
        /// <param name="searchedVerseTypeId"></param>
        /// <param name="searchedStartPositionId"></param>
        /// <param name="searchedEndPositionId"></param>
        /// <param name="searchedFinalFigureId"></param>
        /// <param name="currentPage"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        Task<VerseChoreographyQueryServiceModel> AllUserVerseChoreographiesAsync(string userId, string? searchTerm = null, int? searchedVerseTypeId = null, int? searchedStartPositionId = null, int? searchedEndPositionId = null, int? searchedFinalFigureId = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage);

        /// <summary>
        /// Returns a collection with all the verse choreographies for the user that start with a certain start position.
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="startPositionId">Id of the start position</param>
        /// <returns></returns>
        Task<IEnumerable<VerseChoreographyTableViewModel>> AllUserVerseChoreographiesStartingWithPositionAsync(string userId, int? startPositionId = null);

        /// <summary>
        /// Returns true if the verse choreography is for this user.
        /// Returns false if the verse choreography doesn't exist at all, or if it is for another user.
        /// </summary>
        /// <param name="id">Id of the verse choreography</param>
        /// <param name="userId">Id of the user</param>
        /// <returns></returns>
        Task<bool> VerseChoreographyExistForThisUserByIdAsync(int id, string userId);

        /// <summary>
        /// Returns true if there is at least one full choreography that uses this verse choreography 
        /// Returns false if the verse choreography is not used for any full choreography. 
        /// Throws an exception if there is no such verse choreography with this id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsVerseChoreographyUsedInFullChoreographies(int id);

        /// <summary>
        /// Adds a new verse choreography for the user with data from the model.
        /// Throws an exception if a user with the given id doesn't exist.
        /// </summary>
        /// <param name="model">FullChoreographyFormViewModel model</param>
        /// <param name="userId">Id of the user</param>
        /// <returns></returns>
        Task<int> AddVerseChoreographyAsync(VerseChoreographyFormViewModel model, string userId);

        /// <summary>
        /// Adds a new verse choreography for the user with data from the model.
        /// Throws an exception if user with the given id doesn't exist.
        /// Throws an exception if the verse type or any of the figure options ids in the model are not valid.
        /// </summary>
        /// <param name="model">VerseChoreographySaveViewModel model</param>
        /// <param name="userId">Id of the user</param>
        /// <returns></returns>
        Task SaveVerseChoreographyAsync(VerseChoreographySaveViewModel model, string userId);

        /// <summary>
        /// Changes a figure in a verse choreography with the new given figure.
        /// Throws an exception if the verse choreography or the new figure don't exist.
        /// Throws an exception if the verse choreography and the new figure option are for different users.
        /// Throws an exception if the verse choreography doesn't have that number of figure order already.
        /// Throws an exception if the old figure in that place doesn't have the same start position, end position and beats count as the new one.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newFigure"></param>
        /// <returns></returns>
        Task ChangeFigureInVerseChoreographyAsync(int id, VerseChoreographyFigureSelectedReplacementServiceModel newFigure);

        /// <summary>
        /// Deletes a verse choreography by its id.
        /// </summary>
        /// <param name="id">Id of the verse choreography</param>
        /// <returns></returns>
        Task DeleteVerseChoreographyAsync(int id);

        /// <summary>
        /// Returns a collection with suggested verse choreographies for the user based on the selected criteria.
        /// </summary>
        /// <param name="query">VerseChoreographyGenerateModel query</param>
        /// <param name="userId">Id of the user</param>
        /// <returns></returns>
        Task<IList<VerseChoreographySaveViewModel>> GenerateChoreographies(VerseChoreographyGenerateModel query, string userId);
    }
}
