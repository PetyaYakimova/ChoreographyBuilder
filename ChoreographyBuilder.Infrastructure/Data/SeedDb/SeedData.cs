using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;
using static ChoreographyBuilder.Infrastructure.Constants.RoleConstants;

namespace ChoreographyBuilder.Infrastructure.Data.SeedDb
{
	internal class SeedData
    {
        public IdentityRole AdminRole { get; set; } = null!;

        public IdentityRole UserRole { get; set; } = null!;

        public IdentityUser DemoUser { get; set; } = null!;

        public IdentityUser AdminUser { get; set; } = null!;

        public IdentityUserRole<string> DemoUserWitRole { get; set; } = null!;

        public IdentityUserRole<string> AdminUserWitRole { get; set; } = null!;

        public VerseType SwingVerse { get; set; } = null!;

        public VerseType BluesVerse { get; set; } = null!;

        public Position OpenPositionLeftShoulderToTheAudience { get; set; } = null!;

        public Position OpenPositionRightShoulderToTheAudience { get; set; } = null!;

        public Position ClosedPositionLeftShoulderToTheAudience { get; set; } = null!;

        public Position ClosedPositionRightShoulderToTheAudience { get; set; } = null!;

        public Figure ChangeOfPlace { get; set; } = null!;

        public Figure AmericanSpin { get; set; } = null!;

        public Figure SpinWithBlock { get; set; } = null!;

        public Figure Tunnel { get; set; } = null!;

        public Figure Cartwheel { get; set; } = null!;

        public Figure SendIn { get; set; } = null!;

        public Figure SwingOut { get; set; } = null!;

        public Figure Helicopter { get; set; } = null!;

        public Figure LeftSidePass { get; set; } = null!;

        public Figure SendOut { get; set; } = null!;

        public FigureOption ChangeOfPlaceOption1 { get; set; } = null!;

        public FigureOption ChangeOfPlaceOption2 { get; set; } = null!;

        public FigureOption AmericanSpinOption1 { get; set; } = null!;

        public FigureOption AmericanSpinOption2 { get; set; } = null!;

        public FigureOption AmericanSpinOption3 { get; set; } = null!;

        public FigureOption AmericanSpinOption4 { get; set; } = null!;

        public FigureOption AmericanSpinOption5 { get; set; } = null!;

        public FigureOption AmericanSpinOption6 { get; set; } = null!;

        public FigureOption AmericanSpinOption7 { get; set; } = null!;

        public FigureOption AmericanSpinOption8 { get; set; } = null!;

        public FigureOption AmericanSpinOption9 { get; set; } = null!;

        public FigureOption AmericanSpinOption10 { get; set; } = null!;

        public FigureOption SpinWithBlockOption1 { get; set; } = null!;

        public FigureOption SpinWithBlockOption2 { get; set; } = null!;

        public FigureOption SpinWithBlockOption3 { get; set; } = null!;

        public FigureOption SpinWithBlockOption4 { get; set; } = null!;

        public FigureOption TunnelOption1 { get; set; } = null!;

        public FigureOption TunnelOption2 { get; set; } = null!;

        public FigureOption CartwheelOption1 { get; set; } = null!;

        public FigureOption SendInOption1 { get; set; } = null!;

        public FigureOption SendInOption2 { get; set; } = null!;

        public FigureOption SwingOutOption1 { get; set; } = null!;

        public FigureOption SwingOutOption2 { get; set; } = null!;

        public FigureOption SwingOutOption3 { get; set; } = null!;

        public FigureOption SwingOutOption4 { get; set; } = null!;

        public FigureOption HelicopterOption1 { get; set; } = null!;

        public FigureOption LeftSidePassOption1 { get; set; } = null!;

        public FigureOption SendOutOption1 { get; set; } = null!;

        public FigureOption SendOutOption2 { get; set; } = null!;

        public VerseChoreography SwingVerseChoreography1 { get; set; } = null!;

        public VerseChoreography SwingVerseChoreography2 { get; set; } = null!;

        public VerseChoreography BluesVerseChoreography { get; set; } = null!;

        public VerseChoreographyFigure SwingVerse1Figure1 { get; set; } = null!;

        public VerseChoreographyFigure SwingVerse1Figure2 { get; set; } = null!;

