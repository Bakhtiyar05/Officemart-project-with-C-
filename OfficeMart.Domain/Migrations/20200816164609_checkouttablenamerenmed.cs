using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OfficeMart.Domain.Migrations
{
    public partial class checkouttablenamerenmed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckoutProducts");

            migrationBuilder.DropTable(
                name: "Checkouts");

            migrationBuilder.CreateTable(
                name: "OrderedProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    RegDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderedProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderCount = table.Column<int>(nullable: false),
                    SaledPrice = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    CheckoutNumber = table.Column<string>(maxLength: 85, nullable: false),
                    BuyerName = table.Column<string>(maxLength: 85, nullable: true),
                    BuyerSurname = table.Column<string>(maxLength: 85, nullable: true),
                    DeliveryAddress = table.Column<string>(maxLength: 600, nullable: false),
                    BuyerPhone = table.Column<string>(maxLength: 85, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderedProducts_ProductId",
                table: "OrderedProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderedProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.CreateTable(
                name: "CheckoutProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2020, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckoutProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Checkouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerName = table.Column<string>(type: "nvarchar(85)", maxLength: 85, nullable: true),
                    BuyerPhone = table.Column<string>(type: "nvarchar(85)", maxLength: 85, nullable: false),
                    BuyerSurname = table.Column<string>(type: "nvarchar(85)", maxLength: 85, nullable: true),
                    CheckoutNumber = table.Column<string>(type: "nvarchar(85)", maxLength: 85, nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    OrderCount = table.Column<int>(type: "int", nullable: false),
                    SaledPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkouts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutProducts_ProductId",
                table: "CheckoutProducts",
                column: "ProductId");
        }
    }
}
