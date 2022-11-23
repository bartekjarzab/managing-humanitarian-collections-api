using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CollectionPoints_CollectionId",
                table: "CollectionPoints",
                column: "CollectionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionPoints_Collections_CollectionId",
                table: "CollectionPoints",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionPoints_Collections_CollectionId",
                table: "CollectionPoints");

            migrationBuilder.DropIndex(
                name: "IX_CollectionPoints_CollectionId",
                table: "CollectionPoints");
        }
    }
}