        public VerseChoreographyFigure SwingVerse1Figure3 { get; set; } = null!;

        public VerseChoreographyFigure SwingVerse1Figure4 { get; set; } = null!;

        public VerseChoreographyFigure SwingVerse2Figure1 { get; set; } = null!;

        public VerseChoreographyFigure SwingVerse2Figure2 { get; set; } = null!;

        public VerseChoreographyFigure SwingVerse2Figure3 { get; set; } = null!;

        public VerseChoreographyFigure BluesVerseFigure1 { get; set; } = null!;

        public VerseChoreographyFigure BluesVerseFigure2 { get; set; } = null!;

        public VerseChoreographyFigure BluesVerseFigure3 { get; set; } = null!;

        public VerseChoreographyFigure BluesVerseFigure4 { get; set; } = null!;

        public VerseChoreographyFigure BluesVerseFigure5 { get; set; } = null!;

        public VerseChoreographyFigure BluesVerseFigure6 { get; set; } = null!;

        public FullChoreography FullChoreography { get; set; } = null!;

        public FullChoreographyVerseChoreography FullChoreographyVerse1 { get; set; } = null!;

        public FullChoreographyVerseChoreography FullChoreographyVerse2 { get; set; } = null!;

        public FullChoreographyVerseChoreography FullChoreographyVerse3 { get; set; } = null!;

        public SeedData()
        {
            SeedRoles();
            SeedUsers();
            SeedUsersInRoles();
            SeedVerseTypes();
            SeedPositions();
            SeedFigures();
            SeedFigureOptions();
            SeedVerseChoreographies();
            SeedVerseChoreographyFigures();
            SeedFullChoreographies();
            SeedFullChoreographyVerses();
        }

        private void SeedRoles()
        {
            AdminRole = new IdentityRole()
            {
                Id = "40271de3-61e3-49a3-b580-087252ce0546",
                Name = AdminRoleName,
                NormalizedName = AdminRoleName.ToUpper()
            };

            UserRole = new IdentityRole()
            {
                Id = "2a18a096-97da-4ae4-9d46-88314532d82b",
                Name = UserRoleName,
                NormalizedName = UserRoleName.ToUpper()
            };
        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            DemoUser = new IdentityUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "demo@mail.com",
                NormalizedUserName = "DEMO@MAIL.COM",
                Email = "demo@mail.com",
                NormalizedEmail = "DEMO@MAIL.COM"
            };

            DemoUser.PasswordHash =
                 hasher.HashPassword(DemoUser, "demo123");

            AdminUser = new IdentityUser()
            {
                Id = "fd6820c7-db68-4695-8a9d-88559d48e0ec",
                UserName = "admin@mail.com",
                NormalizedUserName = "ADMIN@MAIL.COM",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM"
            };

            AdminUser.PasswordHash =
                 hasher.HashPassword(DemoUser, "admin123");
        }

        private void SeedUsersInRoles()
        {
            DemoUserWitRole = new IdentityUserRole<string>()
            {
                UserId = DemoUser.Id,
                RoleId = UserRole.Id
            };

            AdminUserWitRole = new IdentityUserRole<string>()
            {
                UserId = AdminUser.Id,
                RoleId = AdminRole.Id
            };
        }

        private void SeedVerseTypes()
        {
            SwingVerse = new VerseType()
            {
                Id = 1,
                Name = "Swing Verse",
                BeatCounts = 32,
                IsActive = true
            };

            BluesVerse = new VerseType()
            {
                Id = 2,
                Name = "Blues Verse",
                BeatCounts = 48,
                IsActive = true
            };
        }

        private void SeedPositions()
        {
            OpenPositionLeftShoulderToTheAudience = new Position()
            {
                Id = 1,
                Name = "Open position with left shoulder to the audience",
                IsActive = true
            };

            OpenPositionRightShoulderToTheAudience = new Position()
            {
                Id = 2,
                Name = "Open position with right shoulder to the audience",
                IsActive = true
            };

            ClosedPositionLeftShoulderToTheAudience = new Position()
            {
                Id = 3,
                Name = "Closed position with left shoulder to the audience",
                IsActive = true
            };

            ClosedPositionRightShoulderToTheAudience = new Position()
            {
                Id = 4,
                Name = "Closed position with right shoulder to the audience",
                IsActive = true
            };
        }

