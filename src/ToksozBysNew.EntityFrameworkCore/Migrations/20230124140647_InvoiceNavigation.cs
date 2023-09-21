using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InvoiceId",
                table: "AppInvoiceDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppInvoiceDetails_InvoiceId",
                table: "AppInvoiceDetails",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppInvoiceDetails_AppInvoices_InvoiceId",
                table: "AppInvoiceDetails",
                column: "InvoiceId",
                principalTable: "AppInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
        }
    }
}
