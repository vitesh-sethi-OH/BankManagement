using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankManagement_ManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class addUsersToDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "AccNo",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 9, 36, 13, 723, DateTimeKind.Local).AddTicks(5550));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "AccNo",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 8, 23, 57, 115, DateTimeKind.Local).AddTicks(9468));
        }
    }
}
