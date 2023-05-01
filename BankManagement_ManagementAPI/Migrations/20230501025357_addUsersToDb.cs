using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankManagement_ManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class addUsersToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalUsers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "AccNo",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 5, 1, 8, 23, 57, 115, DateTimeKind.Local).AddTicks(9468));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalUsers");

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "AccNo",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 4, 30, 2, 43, 10, 166, DateTimeKind.Local).AddTicks(2089));
        }
    }
}
