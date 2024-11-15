using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brewery.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddBeerStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales",
                schema: "brewery");

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
                name: "BeerStocks",
                schema: "brewery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BeerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerStocks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeerSales_WholesalerId",
                schema: "brewery",
                table: "BeerSales",
                column: "WholesalerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeerSales",
                schema: "brewery");

            migrationBuilder.DropTable(
                name: "BeerStocks",
                schema: "brewery");

            migrationBuilder.CreateTable(
                name: "Sales",
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
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Wholesalers_WholesalerId",
                        column: x => x.WholesalerId,
                        principalSchema: "brewery",
                        principalTable: "Wholesalers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_WholesalerId",
                schema: "brewery",
                table: "Sales",
                column: "WholesalerId");
        }
    }
}
