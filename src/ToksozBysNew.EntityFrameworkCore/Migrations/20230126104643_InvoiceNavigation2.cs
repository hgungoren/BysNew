using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceNavigation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInvoices_AppInvoiceDetails_InvoiceDetailId",
                table: "AppInvoices");

            migrationBuilder.DropTable(
                name: "AppDenemeDetails");

            migrationBuilder.DropTable(
                name: "AppDenemes");

            migrationBuilder.DropIndex(
                name: "IX_AppInvoices_InvoiceDetailId",
                table: "AppInvoices");

            migrationBuilder.DropColumn(
                name: "InvoiceDetailId",
                table: "AppInvoices");

            migrationBuilder.AddColumn<Guid>(
                name: "InvoiceId",
                table: "AppInvoiceDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppInvoiceDetails_InvoiceId",
                table: "AppInvoiceDetails",
                column: "InvoiceId",
                unique: true,
                filter: "[InvoiceId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInvoiceDetails_AppInvoices_InvoiceId",
                table: "AppInvoiceDetails",
                column: "InvoiceId",
                principalTable: "AppInvoices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInvoiceDetails_AppInvoices_InvoiceId",
                table: "AppInvoiceDetails");

            migrationBuilder.DropIndex(
                name: "IX_AppInvoiceDetails_InvoiceId",
                table: "AppInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "AppInvoiceDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "InvoiceDetailId",
                table: "AppInvoices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppDenemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DenemeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DenemeSayi = table.Column<int>(type: "int", nullable: false),
                    DenemeValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDenemes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppDenemeDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DenemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DetailName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetailNetValue = table.Column<int>(type: "int", nullable: false),
                    DetailNumber = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDenemeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDenemeDetails_AppDenemes_DenemeId",
                        column: x => x.DenemeId,
                        principalTable: "AppDenemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppInvoices_InvoiceDetailId",
                table: "AppInvoices",
                column: "InvoiceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDenemeDetails_DenemeId",
                table: "AppDenemeDetails",
                column: "DenemeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppInvoices_AppInvoiceDetails_InvoiceDetailId",
                table: "AppInvoices",
                column: "InvoiceDetailId",
                principalTable: "AppInvoiceDetails",
                principalColumn: "Id");
        }
    }
}
