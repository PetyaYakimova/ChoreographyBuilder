using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoreographyBuilder.Infrastructure.Migrations
{
    public partial class SeedRolesAdminAndFullChoreography : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a18a096-97da-4ae4-9d46-88314532d82b", "6ad0f323-1844-4155-a6d5-0d57553c7e93", "User", "USER" },
                    { "40271de3-61e3-49a3-b580-087252ce0546", "917beabd-8b06-4d25-9794-5d6de1054f33", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b7120f0-76d2-4f29-b5ff-58ee25bc1c0d", "AQAAAAEAACcQAAAAEGZVNEAHiULxhERiezZpbufDfjJEzch0v9cG8hBTiI7qQyQ/e+IbdO48SveYkKpfRw==", "f9106163-71ff-405e-81f1-f78a2eedf8fc" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fd6820c7-db68-4695-8a9d-88559d48e0ec", 0, "acda5dbe-d635-4d6c-aa63-ca9b76fc9b59", "admin@mail.com", false, false, null, "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAEAACcQAAAAEBcPmSG/hDqX3MShqhF0y+SDa4LEsjn5EbwKZ2dEopWp7nrRHGuukAVKlWpFJy4jxw==", null, false, "139505a6-ef57-40aa-a61f-3acf5e86a44d", false, "admin@mail.com" });

            migrationBuilder.InsertData(
                table: "FullChoreographies",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[] { 1, "Great Balls of Fire", "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2a18a096-97da-4ae4-9d46-88314532d82b", "dea12856-c198-4129-b3f3-b893d8395082" },
                    { "40271de3-61e3-49a3-b580-087252ce0546", "fd6820c7-db68-4695-8a9d-88559d48e0ec" }
                });

            migrationBuilder.InsertData(
                table: "FullChoreographiesVerseChoreographies",
                columns: new[] { "Id", "FullChoreographyId", "VerseChoreographyId", "VerseChoreographyOrder" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 1, 3, 2 },
                    { 3, 1, 2, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2a18a096-97da-4ae4-9d46-88314532d82b", "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "40271de3-61e3-49a3-b580-087252ce0546", "fd6820c7-db68-4695-8a9d-88559d48e0ec" });

            migrationBuilder.DeleteData(
                table: "FullChoreographiesVerseChoreographies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FullChoreographiesVerseChoreographies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FullChoreographiesVerseChoreographies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a18a096-97da-4ae4-9d46-88314532d82b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40271de3-61e3-49a3-b580-087252ce0546");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd6820c7-db68-4695-8a9d-88559d48e0ec");

            migrationBuilder.DeleteData(
                table: "FullChoreographies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9544bf4e-d70f-4fef-b1f1-1294c6f23d41", "AQAAAAEAACcQAAAAECU+hIkNXH/25BmnL1Twka/s67PgDwf6yXpqbHAD4yw8WWjhatrBUEC4DPZ12NUMlA==", "8d18407b-5b1d-4d51-aa01-b7c7eb347205" });
        }
    }
}
