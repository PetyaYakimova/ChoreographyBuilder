using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoreographyBuilder.Infrastructure.Migrations;

public partial class FixTyposINCOlumnNames : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "Id",
            table: "VerseChoreographiesFigures",
            type: "int",
            nullable: false,
            comment: "Verse Choreography Figure Identifier",
            oldClrType: typeof(int),
            oldType: "int",
            oldComment: "Verse Choreograhy Figure Identifier")
            .Annotation("SqlServer:Identity", "1, 1")
            .OldAnnotation("SqlServer:Identity", "1, 1");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "VerseChoreographies",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            comment: "Verse Choreography Name",
            oldClrType: typeof(string),
            oldType: "nvarchar(50)",
            oldMaxLength: 50,
            oldComment: "Verse Chorography Name");

        migrationBuilder.AlterColumn<int>(
            name: "Id",
            table: "VerseChoreographies",
            type: "int",
            nullable: false,
            comment: "Verse Choreography Identifier",
            oldClrType: typeof(int),
            oldType: "int",
            oldComment: "Verse Choreograhy Identifier")
            .Annotation("SqlServer:Identity", "1, 1")
            .OldAnnotation("SqlServer:Identity", "1, 1");

        migrationBuilder.AlterColumn<int>(
            name: "FullChoreographyId",
            table: "FullChoreographiesVerseChoreographies",
            type: "int",
            nullable: false,
            comment: "Full Choreography Identifier",
            oldClrType: typeof(int),
            oldType: "int",
            oldComment: "Full Choreogprahy Identifier");

        migrationBuilder.UpdateData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: "2a18a096-97da-4ae4-9d46-88314532d82b",
            column: "ConcurrencyStamp",
            value: "bf3f0ee2-07a0-401d-9e57-6ff3fe2bf925");

        migrationBuilder.UpdateData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: "40271de3-61e3-49a3-b580-087252ce0546",
            column: "ConcurrencyStamp",
            value: "855a29db-cf18-46a5-9b3d-a2eb4a266a72");

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "a2a596d4-c39a-4545-855b-9ecea1a26147", "AQAAAAEAACcQAAAAEAjJt0WLPsLLH1i4ipAZUYd8gVT4hbvr4wH6lyG4JCBriECyEsJAw9dTvTaGpy9+Xw==", "0bcd8317-1aea-44fd-93d1-b2732e4f0095" });

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: "fd6820c7-db68-4695-8a9d-88559d48e0ec",
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "c6b5c630-6796-40f2-8cc5-e006722405d2", "AQAAAAEAACcQAAAAEFALiBZYrNW+2RXIu/KhvH2hHGTlr+9buBQwN+/8hpzLMngDzWdvmuEq8GVLNq+f8g==", "46234cd0-d58d-4dd2-8496-e337119b866c" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "Id",
            table: "VerseChoreographiesFigures",
            type: "int",
            nullable: false,
            comment: "Verse Choreograhy Figure Identifier",
            oldClrType: typeof(int),
            oldType: "int",
            oldComment: "Verse Choreography Figure Identifier")
            .Annotation("SqlServer:Identity", "1, 1")
            .OldAnnotation("SqlServer:Identity", "1, 1");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "VerseChoreographies",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            comment: "Verse Chorography Name",
            oldClrType: typeof(string),
            oldType: "nvarchar(50)",
            oldMaxLength: 50,
            oldComment: "Verse Choreography Name");

        migrationBuilder.AlterColumn<int>(
            name: "Id",
            table: "VerseChoreographies",
            type: "int",
            nullable: false,
            comment: "Verse Choreograhy Identifier",
            oldClrType: typeof(int),
            oldType: "int",
            oldComment: "Verse Choreography Identifier")
            .Annotation("SqlServer:Identity", "1, 1")
            .OldAnnotation("SqlServer:Identity", "1, 1");

        migrationBuilder.AlterColumn<int>(
            name: "FullChoreographyId",
            table: "FullChoreographiesVerseChoreographies",
            type: "int",
            nullable: false,
            comment: "Full Choreogprahy Identifier",
            oldClrType: typeof(int),
            oldType: "int",
            oldComment: "Full Choreography Identifier");

        migrationBuilder.UpdateData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: "2a18a096-97da-4ae4-9d46-88314532d82b",
            column: "ConcurrencyStamp",
            value: "6ad0f323-1844-4155-a6d5-0d57553c7e93");

        migrationBuilder.UpdateData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: "40271de3-61e3-49a3-b580-087252ce0546",
            column: "ConcurrencyStamp",
            value: "917beabd-8b06-4d25-9794-5d6de1054f33");

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "1b7120f0-76d2-4f29-b5ff-58ee25bc1c0d", "AQAAAAEAACcQAAAAEGZVNEAHiULxhERiezZpbufDfjJEzch0v9cG8hBTiI7qQyQ/e+IbdO48SveYkKpfRw==", "f9106163-71ff-405e-81f1-f78a2eedf8fc" });

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: "fd6820c7-db68-4695-8a9d-88559d48e0ec",
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "acda5dbe-d635-4d6c-aa63-ca9b76fc9b59", "AQAAAAEAACcQAAAAEBcPmSG/hDqX3MShqhF0y+SDa4LEsjn5EbwKZ2dEopWp7nrRHGuukAVKlWpFJy4jxw==", "139505a6-ef57-40aa-a61f-3acf5e86a44d" });
    }
}
