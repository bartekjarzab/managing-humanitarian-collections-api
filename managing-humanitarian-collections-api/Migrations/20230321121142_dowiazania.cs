using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class dowiazania : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Users_UserId",
                table: "Profiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_CollectionProducts_CollectionProductId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Users_UserId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_CollectionProductId",
                table: "OrderProducts");
        }
    }
}
