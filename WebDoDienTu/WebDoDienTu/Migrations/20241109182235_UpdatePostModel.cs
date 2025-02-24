using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDoDienTu.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "ProductRecommendations",
                columns: table => new
                {
                    ProductRecommendationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseCount = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRecommendations", x => x.ProductRecommendationId);
                });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 20, 1, 22, 32, 535, DateTimeKind.Local).AddTicks(7965), new DateTime(2024, 10, 31, 1, 22, 32, 535, DateTimeKind.Local).AddTicks(7947) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 25, 1, 22, 32, 535, DateTimeKind.Local).AddTicks(7968), new DateTime(2024, 11, 5, 1, 22, 32, 535, DateTimeKind.Local).AddTicks(7967) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 30, 1, 22, 32, 535, DateTimeKind.Local).AddTicks(7970), new DateTime(2024, 11, 9, 1, 22, 32, 535, DateTimeKind.Local).AddTicks(7969) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 12, 10, 1, 22, 32, 535, DateTimeKind.Local).AddTicks(7972), new DateTime(2024, 10, 11, 1, 22, 32, 535, DateTimeKind.Local).AddTicks(7971) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductRecommendations");

            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 15, 14, 59, 59, 921, DateTimeKind.Local).AddTicks(72), new DateTime(2024, 10, 26, 14, 59, 59, 921, DateTimeKind.Local).AddTicks(53) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 20, 14, 59, 59, 921, DateTimeKind.Local).AddTicks(75), new DateTime(2024, 10, 31, 14, 59, 59, 921, DateTimeKind.Local).AddTicks(74) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 25, 14, 59, 59, 921, DateTimeKind.Local).AddTicks(77), new DateTime(2024, 11, 4, 14, 59, 59, 921, DateTimeKind.Local).AddTicks(77) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 12, 5, 14, 59, 59, 921, DateTimeKind.Local).AddTicks(80), new DateTime(2024, 10, 6, 14, 59, 59, 921, DateTimeKind.Local).AddTicks(79) });
        }
    }
}
