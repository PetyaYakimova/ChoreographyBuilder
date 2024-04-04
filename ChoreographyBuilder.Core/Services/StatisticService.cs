using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.Statistics;
using ChoreographyBuilder.Infrastructure.Data.Common;
using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChoreographyBuilder.Core.Services
{
	public class StatisticService : IStatisticService
	{
		private readonly IRepository repository;
		public StatisticService(IRepository repository)
		{
			this.repository = repository;
		}

		public async Task<AdminStatisticModel> GetAdminStatisticsAsync()
		{
			AdminStatisticModel model = new AdminStatisticModel();

			model.TotalNumberOfPositions = await repository.AllAsReadOnly<Position>()
				.CountAsync();

			model.NumberOfActivePositions = await repository.AllAsReadOnly<Position>()
				.Where(p => p.IsActive)
				.CountAsync();

			model.TotalNumberOfVerseTypes = await repository.AllAsReadOnly<VerseType>()
				.CountAsync();

			model.NumberOfActiveVerseTypes = await repository.AllAsReadOnly<VerseType>()
				.Where(vt => vt.IsActive)
				.CountAsync();

			model.TotalNumberOfFigures = await repository.AllAsReadOnly<Figure>()
				.CountAsync();

			model.UsersWithAtLeastOneFigure = await repository.AllAsReadOnly<Figure>()
				.Select(f => f.UserId)
				.Distinct()
				.CountAsync();

			model.TotalNumberOfSavedVerseChoreographies = await repository.AllAsReadOnly<VerseChoreography>()
				.CountAsync();

			model.UsersWithAtLeastOneVerseChoreography = await repository.AllAsReadOnly<VerseChoreography>()
				.Select(f => f.UserId)
				.Distinct()
				.CountAsync();

			model.TotalNumberOfSavedFullChoreographies = await repository.AllAsReadOnly<FullChoreography>()
				.CountAsync();

			model.UsersWithAtLeastOneFullChoreographye = await repository.AllAsReadOnly<FullChoreography>()
				.Select(f => f.UserId)
				.Distinct()
				.CountAsync();

			return model;
		}

		public async Task<UserStatisticModel> GetUserStatisticsAsync(string userId)
		{
			UserStatisticModel model = new UserStatisticModel();

			model.MyTotalNumberOfFigures = await repository.AllAsReadOnly<Figure>()
				.Where(f => f.UserId == userId)
				.CountAsync();

			model.MyTotalNumberOfVerseChoreographies = await repository.AllAsReadOnly<VerseChoreography>()
				.Where(c => c.UserId == userId)
				.CountAsync();

			model.MyTotalNumberOfFullChoreographies = await repository.AllAsReadOnly<FullChoreography>()
				.Where(c => c.UserId == userId)
				.CountAsync();

			return model;
		}
	}
}
