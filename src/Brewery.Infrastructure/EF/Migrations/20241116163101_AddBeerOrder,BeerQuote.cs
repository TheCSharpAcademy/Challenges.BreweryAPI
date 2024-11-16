using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brewery.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddBeerOrderBeerQuote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BeerSales_Wholesalers_WholesalerId",
                schema: "brewery",
                table: "BeerSales");

            migrationBuilder.AlterColumn<Guid>(
                name: "WholesalerId",
                schema: "brewery",
                table: "BeerSales",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BeerQuotes",
                schema: "brewery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiscountInPercent = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerQuotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BeerOrder",
                schema: "brewery",
                columns: table => new
                {
                    BeerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    BeerQuoteId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerOrder", x => x.BeerId);
                    table.ForeignKey(
                        name: "FK_BeerOrder_BeerQuotes_BeerQuoteId",
                        column: x => x.BeerQuoteId,
                        principalSchema: "brewery",
                        principalTable: "BeerQuotes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeerOrder_BeerQuoteId",
                schema: "brewery",
                table: "BeerOrder",
                column: "BeerQuoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_BeerSales_Wholesalers_WholesalerId",
                schema: "brewery",
                table: "BeerSales",
                column: "WholesalerId",
                principalSchema: "brewery",
                principalTable: "Wholesalers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BeerSales_Wholesalers_WholesalerId",
                schema: "brewery",
                table: "BeerSales");

            migrationBuilder.DropTable(
                name: "BeerOrder",
                schema: "brewery");

            migrationBuilder.DropTable(
                name: "BeerQuotes",
                schema: "brewery");

            migrationBuilder.AlterColumn<Guid>(
                name: "WholesalerId",
                schema: "brewery",
                table: "BeerSales",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_BeerSales_Wholesalers_WholesalerId",
                schema: "brewery",
                table: "BeerSales",
                column: "WholesalerId",
                principalSchema: "brewery",
                principalTable: "Wholesalers",
                principalColumn: "Id");
        }
    }
}
