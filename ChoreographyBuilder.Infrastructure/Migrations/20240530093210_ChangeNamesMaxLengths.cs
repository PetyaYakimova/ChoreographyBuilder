using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoreographyBuilder.Infrastructure.Migrations;

public partial class ChangeNamesMaxLengths : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "VerseChoreographies",
            type: "nvarchar(70)",
            maxLength: 70,
            nullable: false,
            comment: "Verse Choreography Name",
            oldClrType: typeof(string),
            oldType: "nvarchar(50)",
            oldMaxLength: 50,
            oldComment: "Verse Choreography Name");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "FullChoreographies",
            type: "nvarchar(70)",
            maxLength: 70,
            nullable: false,
            comment: "Full Choreography Name",
            oldClrType: typeof(string),
            oldType: "nvarchar(50)",
            oldMaxLength: 50,
            oldComment: "Full Choreography Name");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Figures",
            type: "nvarchar(70)",
            maxLength: 70,
            nullable: false,
            comment: "Figure Name",
            oldClrType: typeof(string),
            oldType: "nvarchar(30)",
            oldMaxLength: 30,
            oldComment: "Figure Name");

        migrationBuilder.UpdateData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: "2a18a096-97da-4ae4-9d46-88314532d82b",
            column: "ConcurrencyStamp",
            value: "5bf331c9-fac7-473e-9c54-a1382db905b3");

        migrationBuilder.UpdateData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: "40271de3-61e3-49a3-b580-087252ce0546",
            column: "ConcurrencyStamp",
            value: "3751398f-ae47-44d9-aa67-27d534431de5");

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "aff040d4-8fb7-4f26-83a3-22aba4c84c38", "AQAAAAEAACcQAAAAEAsXKkpWT0KrNy5XidvIBskQyUT+00grXeNB4ysd71AoLW2b5h9QuexNB0Zd/IsfBw==", "2f1bd226-bb1e-4f29-af0a-29c8052bac6a" });

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: "fd6820c7-db68-4695-8a9d-88559d48e0ec",
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "8b078560-6e0b-4fea-a6a7-e866e36d2f66", "AQAAAAEAACcQAAAAEA3jqEMwJBRwTrfCAm+03xiakDK/63q8RRP0TKFx3Y6Kcp65Faet3hnrutXCx7fqOg==", "07b6bef7-9c2f-467c-8da9-27eb33a9808d" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "VerseChoreographies",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            comment: "Verse Choreography Name",
            oldClrType: typeof(string),
            oldType: "nvarchar(70)",
            oldMaxLength: 70,
            oldComment: "Verse Choreography Name");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "FullChoreographies",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            comment: "Full Choreography Name",
            oldClrType: typeof(string),
            oldType: "nvarchar(70)",
            oldMaxLength: 70,
            oldComment: "Full Choreography Name");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Figures",
            type: "nvarchar(30)",
            maxLength: 30,
            nullable: false,
            comment: "Figure Name",
            oldClrType: typeof(string),
            oldType: "nvarchar(70)",
            oldMaxLength: 70,
            oldComment: "Figure Name");

        migrationBuilder.UpdateData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: "2a18a096-97da-4ae4-9d46-88314532d82b",
            column: "ConcurrencyStamp",
            value: "495e01ce-e95d-4425-8c00-7251e6c2dce4");

        migrationBuilder.UpdateData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: "40271de3-61e3-49a3-b580-087252ce0546",
            column: "ConcurrencyStamp",
            value: "c39d4527-cea1-4e5d-9ba0-d212c9c10cfa");

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "d1547cf2-50d2-45c0-81fe-acde0ac1a903", "AQAAAAEAACcQAAAAECU9YKgTZtO2shCBRjeqOUwxv4PLAzD3V1SpYS28oAMY7AyzyF/Vi7SZnYyxMCZMRQ==", "60f18afc-c3d8-4b46-84a9-05b7fd04cff0" });

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: "fd6820c7-db68-4695-8a9d-88559d48e0ec",
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "e83f55c1-354d-4f34-a0f3-c72571d0cf7d", "AQAAAAEAACcQAAAAEHZ6rDRf+sz2TV2dLIpH4NFeYlOi7euSfQs26iCcE8zjvUpDLT+cEqlvQENTo6kr7w==", "897c9fe8-e210-4422-a56e-306e5ae9a38d" });
    }
}
