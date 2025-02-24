using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDoDienTu.Migrations
{
    /// <inheritdoc />
    public partial class AddActionPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionPosts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ActionPosts_PostId",
                table: "ActionPosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionPosts_UserId",
                table: "ActionPosts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionPosts");

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
    }
}
