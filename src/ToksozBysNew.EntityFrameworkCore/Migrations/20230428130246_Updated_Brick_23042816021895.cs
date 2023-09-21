using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBrick23042816021895 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppBricks_AppDoctors_DoctorId",
                table: "AppBricks");

            migrationBuilder.DropIndex(
                name: "IX_AppBricks_DoctorId",
                table: "AppBricks");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "AppBricks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "AppBricks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppBricks_DoctorId",
                table: "AppBricks",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBricks_AppDoctors_DoctorId",
                table: "AppBricks",
                column: "DoctorId",
                principalTable: "AppDoctors",
                principalColumn: "Id");
        }
    }
}
