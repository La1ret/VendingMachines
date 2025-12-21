using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VendingMachines.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SystemName = table.Column<string>(maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    FailedLoginAttempts = table.Column<int>(nullable: false),
                    LockoutEnd = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Description", "DisplayName", "SystemName" },
                values: new object[] { 1, "Полный доступ ко всем функциям системы.", "Администратор", "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Description", "DisplayName", "SystemName" },
                values: new object[] { 2, "Доступ к основным рабочим функциям (обработке заказов/данных).", "Оператор", "Operator" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Description", "DisplayName", "SystemName" },
                values: new object[] { 3, "Стандартный пользователь системы.", "Пользователь", "User" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Description", "DisplayName", "SystemName" },
                values: new object[] { 4, "Минимальные права (просмотр публичной информации).", "Гость", "Guest" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FailedLoginAttempts", "FullName", "LockoutEnd", "PasswordHash", "RoleId", "Username" },
                values: new object[] { 1, "may@mail.ru", 0, "Майская Мирослава Андреевна", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$Ea/2yGvjcKbl9VPGdQZDV.Nk7.byt3zLu1o7M0dw79dEi68iVid32", 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FailedLoginAttempts", "FullName", "LockoutEnd", "PasswordHash", "RoleId", "Username" },
                values: new object[] { 2, "Pahomov@yandex.ru", 0, "Пахомов Ярослав Константинович", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "$2a$11$SZdWFoaAgDKsszJuuutI6.FSNHDrOtpkycZgFNVOpHJ9enhdyelcu", 2, "Manager" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
