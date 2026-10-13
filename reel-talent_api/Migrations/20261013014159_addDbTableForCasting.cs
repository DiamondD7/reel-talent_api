using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace reel_talent_api.Migrations
{
    /// <inheritdoc />
    public partial class addDbTableForCasting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CastingProfile_Users_UserId",
                table: "CastingProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CastingProfile",
                table: "CastingProfile");

            migrationBuilder.RenameTable(
                name: "CastingProfile",
                newName: "CastingProfiles");

            migrationBuilder.RenameIndex(
                name: "IX_CastingProfile_UserId",
                table: "CastingProfiles",
                newName: "IX_CastingProfiles_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CastingProfiles",
                table: "CastingProfiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CastingProfiles_Users_UserId",
                table: "CastingProfiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CastingProfiles_Users_UserId",
                table: "CastingProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CastingProfiles",
                table: "CastingProfiles");

            migrationBuilder.RenameTable(
                name: "CastingProfiles",
                newName: "CastingProfile");

            migrationBuilder.RenameIndex(
                name: "IX_CastingProfiles_UserId",
                table: "CastingProfile",
                newName: "IX_CastingProfile_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CastingProfile",
                table: "CastingProfile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CastingProfile_Users_UserId",
                table: "CastingProfile",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
