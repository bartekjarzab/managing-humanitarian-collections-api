using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class _15xd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionProducts_Products_ProductId",
                table: "CollectionProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_CollectionProducts_CollectionProductId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_CollectionProductId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_CollectionProducts_ProductId",
                table: "CollectionProducts");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CollectionProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ProductCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "CollectionProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_CollectionProductId",
                table: "OrderProducts",
                column: "CollectionProductId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_CollectionProducts_CollectionProductId",
                table: "OrderProducts",
                column: "CollectionProductId",
                principalTable: "CollectionProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
