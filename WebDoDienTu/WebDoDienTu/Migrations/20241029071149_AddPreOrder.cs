using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDoDienTu.Migrations
{
    /// <inheritdoc />
    public partial class AddPreOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPreOrder",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "IsPreOrder", "ReleaseDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "IsPreOrder", "ReleaseDate" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "IsPreOrder", "ReleaseDate" },
                values: new object[] { false, null });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPreOrder",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 10, 28, 1, 14, 20, 644, DateTimeKind.Local).AddTicks(7136), new DateTime(2024, 10, 8, 1, 14, 20, 644, DateTimeKind.Local).AddTicks(7111) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 2, 1, 14, 20, 644, DateTimeKind.Local).AddTicks(7141), new DateTime(2024, 10, 13, 1, 14, 20, 644, DateTimeKind.Local).AddTicks(7140) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 7, 1, 14, 20, 644, DateTimeKind.Local).AddTicks(7146), new DateTime(2024, 10, 17, 1, 14, 20, 644, DateTimeKind.Local).AddTicks(7145) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 17, 1, 14, 20, 644, DateTimeKind.Local).AddTicks(7149), new DateTime(2024, 9, 18, 1, 14, 20, 644, DateTimeKind.Local).AddTicks(7148) });
        }
    }
}
