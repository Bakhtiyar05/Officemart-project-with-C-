using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OfficeMart.Domain.Migrations
{
    public partial class AddedIsActiveColumnToProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegDate",
                table: "Products",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Products",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegDate",
                table: "ProductImages",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegDate",
                table: "Categories",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegDate",
                table: "ProductImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
