using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class colluseridadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Users_CreatedByOrganizerId",
                table: "Collections");

            migrationBuilder.RenameColumn(
                name: "OrganizerId",
                table: "Collections",
                newName: "CreatedByOrganiserId");

            migrationBuilder.RenameColumn(
                name: "CreatedByOrganizerId",
                table: "Collections",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Collections_CreatedByOrganizerId",
                table: "Collections",
                newName: "IX_Collections_CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Users_CreatedById",
                table: "Collections",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Users_CreatedById",
                table: "Collections");

            migrationBuilder.RenameColumn(
                name: "CreatedByOrganiserId",
                table: "Collections",
                newName: "OrganizerId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Collections",
                newName: "CreatedByOrganizerId");

            migrationBuilder.RenameIndex(
                name: "IX_Collections_CreatedById",
                table: "Collections",
                newName: "IX_Collections_CreatedByOrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Users_CreatedByOrganizerId",
                table: "Collections",
                column: "CreatedByOrganizerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
