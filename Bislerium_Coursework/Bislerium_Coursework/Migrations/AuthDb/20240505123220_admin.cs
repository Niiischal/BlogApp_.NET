using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bislerium_Coursework.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "82c6b8eb-46e3-4cdf-83cc-dfaee81e1f3d", "admin@gmail.com", "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEF+8Vjo4mC4Zlgtj7APQ5KvMmeOPuivTsn9xWh8ddVOgsaL1zX0bZufkkCkmYBDyQQ==", "1691dfdf-a4df-4f16-9e0e-c84b062e8d89", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "a1e7c473-186d-495d-a23c-a28f079dd4e5", "admin@example.com", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMWaNNE27HvTYKfUIkYoZs153HNOZxoZDR8FE/Pfb7cq37JxHgo5W1sWYhz77F+xtg==", "7d0029fc-81aa-44d8-9c1d-9149bd3f9e89", "admin@example.com" });
        }
    }
}
