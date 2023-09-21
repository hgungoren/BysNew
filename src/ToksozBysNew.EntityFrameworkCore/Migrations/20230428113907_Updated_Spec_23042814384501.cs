using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSpec23042814384501 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "AppSpecs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSpecs_DoctorId",
                table: "AppSpecs",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSpecs_AppDoctors_DoctorId",
                table: "AppSpecs",
                column: "DoctorId",
                principalTable: "AppDoctors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSpecs_AppDoctors_DoctorId",
                table: "AppSpecs");

            migrationBuilder.DropIndex(
                name: "IX_AppSpecs_DoctorId",
                table: "AppSpecs");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "AppSpecs");
        }
    }
}
