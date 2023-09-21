using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class AddedVisitDailyAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppVisitDailyActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitDailyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitDaily1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VisitDaily2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VisitDaily3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VisitDaily4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VisitDaily5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VisitDaily6 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VisitDaily7 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VisitDaily8 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VisitDaily9 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VisitDaily10 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VisitDaily11 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VisitDaily12 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VisitDaily13 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VisitDaily14 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VisitDaily15 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VisitDailyCloseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitDailyNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_AppVisitDailyActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppVisitDailyActions_AbpUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppVisitDailyActions_IdentityUserId",
                table: "AppVisitDailyActions",
                column: "IdentityUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppVisitDailyActions");
        }
    }
}