        private void SeedFigures()
        {
            ChangeOfPlace = new Figure()
            {
                Id = 1,
                Name = "Change of place",
                IsHighlight = false,
                IsFavourite = false,
                UserId = DemoUser.Id
            };

            AmericanSpin = new Figure()
            {
                Id = 2,
                Name = "American Spin",
                IsHighlight = false,
                IsFavourite = false,
                UserId = DemoUser.Id
            };

            SpinWithBlock = new Figure()
            {
                Id = 3,
                Name = "Spin with block",
                IsHighlight = false,
                IsFavourite = true,
                UserId = DemoUser.Id
            };

            Tunnel = new Figure()
            {
                Id = 4,
                Name = "Tunnel",
                IsHighlight = true,
                IsFavourite = true,
                UserId = DemoUser.Id
            };

            Cartwheel = new Figure()
            {
                Id = 5,
                Name = "Cartwheel",
                IsHighlight = true,
                IsFavourite = true,
                UserId = DemoUser.Id
            };

            SendIn = new Figure()
            {
                Id = 6,
                Name = "Get into closed",
                IsHighlight = false,
                IsFavourite = false,
                UserId = DemoUser.Id
            };

            SwingOut = new Figure()
            {
                Id = 7,
                Name = "Swing Out",
                IsHighlight = false,
                IsFavourite = false,
                UserId = DemoUser.Id
            };

            Helicopter = new Figure()
            {
                Id = 8,
                Name = "Helicopter",
                IsHighlight = true,
                IsFavourite = false,
                UserId = DemoUser.Id
            };

            LeftSidePass = new Figure()
            {
                Id = 9,
                Name = "Left Side Pass",
                IsHighlight = false,
                IsFavourite = false,
                UserId = DemoUser.Id
            };

            SendOut = new Figure()
            {
                Id = 10,
                Name = "Send out",
                IsHighlight = false,
                IsFavourite = true,
                UserId = DemoUser.Id
            };
        }

