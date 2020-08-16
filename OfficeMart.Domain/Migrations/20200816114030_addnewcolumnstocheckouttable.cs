using Microsoft.EntityFrameworkCore.Migrations;

namespace OfficeMart.Domain.Migrations
{
    public partial class addnewcolumnstocheckouttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerName",
                table: "Checkouts",
                maxLength: 85,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerPhone",
                table: "Checkouts",
                maxLength: 85,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BuyerSurname",
                table: "Checkouts",
                maxLength: 85,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "Checkouts",
                maxLength: 600,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerName",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "BuyerPhone",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "BuyerSurname",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "Checkouts");
        }
    }
}
