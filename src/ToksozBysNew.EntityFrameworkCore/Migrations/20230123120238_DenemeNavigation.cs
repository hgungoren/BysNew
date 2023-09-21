using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class DenemeNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DenemeId",
                table: "AppDenemeDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppDenemeDetails_DenemeId",
                table: "AppDenemeDetails",
                column: "DenemeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppDenemeDetails_AppDenemes_DenemeId",
                table: "AppDenemeDetails",
                column: "DenemeId",
                principalTable: "AppDenemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDenemeDetails_AppDenemes_DenemeId",
                table: "AppDenemeDetails");

            migrationBuilder.DropIndex(
                name: "IX_AppDenemeDetails_DenemeId",
                table: "AppDenemeDetails");

            migrationBuilder.DropColumn(
                name: "DenemeId",
                table: "AppDenemeDetails");
        }
    }
}
