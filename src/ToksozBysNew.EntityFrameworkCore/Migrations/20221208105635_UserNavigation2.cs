using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    public partial class UserNavigation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_AppBudgetDistributions_BudgetDistributionId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_BudgetDistributionId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "BudgetDistributionId",
                table: "AbpUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BudgetDistributionId",
                table: "AbpUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_BudgetDistributionId",
                table: "AbpUsers",
                column: "BudgetDistributionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_AppBudgetDistributions_BudgetDistributionId",
                table: "AbpUsers",
                column: "BudgetDistributionId",
                principalTable: "AppBudgetDistributions",
                principalColumn: "Id");
        }
    }
}
