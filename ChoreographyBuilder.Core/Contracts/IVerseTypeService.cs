using ChoreographyBuilder.Core.Models.VerseType;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IVerseTypeService
	{
		Task<IEnumerable<VerseTypeViewModel>> AllVerseTypesAsync();
	}
}
