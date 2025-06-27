using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RakbnyMa_aak.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasEnded",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasStarted",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasEnded",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "HasStarted",
                table: "Bookings");
        }
    }
}
