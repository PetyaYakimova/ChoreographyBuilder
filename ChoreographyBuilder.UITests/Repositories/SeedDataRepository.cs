using ChoreographyBuilder.Infrastructure.Data.Models;
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

    public VerseChoreographyFigure SecondVerseChoreographyFirstFigure { get; private set; } = null!;

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
            Name = "AutoTest position 1",
            IsActive = true
        };

        SecondPosition = new Position()
        {
            Name = "AutoTest position 2",
            IsActive = true
        };

        ThirdPosition = new Position()
        {
            Name = "AutoTest position 3",
            IsActive = true
        };

        InactivePosition = new Position()
        {
            Name = "AutoTest inactive position",
            IsActive = false
        };

        context.Positions.AddRange(new List<Position>() { FirstPosition, SecondPosition, ThirdPosition, InactivePosition });
    }
}
