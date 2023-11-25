using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HackChallengeApi.Migrations
{
    /// <inheritdoc />
    public partial class AudioFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentTrackId",
                table: "Rooms",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AudioFiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CurrentTrackId",
                table: "Rooms",
                column: "CurrentTrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_AudioFiles_CurrentTrackId",
                table: "Rooms",
                column: "CurrentTrackId",
                principalTable: "AudioFiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_AudioFiles_CurrentTrackId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_CurrentTrackId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CurrentTrackId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AudioFiles");
        }
    }
}
