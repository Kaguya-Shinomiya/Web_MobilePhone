using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDoDienTu.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsHidden_ProductReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "ProductReviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "ProductReviews");

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 8, 14, 11, 48, 705, DateTimeKind.Local).AddTicks(9288), new DateTime(2024, 10, 19, 14, 11, 48, 705, DateTimeKind.Local).AddTicks(9262) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 13, 14, 11, 48, 705, DateTimeKind.Local).AddTicks(9291), new DateTime(2024, 10, 24, 14, 11, 48, 705, DateTimeKind.Local).AddTicks(9290) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 18, 14, 11, 48, 705, DateTimeKind.Local).AddTicks(9293), new DateTime(2024, 10, 28, 14, 11, 48, 705, DateTimeKind.Local).AddTicks(9292) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 28, 14, 11, 48, 705, DateTimeKind.Local).AddTicks(9295), new DateTime(2024, 9, 29, 14, 11, 48, 705, DateTimeKind.Local).AddTicks(9294) });
        }
    }
}
