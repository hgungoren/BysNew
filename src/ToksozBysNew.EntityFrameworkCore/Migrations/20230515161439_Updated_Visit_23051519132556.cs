using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedVisit23051519132556 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SpecId",
                table: "AppVisits",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppVisits_SpecId",
                table: "AppVisits",
                column: "SpecId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppVisits_AppSpecs_SpecId",
                table: "AppVisits",
                column: "SpecId",
                principalTable: "AppSpecs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppVisits_AppSpecs_SpecId",
                table: "AppVisits");

            migrationBuilder.DropIndex(
                name: "IX_AppVisits_SpecId",
                table: "AppVisits");

            migrationBuilder.DropColumn(
                name: "SpecId",
                table: "AppVisits");
        }
    }
}
