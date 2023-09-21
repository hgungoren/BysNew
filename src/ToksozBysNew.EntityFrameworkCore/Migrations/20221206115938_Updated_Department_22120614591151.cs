using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    public partial class Updated_Department_22120614591151 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "AppDepartments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppDepartments_CompanyId",
                table: "AppDepartments",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDepartments_AppCompanies_CompanyId",
                table: "AppDepartments",
                column: "CompanyId",
                principalTable: "AppCompanies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDepartments_AppCompanies_CompanyId",
                table: "AppDepartments");

            migrationBuilder.DropIndex(
                name: "IX_AppDepartments_CompanyId",
                table: "AppDepartments");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AppDepartments");
        }
    }
}
