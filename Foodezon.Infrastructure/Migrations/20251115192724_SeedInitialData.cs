using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foodezon.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2025, 11, 15, 19, 27, 24, 106, DateTimeKind.Utc).AddTicks(193), "Indian cuisine", "Indian", null });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "IsAvailable", "Name", "Price", "UpdatedAt" },
                values: new object[] { 1, 1, new DateTime(2025, 11, 15, 19, 27, 24, 106, DateTimeKind.Utc).AddTicks(3784), "Grilled paneer with spices", "https://example.com/paneer-tikka.jpg", true, "Paneer Tikka", 9.99m, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dishes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
