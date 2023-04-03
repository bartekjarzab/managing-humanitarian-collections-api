using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class addressremake2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Voivodeships_VoivodeshipId1",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_VoivodeshipId1",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "VoivodeshipId1",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "VoivodeshipId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_VoivodeshipId",
                table: "Addresses",
                column: "VoivodeshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Voivodeships_VoivodeshipId",
                table: "Addresses",
                column: "VoivodeshipId",
                principalTable: "Voivodeships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Voivodeships_VoivodeshipId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_VoivodeshipId",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "VoivodeshipId",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "VoivodeshipId1",
                table: "Addresses",
                type: "int",
                nullable: true);

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
    }
}
