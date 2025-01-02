using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionVoitureFrontOffice.Migrations
{
    /// <inheritdoc />
    public partial class tt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TragerVehicule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idVehicle = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    idTragerDepart = table.Column<int>(type: "int", nullable: false),
                    TragerDepartId = table.Column<int>(type: "int", nullable: false),
                    idTragerArriver = table.Column<int>(type: "int", nullable: false),
                    TragerArriverId = table.Column<int>(type: "int", nullable: false),
                    prixLocation = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TragerVehicule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TragerVehicule_NosTrager_TragerArriverId",
                        column: x => x.TragerArriverId,
                        principalTable: "NosTrager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TragerVehicule_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TragerVehicule_TragerArriverId",
                table: "TragerVehicule",
                column: "TragerArriverId");

            migrationBuilder.CreateIndex(
                name: "IX_TragerVehicule_TragerDepartId",
                table: "TragerVehicule",
                column: "TragerDepartId");

            migrationBuilder.CreateIndex(
                name: "IX_TragerVehicule_VehicleId",
                table: "TragerVehicule",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TragerVehicule");
        }
    }
}
