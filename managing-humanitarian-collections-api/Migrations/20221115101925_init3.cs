using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionPoints_Addresses_AddressId",
                table: "CollectionPoints");

            migrationBuilder.DropIndex(
                name: "IX_CollectionPoints_AddressId",
                table: "CollectionPoints");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "CollectionPoints");

            migrationBuilder.AddColumn<int>(
                name: "CollectionPointId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CollectionPointId",
                table: "Addresses",
                column: "CollectionPointId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_CollectionPoints_CollectionPointId",
                table: "Addresses",
                column: "CollectionPointId",
                principalTable: "CollectionPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_CollectionPoints_CollectionPointId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CollectionPointId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CollectionPointId",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "CollectionPoints",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollectionPoints_AddressId",
                table: "CollectionPoints",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionPoints_Addresses_AddressId",
                table: "CollectionPoints",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
