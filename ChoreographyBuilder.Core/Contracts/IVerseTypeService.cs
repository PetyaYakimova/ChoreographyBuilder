using ChoreographyBuilder.Core.Models.Figure;
using ChoreographyBuilder.Core.Models.VerseType;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IVerseTypeService
	{
		Task<IEnumerable<VerseTypeTableViewModel>> AllVerseTypesAsync();

		Task<IEnumerable<VerseTypeForChoreographiesViewModel>> AllActiveVerseTypesOrSelectedVerseTypeAsync(int selectedVerseTypeId);

		Task AddVerseTypeAsync(VerseTypeFormViewModel model);
	}
}
