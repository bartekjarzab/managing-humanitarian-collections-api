using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class collectionrefactor2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Users_CreatedById",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_CreatedById",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Collections");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CreatedByOrganiserId",
                table: "Collections",
                column: "CreatedByOrganiserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Users_CreatedByOrganiserId",
                table: "Collections",
                column: "CreatedByOrganiserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Users_CreatedByOrganiserId",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_CreatedByOrganiserId",
                table: "Collections");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Collections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CreatedById",
                table: "Collections",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Users_CreatedById",
                table: "Collections",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
