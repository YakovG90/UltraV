using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ultra");

            migrationBuilder.CreateTable(
                name: "Characters",
                schema: "ultra",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CharacterName = table.Column<string>(nullable: true),
                    CharacterClass = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    PublicId = table.Column<Guid>(nullable: false),
                    GuildRank = table.Column<int>(nullable: false),
                    ItemLevel = table.Column<int>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    Specialization = table.Column<string>(nullable: true),
                    AchievementPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters",
                schema: "ultra");
        }
    }
}
