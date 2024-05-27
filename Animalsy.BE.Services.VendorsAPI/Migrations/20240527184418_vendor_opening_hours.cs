using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animalsy.BE.Services.VendorsAPI.Migrations
{
    /// <inheritdoc />
    public partial class vendor_opening_hours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "ClosingHour",
                table: "Vendors",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "OpeningHour",
                table: "Vendors",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingHour",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "OpeningHour",
                table: "Vendors");
        }
    }
}
