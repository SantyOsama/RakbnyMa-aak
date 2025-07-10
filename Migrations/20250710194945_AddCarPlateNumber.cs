using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RakbnyMa_aak.Migrations
{
    /// <inheritdoc />
    public partial class AddCarPlateNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarPlateNumber",
                table: "Drivers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarPlateNumber",
                table: "Drivers");
        }
    }
}
