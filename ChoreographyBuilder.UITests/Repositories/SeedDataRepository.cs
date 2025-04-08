using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using ChoreographyBuilder.UITests.Helpers;
using ChoreographyBuilder.UITests.Setup;
using Microsoft.AspNetCore.Identity;

namespace ChoreographyBuilder.UITests.Repositories;

public class SeedDataRepository : BaseRepository
{
    public SeedDataRepository(AppSettings settings) : base(settings)
    {
    }
    public IdentityUser FirstUser { get; private set; } = null!;

    public IdentityUser SecondUser { get; private set; } = null!;

    public IdentityUser AdminUser { get; private set; } = null!;

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

    public FigureOption FirstFigureSecondOption { get; private set; } = null!;

    public FigureOption SecondFigureFirstOption { get; private set; } = null!;

    public FigureOption SecondFigureSecondOption { get; private set; } = null!;

    public FigureOption SecondFigureThirdOption { get; private set; } = null!;

    public FigureOption HighlightFigureFirstOption { get; private set; } = null!;

    public FigureOption FourthFigureFirstOption { get; private set; } = null!;

    public VerseChoreography FirstVerseChoreography { get; private set; } = null!;

    public VerseChoreography SecondVerseChoreography { get; private set; } = null!;

    public VerseChoreography ThirdVerseChoreography { get; private set; } = null!;

    public VerseChoreography FourthVerseChoreography { get; private set; } = null!;

    public VerseChoreographyFigure FirstVerseChoreographyFirstFigure { get; private set; } = null!;

    public VerseChoreographyFigure FirstVerseChoreographySecondFigure { get; private set; } = null!;

    public VerseChoreographyFigure FirstVerseChoreographyThirdFigure { get; private set; } = null!;

    public VerseChoreographyFigure FirstVerseChoreographyFourthFigure { get; private set; } = null!;

    public VerseChoreographyFigure SecondVerseChoreographyFirstFigure { get; private set; } = null!;

    public VerseChoreographyFigure SecondVerseChoreographySecondFigure { get; private set; } = null!;

    public VerseChoreographyFigure ThirdVerseChoreographyFirstFigure { get; private set; } = null!;

    public FullChoreography FirstFullChoreography { get; private set; } = null!;

    public FullChoreography SecondFullChoreography { get; private set; } = null!;

    public FullChoreographyVerseChoreography FirstFullChoreographyFirstVerse { get; private set; } = null!;

    public FullChoreographyVerseChoreography FirstFullChoreographySecondVerse { get; private set; } = null!;

    public void SeedInitialUsersData()
    {
        SeedUsers();

        SeedPositions();
        SeedVerseTypes();

        SeedFigures();
        SeedFigureOptions();

        SeedVerseChoreographies();
        SeedVerseChoreographyFigures();

        SeedFullChoreographies();
        SeedFullChoreographyVerseChoreographies();

        context.SaveChanges();
    }

    public void DeleteSeededData()
    {
        List<string> userIds = new List<string> { FirstUser.Id, SecondUser.Id };

        List<Position> positions = context.Positions.Where(p => p.Name.StartsWith(TestConstants.AutomationTestPrefix)).ToList();
        List<VerseType> verseTypes = context.VerseTypes.Where(v => v.Name.StartsWith(TestConstants.AutomationTestPrefix)).ToList();
        List<Figure> figures = context.Figures.Where(f => userIds.Contains(f.UserId)).ToList();
        List<FigureOption> figureOptions = context.FigureOptions.Where(f => figures.Select(fig => fig.Id).Contains(f.FigureId)).ToList();
        List<VerseChoreography> verseChoreographies = context.VerseChoreographies.Where(v => userIds.Contains(v.UserId)).ToList();
        List<VerseChoreographyFigure> verseChoreographyFigures = context.VerseChoreographiesFigures.Where(v => verseChoreographies.Select(vc => vc.Id).Contains(v.VerseChoreographyId)).ToList();

        context.RemoveRange(verseChoreographyFigures);
        context.RemoveRange(verseChoreographies);

        context.RemoveRange(figureOptions);
        context.RemoveRange(figures);

        context.RemoveRange(positions);
        context.RemoveRange(verseTypes);

        context.RemoveRange(new List<IdentityUser>() { FirstUser, SecondUser, AdminUser });
    }

