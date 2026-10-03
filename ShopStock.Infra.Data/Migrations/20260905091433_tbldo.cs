using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopStock.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class tbldo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "catgories",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "catgories");
        }
    }
}
