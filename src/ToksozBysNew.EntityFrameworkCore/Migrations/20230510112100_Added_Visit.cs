using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToksozBysNew.Migrations
{
    /// <inheritdoc />
    public partial class AddedVisit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClinicId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_AppVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppVisits_AppBricks_BrickId",
                        column: x => x.BrickId,
                        principalTable: "AppBricks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppVisits_AppClinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "AppClinics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppVisits_AppDoctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "AppDoctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppVisits_AppUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "AppUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppVisits_BrickId",
                table: "AppVisits",
                column: "BrickId");

            migrationBuilder.CreateIndex(
                name: "IX_AppVisits_ClinicId",
                table: "AppVisits",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_AppVisits_DoctorId",
                table: "AppVisits",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppVisits_UnitId",
                table: "AppVisits",
                column: "UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppVisits");
        }
    }
}
