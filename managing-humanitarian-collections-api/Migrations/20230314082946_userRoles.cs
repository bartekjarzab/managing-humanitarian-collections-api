using Microsoft.EntityFrameworkCore.Migrations;

namespace managing_humanitarian_collections_api.Migrations
{
    public partial class userRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CreatedByDonatorId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CreatedByDonatorId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedByDonatorId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DonatorId",
                table: "Orders");
        }
    }
}
