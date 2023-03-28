using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class _15xdl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "CollectionProducts",
                type: "int",
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
                onDelete: ReferentialAction.Restrict);
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
                name: "ProductId",
                table: "CollectionProducts");
        }
    }
}
