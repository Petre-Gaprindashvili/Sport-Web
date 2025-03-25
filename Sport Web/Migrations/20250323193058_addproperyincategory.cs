using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sport_Web.Migrations
{
    /// <inheritdoc />
    public partial class addproperyincategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubCategoryNavbar",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCategoryNavbar",
                table: "Categories");
        }
    }
}