    private void SeedUsers()
    {
        var hasher = new PasswordHasher<IdentityUser>();
        string userRoleId = context.Roles.FirstOrDefault(r => r.Name == "User").Id;
        string adminRoleId = context.Roles.FirstOrDefault(r => r.Name == "Administrator").Id;

        FirstUser = new IdentityUser()
        {
            Id = Guid.NewGuid().ToString(),
            Email = "first.user@mail.com",
            NormalizedEmail = "FIRST.USER@MAIL.COM",
            UserName = "first.user@mail.com",
            NormalizedUserName = "FIRST.USER@MAIL.COM"
        };
        FirstUser.PasswordHash = hasher.HashPassword(FirstUser, "firstUser123");

        context.Users.Add(FirstUser);

        context.UserRoles.Add(new IdentityUserRole<string>()
        {
            UserId = FirstUser.Id,
            RoleId = userRoleId
        });

        SecondUser = new IdentityUser()
        {
            Id = Guid.NewGuid().ToString(),
            Email = "second.user@mail.com",
            NormalizedEmail = "SECOND.USER@MAIL.COM",
            UserName = "second.user@mail.com",
            NormalizedUserName = "SECOND.USER@MAIL.COM"
        };
        SecondUser.PasswordHash = hasher.HashPassword(SecondUser, "secondUser123456");
        context.Users.Add(SecondUser);

        context.UserRoles.Add(new IdentityUserRole<string>()
        {
            UserId = SecondUser.Id,
            RoleId = userRoleId
        });

        AdminUser = new IdentityUser()
        {
            Id = Guid.NewGuid().ToString(),
            Email = "admin.user@mail.com",
            NormalizedEmail = "ADMIN.USER@MAIL.COM",
            UserName = "admin.user@mail.com",
            NormalizedUserName = "ADMIN.USER@MAIL.COM",
        };
        AdminUser.PasswordHash = hasher.HashPassword(AdminUser, "Admin987");
        context.Users.Add(AdminUser);

        context.UserRoles.Add(new IdentityUserRole<string>()
        {
            UserId = AdminUser.Id,
            RoleId = adminRoleId
        });
    }

    private void SeedPositions()
    {
        FirstPosition = new Position()
        {
            Name = TestConstants.AutomationTestPrefix + " position 1",
            IsActive = true
        };

        SecondPosition = new Position()
        {
            Name = TestConstants.AutomationTestPrefix + " position 2",
            IsActive = true
        };

        ThirdPosition = new Position()
        {
            Name = TestConstants.AutomationTestPrefix + " position 3",
            IsActive = true
        };

        InactivePosition = new Position()
        {
            Name = TestConstants.AutomationTestPrefix + " inactive position",
            IsActive = false
        };

        context.Positions.AddRange(new List<Position>() { FirstPosition, SecondPosition, ThirdPosition, InactivePosition });
    }

    private void SeedVerseTypes()
    {
        FirstVerseType = new VerseType()
        {
            Name = TestConstants.AutomationTestPrefix + " verse 1",
            BeatCounts = 32,
            IsActive = true
        };

        SecondVerseType = new VerseType()
        {
            Name = TestConstants.AutomationTestPrefix + " verse 2",
            BeatCounts = 48,
            IsActive = true
        };

        InactiveVerseType = new VerseType()
        {
            Name = TestConstants.AutomationTestPrefix + " inactive verse",
            BeatCounts = 24,
            IsActive = false
        };

        context.VerseTypes.AddRange(new List<VerseType> { FirstVerseType, SecondVerseType, InactiveVerseType });
    }

    private void SeedFigures()
    {
        FirstFigure = new Figure()
        {
            Name = "First figure",
            IsHighlight = false,
            IsFavourite = false,
            UserId = FirstUser.Id,
            CanBeShared = true
        };

        SecondFigure = new Figure()
        {
            Name = "Second figure",
            IsHighlight = false,
            IsFavourite = false,
            UserId = FirstUser.Id,
            CanBeShared = false
        };

        ThirdFigure = new Figure()
        {
            Name = "Third figure",
            IsHighlight = false,
            IsFavourite = true,
            UserId = FirstUser.Id,
            CanBeShared = false
        };

        FourthFigure = new Figure()
        {
            Name = "Fourth figure",
            IsHighlight = true,
            IsFavourite = true,
            UserId = SecondUser.Id,
            CanBeShared = false
        };

        HighlightFigure = new Figure()
        {
            Name = "Highlight figure",
            IsHighlight = true,
            IsFavourite = true,
            UserId = FirstUser.Id,
            CanBeShared = false
        };

        context.Figures.AddRange(new List<Figure>() { FirstFigure, SecondFigure, ThirdFigure, FourthFigure, HighlightFigure });
    }

