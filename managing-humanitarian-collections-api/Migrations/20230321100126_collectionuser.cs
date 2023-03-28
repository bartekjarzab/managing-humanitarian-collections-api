using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class collectionuser : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
