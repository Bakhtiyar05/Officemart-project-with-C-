using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OfficeMart.Domain.Migrations
{
    public partial class removedorderedproductid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderedProducts_OrderedProductId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderedProducts");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderedProductId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderedProductId",
                table: "Orders");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BuyerUserId",
                table: "OrderNumbers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegDate",
                table: "OrderNumbers",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuyerUserId",
                table: "OrderNumbers");

            migrationBuilder.DropColumn(
                name: "RegDate",
                table: "OrderNumbers");

            migrationBuilder.AddColumn<int>(
                name: "OrderedProductId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderedProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2020, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedProducts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderedProductId",
                table: "Orders",
                column: "OrderedProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderedProducts_OrderedProductId",
                table: "Orders",
                column: "OrderedProductId",
                principalTable: "OrderedProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
