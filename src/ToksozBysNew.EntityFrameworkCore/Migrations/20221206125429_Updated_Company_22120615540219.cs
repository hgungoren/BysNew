using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    public partial class Updated_Company_22120615540219 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdentityUserId",
                table: "AppCompanies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCompanies_IdentityUserId",
                table: "AppCompanies",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCompanies_AbpUsers_IdentityUserId",
                table: "AppCompanies",
                column: "IdentityUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCompanies_AbpUsers_IdentityUserId",
                table: "AppCompanies");

            migrationBuilder.DropIndex(
                name: "IX_AppCompanies_IdentityUserId",
                table: "AppCompanies");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "AppCompanies");
        }
    }
}
