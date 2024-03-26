namespace ChoreographyBuilder.Core.Models.VerseChoreography
{
	public class VerseChoreographyWithScoreServiceModel
	{
		public VerseChoreographyWithScoreServiceModel(int score, IList<Infrastructure.Data.Models.FigureOption> choreography)
		{
			Score = score;
			Choreography = choreography;
		}

		public int Score { get; set; }

		public IList<Infrastructure.Data.Models.FigureOption> Choreography { get; set; } = new List<Infrastructure.Data.Models.FigureOption>();
	}
}
