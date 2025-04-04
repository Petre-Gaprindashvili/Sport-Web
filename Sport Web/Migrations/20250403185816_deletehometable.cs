using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sport_Web.Migrations
{
    /// <inheritdoc />
    public partial class deletehometable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Homes");
        }

        /// <inheritdoc />
        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.CreateTable(
        //        name: "Homes",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            SectionContentId = table.Column<int>(type: "int", nullable: false),
        //            Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Homes", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_Homes_Categories_SectionContentId",
        //                column: x => x.SectionContentId,
        //                principalTable: "Categories",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "FK_Homes_SectionContents_SectionContentId",
        //                column: x => x.SectionContentId,
        //                principalTable: "SectionContents",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateIndex(
        //        name: "IX_Homes_SectionContentId",
        //        table: "Homes",
        //        column: "SectionContentId");
        //}
    }
}
