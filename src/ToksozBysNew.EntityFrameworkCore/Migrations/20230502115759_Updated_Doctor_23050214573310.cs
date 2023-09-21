using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDoctor23050214573310 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "AppDoctors");

            migrationBuilder.AddColumn<Guid>(
                name: "SpecId",
                table: "AppDoctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppDoctors_SpecId",
                table: "AppDoctors",
                column: "SpecId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDoctors_AppSpecs_SpecId",
                table: "AppDoctors",
                column: "SpecId",
                principalTable: "AppSpecs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDoctors_AppSpecs_SpecId",
                table: "AppDoctors");

            migrationBuilder.DropIndex(
                name: "IX_AppDoctors_SpecId",
                table: "AppDoctors");

            migrationBuilder.DropColumn(
                name: "SpecId",
                table: "AppDoctors");

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "AppDoctors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
