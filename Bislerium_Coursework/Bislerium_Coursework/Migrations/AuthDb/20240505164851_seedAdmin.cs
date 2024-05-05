using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bislerium_Coursework.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class seedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1afbe811-a6eb-4752-ac35-c08624b92273", "AQAAAAIAAYagAAAAEGz5rP8c6Qwev8oIaV8NqHWI+05msY2yABlFNTC1YbUOqKthhpSv6ImZ1G/dpb7a0g==", "427f81b7-0d87-4c25-89b9-626fafeaac34" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "585db254-d6f0-4b7e-bfd5-b0614209d75a", "AQAAAAIAAYagAAAAEMlEhDVbErpkeeGbs1pdo/rgf3jOuF1eJJU5b0+cyOeYp/9E29kKVQJOvZO69ASJSQ==", "ffeae73d-6558-4754-82cb-ac1499a909a8" });
        }
    }
}
