using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopStock.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class countpro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Prouducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Prouducts");
        }
    }
}
