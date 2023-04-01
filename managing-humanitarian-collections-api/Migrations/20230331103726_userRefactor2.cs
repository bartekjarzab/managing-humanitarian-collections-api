using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class userRefactor2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Avatars_AvatarId",
                table: "Profiles");

            migrationBuilder.DropTable(
                name: "Avatars");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_AvatarId",
                table: "Profiles");

            migrationBuilder.RenameColumn(
                name: "AvatarId",
                table: "Profiles",
                newName: "Avatar");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "Profiles",
                newName: "AvatarId");

            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvatarType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_AvatarId",
                table: "Profiles",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Avatars_AvatarId",
                table: "Profiles",
                column: "AvatarId",
                principalTable: "Avatars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
