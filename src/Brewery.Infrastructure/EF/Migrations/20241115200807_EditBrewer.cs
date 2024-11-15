using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brewery.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class EditBrewer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BrewerId",
                schema: "brewery",
                table: "BeerStocks",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BeerStocks_BrewerId",
                schema: "brewery",
                table: "BeerStocks",
                column: "BrewerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BeerStocks_Brewers_BrewerId",
                schema: "brewery",
                table: "BeerStocks",
                column: "BrewerId",
                principalSchema: "brewery",
                principalTable: "Brewers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BeerStocks_Brewers_BrewerId",
                schema: "brewery",
                table: "BeerStocks");

            migrationBuilder.DropIndex(
                name: "IX_BeerStocks_BrewerId",
                schema: "brewery",
                table: "BeerStocks");

            migrationBuilder.DropColumn(
                name: "BrewerId",
                schema: "brewery",
                table: "BeerStocks");
        }
    }
}
