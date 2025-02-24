using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDoDienTu.Migrations
{
    /// <inheritdoc />
    public partial class AddActionPostToPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 27, 15, 47, 24, 808, DateTimeKind.Local).AddTicks(4079), new DateTime(2024, 11, 7, 15, 47, 24, 808, DateTimeKind.Local).AddTicks(4057) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 47, 24, 808, DateTimeKind.Local).AddTicks(4083), new DateTime(2024, 11, 12, 15, 47, 24, 808, DateTimeKind.Local).AddTicks(4082) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 12, 7, 15, 47, 24, 808, DateTimeKind.Local).AddTicks(4086), new DateTime(2024, 11, 16, 15, 47, 24, 808, DateTimeKind.Local).AddTicks(4085) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 12, 17, 15, 47, 24, 808, DateTimeKind.Local).AddTicks(4089), new DateTime(2024, 10, 18, 15, 47, 24, 808, DateTimeKind.Local).AddTicks(4088) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 27, 15, 41, 50, 755, DateTimeKind.Local).AddTicks(177), new DateTime(2024, 11, 7, 15, 41, 50, 755, DateTimeKind.Local).AddTicks(163) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 41, 50, 755, DateTimeKind.Local).AddTicks(181), new DateTime(2024, 11, 12, 15, 41, 50, 755, DateTimeKind.Local).AddTicks(181) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 12, 7, 15, 41, 50, 755, DateTimeKind.Local).AddTicks(184), new DateTime(2024, 11, 16, 15, 41, 50, 755, DateTimeKind.Local).AddTicks(183) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 12, 17, 15, 41, 50, 755, DateTimeKind.Local).AddTicks(187), new DateTime(2024, 10, 18, 15, 41, 50, 755, DateTimeKind.Local).AddTicks(186) });
        }
    }
}
