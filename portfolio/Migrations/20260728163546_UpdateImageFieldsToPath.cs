using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portfolio_web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImageFieldsToPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificateUrl",
                table: "certificates");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "projects",
                newName: "ImagePath");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "certificates",
                newName: "ImagePath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "projects",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "certificates",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "CertificateUrl",
                table: "certificates",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
