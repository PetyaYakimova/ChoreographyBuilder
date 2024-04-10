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

		public IdentityUser FirstUser { get; private set; } = null!;

		public IdentityUser SecondUser { get; private set; } = null!;

		public IdentityUser AdminUser { get; private set; } = null!;

		public IdentityRole UserRole { get; private set; } = null!;

		public IdentityRole AdminRole { get; private set; } = null!;

		public Position FirstPosition { get; private set; } = null!;

		public Position SecondPosition { get; private set; } = null!;

		public Position ThirdPosition { get; private set; } = null!;

		public Position InactivePosition { get; private set; } = null!;

		public VerseType FirstVerseType { get; private set; } = null!;

		public VerseType SecondVerseType { get; private set; } = null!;

		public VerseType InactiveVerseType { get; private set; } = null!;

		public Figure FirstFigure { get; private set; } = null!;

		public Figure SecondFigure { get; private set; } = null!;

		public Figure ThirdFigure { get; private set; } = null!;

		public Figure FourthFigure { get; private set; } = null!;

		public Figure HighlightFigure { get; private set; } = null!;

		public FigureOption FirstFigureFirstOption { get; private set; } = null!;

		public VerseChoreography FirstVerseChoreography { get; private set; } = null!;

		public VerseChoreography SecondVerseChoreography { get; private set; } = null!;

		public VerseChoreographyFigure FirstVerseChoreographyFirstFigure { get; private set; } = null!;

		public VerseChoreographyFigure FirstVerseChoreographySecondFigure { get; private set; } = null!;

		private void SeedDatabase()
		{
			this.SeedUsersAndRoles();

			this.SeedPositions();
			this.SeedVerseTypes();

			this.SeedFigures();
			this.SeedFigureOptions();

			this.SeedVerseChoreographies();
			this.SeedVerseChoreographyFigures();

			data.SaveChanges();
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

		private void SeedVerseTypes()
		{
			FirstVerseType = new VerseType()
			{
				Id = 1,
				Name = "First verse type",
				BeatCounts = 32,
				IsActive = true
			};
			data.VerseTypes.Add(FirstVerseType);

			SecondVerseType = new VerseType()
			{
				Id = 2,
				Name = "Second verse type",
				BeatCounts = 48,
				IsActive = true
			};
			data.VerseTypes.Add(SecondVerseType);

			InactiveVerseType = new VerseType()
			{
				Id = 3,
				Name = "Inactive verse type",
				BeatCounts = 24,
				IsActive = false
			};
			data.VerseTypes.Add(InactiveVerseType);
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

		private void SeedVerseChoreographies()
		{
			FirstVerseChoreography = new VerseChoreography()
			{
				Id = 1,
				Name = "First verse choreography",
				Score = 5,
				VerseTypeId = FirstVerseType.Id,
				UserId = FirstUser.Id
			};
			data.VerseChoreographies.Add(FirstVerseChoreography);

			SecondVerseChoreography = new VerseChoreography()
			{
				Id = 2,
				Name = "Second verse choreography",
				Score = 5,
				VerseTypeId = SecondVerseType.Id,
				UserId = FirstUser.Id
			};
			data.VerseChoreographies.Add(SecondVerseChoreography);
		}

		private void SeedVerseChoreographyFigures()
		{
			FirstVerseChoreographyFirstFigure = new VerseChoreographyFigure()
			{
				Id = 1,
				FigureOptionId = FirstFigureFirstOption.Id,
				FigureOrder = 1,
				VerseChoreographyId = FirstVerseChoreography.Id
			};
			data.VerseChoreographiesFigures.Add(FirstVerseChoreographyFirstFigure);

			FirstVerseChoreographySecondFigure = new VerseChoreographyFigure()
			{
				Id = 2,
				FigureOptionId = FirstFigureFirstOption.Id,
				FigureOrder = 2,
				VerseChoreographyId = FirstVerseChoreography.Id
			};
			data.VerseChoreographiesFigures.Add(FirstVerseChoreographySecondFigure);
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
