using ChoreographyBuilder.Infrastructure.Data.Models;
using ChoreographyBuilder.Infrastructure.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace ChoreographyBuilder.Infrastructure.Data.SeedDb
{
	internal class SeedData
	{
		public IdentityUser DemoUser { get; set; }

		public VerseType SwingVerse { get; set; }

		public VerseType BluesVerse { get; set; }

		public Position OpenPositionLeftShoulderToTheAudience { get; set; }

		public Position OpenPositionRightShoulderToTheAudience { get; set; }

		public Position ClosedPositionLeftShoulderToTheAudience { get; set; }

		public Position ClosedPositionRightShoulderToTheAudience { get; set; }

		public Figure ChangeOfPlace { get; set; }

		public Figure AmericanSpin { get; set; }

		public Figure SpinWithBlock { get; set; }

		public Figure Tunnel { get; set; }

		public Figure Cartwheel { get; set; }

		public Figure SendIn { get; set; }

		public Figure SwingOut { get; set; }

		public Figure Helicopter { get; set; }

		public Figure LeftSidePass { get; set; }

		public Figure SendOut { get; set; }

		public FigureOption ChangeOfPlaceOption1 { get; set; }

		public FigureOption ChangeOfPlaceOption2 { get; set; }

		public FigureOption AmericanSpinOption1 { get; set; }

		public FigureOption AmericanSpinOption2 { get; set; }

		public FigureOption AmericanSpinOption3 { get; set; }

		public FigureOption AmericanSpinOption4 { get; set; }

		public FigureOption AmericanSpinOption5 { get; set; }

		public FigureOption AmericanSpinOption6 { get; set; }

		public FigureOption AmericanSpinOption7 { get; set; }

		public FigureOption AmericanSpinOption8 { get; set; }

		public FigureOption AmericanSpinOption9 { get; set; }

		public FigureOption AmericanSpinOption10 { get; set; }

		public FigureOption SpinWithBlockOption1 { get; set; }

		public FigureOption SpinWithBlockOption2 { get; set; }

		public FigureOption SpinWithBlockOption3 { get; set; }

		public FigureOption SpinWithBlockOption4 { get; set; }

		public FigureOption TunnelOption1 { get; set; }

		public FigureOption TunnelOption2 { get; set; }

		public FigureOption CartwheelOption1 { get; set; }

		public FigureOption SendInOption1 { get; set; }

		public FigureOption SendInOption2 { get; set; }

		public FigureOption SwingOutOption1 { get; set; }

		public FigureOption SwingOutOption2 { get; set; }

		public FigureOption SwingOutOption3 { get; set; }

		public FigureOption SwingOutOption4 { get; set; }

		public FigureOption HelicopterOption1 { get; set; }

		public FigureOption LeftSidePassOption1 { get; set; }

		public FigureOption SendOutOption1 { get; set; }

		public FigureOption SendOutOption2 { get; set; }

		public VerseChoreography SwingVerseChoreography1 { get; set; }

		public VerseChoreography SwingVerseChoreography2 { get; set; }

		public VerseChoreography BluesVerseChoreography { get; set; }

		public VerseChoreographyFigure SwingVerse1Figure1 { get; set; }

		public VerseChoreographyFigure SwingVerse1Figure2 { get; set; }

		public VerseChoreographyFigure SwingVerse1Figure3 { get; set; }

		public VerseChoreographyFigure SwingVerse1Figure4 { get; set; }

		public VerseChoreographyFigure SwingVerse2Figure1 { get; set; }

		public VerseChoreographyFigure SwingVerse2Figure2 { get; set; }

		public VerseChoreographyFigure SwingVerse2Figure3 { get; set; }

		public VerseChoreographyFigure BluesVerseFigure1 { get; set; }

		public VerseChoreographyFigure BluesVerseFigure2 { get; set; }

		public VerseChoreographyFigure BluesVerseFigure3 { get; set; }

		public VerseChoreographyFigure BluesVerseFigure4 { get; set; }

		public VerseChoreographyFigure BluesVerseFigure5 { get; set; }

		public VerseChoreographyFigure BluesVerseFigure6 { get; set; }


		public SeedData()
		{
			SeedUsers();
			SeedVerseTypes();
			SeedPositions();
			SeedFigures();
			SeedFigureOptions();
			SeedVerseChoreogprahies();
			SeedVerseChoreographyFigures();
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

		private void SeedVerseChoreogprahies()
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

	}
}
