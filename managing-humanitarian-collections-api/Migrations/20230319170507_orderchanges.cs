using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class orderchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CreatedByDonatorId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CreatedByDonatorId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedByDonatorId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DonatorId",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByDonatorId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DonatorId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedByDonatorId",
                table: "Orders",
                column: "CreatedByDonatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CreatedByDonatorId",
                table: "Orders",
                column: "CreatedByDonatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
