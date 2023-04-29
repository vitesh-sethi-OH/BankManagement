using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankManagement_ManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBankLockerToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountNumber",
                columns: table => new
                {
                    AccountNumber = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountNumber", x => x.AccountNumber);
                });

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "AccNo",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 30, 1, 57, 47, 48, DateTimeKind.Local).AddTicks(8293));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountNumber");

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "AccNo",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 24, 16, 19, 39, 779, DateTimeKind.Local).AddTicks(8022));
        }
    }
}
