using ChoreographyBuilder.Core.Models.Figure;

namespace ChoreographyBuilder.Core.Models.BaseModels
{
	public abstract class EntityQueryBaseModel<T>
	{
		public int TotalCount { get; set; }

		public IEnumerable<T> Entities { get; set; } = new List<T>();
	}
}
