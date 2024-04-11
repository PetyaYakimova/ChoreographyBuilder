using ChoreographyBuilder.Core.Contracts;
using ChoreographyBuilder.Core.Models.Position;
using ChoreographyBuilder.Core.Models.Statistics;
using ChoreographyBuilder.Core.Models.User;
using ChoreographyBuilder.Infrastructure.Data.Common;
using ChoreographyBuilder.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static ChoreographyBuilder.Core.Constants.LimitConstants;

namespace ChoreographyBuilder.Core.Services
{
    public class UserService : IUserService
	{
		private readonly IRepository repository;
		public UserService(IRepository repository)
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

			model.UsersWithAtLeastOneFullChoreography = await repository.AllAsReadOnly<FullChoreography>()
				.Select(f => f.UserId)
				.Distinct()
				.CountAsync();

			return model;
		}

		public async Task<UserQueryServiceModel> GetAllUserStatisticsAsync(string? searchTerm = null, int currentPage = 1, int itemsPerPage = DefaultNumberOfItemsPerPage)
		{
			var usersToShow = repository.AllAsReadOnly<IdentityUser>();

			if (searchTerm != null)
			{
				string normalizedSearchTerm = searchTerm.ToLower();
				usersToShow = usersToShow
					.Where(p => p.Email.ToLower().Contains(normalizedSearchTerm));
			}

			var users = await usersToShow
				.OrderBy(u => u.Id)
				.Skip((currentPage - 1) * itemsPerPage)
				.Take(itemsPerPage)
				.Select(u => new UserTableViewModel()
				{
					Id = u.Id,
					Email = u.Email
				})
				.ToListAsync();

			foreach (var user in users)
			{
				UserStatisticModel statistics = await this.GetUserStatisticsAsync(user.Id);
				user.MyTotalNumberOfFigures = statistics.MyTotalNumberOfFigures;
				user.MyTotalNumberOfFullChoreographies = statistics.MyTotalNumberOfFullChoreographies;
				user.MyTotalNumberOfVerseChoreographies = statistics.MyTotalNumberOfVerseChoreographies;
			}

			int totalUsersToShow = await usersToShow.CountAsync();

			return new UserQueryServiceModel()
			{
				TotalCount = totalUsersToShow,
				Entities = users
			};
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
