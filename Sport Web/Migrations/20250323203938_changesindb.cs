using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sport_Web.Migrations
{
    /// <inheritdoc />
    public partial class changesindb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCategoryNavbar",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "SubCategoryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategoryItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoryItems_CategoryId",
                table: "SubCategoryItems",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubCategoryItems");

            migrationBuilder.AddColumn<string>(
                name: "SubCategoryNavbar",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
