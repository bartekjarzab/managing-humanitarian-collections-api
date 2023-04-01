using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class orderrefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_CollectionProducts_CollectionProductId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_CollectionProductId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "CollectionProductId",
                table: "OrderProducts");

            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderProductId",
                table: "CollectionProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CollectionId",
                table: "Orders",
                column: "CollectionId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Collections_CollectionId",
                table: "Orders",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionProducts_OrderProducts_OrderProductId",
                table: "CollectionProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Collections_CollectionId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CollectionId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_CollectionProducts_OrderProductId",
                table: "CollectionProducts");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderProductId",
                table: "CollectionProducts");

            migrationBuilder.AddColumn<int>(
                name: "CollectionProductId",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_CollectionProductId",
                table: "OrderProducts",
                column: "CollectionProductId",
                unique: true);

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
