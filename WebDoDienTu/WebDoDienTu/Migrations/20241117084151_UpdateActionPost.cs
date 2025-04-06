using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDoDienTu.Migrations
{
    /// <inheritdoc />
    public partial class UpdateActionPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "ActionPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "ActionPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "ActionPosts");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "ActionPosts");

            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 27, 15, 36, 1, 376, DateTimeKind.Local).AddTicks(6283), new DateTime(2024, 11, 7, 15, 36, 1, 376, DateTimeKind.Local).AddTicks(6264) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 12, 2, 15, 36, 1, 376, DateTimeKind.Local).AddTicks(6287), new DateTime(2024, 11, 12, 15, 36, 1, 376, DateTimeKind.Local).AddTicks(6287) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 12, 7, 15, 36, 1, 376, DateTimeKind.Local).AddTicks(6290), new DateTime(2024, 11, 16, 15, 36, 1, 376, DateTimeKind.Local).AddTicks(6289) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 12, 17, 15, 36, 1, 376, DateTimeKind.Local).AddTicks(6292), new DateTime(2024, 10, 18, 15, 36, 1, 376, DateTimeKind.Local).AddTicks(6292) });
        }
    }
}