        private void SeedFigureOptions()
        {
            //Change of place figure options
            ChangeOfPlaceOption1 = new FigureOption()
            {
                Id = 1,
                FigureId = ChangeOfPlace.Id,
                StartPositionId = OpenPositionRightShoulderToTheAudience.Id,
                EndPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                BeatCounts = 6,
                DynamicsType = DynamicsType.Regular
            };

            ChangeOfPlaceOption2 = new FigureOption()
            {
                Id = 2,
                FigureId = ChangeOfPlace.Id,
                StartPositionId = OpenPositionRightShoulderToTheAudience.Id,
                EndPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                BeatCounts = 4,
                DynamicsType = DynamicsType.Fast
            };

            //American spin figure options
            AmericanSpinOption1 = new FigureOption()
            {
                Id = 3,
                FigureId = AmericanSpin.Id,
                StartPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                EndPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                BeatCounts = 6,
                DynamicsType = DynamicsType.Regular
            };

            AmericanSpinOption2 = new FigureOption()
            {
                Id = 4,
                FigureId = AmericanSpin.Id,
                StartPositionId = OpenPositionRightShoulderToTheAudience.Id,
                EndPositionId = OpenPositionRightShoulderToTheAudience.Id,
                BeatCounts = 6,
                DynamicsType = DynamicsType.Regular
            };

            AmericanSpinOption3 = new FigureOption()
            {
                Id = 5,
                FigureId = AmericanSpin.Id,
                StartPositionId = OpenPositionRightShoulderToTheAudience.Id,
                EndPositionId = OpenPositionRightShoulderToTheAudience.Id,
                BeatCounts = 4,
                DynamicsType = DynamicsType.Fast
            };

            AmericanSpinOption4 = new FigureOption()
            {
                Id = 6,
                FigureId = AmericanSpin.Id,
                StartPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                EndPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                BeatCounts = 4,
                DynamicsType = DynamicsType.Fast
            };

            AmericanSpinOption5 = new FigureOption()
            {
                Id = 7,
                FigureId = AmericanSpin.Id,
                StartPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                EndPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                BeatCounts = 2,
                DynamicsType = DynamicsType.Fast
            };

            AmericanSpinOption6 = new FigureOption()
            {
                Id = 8,
                FigureId = AmericanSpin.Id,
                StartPositionId = OpenPositionRightShoulderToTheAudience.Id,
                EndPositionId = OpenPositionRightShoulderToTheAudience.Id,
                BeatCounts = 2,
                DynamicsType = DynamicsType.Fast
            };

            AmericanSpinOption7 = new FigureOption()
            {
                Id = 9,
                FigureId = AmericanSpin.Id,
                StartPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                EndPositionId = ClosedPositionLeftShoulderToTheAudience.Id,
                BeatCounts = 6,
                DynamicsType = DynamicsType.Regular
            };

            AmericanSpinOption8 = new FigureOption()
            {
                Id = 10,
                FigureId = AmericanSpin.Id,
                StartPositionId = OpenPositionRightShoulderToTheAudience.Id,
                EndPositionId = ClosedPositionRightShoulderToTheAudience.Id,
                BeatCounts = 6,
                DynamicsType = DynamicsType.Regular
            };

            AmericanSpinOption9 = new FigureOption()
            {
                Id = 11,
                FigureId = AmericanSpin.Id,
                StartPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                EndPositionId = ClosedPositionLeftShoulderToTheAudience.Id,
                BeatCounts = 4,
                DynamicsType = DynamicsType.Fast
            };

            AmericanSpinOption10 = new FigureOption()
            {
                Id = 12,
                FigureId = AmericanSpin.Id,
                StartPositionId = OpenPositionRightShoulderToTheAudience.Id,
                EndPositionId = ClosedPositionRightShoulderToTheAudience.Id,
                BeatCounts = 4,
                DynamicsType = DynamicsType.Fast
            };

            //Spin with block figure options
            SpinWithBlockOption1 = new FigureOption()
            {
                Id = 13,
                FigureId = SpinWithBlock.Id,
                StartPositionId = OpenPositionRightShoulderToTheAudience.Id,
                EndPositionId = OpenPositionRightShoulderToTheAudience.Id,
                BeatCounts = 6,
                DynamicsType = DynamicsType.Regular
            };

            SpinWithBlockOption2 = new FigureOption()
            {
                Id = 14,
                FigureId = SpinWithBlock.Id,
                StartPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                EndPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                BeatCounts = 6,
                DynamicsType = DynamicsType.Regular
            };

            SpinWithBlockOption3 = new FigureOption()
            {
                Id = 15,
                FigureId = SpinWithBlock.Id,
                StartPositionId = OpenPositionRightShoulderToTheAudience.Id,
                EndPositionId = OpenPositionRightShoulderToTheAudience.Id,
                BeatCounts = 4,
                DynamicsType = DynamicsType.Fast
            };

            SpinWithBlockOption4 = new FigureOption()
            {
                Id = 16,
                FigureId = SpinWithBlock.Id,
                StartPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                EndPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                BeatCounts = 4,
                DynamicsType = DynamicsType.Fast
            };

            //Tunnel figure options
            TunnelOption1 = new FigureOption()
            {
                Id = 17,
                FigureId = Tunnel.Id,
                StartPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                EndPositionId = OpenPositionRightShoulderToTheAudience.Id,
                BeatCounts = 16,
                DynamicsType = DynamicsType.Regular
            };

            TunnelOption2 = new FigureOption()
            {
                Id = 18,
                FigureId = Tunnel.Id,
                StartPositionId = OpenPositionRightShoulderToTheAudience.Id,
                EndPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                BeatCounts = 16,
                DynamicsType = DynamicsType.Regular
            };

            //Catwheel figure options
            CartwheelOption1 = new FigureOption()
            {
                Id = 19,
                FigureId = Cartwheel.Id,
                StartPositionId = ClosedPositionLeftShoulderToTheAudience.Id,
                EndPositionId = OpenPositionRightShoulderToTheAudience.Id,
                BeatCounts = 16,
                DynamicsType = DynamicsType.Fast
            };

            //Send in figure options
            SendInOption1 = new FigureOption()
            {
                Id = 20,
                FigureId = SendIn.Id,
                StartPositionId = OpenPositionRightShoulderToTheAudience.Id,
                EndPositionId = ClosedPositionLeftShoulderToTheAudience.Id,
                BeatCounts = 6,
                DynamicsType = DynamicsType.Regular
            };

            SendInOption2 = new FigureOption()
            {
                Id = 21,
                FigureId = SendIn.Id,
                StartPositionId = OpenPositionRightShoulderToTheAudience.Id,
                EndPositionId = ClosedPositionLeftShoulderToTheAudience.Id,
                BeatCounts = 4,
                DynamicsType = DynamicsType.Fast
            };

            //Swing out figure options
            SwingOutOption1 = new FigureOption()
            {
                Id = 22,
                FigureId = SwingOut.Id,
                StartPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                EndPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                BeatCounts = 8,
                DynamicsType = DynamicsType.Slow
            };

            SwingOutOption2 = new FigureOption()
            {
                Id = 23,
                FigureId = SwingOut.Id,
                StartPositionId = OpenPositionRightShoulderToTheAudience.Id,
                EndPositionId = OpenPositionRightShoulderToTheAudience.Id,
                BeatCounts = 8,
                DynamicsType = DynamicsType.Slow
            };

            SwingOutOption3 = new FigureOption()
            {
                Id = 24,
                FigureId = SwingOut.Id,
                StartPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                EndPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                BeatCounts = 6,
                DynamicsType = DynamicsType.Fast
            };

            SwingOutOption4 = new FigureOption()
            {
                Id = 25,
                FigureId = SwingOut.Id,
                StartPositionId = OpenPositionRightShoulderToTheAudience.Id,
                EndPositionId = OpenPositionRightShoulderToTheAudience.Id,
                BeatCounts = 6,
                DynamicsType = DynamicsType.Fast
            };

            //Helicopter figure options
            HelicopterOption1 = new FigureOption()
            {
                Id = 26,
                FigureId = Helicopter.Id,
                StartPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                EndPositionId = ClosedPositionLeftShoulderToTheAudience.Id,
                BeatCounts = 20,
                DynamicsType = DynamicsType.Regular
            };

            //Left side pass figure options
            LeftSidePassOption1 = new FigureOption()
            {
                Id = 27,
                FigureId = LeftSidePass.Id,
                StartPositionId = OpenPositionLeftShoulderToTheAudience.Id,
                EndPositionId = OpenPositionRightShoulderToTheAudience.Id,
                BeatCounts = 6,
                DynamicsType = DynamicsType.Regular
            };

            //SendOut figure options
            SendOutOption1 = new FigureOption()
            {
                Id = 28,
                FigureId = SendOut.Id,
                StartPositionId = ClosedPositionLeftShoulderToTheAudience.Id,
                EndPositionId = OpenPositionRightShoulderToTheAudience.Id,
                BeatCounts = 6,
                DynamicsType = DynamicsType.Regular
            };

            SendOutOption2 = new FigureOption()
            {
                Id = 29,
                FigureId = SendOut.Id,
                StartPositionId = ClosedPositionLeftShoulderToTheAudience.Id,
                EndPositionId = OpenPositionRightShoulderToTheAudience.Id,
                BeatCounts = 4,
                DynamicsType = DynamicsType.Fast
            };
        }

