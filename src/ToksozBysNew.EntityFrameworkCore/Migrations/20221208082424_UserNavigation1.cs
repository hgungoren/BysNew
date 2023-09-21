using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    public partial class UserNavigation1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BudgetDistributionId",
                table: "AbpUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "AbpUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "AbpUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_BudgetDistributionId",
                table: "AbpUsers",
                column: "BudgetDistributionId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_CompanyId",
                table: "AbpUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_DepartmentId",
                table: "AbpUsers",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_AppBudgetDistributions_BudgetDistributionId",
                table: "AbpUsers",
                column: "BudgetDistributionId",
                principalTable: "AppBudgetDistributions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_AppCompanies_CompanyId",
                table: "AbpUsers",
                column: "CompanyId",
                principalTable: "AppCompanies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_AppDepartments_DepartmentId",
                table: "AbpUsers",
                column: "DepartmentId",
                principalTable: "AppDepartments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_AppBudgetDistributions_BudgetDistributionId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_AppCompanies_CompanyId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_AppDepartments_DepartmentId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_BudgetDistributionId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_CompanyId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_DepartmentId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "BudgetDistributionId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AbpUsers");
        }
    }
}
