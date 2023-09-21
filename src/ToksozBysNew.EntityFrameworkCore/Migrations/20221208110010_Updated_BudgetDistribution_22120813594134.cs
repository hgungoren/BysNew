using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    public partial class Updated_BudgetDistribution_22120813594134 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdentityUserId",
                table: "AppBudgetDistributions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppBudgetDistributions_IdentityUserId",
                table: "AppBudgetDistributions",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBudgetDistributions_AbpUsers_IdentityUserId",
                table: "AppBudgetDistributions",
                column: "IdentityUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppBudgetDistributions_AbpUsers_IdentityUserId",
                table: "AppBudgetDistributions");

            migrationBuilder.DropIndex(
                name: "IX_AppBudgetDistributions_IdentityUserId",
                table: "AppBudgetDistributions");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "AppBudgetDistributions");
        }
    }
}
