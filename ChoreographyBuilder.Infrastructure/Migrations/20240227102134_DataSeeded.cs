using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoreographyBuilder.Infrastructure.Migrations;

public partial class DataSeeded : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Positions",
            type: "nvarchar(70)",
            maxLength: 70,
            nullable: false,
            comment: "Position Name",
            oldClrType: typeof(string),
            oldType: "nvarchar(20)",
            oldMaxLength: 20,
            oldComment: "Position Name");

        migrationBuilder.InsertData(
            table: "AspNetUsers",
            columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
            values: new object[] { "dea12856-c198-4129-b3f3-b893d8395082", 0, "9544bf4e-d70f-4fef-b1f1-1294c6f23d41", "demo@mail.com", false, false, null, "DEMO@MAIL.COM", "DEMO@MAIL.COM", "AQAAAAEAACcQAAAAECU+hIkNXH/25BmnL1Twka/s67PgDwf6yXpqbHAD4yw8WWjhatrBUEC4DPZ12NUMlA==", null, false, "8d18407b-5b1d-4d51-aa01-b7c7eb347205", false, "demo@mail.com" });

        migrationBuilder.InsertData(
            table: "Positions",
            columns: new[] { "Id", "IsActive", "Name" },
            values: new object[,]
            {
                { 1, true, "Open position with left shoulder to the audience" },
                { 2, true, "Open position with right shoulder to the audience" },
                { 3, true, "Closed position with left shoulder to the audience" },
                { 4, true, "Closed position with right shoulder to the audience" }
            });

        migrationBuilder.InsertData(
            table: "VerseTypes",
            columns: new[] { "Id", "BeatCounts", "IsActive", "Name" },
            values: new object[,]
            {
                { 1, 32, true, "Swing Verse" },
                { 2, 48, true, "Blues Verse" }
            });

        migrationBuilder.InsertData(
            table: "Figures",
            columns: new[] { "Id", "IsFavourite", "IsHighlight", "Name", "UserId" },
            values: new object[,]
            {
                { 1, false, false, "Change of place", "dea12856-c198-4129-b3f3-b893d8395082" },
                { 2, false, false, "American Spin", "dea12856-c198-4129-b3f3-b893d8395082" },
                { 3, true, false, "Spin with block", "dea12856-c198-4129-b3f3-b893d8395082" },
                { 4, true, true, "Tunnel", "dea12856-c198-4129-b3f3-b893d8395082" },
                { 5, true, true, "Cartwheel", "dea12856-c198-4129-b3f3-b893d8395082" },
                { 6, false, false, "Get into closed", "dea12856-c198-4129-b3f3-b893d8395082" },
                { 7, false, false, "Swing Out", "dea12856-c198-4129-b3f3-b893d8395082" },
                { 8, false, true, "Helicopter", "dea12856-c198-4129-b3f3-b893d8395082" },
                { 9, false, false, "Left Side Pass", "dea12856-c198-4129-b3f3-b893d8395082" },
                { 10, true, false, "Send out", "dea12856-c198-4129-b3f3-b893d8395082" }
            });

        migrationBuilder.InsertData(
            table: "VerseChoreographies",
            columns: new[] { "Id", "Name", "Score", "UserId", "VerseTypeId" },
            values: new object[,]
            {
                { 1, "Swing verse choreo for start", 3, "dea12856-c198-4129-b3f3-b893d8395082", 1 },
                { 2, "Swing verse choreo for final", 4, "dea12856-c198-4129-b3f3-b893d8395082", 1 },
                { 3, "Blues verse choreo in the middle", 2, "dea12856-c198-4129-b3f3-b893d8395082", 2 }
            });

        migrationBuilder.InsertData(
            table: "FigureOptions",
            columns: new[] { "Id", "BeatCounts", "DynamicsType", "EndPositionId", "FigureId", "StartPositionId" },
            values: new object[,]
            {
                { 1, 6, 1, 1, 1, 2 },
                { 2, 4, 2, 1, 1, 2 },
                { 3, 6, 1, 1, 2, 1 },
                { 4, 6, 1, 2, 2, 2 },
                { 5, 4, 2, 2, 2, 2 },
                { 6, 4, 2, 1, 2, 1 },
                { 7, 2, 2, 1, 2, 1 },
                { 8, 2, 2, 2, 2, 2 },
                { 9, 6, 1, 3, 2, 1 },
                { 10, 6, 1, 4, 2, 2 },
                { 11, 4, 2, 3, 2, 1 },
                { 12, 4, 2, 4, 2, 2 },
                { 13, 6, 1, 2, 3, 2 },
                { 14, 6, 1, 1, 3, 1 },
                { 15, 4, 2, 2, 3, 2 },
                { 16, 4, 2, 1, 3, 1 },
                { 17, 16, 1, 2, 4, 1 },
                { 18, 16, 1, 1, 4, 2 },
                { 19, 16, 2, 2, 5, 3 },
                { 20, 6, 1, 3, 6, 2 },
                { 21, 4, 2, 3, 6, 2 },
                { 22, 8, 0, 1, 7, 1 },
                { 23, 8, 0, 2, 7, 2 },
                { 24, 6, 2, 1, 7, 1 },
                { 25, 6, 2, 2, 7, 2 },
                { 26, 20, 1, 3, 8, 1 },
                { 27, 6, 1, 2, 9, 1 },
                { 28, 6, 1, 2, 10, 3 },
                { 29, 4, 2, 2, 10, 3 }
            });

        migrationBuilder.InsertData(
            table: "VerseChoreographiesFigures",
            columns: new[] { "Id", "FigureOptionId", "FigureOrder", "VerseChoreographyId" },
            values: new object[,]
            {
                { 1, 28, 1, 1 },
                { 2, 1, 2, 1 },
                { 3, 6, 3, 1 },
                { 4, 17, 4, 1 },
                { 5, 1, 1, 2 },
                { 6, 3, 2, 2 },
                { 7, 26, 3, 2 },
                { 8, 1, 1, 3 },
                { 9, 14, 2, 3 },
                { 10, 27, 3, 3 },
                { 11, 23, 4, 3 },
                { 12, 20, 5, 3 },
                { 13, 19, 6, 3 }
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 4);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 5);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 7);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 8);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 9);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 10);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 11);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 12);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 13);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 15);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 16);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 18);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 21);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 22);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 24);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 25);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 29);

        migrationBuilder.DeleteData(
            table: "VerseChoreographiesFigures",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "VerseChoreographiesFigures",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "VerseChoreographiesFigures",
            keyColumn: "Id",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "VerseChoreographiesFigures",
            keyColumn: "Id",
            keyValue: 4);

        migrationBuilder.DeleteData(
            table: "VerseChoreographiesFigures",
            keyColumn: "Id",
            keyValue: 5);

        migrationBuilder.DeleteData(
            table: "VerseChoreographiesFigures",
            keyColumn: "Id",
            keyValue: 6);

        migrationBuilder.DeleteData(
            table: "VerseChoreographiesFigures",
            keyColumn: "Id",
            keyValue: 7);

        migrationBuilder.DeleteData(
            table: "VerseChoreographiesFigures",
            keyColumn: "Id",
            keyValue: 8);

        migrationBuilder.DeleteData(
            table: "VerseChoreographiesFigures",
            keyColumn: "Id",
            keyValue: 9);

        migrationBuilder.DeleteData(
            table: "VerseChoreographiesFigures",
            keyColumn: "Id",
            keyValue: 10);

        migrationBuilder.DeleteData(
            table: "VerseChoreographiesFigures",
            keyColumn: "Id",
            keyValue: 11);

        migrationBuilder.DeleteData(
            table: "VerseChoreographiesFigures",
            keyColumn: "Id",
            keyValue: 12);

        migrationBuilder.DeleteData(
            table: "VerseChoreographiesFigures",
            keyColumn: "Id",
            keyValue: 13);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 6);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 14);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 17);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 19);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 20);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 23);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 26);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 27);

        migrationBuilder.DeleteData(
            table: "FigureOptions",
            keyColumn: "Id",
            keyValue: 28);

        migrationBuilder.DeleteData(
            table: "Positions",
            keyColumn: "Id",
            keyValue: 4);

        migrationBuilder.DeleteData(
            table: "VerseChoreographies",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "VerseChoreographies",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "VerseChoreographies",
            keyColumn: "Id",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "Figures",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Figures",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "Figures",
            keyColumn: "Id",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "Figures",
            keyColumn: "Id",
            keyValue: 4);

        migrationBuilder.DeleteData(
            table: "Figures",
            keyColumn: "Id",
            keyValue: 5);

        migrationBuilder.DeleteData(
            table: "Figures",
            keyColumn: "Id",
            keyValue: 6);

        migrationBuilder.DeleteData(
            table: "Figures",
            keyColumn: "Id",
            keyValue: 7);

        migrationBuilder.DeleteData(
            table: "Figures",
            keyColumn: "Id",
            keyValue: 8);

        migrationBuilder.DeleteData(
            table: "Figures",
            keyColumn: "Id",
            keyValue: 9);

        migrationBuilder.DeleteData(
            table: "Figures",
            keyColumn: "Id",
            keyValue: 10);

        migrationBuilder.DeleteData(
            table: "Positions",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Positions",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "Positions",
            keyColumn: "Id",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "VerseTypes",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "VerseTypes",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Positions",
            type: "nvarchar(20)",
            maxLength: 20,
            nullable: false,
            comment: "Position Name",
            oldClrType: typeof(string),
            oldType: "nvarchar(70)",
            oldMaxLength: 70,
            oldComment: "Position Name");
    }
}
