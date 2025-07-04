using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RakbnyMa_aak.Migrations
{
    /// <inheritdoc />
    public partial class PricePerSeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Bookings");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerSeat",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerSeat",
                table: "Bookings");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
