using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class TaxListId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        { 

            migrationBuilder.AddColumn<string>(
                name: "TaxName",
                table: "AppInvoiceDetails",
                type: "nvarchar(max)",
                nullable: true);
              
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        { 
             
             

            migrationBuilder.DropColumn(
                name: "TaxName",
                table: "AppInvoiceDetails");
        }
    }
}
