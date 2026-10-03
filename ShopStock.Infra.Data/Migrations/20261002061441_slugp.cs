using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopStock.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class slugp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Prouducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Prouducts");
        }
    }
}
