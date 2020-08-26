using Microsoft.EntityFrameworkCore.Migrations;

namespace OfficeMart.Domain.Migrations
{
    public partial class descriptionRequiringDisabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                maxLength: 850,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(850)",
                oldMaxLength: 850);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(850)",
                maxLength: 850,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 850,
                oldNullable: true);
        }
    }
}
