using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDoDienTu.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "VideoUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "VideoUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "VideoUrl",
                value: null);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 10, 28, 0, 43, 18, 722, DateTimeKind.Local).AddTicks(7813), new DateTime(2024, 10, 8, 0, 43, 18, 722, DateTimeKind.Local).AddTicks(7794) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 2, 0, 43, 18, 722, DateTimeKind.Local).AddTicks(7816), new DateTime(2024, 10, 13, 0, 43, 18, 722, DateTimeKind.Local).AddTicks(7816) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 7, 0, 43, 18, 722, DateTimeKind.Local).AddTicks(7819), new DateTime(2024, 10, 17, 0, 43, 18, 722, DateTimeKind.Local).AddTicks(7818) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 17, 0, 43, 18, 722, DateTimeKind.Local).AddTicks(7821), new DateTime(2024, 9, 18, 0, 43, 18, 722, DateTimeKind.Local).AddTicks(7821) });
        }
    }
}
