using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDoctor23050514095124 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerTypeId",
                table: "AppDoctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppDoctors_CustomerTypeId",
                table: "AppDoctors",
                column: "CustomerTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDoctors_AppCustomerTypes_CustomerTypeId",
                table: "AppDoctors",
                column: "CustomerTypeId",
                principalTable: "AppCustomerTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDoctors_AppCustomerTypes_CustomerTypeId",
                table: "AppDoctors");

            migrationBuilder.DropIndex(
                name: "IX_AppDoctors_CustomerTypeId",
                table: "AppDoctors");

            migrationBuilder.DropColumn(
                name: "CustomerTypeId",
                table: "AppDoctors");
        }
    }
}
