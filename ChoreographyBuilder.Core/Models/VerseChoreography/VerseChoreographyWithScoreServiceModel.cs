namespace ChoreographyBuilder.Core.Models.VerseChoreography
{
	/// <summary>
	/// A model used in a service for keeping information about choreography with score when generating suggestions for choreographies.
	/// No added validation attributes.
	/// </summary>
	public class VerseChoreographyWithScoreServiceModel
	{
		public VerseChoreographyWithScoreServiceModel(int score, IList<ChoreographyBuilder.Infrastructure.Data.Models.FigureOption> choreography)
		{
			Score = score;
			Choreography = choreography;
		}

		public int Score { get; set; }

		public IList<ChoreographyBuilder.Infrastructure.Data.Models.FigureOption> Choreography { get; set; } = new List<ChoreographyBuilder.Infrastructure.Data.Models.FigureOption>();
	}
}
