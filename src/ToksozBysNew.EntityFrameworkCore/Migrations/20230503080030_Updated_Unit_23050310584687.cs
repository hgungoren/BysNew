using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUnit23050310584687 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BrickId",
                table: "AppUnits",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUnits_BrickId",
                table: "AppUnits",
                column: "BrickId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUnits_AppBricks_BrickId",
                table: "AppUnits",
                column: "BrickId",
                principalTable: "AppBricks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUnits_AppBricks_BrickId",
                table: "AppUnits");

            migrationBuilder.DropIndex(
                name: "IX_AppUnits_BrickId",
                table: "AppUnits");

            migrationBuilder.DropColumn(
                name: "BrickId",
                table: "AppUnits");
        }
    }
}
