using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoreographyBuilder.Infrastructure.Migrations
{
    public partial class AddDynamicsType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DynamicsType",
                table: "FigureOptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DynamicsType",
                table: "FigureOptions");
        }
    }
}
