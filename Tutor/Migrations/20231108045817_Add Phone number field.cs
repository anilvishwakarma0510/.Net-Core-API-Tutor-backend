using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutor.Migrations
{
    public partial class AddPhonenumberfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "users",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 11, 8, 10, 28, 17, 55, DateTimeKind.Local).AddTicks(2582), new DateTime(2023, 11, 8, 10, 28, 17, 55, DateTimeKind.Local).AddTicks(2589) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 11, 8, 10, 28, 17, 55, DateTimeKind.Local).AddTicks(2590), new DateTime(2023, 11, 8, 10, 28, 17, 55, DateTimeKind.Local).AddTicks(2591) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 11, 8, 10, 28, 17, 55, DateTimeKind.Local).AddTicks(2591), new DateTime(2023, 11, 8, 10, 28, 17, 55, DateTimeKind.Local).AddTicks(2592) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 11, 8, 10, 28, 17, 55, DateTimeKind.Local).AddTicks(2592), new DateTime(2023, 11, 8, 10, 28, 17, 55, DateTimeKind.Local).AddTicks(2593) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password", "PhoneNumber", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 11, 8, 10, 28, 17, 55, DateTimeKind.Local).AddTicks(2683), "$2a$11$nwDONbd.E2shKjVHZa4DP.MEbzLnahFrvKzcZooVRYCHWdO58V0na", "9109916688", new DateTime(2023, 11, 8, 10, 28, 17, 55, DateTimeKind.Local).AddTicks(2684) });

            migrationBuilder.CreateIndex(
                name: "IX_users_PhoneNumber",
                table: "users",
                column: "PhoneNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_PhoneNumber",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "users");

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 11, 6, 17, 34, 34, 283, DateTimeKind.Local).AddTicks(3981), new DateTime(2023, 11, 6, 17, 34, 34, 283, DateTimeKind.Local).AddTicks(3993) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 11, 6, 17, 34, 34, 283, DateTimeKind.Local).AddTicks(3995), new DateTime(2023, 11, 6, 17, 34, 34, 283, DateTimeKind.Local).AddTicks(3995) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 11, 6, 17, 34, 34, 283, DateTimeKind.Local).AddTicks(3996), new DateTime(2023, 11, 6, 17, 34, 34, 283, DateTimeKind.Local).AddTicks(3996) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 11, 6, 17, 34, 34, 283, DateTimeKind.Local).AddTicks(3997), new DateTime(2023, 11, 6, 17, 34, 34, 283, DateTimeKind.Local).AddTicks(3998) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 11, 6, 17, 34, 34, 283, DateTimeKind.Local).AddTicks(4138), "$2a$11$HEOS7Tlk3GCt3tYSJpLL/OKS3WfznYaAfSRS3.KQFP.lQPJXhoz8W", new DateTime(2023, 11, 6, 17, 34, 34, 283, DateTimeKind.Local).AddTicks(4139) });
        }
    }
}
