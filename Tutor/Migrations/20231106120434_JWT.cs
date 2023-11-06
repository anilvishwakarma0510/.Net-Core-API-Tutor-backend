using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutor.Migrations
{
    public partial class JWT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8500), new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8508) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8509), new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8510) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8511), new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8512) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8512), new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8513) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8585), "$2a$11$CwnYVnSCncr9kFiwussz8eEKp3Bdz3VUTw9sZ6BdSR9lezdXOLweq", new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8585) });
        }
    }
}
