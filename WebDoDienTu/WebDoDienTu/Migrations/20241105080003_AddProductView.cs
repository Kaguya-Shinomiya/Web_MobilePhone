using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDoDienTu.Migrations
{
    /// <inheritdoc />
    public partial class AddProductView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductViews",
                columns: table => new
                {
                    ProductViewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    LastViewedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductViews", x => x.ProductViewId);
                    table.ForeignKey(
                        name: "FK_ProductViews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductViews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductViews_ProductId",
                table: "ProductViews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductViews_UserId",
                table: "ProductViews",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductViews");

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 14, 23, 50, 32, 983, DateTimeKind.Local).AddTicks(9362), new DateTime(2024, 10, 25, 23, 50, 32, 983, DateTimeKind.Local).AddTicks(9342) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 19, 23, 50, 32, 983, DateTimeKind.Local).AddTicks(9365), new DateTime(2024, 10, 30, 23, 50, 32, 983, DateTimeKind.Local).AddTicks(9364) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 11, 24, 23, 50, 32, 983, DateTimeKind.Local).AddTicks(9368), new DateTime(2024, 11, 3, 23, 50, 32, 983, DateTimeKind.Local).AddTicks(9367) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 12, 4, 23, 50, 32, 983, DateTimeKind.Local).AddTicks(9373), new DateTime(2024, 10, 5, 23, 50, 32, 983, DateTimeKind.Local).AddTicks(9372) });
        }
    }
}
