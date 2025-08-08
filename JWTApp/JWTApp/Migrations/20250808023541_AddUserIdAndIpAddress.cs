using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTApp.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdAndIpAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "ApiLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ApiLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "ApiLogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ApiLogs");
        }
    }
}
