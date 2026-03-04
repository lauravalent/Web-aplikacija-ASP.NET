using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rad.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PricePerNight",
                table: "Accomodations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 1,
                column: "PricePerNight",
                value: 75.00m);

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 2,
                column: "PricePerNight",
                value: 65.00m);

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 3,
                column: "PricePerNight",
                value: 85.00m);

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 4,
                column: "PricePerNight",
                value: 70.00m);

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 5,
                column: "PricePerNight",
                value: 90.00m);

            migrationBuilder.UpdateData(
                table: "Accomodations",
                keyColumn: "ID",
                keyValue: 6,
                column: "PricePerNight",
                value: 150.00m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerNight",
                table: "Accomodations");
        }
    }
}
