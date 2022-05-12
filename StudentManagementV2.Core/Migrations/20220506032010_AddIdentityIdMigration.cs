using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagementV2.Core.Migrations
{
    public partial class AddIdentityIdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IDENTITY_ID",
                table: "USER",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDENTITY_ID",
                table: "USER");
        }
    }
}
