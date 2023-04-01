using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class cat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionProducts_OrderProducts_OrderProductId",
                table: "CollectionProducts");

            migrationBuilder.DropIndex(
                name: "IX_CollectionProducts_OrderProductId",
                table: "CollectionProducts");

            migrationBuilder.DropColumn(
                name: "OrderProductId",
                table: "CollectionProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderProductId",
                table: "CollectionProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollectionProducts_OrderProductId",
                table: "CollectionProducts",
                column: "OrderProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionProducts_OrderProducts_OrderProductId",
                table: "CollectionProducts",
                column: "OrderProductId",
                principalTable: "OrderProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
