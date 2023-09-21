using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    public partial class Updated_Company_22120614400409 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCompanies_AppAccountGroups_AccountGroupId",
                table: "AppCompanies");

            migrationBuilder.DropIndex(
                name: "IX_AppCompanies_AccountGroupId",
                table: "AppCompanies");

            migrationBuilder.DropColumn(
                name: "AccountGroupId",
                table: "AppCompanies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountGroupId",
                table: "AppCompanies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCompanies_AccountGroupId",
                table: "AppCompanies",
                column: "AccountGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCompanies_AppAccountGroups_AccountGroupId",
                table: "AppCompanies",
                column: "AccountGroupId",
                principalTable: "AppAccountGroups",
                principalColumn: "Id");
        }
    }
}
