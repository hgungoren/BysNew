using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBrick23050310511870 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppBricks_AppUnits_UnitId",
                table: "AppBricks");

            migrationBuilder.DropIndex(
                name: "IX_AppBricks_UnitId",
                table: "AppBricks");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "AppBricks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "AppBricks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppBricks_UnitId",
                table: "AppBricks",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBricks_AppUnits_UnitId",
                table: "AppBricks",
                column: "UnitId",
                principalTable: "AppUnits",
                principalColumn: "Id");
        }
    }
}
