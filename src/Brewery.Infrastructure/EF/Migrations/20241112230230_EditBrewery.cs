using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brewery.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class EditBrewery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breweries_Brewers_BrewerId",
                schema: "brewery",
                table: "Breweries");

            migrationBuilder.DropIndex(
                name: "IX_Breweries_BrewerId",
                schema: "brewery",
                table: "Breweries");

            migrationBuilder.DropColumn(
                name: "BrewerId",
                schema: "brewery",
                table: "Breweries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BrewerId",
                schema: "brewery",
                table: "Breweries",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Breweries_BrewerId",
                schema: "brewery",
                table: "Breweries",
                column: "BrewerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Breweries_Brewers_BrewerId",
                schema: "brewery",
                table: "Breweries",
                column: "BrewerId",
                principalSchema: "brewery",
                principalTable: "Brewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
