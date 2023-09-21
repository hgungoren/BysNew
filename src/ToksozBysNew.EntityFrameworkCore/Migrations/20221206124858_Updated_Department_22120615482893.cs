using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    public partial class Updated_Department_22120615482893 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdentityUserId",
                table: "AppDepartments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppDepartments_IdentityUserId",
                table: "AppDepartments",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDepartments_AbpUsers_IdentityUserId",
                table: "AppDepartments",
                column: "IdentityUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDepartments_AbpUsers_IdentityUserId",
                table: "AppDepartments");

            migrationBuilder.DropIndex(
                name: "IX_AppDepartments_IdentityUserId",
                table: "AppDepartments");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "AppDepartments");
        }
    }
}
