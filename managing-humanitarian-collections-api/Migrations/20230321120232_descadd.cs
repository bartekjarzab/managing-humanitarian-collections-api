using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class descadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "CollectionProducts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollectionProducts_ProductId",
                table: "CollectionProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionProducts_Products_ProductId",
                table: "CollectionProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionProducts_Products_ProductId",
                table: "CollectionProducts");

            migrationBuilder.DropIndex(
                name: "IX_CollectionProducts_ProductId",
                table: "CollectionProducts");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "CollectionProducts");
        }
    }
}
