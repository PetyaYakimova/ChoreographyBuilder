using ChoreographyBuilder.Core.Models.Figure;

namespace ChoreographyBuilder.Core.Contracts
{
	public interface IFigureService
	{
		Task<IEnumerable<FigureTableViewModel>> AllUserFiguresAsync(string userId);
	}
}
