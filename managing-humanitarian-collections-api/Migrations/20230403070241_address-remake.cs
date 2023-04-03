using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class addressremake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Voivodeship",
                table: "Addresses",
                newName: "VoivodeshipId");

            migrationBuilder.AddColumn<int>(
                name: "VoivodeshipId1",
                table: "Addresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Voivodeships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voivodeships", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_VoivodeshipId1",
                table: "Addresses",
                column: "VoivodeshipId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Voivodeships_VoivodeshipId1",
                table: "Addresses",
                column: "VoivodeshipId1",
                principalTable: "Voivodeships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Voivodeships_VoivodeshipId1",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "Voivodeships");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_VoivodeshipId1",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "VoivodeshipId1",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "VoivodeshipId",
                table: "Addresses",
                newName: "Voivodeship");
        }
    }
}