        private void SeedVerseChoreographies()
        {
            SwingVerseChoreography1 = new VerseChoreography()
            {
                Id = 1,
                Name = "Swing verse choreo for start",
                VerseTypeId = SwingVerse.Id,
                Score = 3,
                UserId = DemoUser.Id
            };

            SwingVerseChoreography2 = new VerseChoreography()
            {
                Id = 2,
                Name = "Swing verse choreo for final",
                VerseTypeId = SwingVerse.Id,
                Score = 4,
                UserId = DemoUser.Id
            };

            BluesVerseChoreography = new VerseChoreography()
            {
                Id = 3,
                Name = "Blues verse choreo in the middle",
                VerseTypeId = BluesVerse.Id,
                Score = 2,
                UserId = DemoUser.Id
            };
        }

        private void SeedVerseChoreographyFigures()
        {
            SwingVerse1Figure1 = new VerseChoreographyFigure()
            {
                Id = 1,
                VerseChoreographyId = SwingVerseChoreography1.Id,
                FigureOptionId = SendOutOption1.Id,
                FigureOrder = 1
            };

            SwingVerse1Figure2 = new VerseChoreographyFigure()
            {
                Id = 2,
                VerseChoreographyId = SwingVerseChoreography1.Id,
                FigureOptionId = ChangeOfPlaceOption1.Id,
                FigureOrder = 2
            };

            SwingVerse1Figure3 = new VerseChoreographyFigure()
            {
                Id = 3,
                VerseChoreographyId = SwingVerseChoreography1.Id,
                FigureOptionId = AmericanSpinOption4.Id,
                FigureOrder = 3
            };

            SwingVerse1Figure4 = new VerseChoreographyFigure()
            {
                Id = 4,
                VerseChoreographyId = SwingVerseChoreography1.Id,
                FigureOptionId = TunnelOption1.Id,
                FigureOrder = 4
            };

            SwingVerse2Figure1 = new VerseChoreographyFigure()
            {
                Id = 5,
                VerseChoreographyId = SwingVerseChoreography2.Id,
                FigureOptionId = ChangeOfPlaceOption1.Id,
                FigureOrder = 1
            };

            SwingVerse2Figure2 = new VerseChoreographyFigure()
            {
                Id = 6,
                VerseChoreographyId = SwingVerseChoreography2.Id,
                FigureOptionId = AmericanSpinOption1.Id,
                FigureOrder = 2
            };

            SwingVerse2Figure3 = new VerseChoreographyFigure()
            {
                Id = 7,
                VerseChoreographyId = SwingVerseChoreography2.Id,
                FigureOptionId = HelicopterOption1.Id,
                FigureOrder = 3
            };

            BluesVerseFigure1 = new VerseChoreographyFigure()
            {
                Id = 8,
                VerseChoreographyId = BluesVerseChoreography.Id,
                FigureOptionId = ChangeOfPlaceOption1.Id,
                FigureOrder = 1
            };

            BluesVerseFigure2 = new VerseChoreographyFigure()
            {
                Id = 9,
                VerseChoreographyId = BluesVerseChoreography.Id,
                FigureOptionId = SpinWithBlockOption2.Id,
                FigureOrder = 2
            };

            BluesVerseFigure3 = new VerseChoreographyFigure()
            {
                Id = 10,
                VerseChoreographyId = BluesVerseChoreography.Id,
                FigureOptionId = LeftSidePassOption1.Id,
                FigureOrder = 3
            };

            BluesVerseFigure4 = new VerseChoreographyFigure()
            {
                Id = 11,
                VerseChoreographyId = BluesVerseChoreography.Id,
                FigureOptionId = SwingOutOption2.Id,
                FigureOrder = 4
            };

            BluesVerseFigure5 = new VerseChoreographyFigure()
            {
                Id = 12,
                VerseChoreographyId = BluesVerseChoreography.Id,
                FigureOptionId = SendInOption1.Id,
                FigureOrder = 5
            };

            BluesVerseFigure6 = new VerseChoreographyFigure()
            {
                Id = 13,
                VerseChoreographyId = BluesVerseChoreography.Id,
                FigureOptionId = CartwheelOption1.Id,
                FigureOrder = 6
            };
        }

        private void SeedFullChoreographies()
        {
            FullChoreography = new FullChoreography()
            {
                Id = 1,
                Name = "Great Balls of Fire",
                UserId = DemoUser.Id
            };
        }

        private void SeedFullChoreographyVerses()
        {
            FullChoreographyVerse1 = new FullChoreographyVerseChoreography()
            {
                Id = 1,
                FullChoreographyId = FullChoreography.Id,
                VerseChoreographyId = SwingVerseChoreography1.Id,
                VerseChoreographyOrder = 1
            };

            FullChoreographyVerse2 = new FullChoreographyVerseChoreography()
            {
                Id = 2,
                FullChoreographyId = FullChoreography.Id,
                VerseChoreographyId = BluesVerseChoreography.Id,
                VerseChoreographyOrder = 2
            };

            FullChoreographyVerse3 = new FullChoreographyVerseChoreography()
            {
                Id = 3,
                FullChoreographyId = FullChoreography.Id,
                VerseChoreographyId = SwingVerseChoreography2.Id,
                VerseChoreographyOrder = 3
            };
        }
    }
}
