using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedInvoiceDetail23020216455559 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceAmount",
                table: "AppInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "AppInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "InvoiceNotes",
                table: "AppInvoiceDetails");

            migrationBuilder.DropColumn(
                name: "InvoiceSerialNo",
                table: "AppInvoiceDetails");

            migrationBuilder.RenameColumn(
                name: "InvoicePaymentDate",
                table: "AppInvoiceDetails",
                newName: "InvoiceDetailDate");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceDetailNote",
                table: "AppInvoiceDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceDetailDate",
                table: "AppInvoiceDetails",
                newName: "InvoicePaymentDate");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceDetailNote",
                table: "AppInvoiceDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "InvoiceAmount",
                table: "AppInvoiceDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "AppInvoiceDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNotes",
                table: "AppInvoiceDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceSerialNo",
                table: "AppInvoiceDetails",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