    public void SeedFigureOptions()
    {
        FirstFigureFirstOption = new FigureOption()
        {
            FigureId = FirstFigure.Id,
            StartPositionId = FirstPosition.Id,
            EndPositionId = FirstPosition.Id,
            BeatCounts = 6,
            DynamicsType = DynamicsType.Regular
        };

        FirstFigureSecondOption = new FigureOption()
        {
            FigureId = FirstFigure.Id,
            StartPositionId = SecondPosition.Id,
            EndPositionId = SecondPosition.Id,
            BeatCounts = 6,
            DynamicsType = DynamicsType.Regular
        };

        SecondFigureFirstOption = new FigureOption()
        {
            FigureId = SecondFigure.Id,
            StartPositionId = SecondPosition.Id,
            EndPositionId = FirstPosition.Id,
            BeatCounts = 6,
            DynamicsType = DynamicsType.Regular
        };

        SecondFigureSecondOption = new FigureOption()
        {
            FigureId = SecondFigure.Id,
            StartPositionId = FirstPosition.Id,
            EndPositionId = SecondPosition.Id,
            BeatCounts = 6,
            DynamicsType = DynamicsType.Regular
        };

        SecondFigureThirdOption = new FigureOption()
        {
            FigureId = SecondFigure.Id,
            StartPositionId = FirstPosition.Id,
            EndPositionId = SecondPosition.Id,
            BeatCounts = 4,
            DynamicsType = DynamicsType.Fast
        };

        HighlightFigureFirstOption = new FigureOption()
        {
            FigureId = HighlightFigure.Id,
            StartPositionId = SecondFigure.Id,
            EndPositionId = SecondPosition.Id,
            BeatCounts = 16,
            DynamicsType = DynamicsType.Slow
        };

        FourthFigureFirstOption = new FigureOption()
        {
            FigureId = FourthFigure.Id,
            StartPositionId = FirstPosition.Id,
            EndPositionId = SecondPosition.Id,
            BeatCounts = 8,
            DynamicsType = DynamicsType.Slow
        };

        context.FigureOptions.AddRange(new List<FigureOption>() { FirstFigureFirstOption, FirstFigureSecondOption, SecondFigureFirstOption, SecondFigureSecondOption, SecondFigureThirdOption, HighlightFigureFirstOption, FourthFigureFirstOption });
    }

    public void SeedVerseChoreographies()
    {
        FirstVerseChoreography = new VerseChoreography()
        {
            Name = "Complete verse choreo 1",
            VerseTypeId = FirstVerseType.Id,
            Score = 3,
            UserId = FirstUser.Id
        };

        SecondVerseChoreography = new VerseChoreography()
        {
            Name = "Complete verse choreo 2",
            VerseTypeId = SecondVerseType.Id,
            Score = 4,
            UserId = FirstUser.Id
        };

        ThirdVerseChoreography = new VerseChoreography()
        {
            Name = "Incomplete verse choreo",
            VerseTypeId = FirstVerseType.Id,
            Score = 2,
            UserId = FirstUser.Id
        };

        FourthVerseChoreography = new VerseChoreography()
        {
            Name = "Other user choreo",
            VerseTypeId = FirstVerseType.Id,
            Score = 2,
            UserId = SecondUser.Id
        };

        context.VerseChoreographies.AddRange(new List<VerseChoreography>() { FirstVerseChoreography, SecondVerseChoreography, ThirdVerseChoreography, FourthVerseChoreography });
    }

    public void SeedVerseChoreographyFigures()
    {
        FirstVerseChoreographyFirstFigure = new VerseChoreographyFigure()
        {
            FigureOrder = 1,
            VerseChoreographyId = FirstVerseChoreography.Id,
            FigureOptionId = FirstFigureSecondOption.Id,
        };

        FirstVerseChoreographySecondFigure = new VerseChoreographyFigure()
        {
            FigureOrder = 2,
            VerseChoreographyId = FirstVerseChoreography.Id,
            FigureOptionId = SecondFigureFirstOption.Id,
        };

        FirstVerseChoreographyThirdFigure = new VerseChoreographyFigure()
        {
            FigureOrder = 3,
            VerseChoreographyId = FirstVerseChoreography.Id,
            FigureOptionId = SecondFigureThirdOption.Id,
        };

        FirstVerseChoreographyFourthFigure = new VerseChoreographyFigure()
        {
            FigureOrder = 4,
            VerseChoreographyId = FirstVerseChoreography.Id,
            FigureOptionId = HighlightFigureFirstOption.Id,
        };

        SecondVerseChoreographyFirstFigure = new VerseChoreographyFigure()
        {
            FigureOrder = 1,
            VerseChoreographyId = SecondVerseChoreography.Id,
            FigureOptionId = HighlightFigure.Id,
        };

        SecondVerseChoreographySecondFigure = new VerseChoreographyFigure()
        {
            FigureOrder = 2,
            VerseChoreographyId = SecondVerseChoreography.Id,
            FigureOptionId = HighlightFigure.Id,
        };

        ThirdVerseChoreographyFirstFigure = new VerseChoreographyFigure()
        {
            FigureOrder = 1,
            VerseChoreographyId = ThirdVerseChoreography.Id,
            FigureOptionId = FirstFigureFirstOption.Id,
        };

        context.AddRange(new List<VerseChoreographyFigure>() { FirstVerseChoreographyFirstFigure, FirstVerseChoreographySecondFigure, FirstVerseChoreographyThirdFigure, FirstVerseChoreographyFourthFigure, SecondVerseChoreographyFirstFigure, SecondVerseChoreographySecondFigure, ThirdVerseChoreographyFirstFigure });
    }
}
