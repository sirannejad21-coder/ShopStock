using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopStock.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class edt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RolleId",
                table: "RolePermissions");

            migrationBuilder.RenameColumn(
                name: "RolleId",
                table: "RolePermissions",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_RolleId",
                table: "RolePermissions",
                newName: "IX_RolePermissions_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "RolePermissions",
                newName: "RolleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                newName: "IX_RolePermissions_RolleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RolleId",
                table: "RolePermissions",
                column: "RolleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
