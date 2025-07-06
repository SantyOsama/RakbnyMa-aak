using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RakbnyMa_aak.Migrations
{
    /// <inheritdoc />
    public partial class ApproveColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedAt",
                table: "Drivers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedAt",
                table: "Drivers");
        }
    }
}
