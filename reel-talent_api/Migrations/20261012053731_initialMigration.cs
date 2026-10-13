using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace reel_talent_api.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Talents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    PasswordHashed = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TalentDetails",
                columns: table => new
                {
                    TalentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Ethnicity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NaturalAccent = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EyeColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HairColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShoeSize = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ClothingSize = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Height = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalentDetails", x => x.TalentId);
                    table.ForeignKey(
                        name: "FK_TalentDetails_Talents_TalentId",
                        column: x => x.TalentId,
                        principalTable: "Talents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TalentExperiences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TalentDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Production = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DirectorOrCompany = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalentExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TalentExperiences_TalentDetails_TalentDetailsId",
                        column: x => x.TalentDetailsId,
                        principalTable: "TalentDetails",
                        principalColumn: "TalentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TalentLanguages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TalentDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalentLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TalentLanguages_TalentDetails_TalentDetailsId",
                        column: x => x.TalentDetailsId,
                        principalTable: "TalentDetails",
                        principalColumn: "TalentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TalentSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TalentDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalentSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TalentSkills_TalentDetails_TalentDetailsId",
                        column: x => x.TalentDetailsId,
                        principalTable: "TalentDetails",
                        principalColumn: "TalentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TalentExperiences_TalentDetailsId",
                table: "TalentExperiences",
                column: "TalentDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_TalentLanguages_TalentDetailsId",
                table: "TalentLanguages",
                column: "TalentDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Talents_Email",
                table: "Talents",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TalentSkills_TalentDetailsId",
                table: "TalentSkills",
                column: "TalentDetailsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TalentExperiences");

            migrationBuilder.DropTable(
                name: "TalentLanguages");

            migrationBuilder.DropTable(
                name: "TalentSkills");

            migrationBuilder.DropTable(
                name: "TalentDetails");

            migrationBuilder.DropTable(
                name: "Talents");
        }
    }
}
