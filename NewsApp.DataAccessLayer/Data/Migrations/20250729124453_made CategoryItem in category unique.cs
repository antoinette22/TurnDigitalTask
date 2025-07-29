using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsApp.DataAccessLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class madeCategoryItemincategoryunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Categories_Title",
                table: "Categories",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_Title",
                table: "Categories");
        }
    }
}
