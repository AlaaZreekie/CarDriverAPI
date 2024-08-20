using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class HD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarsDrivers_Cars_carId",
                table: "CarsDrivers");

            migrationBuilder.DropForeignKey(
                name: "FK_CarsDrivers_Drivers_driverId",
                table: "CarsDrivers");

            migrationBuilder.RenameColumn(
                name: "driverId",
                table: "CarsDrivers",
                newName: "DriverId");

            migrationBuilder.RenameColumn(
                name: "carId",
                table: "CarsDrivers",
                newName: "CarId");

            migrationBuilder.RenameIndex(
                name: "IX_CarsDrivers_driverId",
                table: "CarsDrivers",
                newName: "IX_CarsDrivers_DriverId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Drivers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarsDrivers_Cars_CarId",
                table: "CarsDrivers");

            migrationBuilder.DropForeignKey(
                name: "FK_CarsDrivers_Drivers_DriverId",
                table: "CarsDrivers");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "CarsDrivers",
                newName: "driverId");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "CarsDrivers",
                newName: "carId");

            migrationBuilder.RenameIndex(
                name: "IX_CarsDrivers_DriverId",
                table: "CarsDrivers",
                newName: "IX_CarsDrivers_driverId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Drivers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CarsDrivers_Cars_carId",
                table: "CarsDrivers",
                column: "carId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarsDrivers_Drivers_driverId",
                table: "CarsDrivers",
                column: "driverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
