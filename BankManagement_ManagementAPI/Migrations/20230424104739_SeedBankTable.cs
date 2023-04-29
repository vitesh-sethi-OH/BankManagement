using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankManagement_ManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedBankTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "AccNo", "AadharCard", "AccName", "AccType", "Address", "CreatedDate", "PanCard" },
                values: new object[] { 1, 64848, "vitesh", "savings", "reikhjtgoiew", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "wert326y8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "AccNo",
                keyValue: 1);
        }
    }
}
