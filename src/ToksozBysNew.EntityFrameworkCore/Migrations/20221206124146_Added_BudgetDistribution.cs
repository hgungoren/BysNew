using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    public partial class Added_BudgetDistribution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppBudgetDistributions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostCenter = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExpenseType = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    ProjectItem = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Unit = table.Column<int>(type: "int", nullable: true),
                    UnitValue = table.Column<float>(type: "real", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Ratio = table.Column<float>(type: "real", nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Memo = table.Column<float>(type: "real", nullable: true),
                    Invoice = table.Column<float>(type: "real", nullable: true),
                    Currency = table.Column<int>(type: "int", nullable: true),
                    CurrencyAmount = table.Column<float>(type: "real", nullable: true),
                    ExpenseCategory = table.Column<int>(type: "int", nullable: true),
                    ExpenseNecessity = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Approval = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BudgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdentityUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBudgetDistributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppBudgetDistributions_AbpUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBudgetDistributions_AppAccountGroups_AccountGroupId",
                        column: x => x.AccountGroupId,
                        principalTable: "AppAccountGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBudgetDistributions_AppAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AppAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBudgetDistributions_AppBudgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "AppBudgets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBudgetDistributions_AppDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "AppDepartments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppBudgetDistributions_AppProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "AppProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppBudgetDistributions_AccountGroupId",
                table: "AppBudgetDistributions",
                column: "AccountGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBudgetDistributions_AccountId",
                table: "AppBudgetDistributions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBudgetDistributions_BudgetId",
                table: "AppBudgetDistributions",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBudgetDistributions_DepartmentId",
                table: "AppBudgetDistributions",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBudgetDistributions_IdentityUserId",
                table: "AppBudgetDistributions",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBudgetDistributions_ProductId",
                table: "AppBudgetDistributions",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppBudgetDistributions");
        }
    }
}
