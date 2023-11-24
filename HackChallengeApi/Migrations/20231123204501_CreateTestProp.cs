using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackChallengeApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateTestProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TestClass",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "TestClass");
        }
    }
}
