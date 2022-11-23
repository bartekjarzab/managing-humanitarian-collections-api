using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CollectionPoints_CollectionId",
                table: "CollectionPoints");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionPoints_CollectionId",
                table: "CollectionPoints",
                column: "CollectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CollectionPoints_CollectionId",
                table: "CollectionPoints");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionPoints_CollectionId",
                table: "CollectionPoints",
                column: "CollectionId",
                unique: true);
        }
    }
}
