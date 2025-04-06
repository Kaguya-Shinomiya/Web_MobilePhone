using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDoDienTu.Migrations
{
    /// <inheritdoc />
    public partial class UpdateActionPostType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "ActionPosts");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "ActionPosts");

            migrationBuilder.AddColumn<bool>(
                name: "Dislike",
                table: "ActionPosts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Like",
                table: "ActionPosts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 27, 16, 26, 29, 582, DateTimeKind.Local).AddTicks(7505), new DateTime(2024, 11, 7, 16, 26, 29, 582, DateTimeKind.Local).AddTicks(7487) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 12, 2, 16, 26, 29, 582, DateTimeKind.Local).AddTicks(7509), new DateTime(2024, 11, 12, 16, 26, 29, 582, DateTimeKind.Local).AddTicks(7508) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 12, 7, 16, 26, 29, 582, DateTimeKind.Local).AddTicks(7511), new DateTime(2024, 11, 16, 16, 26, 29, 582, DateTimeKind.Local).AddTicks(7511) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 12, 17, 16, 26, 29, 582, DateTimeKind.Local).AddTicks(7514), new DateTime(2024, 10, 18, 16, 26, 29, 582, DateTimeKind.Local).AddTicks(7513) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislike",
                table: "ActionPosts");

            migrationBuilder.DropColumn(
                name: "Like",
                table: "ActionPosts");

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
    }
}
