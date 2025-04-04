using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sport_Web.Migrations
{
    /// <inheritdoc />
    public partial class ahhhhhhhkkkkiaaaaaaaaaaaaaaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
        //{
        //    migrationBuilder.CreateTable(
        //        name: "Categories",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            ParentCategoryId = table.Column<int>(type: "int", nullable: true),
        //            ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Categories", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_Categories_Categories_ParentCategoryId",
        //                column: x => x.ParentCategoryId,
        //                principalTable: "Categories",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.SetNull);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Users",
        //        columns: table => new
        //        {
        //            UserId = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            IsActive = table.Column<bool>(type: "bit", nullable: false),
        //            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Users", x => x.UserId);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "categorySections",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            TabName = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            CategoryId = table.Column<int>(type: "int", nullable: true),
        //            CategoryType = table.Column<string>(type: "nvarchar(max)", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_categorySections", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_categorySections_Categories_CategoryId",
        //                column: x => x.CategoryId,
        //                principalTable: "Categories",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Carts",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            UserId = table.Column<int>(type: "int", nullable: false),
        //            IsActive = table.Column<bool>(type: "bit", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Carts", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_Carts_Users_UserId",
        //                column: x => x.UserId,
        //                principalTable: "Users",
        //                principalColumn: "UserId",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "PasswordResetTokens",
        //        columns: table => new
        //        {
        //            TokenId = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            UserId = table.Column<int>(type: "int", nullable: false),
        //            Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            IsUsed = table.Column<bool>(type: "bit", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_PasswordResetTokens", x => x.TokenId);
        //            table.ForeignKey(
        //                name: "FK_PasswordResetTokens_Users_UserId",
        //                column: x => x.UserId,
        //                principalTable: "Users",
        //                principalColumn: "UserId",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "SectionContents",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            CategorySectionId = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_SectionContents", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_SectionContents_categorySections_CategorySectionId",
        //                column: x => x.CategorySectionId,
        //                principalTable: "categorySections",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "Deliveries",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            CartId = table.Column<int>(type: "int", nullable: false),
        //            Status = table.Column<int>(type: "int", nullable: false),
        //            DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_Deliveries", x => x.Id);
        //            table.ForeignKey(
        //                name: "FK_Deliveries_Carts_CartId",
        //                column: x => x.CartId,
        //                principalTable: "Carts",
        //                principalColumn: "Id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
			            onDelete: ReferentialAction.Cascade);

					table.ForeignKey(
                        name: "FK_Articles_SectionContents_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "SectionContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "Homes",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        SectionContentId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Homes", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Homes_Categories_SectionContentId",
            //            column: x => x.SectionContentId,
            //            principalTable: "Categories",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Homes_SectionContents_SectionContentId",
            //            column: x => x.SectionContentId,
            //            principalTable: "SectionContents",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Teams",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CategoryId = table.Column<int>(type: "int", nullable: false),
            //        LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Teams", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Teams_Categories_CategoryId",
            //            column: x => x.CategoryId,
            //            principalTable: "Categories",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Teams_SectionContents_CategoryId",
            //            column: x => x.CategoryId,
            //            principalTable: "SectionContents",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Matches",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        HomeTeamId = table.Column<int>(type: "int", nullable: false),
            //        AwayTeamId = table.Column<int>(type: "int", nullable: false),
            //        HomeScore = table.Column<int>(type: "int", nullable: false),
            //        AwayScore = table.Column<int>(type: "int", nullable: false),
            //        MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        HomeTeamlogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        AwayTeamlogo = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Matches", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Matches_Teams_AwayTeamId",
            //            column: x => x.AwayTeamId,
            //            principalTable: "Teams",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Matches_Teams_HomeTeamId",
            //            column: x => x.HomeTeamId,
            //            principalTable: "Teams",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "NewsArticle",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Stock = table.Column<int>(type: "int", nullable: false),
            //        IsAvailable = table.Column<bool>(type: "bit", nullable: false),
            //        TeamId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NewsArticle", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_NewsArticle_Teams_TeamId",
            //            column: x => x.TeamId,
            //            principalTable: "Teams",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Players",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        TeamId = table.Column<int>(type: "int", nullable: true),
            //        Age = table.Column<int>(type: "int", nullable: false),
            //        Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Height = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Players", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Players_Teams_TeamId",
            //            column: x => x.TeamId,
            //            principalTable: "Teams",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CartItems",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CartId = table.Column<int>(type: "int", nullable: false),
            //        ProductId = table.Column<int>(type: "int", nullable: false),
            //        Quantity = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CartItems", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_CartItems_Carts_CartId",
            //            column: x => x.CartId,
            //            principalTable: "Carts",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_CartItems_NewsArticle_ProductId",
            //            column: x => x.ProductId,
            //            principalTable: "NewsArticle",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Articles_CategoryId1",
            //    table: "Articles",
            //    column: "CategoryId1");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CartItems_CartId",
            //    table: "CartItems",
            //    column: "CartId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CartItems_ProductId",
            //    table: "CartItems",
            //    column: "ProductId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Carts_UserId",
            //    table: "Carts",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Categories_ParentCategoryId",
            //    table: "Categories",
            //    column: "ParentCategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_categorySections_CategoryId",
            //    table: "categorySections",
            //    column: "CategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Deliveries_CartId",
            //    table: "Deliveries",
            //    column: "CartId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Homes_SectionContentId",
            //    table: "Homes",
            //    column: "SectionContentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Matches_AwayTeamId",
            //    table: "Matches",
            //    column: "AwayTeamId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Matches_HomeTeamId",
            //    table: "Matches",
            //    column: "HomeTeamId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_NewsArticle_TeamId",
            //    table: "NewsArticle",
            //    column: "TeamId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PasswordResetTokens_UserId",
            //    table: "PasswordResetTokens",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Players_TeamId",
            //    table: "Players",
            //    column: "TeamId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SectionContents_CategorySectionId",
            //    table: "SectionContents",
            //    column: "CategorySectionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Teams_CategoryId",
            //    table: "Teams",
            //    column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            //migrationBuilder.DropTable(
            //    name: "CartItems");

            //migrationBuilder.DropTable(
            //    name: "Deliveries");

            //migrationBuilder.DropTable(
            //    name: "Homes");

            //migrationBuilder.DropTable(
            //    name: "Matches");

            //migrationBuilder.DropTable(
            //    name: "PasswordResetTokens");

            //migrationBuilder.DropTable(
            //    name: "Players");

            //migrationBuilder.DropTable(
            //    name: "NewsArticle");

            //migrationBuilder.DropTable(
            //    name: "Carts");

            //migrationBuilder.DropTable(
            //    name: "Teams");

            //migrationBuilder.DropTable(
            //    name: "Users");

            //migrationBuilder.DropTable(
            //    name: "SectionContents");

            //migrationBuilder.DropTable(
            //    name: "categorySections");

            //migrationBuilder.DropTable(
            //    name: "Categories");
        }
    }
}
