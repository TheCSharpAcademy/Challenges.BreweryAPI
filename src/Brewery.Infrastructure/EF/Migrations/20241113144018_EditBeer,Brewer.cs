using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brewery.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class EditBeerBrewer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_Breweries_BreweryId",
                schema: "brewery",
                table: "Beers");

            migrationBuilder.DropIndex(
                name: "IX_Beers_BreweryId",
                schema: "brewery",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "BreweryId",
                schema: "brewery",
                table: "Beers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BreweryId",
                schema: "brewery",
                table: "Beers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beers_BreweryId",
                schema: "brewery",
                table: "Beers",
                column: "BreweryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_Breweries_BreweryId",
                schema: "brewery",
                table: "Beers",
                column: "BreweryId",
                principalSchema: "brewery",
                principalTable: "Breweries",
                principalColumn: "Id");
        }
    }
}
