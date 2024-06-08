using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Premia_API.Migrations
{
    /// <inheritdoc />
    public partial class NewEnitities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerUser_Customer_CustomerId",
                table: "CustomerUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerUser_Users_UserId",
                table: "CustomerUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerUser",
                table: "CustomerUser");

            migrationBuilder.RenameTable(
                name: "CustomerUser",
                newName: "CustomerUsers");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerUser_UserId",
                table: "CustomerUsers",
                newName: "IX_CustomerUsers_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerUsers",
                table: "CustomerUsers",
                columns: new[] { "CustomerId", "UserId" });

            migrationBuilder.CreateTable(
                name: "RegistrationRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationRequests", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerUsers_Customer_CustomerId",
                table: "CustomerUsers",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerUsers_Users_UserId",
                table: "CustomerUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerUsers_Customer_CustomerId",
                table: "CustomerUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerUsers_Users_UserId",
                table: "CustomerUsers");

            migrationBuilder.DropTable(
                name: "RegistrationRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerUsers",
                table: "CustomerUsers");

            migrationBuilder.RenameTable(
                name: "CustomerUsers",
                newName: "CustomerUser");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerUsers_UserId",
                table: "CustomerUser",
                newName: "IX_CustomerUser_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerUser",
                table: "CustomerUser",
                columns: new[] { "CustomerId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerUser_Customer_CustomerId",
                table: "CustomerUser",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerUser_Users_UserId",
                table: "CustomerUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
