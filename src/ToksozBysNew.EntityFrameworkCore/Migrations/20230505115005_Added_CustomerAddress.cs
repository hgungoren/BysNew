using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class AddedCustomerAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppCustomerAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BrickId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_AppCustomerAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCustomerAddresses_AppBricks_BrickId",
                        column: x => x.BrickId,
                        principalTable: "AppBricks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppCustomerAddresses_AppDoctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AppDoctors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCustomerAddresses_BrickId",
                table: "AppCustomerAddresses",
                column: "BrickId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCustomerAddresses_DoctorId",
                table: "AppCustomerAddresses",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCustomerAddresses");
        }
    }
}
