using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class usercontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OrderProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByOrganizerId",
                table: "Collections",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrganizerId",
                table: "Collections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CreatedByOrganizerId",
                table: "Collections",
                column: "CreatedByOrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Users_CreatedByOrganizerId",
                table: "Collections",
                column: "CreatedByOrganizerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Users_CreatedByOrganizerId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_Collections_CreatedByOrganizerId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "CreatedByOrganizerId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                table: "Collections");
        }
    }
}
