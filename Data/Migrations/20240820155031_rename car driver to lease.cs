using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class renamecardrivertolease : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarsDrivers_Cars_CarId",
                table: "CarsDrivers");

            migrationBuilder.DropForeignKey(
                name: "FK_CarsDrivers_Drivers_DriverId",
                table: "CarsDrivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarsDrivers",
                table: "CarsDrivers");

            migrationBuilder.RenameTable(
                name: "CarsDrivers",
                newName: "Lease");

            migrationBuilder.RenameIndex(
                name: "IX_CarsDrivers_DriverId",
                table: "Lease",
                newName: "IX_Lease_DriverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lease",
                table: "Lease",
                columns: new[] { "CarId", "DriverId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Lease_Cars_CarId",
                table: "Lease",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lease_Drivers_DriverId",
                table: "Lease",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lease_Cars_CarId",
                table: "Lease");

            migrationBuilder.DropForeignKey(
                name: "FK_Lease_Drivers_DriverId",
                table: "Lease");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lease",
                table: "Lease");

            migrationBuilder.RenameTable(
                name: "Lease",
                newName: "CarsDrivers");

            migrationBuilder.RenameIndex(
                name: "IX_Lease_DriverId",
                table: "CarsDrivers",
                newName: "IX_CarsDrivers_DriverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarsDrivers",
                table: "CarsDrivers",
                columns: new[] { "CarId", "DriverId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarsDrivers_Cars_CarId",
                table: "CarsDrivers",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarsDrivers_Drivers_DriverId",
                table: "CarsDrivers",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
