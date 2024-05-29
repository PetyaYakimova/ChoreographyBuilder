using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoreographyBuilder.Infrastructure.Migrations
{
    public partial class AddCanBeSharedPropertyFOrFigure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanBeShared",
                table: "Figures",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Figure Can Be Shared With Other Users");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanBeShared",
                table: "Figures");

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
    }
}
