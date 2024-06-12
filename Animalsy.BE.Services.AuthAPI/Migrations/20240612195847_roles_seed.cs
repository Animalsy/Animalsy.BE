using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Animalsy.BE.Services.AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class roles_seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("36762af0-a95a-46f8-a78a-54009faaf1ad"), null, "Admin", "ADMIN" },
                    { new Guid("373f68c3-ada2-4544-9174-5739773f2a7b"), null, "Customer", "CUSTOMER" },
                    { new Guid("ddc435c7-aef6-4ba3-a513-39879fa6d3e1"), null, "Vendor", "VENDOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("36762af0-a95a-46f8-a78a-54009faaf1ad"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("373f68c3-ada2-4544-9174-5739773f2a7b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ddc435c7-aef6-4ba3-a513-39879fa6d3e1"));
        }
    }
}
