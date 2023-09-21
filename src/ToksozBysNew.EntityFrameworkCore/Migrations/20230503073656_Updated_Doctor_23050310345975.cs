using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDoctor23050310345975 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "AppDoctors");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "AppDoctors");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerTitleId",
                table: "AppDoctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "AppDoctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppDoctors_CustomerTitleId",
                table: "AppDoctors",
                column: "CustomerTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDoctors_UnitId",
                table: "AppDoctors",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDoctors_AppCustomerTitles_CustomerTitleId",
                table: "AppDoctors",
                column: "CustomerTitleId",
                principalTable: "AppCustomerTitles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDoctors_AppUnits_UnitId",
                table: "AppDoctors",
                column: "UnitId",
                principalTable: "AppUnits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDoctors_AppCustomerTitles_CustomerTitleId",
                table: "AppDoctors");

            migrationBuilder.DropForeignKey(
                name: "FK_AppDoctors_AppUnits_UnitId",
                table: "AppDoctors");

            migrationBuilder.DropIndex(
                name: "IX_AppDoctors_CustomerTitleId",
                table: "AppDoctors");

            migrationBuilder.DropIndex(
                name: "IX_AppDoctors_UnitId",
                table: "AppDoctors");

            migrationBuilder.DropColumn(
                name: "CustomerTitleId",
                table: "AppDoctors");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "AppDoctors");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AppDoctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "AppDoctors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
