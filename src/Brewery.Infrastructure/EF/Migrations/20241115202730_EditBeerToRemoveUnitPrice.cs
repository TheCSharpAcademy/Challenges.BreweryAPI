using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brewery.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class EditBeerToRemoveUnitPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "brewery");

            migrationBuilder.CreateTable(
                name: "Breweries",
                schema: "brewery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breweries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wholesalers",
                schema: "brewery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wholesalers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brewers",
                schema: "brewery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BreweryId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brewers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brewers_Breweries_BreweryId",
                        column: x => x.BreweryId,
                        principalSchema: "brewery",
                        principalTable: "Breweries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BeerSales",
                schema: "brewery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BeerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    WholesalerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeerSales_Wholesalers_WholesalerId",
                        column: x => x.WholesalerId,
                        principalSchema: "brewery",
                        principalTable: "Wholesalers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Beers",
                schema: "brewery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BrewerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beers_Brewers_BrewerId",
                        column: x => x.BrewerId,
                        principalSchema: "brewery",
                        principalTable: "Brewers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BeerStocks",
                schema: "brewery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BeerId = table.Column<Guid>(type: "uuid", nullable: false),
                    BrewerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeerStocks_Brewers_BrewerId",
                        column: x => x.BrewerId,
                        principalSchema: "brewery",
                        principalTable: "Brewers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beers_BrewerId",
                schema: "brewery",
                table: "Beers",
                column: "BrewerId");

            migrationBuilder.CreateIndex(
                name: "IX_BeerSales_WholesalerId",
                schema: "brewery",
                table: "BeerSales",
                column: "WholesalerId");

            migrationBuilder.CreateIndex(
                name: "IX_BeerStocks_BrewerId",
                schema: "brewery",
                table: "BeerStocks",
                column: "BrewerId");

            migrationBuilder.CreateIndex(
                name: "IX_Brewers_BreweryId",
                schema: "brewery",
                table: "Brewers",
                column: "BreweryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beers",
                schema: "brewery");

            migrationBuilder.DropTable(
                name: "BeerSales",
                schema: "brewery");

            migrationBuilder.DropTable(
                name: "BeerStocks",
                schema: "brewery");

            migrationBuilder.DropTable(
                name: "Wholesalers",
                schema: "brewery");

            migrationBuilder.DropTable(
                name: "Brewers",
                schema: "brewery");

            migrationBuilder.DropTable(
                name: "Breweries",
                schema: "brewery");
        }
    }
}
