using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedInvoiceDetail23012610114065 : Migration
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

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "AppInvoiceDetails",
                newName: "InvoiceDetailQuantity");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "AppInvoiceDetails",
                newName: "InvoiceDetailPrice");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "AppInvoiceDetails",
                newName: "InvoiceSerialNo");

            migrationBuilder.RenameColumn(
                name: "InvoicePrice",
                table: "AppInvoiceDetails",
                newName: "InvoiceAmount");

            migrationBuilder.AddColumn<Guid>(
                name: "InvoiceDetailId",
                table: "AppInvoices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "AppInvoiceDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvoiceDetailNote",
                table: "AppInvoiceDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNotes",
                table: "AppInvoiceDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InvoicePaymentDate",
                table: "AppInvoiceDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_AppInvoices_InvoiceDetailId",
                table: "AppInvoices",
                column: "InvoiceDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInvoices_AppInvoiceDetails_InvoiceDetailId",
                table: "AppInvoices",
                column: "InvoiceDetailId",
                principalTable: "AppInvoiceDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInvoices_AppInvoiceDetails_InvoiceDetailId",
                table: "AppInvoices");

            migrationBuilder.DropIndex(
                name: "IX_AppInvoices_InvoiceDetailId",
                table: "AppInvoices");

            migrationBuilder.DropColumn(
                name: "InvoiceDetailId",
                table: "AppInvoices");

            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "AppInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "InvoiceDetailNote",
                table: "AppInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "InvoiceNotes",
                table: "AppInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "InvoicePaymentDate",
                table: "AppInvoiceDetails");

            migrationBuilder.RenameColumn(
                name: "InvoiceSerialNo",
                table: "AppInvoiceDetails",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "InvoiceDetailQuantity",
                table: "AppInvoiceDetails",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "InvoiceDetailPrice",
                table: "AppInvoiceDetails",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "InvoiceAmount",
                table: "AppInvoiceDetails",
                newName: "InvoicePrice");

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
    }
}
