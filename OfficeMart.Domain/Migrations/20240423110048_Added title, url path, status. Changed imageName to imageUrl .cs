using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeMart.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddedtitleurlpathstatusChangedimageNametoimageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Sliders",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UrlPath",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "UrlPath",
                table: "Sliders");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Sliders",
                newName: "ImageName");
        }
    }
}
