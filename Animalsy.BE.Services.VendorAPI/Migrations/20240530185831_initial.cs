﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animalsy.BE.Services.VendorAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nip = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Building = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Flat = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OpeningHour = table.Column<TimeOnly>(type: "time", nullable: false),
                    ClosingHour = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
