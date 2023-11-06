using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Tutor.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ProfileImage = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    EmailVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8500), "Administrator", new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8508) },
                    { 2, new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8509), "Subadmin", new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8510) },
                    { 3, new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8511), "Tutor", new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8512) },
                    { 4, new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8512), "Student", new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8513) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "CreatedAt", "Email", "EmailVerified", "FirstName", "LastName", "Password", "ProfileImage", "RoleId", "Status", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8585), "admin@gmail.com", true, "Super", "Admin", "$2a$11$CwnYVnSCncr9kFiwussz8eEKp3Bdz3VUTw9sZ6BdSR9lezdXOLweq", null, 1, true, new DateTime(2023, 11, 6, 13, 53, 25, 313, DateTimeKind.Local).AddTicks(8585) });

            migrationBuilder.CreateIndex(
                name: "IX_role_Name",
                table: "role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                table: "users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
