using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCustomerAddress23050515114415 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "AppCustomerAddresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictId",
                table: "AppCustomerAddresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProvinceId",
                table: "AppCustomerAddresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCustomerAddresses_CountryId",
                table: "AppCustomerAddresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCustomerAddresses_DistrictId",
                table: "AppCustomerAddresses",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCustomerAddresses_ProvinceId",
                table: "AppCustomerAddresses",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCustomerAddresses_AppCountries_CountryId",
                table: "AppCustomerAddresses",
                column: "CountryId",
                principalTable: "AppCountries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCustomerAddresses_AppDistricts_DistrictId",
                table: "AppCustomerAddresses",
                column: "DistrictId",
                principalTable: "AppDistricts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCustomerAddresses_AppProvinces_ProvinceId",
                table: "AppCustomerAddresses",
                column: "ProvinceId",
                principalTable: "AppProvinces",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCustomerAddresses_AppCountries_CountryId",
                table: "AppCustomerAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_AppCustomerAddresses_AppDistricts_DistrictId",
                table: "AppCustomerAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_AppCustomerAddresses_AppProvinces_ProvinceId",
                table: "AppCustomerAddresses");

            migrationBuilder.DropIndex(
                name: "IX_AppCustomerAddresses_CountryId",
                table: "AppCustomerAddresses");

            migrationBuilder.DropIndex(
                name: "IX_AppCustomerAddresses_DistrictId",
                table: "AppCustomerAddresses");

            migrationBuilder.DropIndex(
                name: "IX_AppCustomerAddresses_ProvinceId",
                table: "AppCustomerAddresses");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "AppCustomerAddresses");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "AppCustomerAddresses");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "AppCustomerAddresses");
        }
    }
}
