using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountAPI2.Migrations
{
    /// <inheritdoc />
    public partial class password : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UodatedAr" },
                values: new object[] { new DateTime(2025, 4, 22, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 4, 22, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UodatedAr" },
                values: new object[] { new DateTime(2025, 4, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 4, 21, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
