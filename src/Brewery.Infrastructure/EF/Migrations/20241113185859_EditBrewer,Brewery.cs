using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brewery.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class EditBrewerBrewery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BreweryId",
                schema: "brewery",
                table: "Brewers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brewers_BreweryId",
                schema: "brewery",
                table: "Brewers",
                column: "BreweryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brewers_Breweries_BreweryId",
                schema: "brewery",
                table: "Brewers",
                column: "BreweryId",
                principalSchema: "brewery",
                principalTable: "Breweries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brewers_Breweries_BreweryId",
                schema: "brewery",
                table: "Brewers");

            migrationBuilder.DropIndex(
                name: "IX_Brewers_BreweryId",
                schema: "brewery",
                table: "Brewers");

            migrationBuilder.DropColumn(
                name: "BreweryId",
                schema: "brewery",
                table: "Brewers");
        }
    }
}
