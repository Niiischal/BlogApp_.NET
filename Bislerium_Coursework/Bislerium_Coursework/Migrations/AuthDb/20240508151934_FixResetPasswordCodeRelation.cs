using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bislerium_Coursework.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class FixResetPasswordCodeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-userid",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14af14ec-9869-4647-8d29-87fab4b7b770", "AQAAAAIAAYagAAAAEIsLY9O2yJdaNvcPrg+/yCX5nTfrAy5vuheJh4J1vj/Wi/lGQFdfCsBrNRQQ9yOfJw==", "1351dc6d-a84c-47f4-9d7c-6b6ce750605c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-userid",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec88c464-1764-450e-9604-a74aee6feb2a", "AQAAAAIAAYagAAAAEPv7032zzjri8yQv7n5pyT3rBwY38326hzQV6aqDPwCAeFfEqWdoPKemtGpgv5+3CA==", "d6221938-0ca8-4ed1-bc93-8e0e21d3e31d" });
        }
    }
}
