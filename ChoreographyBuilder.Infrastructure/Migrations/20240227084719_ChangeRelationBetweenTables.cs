using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoreographyBuilder.Infrastructure.Migrations;

public partial class ChangeRelationBetweenTables : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_VerseChoreographiesFigures_Figures_FigureId",
            table: "VerseChoreographiesFigures");

        migrationBuilder.DropIndex(
            name: "IX_VerseChoreographiesFigures_FigureId",
            table: "VerseChoreographiesFigures");

        migrationBuilder.DropColumn(
            name: "FigureId",
            table: "VerseChoreographiesFigures");

        migrationBuilder.AddColumn<int>(
            name: "FigureOptionId",
            table: "VerseChoreographiesFigures",
            type: "int",
            nullable: false,
            defaultValue: 0,
            comment: "Figure Option Identifier");

        migrationBuilder.CreateIndex(
            name: "IX_VerseChoreographiesFigures_FigureOptionId",
            table: "VerseChoreographiesFigures",
            column: "FigureOptionId");

        migrationBuilder.AddForeignKey(
            name: "FK_VerseChoreographiesFigures_FigureOptions_FigureOptionId",
            table: "VerseChoreographiesFigures",
            column: "FigureOptionId",
            principalTable: "FigureOptions",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_VerseChoreographiesFigures_FigureOptions_FigureOptionId",
            table: "VerseChoreographiesFigures");

        migrationBuilder.DropIndex(
            name: "IX_VerseChoreographiesFigures_FigureOptionId",
            table: "VerseChoreographiesFigures");

        migrationBuilder.DropColumn(
            name: "FigureOptionId",
            table: "VerseChoreographiesFigures");

        migrationBuilder.AddColumn<int>(
            name: "FigureId",
            table: "VerseChoreographiesFigures",
            type: "int",
            nullable: false,
            defaultValue: 0,
            comment: "Figure Identifier");

        migrationBuilder.CreateIndex(
            name: "IX_VerseChoreographiesFigures_FigureId",
            table: "VerseChoreographiesFigures",
            column: "FigureId");

        migrationBuilder.AddForeignKey(
            name: "FK_VerseChoreographiesFigures_Figures_FigureId",
            table: "VerseChoreographiesFigures",
            column: "FigureId",
            principalTable: "Figures",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
