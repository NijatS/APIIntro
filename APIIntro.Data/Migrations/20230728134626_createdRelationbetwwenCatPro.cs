using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIIntro.Data.Migrations
{
    /// <inheritdoc />
    public partial class createdRelationbetwwenCatPro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 28, 17, 46, 26, 15, DateTimeKind.Utc).AddTicks(8104),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 28, 17, 38, 32, 152, DateTimeKind.Utc).AddTicks(38));

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 28, 17, 46, 26, 15, DateTimeKind.Utc).AddTicks(6840),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 28, 17, 38, 32, 151, DateTimeKind.Utc).AddTicks(8816));

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 28, 17, 38, 32, 152, DateTimeKind.Utc).AddTicks(38),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 28, 17, 46, 26, 15, DateTimeKind.Utc).AddTicks(8104));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 28, 17, 38, 32, 151, DateTimeKind.Utc).AddTicks(8816),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 7, 28, 17, 46, 26, 15, DateTimeKind.Utc).AddTicks(6840));
        }
    }
}
