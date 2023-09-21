using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class NavigationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
