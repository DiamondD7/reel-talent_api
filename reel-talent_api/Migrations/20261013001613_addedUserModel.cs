using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace reel_talent_api.Migrations
{
    /// <inheritdoc />
    public partial class addedUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Talents_Email",
                table: "Talents");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Talents");

            migrationBuilder.DropColumn(
                name: "PasswordHashed",
                table: "Talents");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Talents");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Talents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Talents_UserId",
                table: "Talents",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Talents_User_UserId",
                table: "Talents",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Talents_User_UserId",
                table: "Talents");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Talents_UserId",
                table: "Talents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Talents");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Talents",
                type: "nvarchar(254)",
                maxLength: 254,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHashed",
                table: "Talents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Talents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Talents_Email",
                table: "Talents",
                column: "Email",
                unique: true);
        }
    }
}
