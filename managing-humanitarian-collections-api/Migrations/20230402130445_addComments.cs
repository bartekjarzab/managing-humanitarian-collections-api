using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class addComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Review",
                table: "Comments",
                newName: "Content");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatedById",
                table: "Comments",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_CreatedById",
                table: "Comments",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_CreatedById",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CreatedById",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Comments",
                newName: "Review");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
