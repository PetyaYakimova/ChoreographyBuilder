﻿using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using ChoreographyBuilder.UITests.Helpers;
using ChoreographyBuilder.UITests.Setup;
using Microsoft.AspNetCore.Identity;

namespace ChoreographyBuilder.UITests.Repositories;

public class ManageDataRepository : BaseRepository
{
    private Credentials credentials;
    public ManageDataRepository(AppSettings settings, Credentials credentials) : base(settings)
    {
        this.credentials = credentials;
    }
    public IdentityUser FirstUser { get; private set; } = null!;

    public IdentityUser SecondUser { get; private set; } = null!;

    public IdentityUser AdminUser { get; private set; } = null!;

    public Position FirstPosition { get; private set; } = null!;

    public Position SecondPosition { get; private set; } = null!;

    public VerseType FirstVerseType { get; private set; } = null!;

    public VerseType SecondVerseType { get; private set; } = null!;

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

        context.SaveChanges();

        SeedFigureOptions();

        SeedVerseChoreographies();

        context.SaveChanges();

        SeedVerseChoreographyFigures();

        SeedFullChoreographies();

        context.SaveChanges();

        SeedFullChoreographyVerseChoreographies();

        context.SaveChanges();
    }

    public void DeleteAutomationData()
    {
        List<IdentityUser> users = context.Users.Where(u => u.Email.EndsWith(TestConstants.AutomationMailEnding)).ToList();
        List<string> userIds = users.Select(u => u.Id).ToList();

        List<Position> positions = context.Positions.Where(p => p.Name.StartsWith(TestConstants.AutomationTestPrefix)).ToList();
        List<VerseType> verseTypes = context.VerseTypes.Where(v => v.Name.StartsWith(TestConstants.AutomationTestPrefix)).ToList();
        List<Figure> figures = context.Figures.Where(f => userIds.Contains(f.UserId)).ToList();
        List<FigureOption> figureOptions = context.FigureOptions.Where(f => figures.Select(fig => fig.Id).Contains(f.FigureId)).ToList();
        List<VerseChoreography> verseChoreographies = context.VerseChoreographies.Where(v => userIds.Contains(v.UserId)).ToList();
        List<VerseChoreographyFigure> verseChoreographyFigures = context.VerseChoreographiesFigures.Where(v => verseChoreographies.Select(vc => vc.Id).Contains(v.VerseChoreographyId)).ToList();
        List<FullChoreography> fullChoreographies = context.FullChoreographies.Where(f => userIds.Contains(f.UserId)).ToList();
        List<FullChoreographyVerseChoreography> fullChoreographiesVerseChoreographies = context.FullChoreographiesVerseChoreographies.Where(f => fullChoreographies.Select(fc => fc.Id).Contains(f.FullChoreographyId)).ToList();

        context.RemoveRange(fullChoreographiesVerseChoreographies);
        context.RemoveRange(fullChoreographies);

        context.RemoveRange(verseChoreographyFigures);
        context.RemoveRange(verseChoreographies);

        context.RemoveRange(figureOptions);
        context.RemoveRange(figures);

        context.RemoveRange(positions);
        context.RemoveRange(verseTypes);

        context.RemoveRange(users);

        context.SaveChanges();
    }

    private void SeedUsers()
    {
        var hasher = new PasswordHasher<IdentityUser>();
        string userRoleId = context.Roles.FirstOrDefault(r => r.Name == "User").Id;
        string adminRoleId = context.Roles.FirstOrDefault(r => r.Name == "Administrator").Id;

        string firstUserEmail = credentials.FirstUser().Email;
        FirstUser = new IdentityUser()
        {
            Id = Guid.NewGuid().ToString(),
            Email = firstUserEmail,
            NormalizedEmail = firstUserEmail.ToUpper(),
            UserName = firstUserEmail,
            NormalizedUserName = firstUserEmail.ToUpper()
        };
        FirstUser.PasswordHash = hasher.HashPassword(FirstUser, credentials.FirstUser().Password);

        context.Users.Add(FirstUser);

        context.UserRoles.Add(new IdentityUserRole<string>()
        {
            UserId = FirstUser.Id,
            RoleId = userRoleId
        });

        string secondUserEmail = credentials.SecondUser().Email;
        SecondUser = new IdentityUser()
        {
            Id = Guid.NewGuid().ToString(),
            Email = secondUserEmail,
            NormalizedEmail = secondUserEmail.ToUpper(),
            UserName = secondUserEmail,
            NormalizedUserName = secondUserEmail.ToUpper()
        };
        SecondUser.PasswordHash = hasher.HashPassword(SecondUser, credentials.SecondUser().Password);
        context.Users.Add(SecondUser);

        context.UserRoles.Add(new IdentityUserRole<string>()
        {
            UserId = SecondUser.Id,
            RoleId = userRoleId
        });

        string adminUserEmail = credentials.AdminUser().Email;
        AdminUser = new IdentityUser()
        {
            Id = Guid.NewGuid().ToString(),
            Email = adminUserEmail,
            NormalizedEmail = adminUserEmail.ToUpper(),
            UserName = adminUserEmail,
            NormalizedUserName = adminUserEmail.ToUpper(),
        };
        AdminUser.PasswordHash = hasher.HashPassword(AdminUser, credentials.AdminUser().Password);
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

        Position activePosition = new Position()
        {
            Name = TestConstants.AutomationTestPrefix + " active position",
            IsActive = true
        };

        Position inactivePosition = new Position()
        {
            Name = TestConstants.AutomationTestPrefix + " inactive position",
            IsActive = false
        };

        Position positionForEdit = new Position()
        {
            Name = TestConstants.AutomationTestPrefix + " position for edit",
            IsActive = true
        };

        Position positionForDelete = new Position()
        {
            Name = TestConstants.AutomationTestPrefix + " position for delete",
            IsActive = true
        };

        context.Positions.AddRange(new List<Position>() { FirstPosition, SecondPosition, activePosition, inactivePosition, positionForEdit, positionForDelete });
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

        VerseType activeVerse = new VerseType()
        {
            Name = TestConstants.AutomationTestPrefix + " active",
            BeatCounts = 44,
            IsActive = true
        };

        VerseType inactiveVerse = new VerseType()
        {
            Name = TestConstants.AutomationTestPrefix + " inactive",
            BeatCounts = 16,
            IsActive = false
        };

        VerseType verseTypeForEdit = new VerseType()
        {
            Name = TestConstants.AutomationTestPrefix + " for edit",
            BeatCounts = 24,
            IsActive = true
        };

        VerseType verseTypeForDelete = new VerseType()
        {
            Name = TestConstants.AutomationTestPrefix + " for delete",
            BeatCounts = 18,
            IsActive = true
        };

        context.VerseTypes.AddRange(new List<VerseType> { FirstVerseType, SecondVerseType, activeVerse, inactiveVerse, verseTypeForEdit, verseTypeForDelete });
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
            StartPositionId = SecondPosition.Id,
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
            FigureOptionId = HighlightFigureFirstOption.Id,
        };

        SecondVerseChoreographySecondFigure = new VerseChoreographyFigure()
        {
            FigureOrder = 2,
            VerseChoreographyId = SecondVerseChoreography.Id,
            FigureOptionId = HighlightFigureFirstOption.Id,
        };

        ThirdVerseChoreographyFirstFigure = new VerseChoreographyFigure()
        {
            FigureOrder = 1,
            VerseChoreographyId = ThirdVerseChoreography.Id,
            FigureOptionId = FirstFigureFirstOption.Id,
        };

        context.AddRange(new List<VerseChoreographyFigure>() { FirstVerseChoreographyFirstFigure, FirstVerseChoreographySecondFigure, FirstVerseChoreographyThirdFigure, FirstVerseChoreographyFourthFigure, SecondVerseChoreographyFirstFigure, SecondVerseChoreographySecondFigure, ThirdVerseChoreographyFirstFigure });
    }

    public void SeedFullChoreographies()
    {
        FirstFullChoreography = new FullChoreography()
        {
            Name = "First full choreo",
            UserId = FirstUser.Id
        };

        SecondFullChoreography = new FullChoreography()
        {
            Name = "Second full choreo",
            UserId = SecondUser.Id
        };

        context.FullChoreographies.AddRange(new List<FullChoreography>() { FirstFullChoreography, SecondFullChoreography });
    }

    public void SeedFullChoreographyVerseChoreographies()
    {
        FirstFullChoreographyFirstVerse = new FullChoreographyVerseChoreography()
        {
            FullChoreographyId = FirstFullChoreography.Id,
            VerseChoreographyId = FirstVerseChoreography.Id,
            VerseChoreographyOrder = 1,
        };

        FirstFullChoreographySecondVerse = new FullChoreographyVerseChoreography()
        {
            FullChoreographyId = FirstFullChoreography.Id,
            VerseChoreographyId = SecondVerseChoreography.Id,
            VerseChoreographyOrder = 2
        };

        context.FullChoreographiesVerseChoreographies.AddRange(new List<FullChoreographyVerseChoreography>() { FirstFullChoreographyFirstVerse, FirstFullChoreographySecondVerse });
    }
}
