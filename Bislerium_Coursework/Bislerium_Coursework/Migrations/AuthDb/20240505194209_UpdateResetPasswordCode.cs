using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bislerium_Coursework.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class UpdateResetPasswordCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResetPasswordCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetPasswordCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResetPasswordCodes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-userid",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec88c464-1764-450e-9604-a74aee6feb2a", "AQAAAAIAAYagAAAAEPv7032zzjri8yQv7n5pyT3rBwY38326hzQV6aqDPwCAeFfEqWdoPKemtGpgv5+3CA==", "d6221938-0ca8-4ed1-bc93-8e0e21d3e31d" });

            migrationBuilder.CreateIndex(
                name: "IX_ResetPasswordCodes_UserId",
                table: "ResetPasswordCodes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResetPasswordCodes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-userid",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90b43045-439d-463f-9a8c-39581e416052", "AQAAAAIAAYagAAAAEIt+0wXINQxAwOjToEaYCKhwbFD41MNwQ5VrOU5A+euVH1laVBx1jZtfHmA15XDStg==", "42dd9041-bb4a-42d3-8d48-d1de75b80a0d" });
        }
    }
}
