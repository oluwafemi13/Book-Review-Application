using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changestoEntityRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_RatingAverages_AverageRatingId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_AverageRatingId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AverageRatingId",
                table: "Books");

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "RatingAverages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RatingAverages_BookId",
                table: "RatingAverages",
                column: "BookId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingAverages_Books_BookId",
                table: "RatingAverages",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingAverages_Books_BookId",
                table: "RatingAverages");

            migrationBuilder.DropIndex(
                name: "IX_RatingAverages_BookId",
                table: "RatingAverages");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "RatingAverages");

            migrationBuilder.AddColumn<int>(
                name: "AverageRatingId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_AverageRatingId",
                table: "Books",
                column: "AverageRatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_RatingAverages_AverageRatingId",
                table: "Books",
                column: "AverageRatingId",
                principalTable: "RatingAverages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
