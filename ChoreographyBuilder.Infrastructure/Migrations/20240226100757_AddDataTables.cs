using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoreographyBuilder.Infrastructure.Migrations;

public partial class AddDataTables : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Figures",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false, comment: "Figure Identifier")
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Figure Name"),
                IsHighlight = table.Column<bool>(type: "bit", nullable: false, comment: "Figure Is Highlight"),
                IsFavourite = table.Column<bool>(type: "bit", nullable: false, comment: "Figure Is Favourite"),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "User Identifier")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Figures", x => x.Id);
                table.ForeignKey(
                    name: "FK_Figures_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            },
            comment: "Figures");

        migrationBuilder.CreateTable(
            name: "FullChoreographies",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false, comment: "Full Choreography Identifier")
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Full Choreography Name"),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "User Identifier")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FullChoreographies", x => x.Id);
                table.ForeignKey(
                    name: "FK_FullChoreographies_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            },
            comment: "Full Choreographies");

        migrationBuilder.CreateTable(
            name: "Positions",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false, comment: "Position Identifier")
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Position Name"),
                IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "Position Is Active")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Positions", x => x.Id);
            },
            comment: "Positions");

        migrationBuilder.CreateTable(
            name: "VerseTypes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false, comment: "Verse Identifier")
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Verse Name"),
                BeatCounts = table.Column<int>(type: "int", nullable: false, comment: "Verse Beat Counts"),
                IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "Verse Is Active")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_VerseTypes", x => x.Id);
            },
            comment: "Verse Types");

        migrationBuilder.CreateTable(
            name: "FigureOptions",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false, comment: "Figure Options Identifier")
                    .Annotation("SqlServer:Identity", "1, 1"),
                FigureId = table.Column<int>(type: "int", nullable: false, comment: "Figure Options Figure Identifier"),
                StartPositionId = table.Column<int>(type: "int", nullable: false, comment: "Figure Options Start Position Identifier"),
                EndPositionId = table.Column<int>(type: "int", nullable: false, comment: "Figure Options End Position Identifier"),
                BeatCounts = table.Column<int>(type: "int", nullable: false, comment: "Figure Option Beat Counts")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FigureOptions", x => x.Id);
                table.ForeignKey(
                    name: "FK_FigureOptions_Figures_FigureId",
                    column: x => x.FigureId,
                    principalTable: "Figures",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_FigureOptions_Positions_EndPositionId",
                    column: x => x.EndPositionId,
                    principalTable: "Positions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_FigureOptions_Positions_StartPositionId",
                    column: x => x.StartPositionId,
                    principalTable: "Positions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            },
            comment: "Figure Options");

        migrationBuilder.CreateTable(
            name: "VerseChoreographies",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false, comment: "Verse Choreograhy Identifier")
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Verse Chorography Name"),
                VerseTypeId = table.Column<int>(type: "int", nullable: false, comment: "Verse Type Identifier"),
                Score = table.Column<int>(type: "int", nullable: false, comment: "Verse Choreography Score at the time of saving it"),
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "User Identifier")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_VerseChoreographies", x => x.Id);
                table.ForeignKey(
                    name: "FK_VerseChoreographies_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_VerseChoreographies_VerseTypes_VerseTypeId",
                    column: x => x.VerseTypeId,
                    principalTable: "VerseTypes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            },
            comment: "Verse Choreographies");

        migrationBuilder.CreateTable(
            name: "FullChoreographiesVerseChoreographies",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false, comment: "Full Choreography Verse Choreography Identifier")
                    .Annotation("SqlServer:Identity", "1, 1"),
                FullChoreographyId = table.Column<int>(type: "int", nullable: false, comment: "Full Choreogprahy Identifier"),
                VerseChoreographyId = table.Column<int>(type: "int", nullable: false, comment: "Verse Choreography Identifier"),
                VerseChoreographyOrder = table.Column<int>(type: "int", nullable: false, comment: "Verse Choreography Order in which it appears in this choreography")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FullChoreographiesVerseChoreographies", x => x.Id);
                table.ForeignKey(
                    name: "FK_FullChoreographiesVerseChoreographies_FullChoreographies_FullChoreographyId",
                    column: x => x.FullChoreographyId,
                    principalTable: "FullChoreographies",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_FullChoreographiesVerseChoreographies_VerseChoreographies_VerseChoreographyId",
                    column: x => x.VerseChoreographyId,
                    principalTable: "VerseChoreographies",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            },
            comment: "Full Choreography Verse Choreographies");

        migrationBuilder.CreateTable(
            name: "VerseChoreographiesFigures",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false, comment: "Verse Choreograhy Figure Identifier")
                    .Annotation("SqlServer:Identity", "1, 1"),
                VerseChoreographyId = table.Column<int>(type: "int", nullable: false, comment: "Verse Choreography Identifier"),
                FigureId = table.Column<int>(type: "int", nullable: false, comment: "Figure Identifier"),
                FigureOrder = table.Column<int>(type: "int", nullable: false, comment: "Figure Order in which it appears in this choreography")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_VerseChoreographiesFigures", x => x.Id);
                table.ForeignKey(
                    name: "FK_VerseChoreographiesFigures_Figures_FigureId",
                    column: x => x.FigureId,
                    principalTable: "Figures",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_VerseChoreographiesFigures_VerseChoreographies_VerseChoreographyId",
                    column: x => x.VerseChoreographyId,
                    principalTable: "VerseChoreographies",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            },
            comment: "Verse Choreography Figures");

        migrationBuilder.CreateIndex(
            name: "IX_FigureOptions_EndPositionId",
            table: "FigureOptions",
            column: "EndPositionId");

        migrationBuilder.CreateIndex(
            name: "IX_FigureOptions_FigureId",
            table: "FigureOptions",
            column: "FigureId");

        migrationBuilder.CreateIndex(
            name: "IX_FigureOptions_StartPositionId",
            table: "FigureOptions",
            column: "StartPositionId");

        migrationBuilder.CreateIndex(
            name: "IX_Figures_UserId",
            table: "Figures",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_FullChoreographies_UserId",
            table: "FullChoreographies",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_FullChoreographiesVerseChoreographies_FullChoreographyId",
            table: "FullChoreographiesVerseChoreographies",
            column: "FullChoreographyId");

        migrationBuilder.CreateIndex(
            name: "IX_FullChoreographiesVerseChoreographies_VerseChoreographyId",
            table: "FullChoreographiesVerseChoreographies",
            column: "VerseChoreographyId");

        migrationBuilder.CreateIndex(
            name: "IX_VerseChoreographies_UserId",
            table: "VerseChoreographies",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_VerseChoreographies_VerseTypeId",
            table: "VerseChoreographies",
            column: "VerseTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_VerseChoreographiesFigures_FigureId",
            table: "VerseChoreographiesFigures",
            column: "FigureId");

        migrationBuilder.CreateIndex(
            name: "IX_VerseChoreographiesFigures_VerseChoreographyId",
            table: "VerseChoreographiesFigures",
            column: "VerseChoreographyId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "FigureOptions");

        migrationBuilder.DropTable(
            name: "FullChoreographiesVerseChoreographies");

        migrationBuilder.DropTable(
            name: "VerseChoreographiesFigures");

        migrationBuilder.DropTable(
            name: "Positions");

        migrationBuilder.DropTable(
            name: "FullChoreographies");

        migrationBuilder.DropTable(
            name: "Figures");

        migrationBuilder.DropTable(
            name: "VerseChoreographies");

        migrationBuilder.DropTable(
            name: "VerseTypes");
    }
}
