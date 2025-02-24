using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDoDienTu.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePreOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DepositAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 12, 18, 43, 12, 703, DateTimeKind.Local).AddTicks(2110), new DateTime(2024, 10, 23, 18, 43, 12, 703, DateTimeKind.Local).AddTicks(2086) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 17, 18, 43, 12, 703, DateTimeKind.Local).AddTicks(2113), new DateTime(2024, 10, 28, 18, 43, 12, 703, DateTimeKind.Local).AddTicks(2112) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 22, 18, 43, 12, 703, DateTimeKind.Local).AddTicks(2115), new DateTime(2024, 11, 1, 18, 43, 12, 703, DateTimeKind.Local).AddTicks(2114) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 12, 2, 18, 43, 12, 703, DateTimeKind.Local).AddTicks(2117), new DateTime(2024, 10, 3, 18, 43, 12, 703, DateTimeKind.Local).AddTicks(2116) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepositAmount",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 8, 22, 58, 16, 182, DateTimeKind.Local).AddTicks(2693), new DateTime(2024, 10, 19, 22, 58, 16, 182, DateTimeKind.Local).AddTicks(2675) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 13, 22, 58, 16, 182, DateTimeKind.Local).AddTicks(2696), new DateTime(2024, 10, 24, 22, 58, 16, 182, DateTimeKind.Local).AddTicks(2696) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 18, 22, 58, 16, 182, DateTimeKind.Local).AddTicks(2699), new DateTime(2024, 10, 28, 22, 58, 16, 182, DateTimeKind.Local).AddTicks(2698) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 28, 22, 58, 16, 182, DateTimeKind.Local).AddTicks(2701), new DateTime(2024, 9, 29, 22, 58, 16, 182, DateTimeKind.Local).AddTicks(2700) });
        }
    }
}
