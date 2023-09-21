using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedVisit23051016063812 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdentityUserId",
                table: "AppVisits",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppVisits_IdentityUserId",
                table: "AppVisits",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppVisits_AbpUsers_IdentityUserId",
                table: "AppVisits",
                column: "IdentityUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppVisits_AbpUsers_IdentityUserId",
                table: "AppVisits");

            migrationBuilder.DropIndex(
                name: "IX_AppVisits_IdentityUserId",
                table: "AppVisits");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "AppVisits");
        }
    }
}
