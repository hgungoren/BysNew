using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class AddedClinic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppClinics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClinicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SpecId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_AppClinics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppClinics_AppSpecs_SpecId",
                        column: x => x.SpecId,
                        principalTable: "AppSpecs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppClinics_AppUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "AppUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppClinics_SpecId",
                table: "AppClinics",
                column: "SpecId");

            migrationBuilder.CreateIndex(
                name: "IX_AppClinics_UnitId",
                table: "AppClinics",
                column: "UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppClinics");
        }
    }
}
