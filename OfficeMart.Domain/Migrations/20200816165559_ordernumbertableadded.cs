using Microsoft.EntityFrameworkCore.Migrations;

namespace OfficeMart.Domain.Migrations
{
    public partial class ordernumbertableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckoutNumber",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderNumberId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderedProductId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderedProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OrderNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderCheckNumber = table.Column<string>(maxLength: 85, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderNumbers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumberId",
                table: "Orders",
                column: "OrderNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderedProductId",
                table: "Orders",
                column: "OrderedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedProducts_OrderId",
                table: "OrderedProducts",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedProducts_Orders_OrderId",
                table: "OrderedProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderNumbers_OrderNumberId",
                table: "Orders",
                column: "OrderNumberId",
                principalTable: "OrderNumbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderedProducts_OrderedProductId",
                table: "Orders",
                column: "OrderedProductId",
                principalTable: "OrderedProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedProducts_Orders_OrderId",
                table: "OrderedProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderNumbers_OrderNumberId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderedProducts_OrderedProductId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderNumbers");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderNumberId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderedProductId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderedProducts_OrderId",
                table: "OrderedProducts");

            migrationBuilder.DropColumn(
                name: "OrderNumberId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderedProductId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderedProducts");

            migrationBuilder.AddColumn<string>(
                name: "CheckoutNumber",
                table: "Orders",
                type: "nvarchar(85)",
                maxLength: 85,
                nullable: false,
                defaultValue: "");
        }
    }
}
