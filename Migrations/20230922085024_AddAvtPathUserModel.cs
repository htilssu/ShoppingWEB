using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingWEB.Migrations
{
    /// <inheritdoc />
    public partial class AddAvtPathUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "avtPath",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avtPath",
                table: "AspNetUsers");
        }
    }
}
