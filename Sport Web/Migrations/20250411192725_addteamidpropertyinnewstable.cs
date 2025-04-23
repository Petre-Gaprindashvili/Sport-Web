using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sport_Web.Migrations
{
    /// <inheritdoc />
    public partial class addteamidpropertyinnewstable : Migration
    {
		/// <inheritdoc />
		/// 
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			// Add the TeamId column to News table
			migrationBuilder.AddColumn<int>(
				name: "TeamId",
				table: "News",
				type: "int",
				nullable: true);  // Nullable because not all news may have an associated team

			// Create index for TeamId in News table
			migrationBuilder.CreateIndex(
				name: "IX_News_TeamId",
				table: "News",
				column: "TeamId");

			// Add foreign key relationship between News and Teams with NO ACTION on delete
			migrationBuilder.AddForeignKey(
				name: "FK_News_Teams_TeamId",
				table: "News",
				column: "TeamId",
				principalTable: "Teams",
				principalColumn: "Id",
				onDelete: ReferentialAction.NoAction);  // Avoid cascade delete or update actions
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			// Remove the foreign key, index, and column if the migration is rolled back
			migrationBuilder.DropForeignKey(
				name: "FK_News_Teams_TeamId",
				table: "News");

			migrationBuilder.DropIndex(
				name: "IX_News_TeamId",
				table: "News");

			migrationBuilder.DropColumn(
				name: "TeamId",
				table: "News");
		}
	}
}

    