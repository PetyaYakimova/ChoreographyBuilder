using AutoMapper;
using ChoreographyBuilder.Infrastructure.Data;
using ChoreographyBuilder.Infrastructure.Data.Common;
using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using ChoreographyBuilder.Tests.Mocks;
using Microsoft.AspNetCore.Identity;
using static ChoreographyBuilder.Infrastructure.Constants.RoleConstants;

namespace ChoreographyBuilder.Tests.UnitTests
{
	public class UnitTestsBase
	{
		protected ChoreographyBuilderDbContext data;
		protected IMapper mapper;
		protected IRepository repository;

		[OneTimeSetUp]
		public void SetUp()
		{
			this.mapper = MapperMock.Instance;
		}

		[SetUp]
		public void SetUpBeforeEveryTest()
		{
			this.data = DatabaseMock.Instance;

			this.repository = new Repository(this.data);

			this.SeedDatabase();
		}

		[TearDown]
		public void TearDownBase()
		{
			data.Dispose();
		}

		public Position FirstPosition { get; protected set; } = null!;

		public Position SecondPosition { get; protected set; } = null!;

		public Position ThirdPosition { get; protected set; } = null!;

		public Position InactivePosition { get; protected set; } = null!;

		public IdentityUser FirstUser { get; protected set; } = null!;

		public IdentityUser SecondUser { get; protected set; } = null!;

		public IdentityUser AdminUser { get; protected set; } = null!;

		public IdentityRole UserRole { get; protected set; } = null!;

		public IdentityRole AdminRole { get; protected set; } = null!;

		public Figure FirstFigure { get; protected set; } = null!;

		public Figure SecondFigure { get; protected set; } = null!;

		public Figure ThirdFigure { get; protected set; } = null!;

		public Figure FourthFigure { get; protected set; } = null!;

		public Figure HighlightFigure { get; protected set; } = null!;

		public FigureOption FirstFigureFirstOption { get; protected set; } = null!;

		private void SeedDatabase()
		{
			this.SeedPositions();
			this.SeedUsersAndRoles();
			this.SeedFigures();
			this.SeedFigureOptions();

			data.SaveChanges();
		}

		private void SeedPositions()
		{
			FirstPosition = new Position()
			{
				Id = 1,
				Name = "First position",
				IsActive = true
			};
			data.Positions.Add(FirstPosition);

			SecondPosition = new Position()
			{
				Id = 2,
				Name = "Second position",
				IsActive = true
			};
			data.Positions.Add(SecondPosition);

			ThirdPosition = new Position()
			{
				Id = 3,
				Name = "Third position",
				IsActive = true
			};
			data.Positions.Add(ThirdPosition);

			InactivePosition = new Position()
			{
				Id = 4,
				Name = "Inactive position",
				IsActive = false
			};
			data.Positions.Add(InactivePosition);
		}

		private void SeedUsersAndRoles()
		{
			UserRole = new IdentityRole()
			{
				Id = "UserRoleId",
				Name = UserRoleName,
				NormalizedName = UserRoleName.ToUpper()
			};
			data.Roles.Add(UserRole);

			AdminRole = new IdentityRole()
			{
				Id = "AdminRoleId",
				Name = AdminRoleName,
				NormalizedName = AdminRoleName.ToUpper()
			};
			data.Roles.Add(AdminRole);

			FirstUser = new IdentityUser()
			{
				Id = "FirstUserId",
				Email = "first.user@mail.com",
				UserName = "first.user@mail.com"
			};
			data.Users.Add(FirstUser);

			data.UserRoles.Add(new IdentityUserRole<string>()
			{
				UserId = FirstUser.Id,
				RoleId = UserRole.Id
			});

			SecondUser = new IdentityUser()
			{
				Id = "SecondUserId",
				Email = "second.user@mail.com",
				UserName = "second.user@mail.com"
			};
			data.Users.Add(SecondUser);

			data.UserRoles.Add(new IdentityUserRole<string>()
			{
				UserId = SecondUser.Id,
				RoleId = UserRole.Id
			});

			AdminUser = new IdentityUser()
			{
				Id = "AdminUserId",
				Email = "admin.user@mail.com",
				UserName = "admin.user@mail.com"
			};
			data.Users.Add(AdminUser);

			data.UserRoles.Add(new IdentityUserRole<string>()
			{
				UserId = AdminUser.Id,
				RoleId = AdminRole.Id
			});
		}

		private void SeedFigures()
		{
			FirstFigure = new Figure()
			{
				Id = 1,
				Name = "First figure",
				IsFavourite = true,
				IsHighlight = false,
				UserId = FirstUser.Id
			};
			data.Figures.Add(FirstFigure);

			SecondFigure = new Figure()
			{
				Id = 2,
				Name = "Second figure",
				IsFavourite = true,
				IsHighlight = false,
				UserId = FirstUser.Id
			};
			data.Figures.Add(SecondFigure);

			ThirdFigure = new Figure()
			{
				Id = 3,
				Name = "Third figure",
				IsFavourite = false,
				IsHighlight = false,
				UserId = FirstUser.Id
			};
			data.Figures.Add(ThirdFigure);

			FourthFigure = new Figure()
			{
				Id = 4,
				Name = "Fourth figure",
				IsFavourite = false,
				IsHighlight = false,
				UserId = SecondUser.Id
			};
			data.Figures.Add(FourthFigure);

			HighlightFigure = new Figure()
			{
				Id = 5,
				Name = "Highlight figure",
				IsFavourite = true,
				IsHighlight = true,
				UserId = FirstUser.Id
			};
			data.Figures.Add(HighlightFigure);
		}

		private void SeedFigureOptions()
		{
			FirstFigureFirstOption = new FigureOption()
			{
				Id = 1,
				FigureId = FirstFigure.Id,
				BeatCounts = 6,
				DynamicsType = DynamicsType.Regular,
				StartPositionId = FirstPosition.Id,
				EndPositionId = FirstPosition.Id
			};
			data.FigureOptions.Add(FirstFigureFirstOption);
		}
	}
}
