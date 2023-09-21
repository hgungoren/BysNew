using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDoctor23042814470236 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PositionId",
                table: "AppDoctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppDoctors_PositionId",
                table: "AppDoctors",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDoctors_AppPositions_PositionId",
                table: "AppDoctors",
                column: "PositionId",
                principalTable: "AppPositions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDoctors_AppPositions_PositionId",
                table: "AppDoctors");

            migrationBuilder.DropIndex(
                name: "IX_AppDoctors_PositionId",
                table: "AppDoctors");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "AppDoctors");
        }
    }
}
