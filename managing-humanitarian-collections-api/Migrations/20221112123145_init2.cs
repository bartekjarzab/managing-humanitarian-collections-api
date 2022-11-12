using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductPropertiess",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPropertiess_ProductId",
                table: "ProductPropertiess",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPropertiess_Products_ProductId",
                table: "ProductPropertiess",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPropertiess_Products_ProductId",
                table: "ProductPropertiess");

            migrationBuilder.DropIndex(
                name: "IX_ProductPropertiess_ProductId",
                table: "ProductPropertiess");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductPropertiess");
        }
    }
}
