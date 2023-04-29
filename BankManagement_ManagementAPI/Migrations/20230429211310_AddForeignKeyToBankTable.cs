using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankManagement_ManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToBankTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "AccountNumber",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "AccNo",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 30, 2, 43, 10, 166, DateTimeKind.Local).AddTicks(2089));

            migrationBuilder.CreateIndex(
                name: "IX_AccountNumber_BankId",
                table: "AccountNumber",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountNumber_Banks_BankId",
                table: "AccountNumber",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "AccNo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountNumber_Banks_BankId",
                table: "AccountNumber");

            migrationBuilder.DropIndex(
                name: "IX_AccountNumber_BankId",
                table: "AccountNumber");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "AccountNumber");

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "AccNo",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 30, 1, 57, 47, 48, DateTimeKind.Local).AddTicks(8293));
        }
    }
}
