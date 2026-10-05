using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExchangeRate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromCurrency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ToCurrency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ExchangeRates",
                columns: new[] { "Id", "FromCurrency", "Rate", "ToCurrency", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "USD", 83.2150m, "INR", new DateTime(2026, 10, 1, 10, 30, 0, 0, DateTimeKind.Utc) },
                    { 2, "EUR", 91.4800m, "INR", new DateTime(2026, 10, 1, 10, 30, 0, 0, DateTimeKind.Utc) },
                    { 3, "GBP", 108.7300m, "INR", new DateTime(2026, 10, 1, 10, 30, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_FromCurrency_ToCurrency",
                table: "ExchangeRates",
                columns: new[] { "FromCurrency", "ToCurrency" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeRates");
        }
    }
}
